using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	internal unsafe class Utf8StringToNative : ICustomMarshaler
	{
		internal static readonly Encoding Utf8NoBom = new UTF8Encoding( false, false );

		public IntPtr MarshalManagedToNative(object managedObj)
		{
			if ( managedObj == null )
				return IntPtr.Zero;

			if ( managedObj is string str )
			{
				fixed ( char* strPtr = str )
				{
					int len = Utf8NoBom.GetByteCount( str );
					var mem = Marshal.AllocHGlobal( len + 1 );

					var wlen = Utf8NoBom.GetBytes( strPtr, str.Length, (byte*)mem, len + 1 );

					( (byte*)mem )[wlen] = 0;

					return mem;
				}
			}

			return IntPtr.Zero;
		}

		public object MarshalNativeToManaged(IntPtr pNativeData) => throw new System.NotImplementedException();
		public void CleanUpNativeData(IntPtr pNativeData) => Marshal.FreeHGlobal( pNativeData );
		public void CleanUpManagedData(object managedObj) => throw new System.NotImplementedException();
		public int GetNativeDataSize() => -1;

		[Preserve]
		public static ICustomMarshaler GetInstance(string cookie) => new Utf8StringToNative();
	}

	internal struct Utf8StringPointer
	{
#pragma warning disable 649
		internal IntPtr ptr;
#pragma warning restore 649

		public unsafe static implicit operator string( Utf8StringPointer p )
		{
			if ( p.ptr == IntPtr.Zero )
				return null;

			var bytes = (byte*)p.ptr;

			var dataLen = 0;
			while ( dataLen < 1024 * 1024 * 64 )
			{
				if ( bytes[dataLen] == 0 )
					break;

				dataLen++;
			}

			return Utf8StringToNative.Utf8NoBom.GetString( bytes, dataLen );
		}
	}
}
