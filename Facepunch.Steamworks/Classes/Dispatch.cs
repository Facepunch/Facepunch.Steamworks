using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Steamworks.Data;
using Steamworks;

namespace Steamworks
{
	/// <summary>
	/// Manually pumps Steam's message queue and dispatches those
	/// events to any waiting callbacks/callresults.
	/// </summary>
	internal static class Dispatch
	{
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
		/// It's important to switch to the manual dipatcher
		/// </summary>
		public static void Init()
		{
			SteamAPI_ManualDispatch_Init();
		}


		/// <summary>
		/// Calls RunFrame and processes events from this Steam Pipe
		/// </summary>
		public static void Frame( HSteamPipe pipe )
		{ 
			SteamAPI_ManualDispatch_RunFrame( pipe );
			SteamNetworkingUtils.OutputDebugMessages();

			CallbackMsg_t msg = default;

			while ( SteamAPI_ManualDispatch_GetNextCallback( pipe, ref msg ) )
			{
				try
				{
					ProcessCallback( msg );
				}
				finally
				{
					SteamAPI_ManualDispatch_FreeLastCallback( pipe );
				}
			}
		}

		/// <summary>
		/// A callback is a general global message
		/// </summary>
		private static void ProcessCallback( CallbackMsg_t msg )
		{
			// Is this a special callback telling us that the call result is ready?
			if ( msg.Type == CallbackType.SteamAPICallCompleted )
			{
				ProcessResult( msg );
				return;
			}

			if ( Callbacks.TryGetValue( msg.Type, out var list ) )
			{
				foreach ( var item in list )
				{
					item.action( msg.Data );
				}
			}
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
				// Do we care? Should we throw errors?
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
		public static async void LoopClientAsync()
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
		public static async void LoopServerAsync()
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
		}

		static Dictionary<ulong, ResultCallback> ResultCallbacks = new Dictionary<ulong, ResultCallback>();

		/// <summary>
		/// Watch for a steam api call
		/// </summary>
		internal static void OnCallComplete( SteamAPICall_t call, Action continuation )
		{
			ResultCallbacks[call.Value] = new ResultCallback
			{
				continuation = continuation
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

		internal static void Wipe()
		{
			Callbacks = new Dictionary<CallbackType, List<Callback>>();
			ResultCallbacks = new Dictionary<ulong, ResultCallback>();
			ClientPipe = 0;
			ServerPipe = 0;
		}
	}
}