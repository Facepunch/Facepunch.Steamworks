using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	internal struct Utf8StringToNative : IDisposable
	{
		public IntPtr Pointer { get; private set; }

		public unsafe Utf8StringToNative( string value )
		{
			if ( value == null )
			{
				Pointer = IntPtr.Zero;
				return;
			}

			fixed ( char* strPtr = value )
			{
				var len = Utility.Utf8NoBom.GetByteCount( value );
				var mem = Marshal.AllocHGlobal( len + 1 );

				var wlen = Utility.Utf8NoBom.GetBytes( strPtr, value.Length, (byte*)mem, len + 1 );

				( (byte*)mem )[wlen] = 0;

				Pointer = mem;
			}
		}

		public void Dispose()
		{
			if ( Pointer != IntPtr.Zero )
			{
				Marshal.FreeHGlobal( Pointer );
				Pointer = IntPtr.Zero;
			}
		}
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

			return Utility.Utf8NoBom.GetString( bytes, dataLen );
		}
	}
}
