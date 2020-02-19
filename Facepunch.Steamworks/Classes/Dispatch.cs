using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Steamworks.Data;
using Steamworks;

namespace Steamworks
{
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
			public IntPtr m_pubParam; // Points to the callback structure
			public int m_cubParam; // Size of the data pointed to by m_pubParam
		};

		#endregion

		internal static HSteamPipe ClientPipe { get; set; }
		internal static HSteamPipe ServerPipe { get; set; }

		public static void Init()
		{
			SteamAPI_ManualDispatch_Init();
		}

		public static void Frame()
		{
			if ( ClientPipe != 0 )
				Frame( ClientPipe );
		
			if ( ServerPipe != 0)
				Frame( ServerPipe );
		}

		public static void Frame( HSteamPipe pipe )
		{ 
			SteamAPI_ManualDispatch_RunFrame( pipe );

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

		private static void ProcessCallback( CallbackMsg_t msg )
		{
			if ( msg.Type == CallbackType.SteamAPICallCompleted )
			{
				ProcessResult( msg );
				return;
			}

			Console.WriteLine( $"Callback: {msg.Type}" );
		}

		private static void ProcessResult( CallbackMsg_t msg )
		{
			var result = msg.m_pubParam.ToType<SteamAPICallCompleted_t>();

			Console.WriteLine( $"Result: {result.AsyncCall} / {result.Callback}" );

			//
			// Do we have an entry added via OnCallComplete
			//
			if ( !ResultCallbacks.TryGetValue( result.AsyncCall, out var callbackInfo ) )
			{
				// Do we care? Should we throw errors?
				return;
			}

			ResultCallbacks.Remove( result.AsyncCall );

			// At this point whatever async routine called this 
			// continues running.
			callbackInfo.continuation();
		}

		public static async void LoopClientAsync()
		{
			while ( ClientPipe != 0 )
			{
				Frame( ClientPipe );
				await Task.Delay( 16 );
			}

			Console.WriteLine( $"Exiting ClientPipe: {ClientPipe}" );
		}

		public static async void LoopServerAsync()
		{
			while ( ServerPipe != 0 )
			{
				Frame( ServerPipe );
				await Task.Delay( 32 );
			}

			Console.WriteLine( $"Exiting ServerPipe: {ServerPipe}" );
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

		static Dictionary<int, List<Callback>> Callbacks = new Dictionary<int, List<Callback>>();

		internal static void Install<T>( Action<T> p, bool server = false ) where T : ICallbackData
		{
			var t = default( T );

			if ( !Callbacks.TryGetValue( t.CallbackId, out var list ) )
			{
				list = new List<Callback>();
				Callbacks[t.CallbackId] = list;
			}

			list.Add( new Callback
			{
				action = x => p( x.ToType<T>() ),
				server = server
			} );
		}

		internal static void Wipe()
		{
			Callbacks = new Dictionary<int, List<Callback>>();
			ResultCallbacks = new Dictionary<ulong, ResultCallback>();
			ClientPipe = 0;
			ServerPipe = 0;
		}
	}
}