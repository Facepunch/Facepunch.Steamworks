using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	internal unsafe class Utf8StringToNative : ICustomMarshaler
	{
		public IntPtr MarshalManagedToNative( object managedObj )
		{
			if ( managedObj == null )
				return IntPtr.Zero;

			if ( managedObj is string str )
			{
				fixed ( char* strPtr = str )
				{
					int len = Encoding.UTF8.GetByteCount( str );
					var mem = Marshal.AllocHGlobal( len + 1 );

					var wlen = System.Text.Encoding.UTF8.GetBytes( strPtr, str.Length, (byte*)mem, len + 1 );

					((byte*)mem)[wlen] = 0;

					return mem;
				}
			}

			return IntPtr.Zero;
		}

		public object MarshalNativeToManaged( IntPtr pNativeData ) => throw new System.NotImplementedException();
		public void CleanUpNativeData( IntPtr pNativeData ) => Marshal.FreeHGlobal( pNativeData );
		public void CleanUpManagedData( object managedObj ) => throw new System.NotImplementedException();
		public int GetNativeDataSize() => -1;

		public static ICustomMarshaler GetInstance( string cookie ) => new Utf8StringToNative();
	}

	internal unsafe class Utf8StringFromNative : ICustomMarshaler
	{
		public IntPtr MarshalManagedToNative( object managedObj ) => throw new System.NotImplementedException();

		public object MarshalNativeToManaged( IntPtr pNativeData )
		{
			if ( pNativeData == IntPtr.Zero )
				return null;

			var bytes = (byte*)pNativeData;

			var dataLen = 0;
			while ( dataLen < 1024 * 1024 * 8 )
			{
				if ( bytes[dataLen] == 0 )
					break;

				dataLen++;
			}

			var str = Encoding.UTF8.GetString( bytes, dataLen );
			return str;
		}

		public void CleanUpNativeData( IntPtr pNativeData ) { }

		public void CleanUpManagedData( object managedObj ) { }

		public int GetNativeDataSize() => -1;

		public static ICustomMarshaler GetInstance( string cookie ) => new Utf8StringFromNative();
	}
}
