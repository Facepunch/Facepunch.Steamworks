using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Steamworks.Data;
using Steamworks;
using System.Linq;

namespace Steamworks
{
	/// <summary>
	/// Responsible for all callback/callresult handling
	/// 
	/// This manually pumps Steam's message queue and dispatches those
	/// events to any waiting callbacks/callresults.
	/// </summary>
	public static class Dispatch
	{
		/// <summary>
		/// If set then we'll call this function every time a callback is generated.
		/// 
		/// This is SLOW!! - it's for debugging - don't keep it on all the time. If you want to access a specific
		/// callback then please create an issue on github and I'll add it!
		/// 
		/// Params are : [Callback Type] [Callback Contents] [server]
		/// 
		/// </summary>
		public static Action<CallbackType, string, bool> OnDebugCallback;

		/// <summary>
		/// Called if an exception happens during a callback/callresult.
		/// This is needed because the exception isn't always accessible when running
		/// async.. and can fail silently. With this hooked you won't be stuck wondering
		/// what happened.
		/// </summary>
		public static Action<Exception> OnException;

		#region interop
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ManualDispatch_Init", CallingConvention = CallingConvention.Cdecl )]
		internal static extern void SteamAPI_ManualDispatch_Init();

		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ManualDispatch_RunFrame", CallingConvention = CallingConvention.Cdecl )]
		internal static extern void SteamAPI_ManualDispatch_RunFrame( HSteamPipe pipe );

		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ManualDispatch_GetNextCallback", CallingConvention = CallingConvention.Cdecl )]
		[return: MarshalAs( UnmanagedType.I1 )]
		internal static extern bool SteamAPI_ManualDispatch_GetNextCallback( HSteamPipe pipe, [In, Out] ref CallbackMsg_t msg );

		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ManualDispatch_FreeLastCallback", CallingConvention = CallingConvention.Cdecl )]
		[return: MarshalAs( UnmanagedType.I1 )]
		internal static extern bool SteamAPI_ManualDispatch_FreeLastCallback( HSteamPipe pipe );		
		
