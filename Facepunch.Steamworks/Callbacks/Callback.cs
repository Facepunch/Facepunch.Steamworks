using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Steamworks.Data;

namespace Steamworks
{
	[StructLayout( LayoutKind.Sequential )]
    internal partial class Callback
    {
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void Run( IntPtr thisptr, IntPtr pvParam );

		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void RunCall( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall );

		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetCallbackSizeBytes( IntPtr thisptr );

		internal enum Flags : byte
        {
            Registered = 0x01,
            GameServer = 0x02
        }

        public IntPtr vTablePtr;
        public byte CallbackFlags;
        public int CallbackId;

		//
		// These are functions that are on CCallback but are never called
		// We could just send a IntPtr.Zero but it's probably safer to throw a 
		// big apeshit message if steam changes its behaviour at some point
		//
		[MonoPInvokeCallback]
		internal static void RunStub( IntPtr self, IntPtr param, bool failure, SteamAPICall_t call ) =>
			throw new System.Exception( "Something changed in the Steam API and now CCallbackBack is calling the CallResult function [Run( void *pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall )]" );

		[MonoPInvokeCallback]
		internal static int SizeStub( IntPtr self ) =>
			throw new System.Exception( "Something changed in the Steam API and now CCallbackBack is calling the GetSize function [GetCallbackSizeBytes()]" );
	};
}
