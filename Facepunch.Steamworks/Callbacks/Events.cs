using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	internal static class Events
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
	}

	//
	// Created on registration of a callback
	//
	internal class Event : IDisposable
	{
		Steamworks.ISteamCallback template;
		public Action<Steamworks.ISteamCallback> Action;

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

		public virtual bool IsValid { get { return true; } }

		internal static Event CreateEvent<T>( Action<T> onresult, bool gameserver = false ) where T: struct, Steamworks.ISteamCallback
		{
			var r = new Event();

			r.Action = ( x ) => onresult( (T) x );

			r.template = new T();
			r.vTablePtr = Callback.VTable.GetVTable( r.OnResult, RunStub, SizeStub, r.Allocations );

			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = r.vTablePtr;
			cb.CallbackFlags = gameserver ? (byte)0x02 : (byte)0;
			cb.CallbackId = r.template.GetCallbackId();

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
				Events.AllServer.Add( r );
			else
				Events.AllClient.Add( r );

			return r;
		}

		[MonoPInvokeCallback]
		internal void OnResult( IntPtr self, IntPtr param )
		{
			var value = template.Fill( param );
			Action( value );
		}

		[MonoPInvokeCallback]
		static void RunStub( IntPtr self, IntPtr param, bool failure, SteamAPICall_t call )
		{
			throw new System.Exception( "Something changed in the Steam API and now CCallbackBack is calling the CallResult function [Run( void *pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall )]" );
		}

		[MonoPInvokeCallback]
		static int SizeStub( IntPtr self )
		{
			throw new System.Exception( "Something changed in the Steam API and now CCallbackBack is calling the GetSize function [GetCallbackSizeBytes()]" );
		}
	}
}