		[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
		internal struct CallbackMsg_t
		{
			public HSteamUser m_hSteamUser; // Specific user to whom this callback applies.
			public CallbackType Type; // Callback identifier.  (Corresponds to the k_iCallback enum in the callback structure.)
			public IntPtr Data; // Points to the callback structure
			public int DataSize; // Size of the data pointed to by m_pubParam
		};

		#endregion

		internal static HSteamPipe ClientPipe { get; set; }
		internal static HSteamPipe ServerPipe { get; set; }

		/// <summary>
		/// This gets called from Client/Server Init
		/// It's important to switch to the manual dispatcher
		/// </summary>
		internal static void Init()
		{
			SteamAPI_ManualDispatch_Init();
		}

		/// <summary>
		/// Make sure we don't call Frame in a callback - because that'll cause some issues for everyone.
		/// </summary>
		static bool runningFrame = false;

		/// <summary>
		/// Calls RunFrame and processes events from this Steam Pipe
		/// </summary>
		internal static void Frame( HSteamPipe pipe )
		{
			if ( runningFrame )
				return;

			try
			{
				runningFrame = true;

				SteamAPI_ManualDispatch_RunFrame( pipe );
				SteamNetworkingUtils.OutputDebugMessages();

				CallbackMsg_t msg = default;

				while ( SteamAPI_ManualDispatch_GetNextCallback( pipe, ref msg ) )
				{
					try
					{
						ProcessCallback( msg, pipe == ServerPipe );
					}
					finally
					{
						SteamAPI_ManualDispatch_FreeLastCallback( pipe );
					}
				}
			}
			catch ( System.Exception e )
			{
				OnException?.Invoke( e );
			}
			finally
			{
				runningFrame = false;
			}
		}

		/// <summary>
		/// To be safe we don't call the continuation functions while iterating
		/// the Callback list. This is maybe overly safe because the only way this
		/// could be an issue is if the callback list is modified in the continuation
		/// which would only happen if starting or shutting down in the callback.
		/// </summary>
		static List<Action<IntPtr>> actionsToCall = new List<Action<IntPtr>>();

		/// <summary>
		/// A callback is a general global message
		/// </summary>
		private static void ProcessCallback( CallbackMsg_t msg, bool isServer )
		{
			OnDebugCallback?.Invoke( msg.Type, CallbackToString( msg.Type, msg.Data, msg.DataSize ), isServer );

			// Is this a special callback telling us that the call result is ready?
			if ( msg.Type == CallbackType.SteamAPICallCompleted )
			{
				ProcessResult( msg );
				return;
			}

			if ( Callbacks.TryGetValue( msg.Type, out var list ) )
			{
				actionsToCall.Clear();

				foreach ( var item in list )
				{
					if ( item.server != isServer )
						continue;

					actionsToCall.Add( item.action );
				}

				foreach ( var action in actionsToCall )
				{
					action( msg.Data );
				}

				actionsToCall.Clear();
			}
		}

		/// <summary>
		/// Given a callback, try to turn it into a string
		/// </summary>
		internal static string CallbackToString( CallbackType type, IntPtr data, int expectedsize )
		{
			if ( !CallbackTypeFactory.All.TryGetValue( type, out var t ) )
				return $"[{type} not in sdk]";

			var strct = data.ToType( t );
			if ( strct == null )
				return "[null]";

			var str = "";

			var fields = t.GetFields( System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic );

			if ( fields.Length == 0 )
				return "[no fields]";

			var columnSize = fields.Max( x => x.Name.Length ) + 1;

			if ( columnSize < 10 )
				columnSize = 10;

			foreach ( var field in fields )
			{
				var spaces = (columnSize - field.Name.Length);
				if ( spaces < 0 ) spaces = 0;

				str += $"{new String( ' ', spaces )}{field.Name}: {field.GetValue( strct )}\n";
			}

			return str.Trim( '\n' );
		}

		/// <summary>
		/// A result is a reply to a specific command
		/// </summary>
		private static void ProcessResult( CallbackMsg_t msg )
		{
			var result = msg.Data.ToType<SteamAPICallCompleted_t>();

			//
			// Do we have an entry added via OnCallComplete
			//
			if ( !ResultCallbacks.TryGetValue( result.AsyncCall, out var callbackInfo ) )
			{
				//
				// This can happen if the callback result was immediately available
				// so we just returned that without actually going through the callback
				// dance. It's okay for this to fail.
				//

				//
				// But still let everyone know that this happened..
				//
				OnDebugCallback?.Invoke( (CallbackType)result.Callback, $"[no callback waiting/required]", false );
				return;
			}

			// Remove it before we do anything, incase the continuation throws exceptions
			ResultCallbacks.Remove( result.AsyncCall );

			// At this point whatever async routine called this 
			// continues running.
			callbackInfo.continuation();
		}

		/// <summary>
		/// Pumps the queue in an async loop so we don't
		/// have to think about it. This has the advantage that
		/// you can call .Wait() on async shit and it still works.
		/// </summary>
		internal static async void LoopClientAsync()
		{
			while ( ClientPipe != 0 )
			{
				Frame( ClientPipe );
				await Task.Delay( 16 );
			}
		}

		/// <summary>
		/// Pumps the queue in an async loop so we don't
		/// have to think about it. This has the advantage that
		/// you can call .Wait() on async shit and it still works.
		/// </summary>
		internal static async void LoopServerAsync()
		{
			while ( ServerPipe != 0 )
			{
				Frame( ServerPipe );
				await Task.Delay( 32 );
			}
		}

		struct ResultCallback
		{
			public Action continuation;
			public bool server;
		}

		static Dictionary<ulong, ResultCallback> ResultCallbacks = new Dictionary<ulong, ResultCallback>();

		/// <summary>
		/// Watch for a steam api call
		/// </summary>
		internal static void OnCallComplete<T>( SteamAPICall_t call, Action continuation, bool server ) where T : struct, ICallbackData
		{
			ResultCallbacks[call.Value] = new ResultCallback
			{
				continuation = continuation,
				server = server
			};
		}

		struct Callback
		{
			public Action<IntPtr> action;
			public bool server;
		}

		static Dictionary<CallbackType, List<Callback>> Callbacks = new Dictionary<CallbackType, List<Callback>>();

		/// <summary>
		/// Install a global callback. The passed function will get called if it's all good.
		/// </summary>
		internal static void Install<T>( Action<T> p, bool server = false ) where T : ICallbackData
		{
			var t = default( T );
			var type = t.CallbackType;

			if ( !Callbacks.TryGetValue( type, out var list ) )
			{
				list = new List<Callback>();
				Callbacks[type] = list;
			}

			list.Add( new Callback
			{
				action = x => p( x.ToType<T>() ),
				server = server
			} );
		}

		internal static void ShutdownServer()
		{
			ServerPipe = 0;

			foreach ( var callback in Callbacks )
			{
				Callbacks[callback.Key].RemoveAll( x => x.server );
			}

			ResultCallbacks = ResultCallbacks.Where( x => !x.Value.server )
											 .ToDictionary( x => x.Key, x => x.Value );
		}

		internal static void ShutdownClient()
		{
			ClientPipe = 0;

			foreach ( var callback in Callbacks )
			{
				Callbacks[callback.Key].RemoveAll( x => !x.server );
			}

			ResultCallbacks = ResultCallbacks.Where( x => x.Value.server )
											 .ToDictionary( x => x.Key, x => x.Value );
		}
	}
}