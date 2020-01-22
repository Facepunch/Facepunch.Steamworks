using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Steamworks
{
	//
	// Created on registration of a callback
	//
	internal class Event : IDisposable
	{
		internal static List<IDisposable> AllClient = new List<IDisposable>();
		internal static List<IDisposable> AllServer = new List<IDisposable>();

		internal static void DisposeAllClient()
		{
			foreach ( var a in AllClient.ToArray() )
			{
				a.Dispose();
			}

			AllClient.Clear();
		}

		internal static void DisposeAllServer()
		{
			foreach ( var a in AllServer.ToArray() )
			{
				a.Dispose();
			}

			AllServer.Clear();
		}

		internal static void Register( Callback.Run func, int size, int callbackId, bool gameserver )
		{
			var r = new Event();
			r.vTablePtr = BuildVTable( func, r.Allocations );

			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = r.vTablePtr;
			cb.CallbackFlags = gameserver ? (byte)0x02 : (byte)0;
			cb.CallbackId = callbackId;

			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			r.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );

			//
			// Register the callback with Steam
			//
			SteamClient.RegisterCallback( r.PinnedCallback.AddrOfPinnedObject(), cb.CallbackId );

			r.IsAllocated = true;

			if ( gameserver )
				Event.AllServer.Add( r );
			else
				Event.AllClient.Add( r );
		}

		static IntPtr BuildVTable( Callback.Run run, List<GCHandle> allocations )
		{
			var RunStub = (Callback.RunCall)Callback.RunStub;
			var SizeStub = (Callback.GetCallbackSizeBytes)Callback.SizeStub;

			allocations.Add( GCHandle.Alloc( run ) );
			allocations.Add( GCHandle.Alloc( RunStub ) );
			allocations.Add( GCHandle.Alloc( SizeStub ) );

			var a = Marshal.GetFunctionPointerForDelegate<Callback.Run>( run );
			var b = Marshal.GetFunctionPointerForDelegate<Callback.RunCall>( RunStub );
			var c = Marshal.GetFunctionPointerForDelegate<Callback.GetCallbackSizeBytes>( SizeStub );

			var vt = Marshal.AllocHGlobal( IntPtr.Size * 3 );

			// Windows switches the function positions
			#if PLATFORM_WIN
			Marshal.WriteIntPtr( vt, IntPtr.Size * 0, b );
			Marshal.WriteIntPtr( vt, IntPtr.Size * 1, a );
			Marshal.WriteIntPtr( vt, IntPtr.Size * 2, c );
			#else
			Marshal.WriteIntPtr( vt, IntPtr.Size * 0, a );
			Marshal.WriteIntPtr( vt, IntPtr.Size * 1, b );
			Marshal.WriteIntPtr( vt, IntPtr.Size * 2, c );
			#endif

			return vt;
		}

		bool IsAllocated;
		List<GCHandle> Allocations = new List<GCHandle>();
		internal IntPtr vTablePtr;
		internal GCHandle PinnedCallback;


		public void Dispose()
		{
			if ( !IsAllocated ) return;
			IsAllocated = false;

			if ( !PinnedCallback.IsAllocated )
				throw new System.Exception( "Callback isn't allocated!?" );

			SteamClient.UnregisterCallback( PinnedCallback.AddrOfPinnedObject() );

			foreach ( var a in Allocations )
			{
				if ( a.IsAllocated )
					a.Free();
			}

			Allocations = null;

			PinnedCallback.Free();

			if ( vTablePtr != IntPtr.Zero )
			{
				Marshal.FreeHGlobal( vTablePtr );
				vTablePtr = IntPtr.Zero;
			}
		}

		~Event()
		{
			Dispose();
		}

	}
}