using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Steamworks.Conformance
{
	/// <summary>
	/// Reads the exported (dynamic) symbol names out of an ELF shared object — a Linux
	/// <c>libsteam_api.so</c>.
	///
	/// <para>
	/// This matters for this library specifically: the headless Linux dedicated server is a
	/// primary target, and it is the one platform where a native/managed ABI mismatch would
	/// otherwise go completely unverified from a Windows dev box.
	/// </para>
	///
	/// <para>
	/// We walk the section header table looking for <c>SHT_DYNSYM</c> (the dynamic symbol
	/// table). Its <c>sh_link</c> field names the string table holding the symbol names. A
	/// symbol counts as "exported" when it is defined in this object (<c>st_shndx</c> is not
	/// <c>SHN_UNDEF</c>) and has <c>GLOBAL</c> or <c>WEAK</c> binding — that is exactly the
	/// set a dynamic linker will resolve for a caller, which is what P/Invoke needs.
	/// </para>
	/// </summary>
	internal static class ElfExportReader
	{
		private const uint SHT_DYNSYM = 11;
		private const ushort SHN_UNDEF = 0;
		private const byte STB_GLOBAL = 1;
		private const byte STB_WEAK = 2;

		public static bool IsElf( byte[] bytes ) =>
			bytes.Length > 4 && bytes[0] == 0x7F && bytes[1] == (byte)'E' && bytes[2] == (byte)'L' && bytes[3] == (byte)'F';

		public static SortedSet<string> ReadExportedNames( string path )
		{
			var bytes = File.ReadAllBytes( path );
			var names = new SortedSet<string>( StringComparer.Ordinal );

			if ( !IsElf( bytes ) )
				throw new InvalidDataException( $"{path}: not an ELF image." );

			var is64 = bytes[4] == 2;                 // EI_CLASS: 1 = ELF32, 2 = ELF64
			var isLittleEndian = bytes[5] == 1;       // EI_DATA
			if ( !isLittleEndian )
				throw new InvalidDataException( $"{path}: big-endian ELF is not supported (Steam ships little-endian only)." );

			ulong ReadUInt( int offset, int size ) => size switch
			{
				2 => BitConverter.ToUInt16( bytes, offset ),
				4 => BitConverter.ToUInt32( bytes, offset ),
				8 => BitConverter.ToUInt64( bytes, offset ),
				_ => throw new ArgumentOutOfRangeException( nameof( size ) )
			};

			// Section header table location differs between ELF32 and ELF64 because the
			// address/offset fields widen from 4 to 8 bytes.
			var shOff = is64 ? (long)ReadUInt( 0x28, 8 ) : (long)ReadUInt( 0x20, 4 );
			var shEntSize = (int)ReadUInt( is64 ? 0x3A : 0x2E, 2 );
			var shNum = (int)ReadUInt( is64 ? 0x3C : 0x30, 2 );

			if ( shOff <= 0 || shNum <= 0 )
				return names;

			// Section header field offsets.
			//                       ELF32                ELF64
			// sh_type               0x04 (4)             0x04 (4)
			// sh_offset             0x10 (4)             0x18 (8)
			// sh_size               0x14 (4)             0x20 (8)
			// sh_link               0x18 (4)             0x28 (4)
			// sh_entsize            0x24 (4)             0x38 (8)
			for ( int i = 0; i < shNum; i++ )
			{
				var sh = (int)(shOff + i * shEntSize);
				if ( sh + shEntSize > bytes.Length ) break;

				var type = (uint)ReadUInt( sh + 4, 4 );
				if ( type != SHT_DYNSYM ) continue;

				var symOffset = (long)(is64 ? ReadUInt( sh + 0x18, 8 ) : ReadUInt( sh + 0x10, 4 ));
				var symSize = (long)(is64 ? ReadUInt( sh + 0x20, 8 ) : ReadUInt( sh + 0x14, 4 ));
				var strIndex = (int)ReadUInt( sh + (is64 ? 0x28 : 0x18), 4 );
				var entSize = (long)(is64 ? ReadUInt( sh + 0x38, 8 ) : ReadUInt( sh + 0x24, 4 ));
				if ( entSize <= 0 ) entSize = is64 ? 24 : 16;

				// The linked string table holds the NUL-terminated symbol names.
				var strSh = (int)(shOff + strIndex * shEntSize);
				if ( strSh + shEntSize > bytes.Length ) continue;
				var strOffset = (long)(is64 ? ReadUInt( strSh + 0x18, 8 ) : ReadUInt( strSh + 0x10, 4 ));

				var count = symSize / entSize;
				for ( long s = 0; s < count; s++ )
				{
					var sym = (int)(symOffset + s * entSize);
					if ( sym + entSize > bytes.Length ) break;

					// Symbol field offsets differ between classes:
					//            ELF32                       ELF64
					// st_name    0x00 (4)                    0x00 (4)
					// st_info    0x0C (1)                    0x04 (1)
					// st_shndx   0x0E (2)                    0x06 (2)
					var nameIndex = (uint)ReadUInt( sym + 0, 4 );
					var info = bytes[sym + (is64 ? 4 : 0x0C)];
					var shndx = (ushort)ReadUInt( sym + (is64 ? 6 : 0x0E), 2 );

					if ( nameIndex == 0 ) continue;
					if ( shndx == SHN_UNDEF ) continue;      // imported, not exported

					var binding = (byte)(info >> 4);
					if ( binding != STB_GLOBAL && binding != STB_WEAK ) continue;

					var start = (int)(strOffset + nameIndex);
					if ( start < 0 || start >= bytes.Length ) continue;

					var end = start;
					while ( end < bytes.Length && bytes[end] != 0 ) end++;

					names.Add( Encoding.ASCII.GetString( bytes, start, end - start ) );
				}
			}

			return names;
		}
	}
}
