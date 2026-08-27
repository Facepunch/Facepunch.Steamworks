using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	public static partial class Utility
    {
	    public static readonly Encoding Utf8NoBom = new UTF8Encoding( false, false );

        /// <summary>
        /// Reads a blittable native struct out of unmanaged memory with no allocation.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the only generic struct reader in the library, and it is on its hottest
        /// path - it runs once per registered handler per delivered callback, so the cost is
        /// paid continuously for the lifetime of the process.
        /// </para>
        /// <para>
        /// <b>It used to be <c>Marshal.PtrToStructure</c>, which boxes.</b> Keep that in
        /// mind before reaching for <c>PtrToStructure</c> again anywhere near a callback.
        /// <i>Both</i> of its overloads allocate: the generic <c>PtrToStructure&lt;T&gt;</c>
        /// looks like it should not, but internally it creates an <c>object</c>, marshals
        /// into that, and unboxes on return. Measured on CoreCLR with a blittable 24-byte
        /// struct:
        /// </para>
        /// <code>
        ///   PtrToStructure&lt;T&gt;( ptr )            40 B/op    126 ns/op
        ///   PtrToStructure( ptr, typeof( T ) )   40 B/op    122 ns/op
        ///   *(T*)ptr                              0 B/op    8.5 ns/op
        /// </code>
        /// <para>
        /// 40 bytes is exactly <c>sizeof(T) + 16</c>, one boxed instance, and it grows with
        /// the struct. The two overloads are equivalent, so swapping between them changes
        /// nothing - an earlier attempt to "fix" the allocation that way was measured to
        /// have no effect. Do not repeat it. For a non-blittable struct
        /// <c>PtrToStructure</c> additionally walks the field list one member at a time,
        /// which cost 3,932 ns per delivery of the connection-status callback before
        /// <c>ConnectionInfo</c> was made blittable.
        /// </para>
        /// <para>
        /// The <c>unmanaged</c> constraint is what makes the raw read safe, and it is
        /// checked by the compiler rather than trusted: it refuses any type containing a
        /// managed reference (such as a <c>[MarshalAs(ByValTStr)] string</c> field), which
        /// is exactly the set that genuinely needs marshalling. A type that will not satisfy
        /// it cannot use this path at all and needs a <c>Marshal.PtrToStructure</c> call
        /// written at its own call site, where the cost is visible.
        /// </para>
        /// <para>
        /// <b>One behavioural difference from <c>PtrToStructure</c>, for the record.</b> A
        /// block copy preserves the byte behind a <c>bool</c> field, where the marshaller
        /// normalises it to 0 or 1. Field offsets are unaffected - the generator emits
        /// <c>[MarshalAs(UnmanagedType.I1)]</c> on every <c>bool</c>, one byte on both
        /// sides - and every comparison anyone actually writes agrees, because C# tests a
        /// <c>bool</c> for non-zero. Only <c>x.Flag == true</c>, which compiles to an
        /// equality test against exactly 1, could tell a stray byte apart. It cannot see
        /// one in practice: these bytes come from C++ <c>bool</c> members, which the
        /// platform ABI defines as 0 or 1, and reading any other value is undefined on the
        /// C++ side too. Verified by filling every one of the 217 callback structs with a
        /// random byte pattern and comparing both readers field by field - the only
        /// disagreements found anywhere were these bool bytes.
        /// </para>
        /// <para>
        /// Deliberately not written using <c>System.Runtime.CompilerServices.Unsafe</c>:
        /// that would add a NuGet dependency to a library shipped into Unity projects as
        /// loose DLLs, and a plain pointer dereference measures identically.
        /// </para>
        /// </remarks>
        static internal unsafe T ToTypeUnmanaged<T>( this IntPtr ptr ) where T : unmanaged
        {
            if ( ptr == IntPtr.Zero )
                return default;

            return *(T*)ptr;
        }

        static internal object ToType( this IntPtr ptr, System.Type t )
        {
            if ( ptr == IntPtr.Zero )
                return default;

            return Marshal.PtrToStructure( ptr, t );
        }

        static internal uint Swap( uint x )
        {
            return ((x & 0x000000ff) << 24) +
                   ((x & 0x0000ff00) << 8) +
                   ((x & 0x00ff0000) >> 8) +
                   ((x & 0xff000000) >> 24);
        }

        static public uint IpToInt32( this IPAddress ipAddress )
        {
            return Swap( (uint) ipAddress.Address );
        }

        static public IPAddress Int32ToIp( uint ipAddress )
        {
            return new IPAddress( Swap( ipAddress ) );
        }

		public static string FormatPrice(string currency, double price)
        {
			var decimaled = price.ToString("0.00");

            switch (currency)
            {
                case "AED": return $"{decimaled}د.إ";
                case "ARS": return $"${decimaled} ARS";
                case "AUD": return $"A${decimaled}";
                case "BRL": return $"R${decimaled}";
                case "CAD": return $"C${decimaled}";
                case "CHF": return $"Fr. {decimaled}";
                case "CLP": return $"${decimaled} CLP";
                case "CNY": return $"{decimaled}元";
                case "COP": return $"COL$ {decimaled}";
                case "CRC": return $"₡{decimaled}";
                case "EUR": return $"€{decimaled}";
                case "SEK": return $"{decimaled}kr";
                case "GBP": return $"£{decimaled}";
                case "HKD": return $"HK${decimaled}";
                case "ILS": return $"₪{decimaled}";
                case "IDR": return $"Rp{decimaled}";
                case "INR": return $"₹{decimaled}";
                case "JPY": return $"¥{decimaled}";
                case "KRW": return $"₩{decimaled}";
                case "KWD": return $"KD {decimaled}";
                case "KZT": return $"{decimaled}₸";
                case "MXN": return $"Mex${decimaled}";
                case "MYR": return $"RM {decimaled}";
                case "NOK": return $"{decimaled} kr";
                case "NZD": return $"${decimaled} NZD";
                case "PEN": return $"S/. {decimaled}";
                case "PHP": return $"₱{decimaled}";
                case "PLN": return $"{decimaled}zł";
                case "QAR": return $"QR {decimaled}";
                case "RUB": return $"{decimaled}₽";
                case "SAR": return $"SR {decimaled}";
                case "SGD": return $"S${decimaled}";
                case "THB": return $"฿{decimaled}";
                case "TRY": return $"₺{decimaled}";
                case "TWD": return $"NT$ {decimaled}";
                case "UAH": return $"₴{decimaled}";
                case "USD": return $"${decimaled}";
                case "UYU": return $"$U {decimaled}"; // yes the U goes after $
                case "VND": return $"₫{decimaled}";
                case "ZAR": return $"R {decimaled}";

                // TODO - check all of them https://partner.steamgames.com/doc/store/pricing/currencies

                default: return $"{decimaled} {currency}";
            }
        }

		static readonly byte[] readBuffer = new byte[1024 * 8];

		public static string ReadNullTerminatedUTF8String( this BinaryReader br )
		{
			lock ( readBuffer )
			{
				byte chr;
				int i = 0;
				while ( (chr = br.ReadByte()) != 0 && i < readBuffer.Length )
				{
					readBuffer[i] = chr;
					i++;
				}

				return Utf8NoBom.GetString( readBuffer, 0, i );
			}
		}

		/// <summary>
		/// Decodes a fixed size native UTF-8 buffer up to its null terminator.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The generated structs hold their native <c>char[N]</c> members as fixed size
		/// buffers rather than <c>[MarshalAs(ByValArray)] byte[]</c>, because the array form
		/// heap-allocates on every marshal. A fixed size buffer carries no length of its own,
		/// so the terminator has to be found by hand.
		/// </para>
		/// <para>
		/// <paramref name="bufferLength"/> is what stops that scan running off the end of the
		/// struct. Steam is under no obligation to terminate a buffer it filled completely -
		/// a 129 byte title holding 129 bytes of text is legal - and an unbounded scan would
		/// then walk into whatever field follows. Truncating at the buffer length is the same
		/// thing the native side does.
		/// </para>
		/// </remarks>
		internal static unsafe string ReadNullTerminatedUTF8String( byte* buffer, int bufferLength )
		{
			if ( buffer == null || bufferLength <= 0 )
				return string.Empty;

			var length = 0;
			while ( length < bufferLength && buffer[length] != 0 )
				length++;

			if ( length == 0 )
				return string.Empty;

			return Utf8NoBom.GetString( buffer, length );
		}
	}
}
