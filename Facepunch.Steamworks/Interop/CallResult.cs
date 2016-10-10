using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    internal unsafe abstract class CallResult<T, TSmall> : CallResult where T: new()
    {
        public static FieldInfo[] SourceFields = typeof( TSmall ).GetFields();
        public static FieldInfo[] DestFields = typeof( T ).GetFields();

        public abstract int CallbackId { get; }
        public Action<T> OnResult;        

        internal override void Run( ISteamUtils utils )
        {
            var packSmall = Config.PackSmall;

            var datasize = packSmall ? Marshal.SizeOf( typeof( TSmall ) ) :  Marshal.SizeOf( typeof( T ) );
            var data = stackalloc byte[ datasize ];
            bool failed = false;

            if ( !utils.GetAPICallResult( Handle, (IntPtr) data, datasize, CallbackId, ref failed ) || failed )
            {
                Console.WriteLine( "FAILURE" );
                return;
            }

            if ( packSmall )
            {
                var dataTarget = new T();
                var dataObject = (TSmall)Marshal.PtrToStructure( (IntPtr) data, typeof( TSmall ) );

                for ( int i=0; i<SourceFields.Length; i++ )
                {
                    DestFields[i].SetValue( dataTarget, SourceFields[i].GetValue( dataObject ) );
                }

                if ( OnResult != null )
                    OnResult( dataTarget );
            }
            else
            {
                var dataObject = (T)Marshal.PtrToStructure( (IntPtr) data, typeof( T ) );

                if ( OnResult != null )
                    OnResult( dataObject );
            }


        }        
    }
}