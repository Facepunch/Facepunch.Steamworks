using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


//
// THANSK AGAIN TO STEAMWORKS.NET
//
// https://github.com/rlabrecque/Steamworks.NET/blob/97935154cf08f60da92c55e2c73ee60a8f456e03/Plugins/Steamworks.NET/CallbackDispatcher.cs
//

// Calling Conventions:
// Unity x86 Windows        - StdCall (No this pointer)
// Unity x86 Linux          - Cdecl
// Unity x86 OSX            - Cdecl
// Unity x64 Windows        - Cdecl
// Unity x64 Linux          - Cdecl
// Unity x64 OSX            - Cdecl
// Microsoft x86 Windows    - ThisCall
// Microsoft x64 Windows    - ThisCall
// Mono x86 Linux           - Cdecl
// Mono x86 OSX             - Cdecl
// Mono x64 Linux           - Cdecl
// Mono x64 OSX             - Cdecl
// Mono on Windows is probably not supported.

namespace Facepunch.Steamworks.Interop
{
    internal partial class Callback : IDisposable
    {
        List<GCHandle> Handles = new List<GCHandle>();

        public virtual void Dispose()
        {
            foreach ( var handle in Handles )
            {
                handle.Free();
            }

            Handles = null;
        }

        internal void AddHandle( GCHandle gCHandle )
        {
            Handles.Add( gCHandle );
        }
    }

    internal partial class Callback<T> : Callback
    {
        public int CallbackId = 0;
        public bool GameServer = false;
        public Action<T> Function;

        private IntPtr vTablePtr = IntPtr.Zero;
        private GCHandle callbackPin;

        private readonly int m_size = Marshal.SizeOf(typeof(T));

        public Callback( bool gameserver, int callbackid, Action<T> func )
        {
            GameServer = gameserver;
            CallbackId = callbackid;
            Function = func;

            BuildVTable();

            Valve.Steamworks.SteamAPI.RegisterCallback( callbackPin.AddrOfPinnedObject(), CallbackId );
        }

        public override void Dispose()
        {
            if ( callbackPin.IsAllocated )
            {
                Valve.Steamworks.SteamAPI.UnregisterCallback( callbackPin.AddrOfPinnedObject() );
                callbackPin.Free();
            }

            if ( vTablePtr != IntPtr.Zero )
            {
                Marshal.FreeHGlobal( vTablePtr );
                vTablePtr = IntPtr.Zero;
            }

            base.Dispose();
        }

        private void OnRunCallback( IntPtr ptr )
        {
            T data = (T) Marshal.PtrToStructure( ptr, typeof(T) );
            Function( data );
        }


        private int GetSize()
        {
            throw new System.NotImplementedException();
        }

        // Steamworks.NET Specific
        private void BuildVTable()
        {
            InitVTable();

            var callbackBase = new CallbackBase()
            {
                vTablePtr = vTablePtr,
                CallbackFlags = GameServer ? (byte) CallbackBase.Flags.GameServer : (byte) 0,
                CallbackId = CallbackId
            };
            callbackPin = GCHandle.Alloc( callbackBase, GCHandleType.Pinned );
        }

        void InitVTable()
        {
            if ( Config.UseThisCall )
            {
                vTablePtr = VTable.This.Callback.Get( OnRunCallback, GetSize, this );
                return;
            }

            throw new System.NotImplementedException();
        }
    }

    [StructLayout( LayoutKind.Sequential )]
    internal class CallbackBase
    {
        internal enum Flags : byte
        {
            Registered = 0x01,
            GameServer = 0x02
        }

        public IntPtr vTablePtr;
        public byte CallbackFlags;
        public int CallbackId;
    };
}