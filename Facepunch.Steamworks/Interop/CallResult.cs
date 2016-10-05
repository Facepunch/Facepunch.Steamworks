using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Valve.Steamworks;

namespace Facepunch.Steamworks.Interop
{
    internal unsafe abstract class CallResult : IDisposable
    {
        public ulong Handle;

        public void Dispose()
        {
            Handle = 0;
        }

        internal abstract void Run( ISteamUtils utils );
    }

    internal unsafe abstract class CallResult<T> : CallResult
    {
        public abstract int CallbackId { get; }
        public Action<T> OnResult;

        internal override void Run( ISteamUtils utils )
        {
            var datasize = Marshal.SizeOf( typeof( T ) );
            var data = stackalloc byte[ datasize ];
            bool failed = false;

            if ( !utils.GetAPICallResult( Handle, (IntPtr) data, datasize, CallbackId, ref failed ) || failed )
            {
                Console.WriteLine( "FAILURE" );
                return;
            }

            var dataObject = (T)Marshal.PtrToStructure( (IntPtr) data, typeof( T ) );

            if ( OnResult != null )
                OnResult( dataObject );
        }        
    }
}