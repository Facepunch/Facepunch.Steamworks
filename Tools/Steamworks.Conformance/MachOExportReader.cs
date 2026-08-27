using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Steamworks.Conformance
{
	/// <summary>
	/// Reads the exported symbol names out of a Mach-O dynamic library — a macOS
	/// <c>libsteam_api.dylib</c>.
	///
	/// <para>
	/// Handles both a thin single-architecture image and a "fat"/universal binary, which is
	/// simply a header followed by several complete Mach-O images (Steam ships x86_64 and
	/// arm64 slices). For a fat file we union the exports of every slice: a P/Invoke only
	/// needs the symbol to be present in the slice that actually loads, and reporting the
	/// union avoids false failures when one slice is checked from the wrong host arch.
	/// </para>
	///
	/// <para>
	/// Within a slice we walk the load commands for <c>LC_SYMTAB</c>, then read the symbol
	/// table (an array of <c>nlist</c>/<c>nlist_64</c>). A symbol is exported when it is
	/// external (<c>N_EXT</c>) and actually defined here (its type is not <c>N_UNDF</c>).
	/// Mach-O prefixes C symbols with an underscore, which we strip so the names line up
	/// with the entry points written in <c>DllImport</c>.
	/// </para>
	/// </summary>
	internal static class MachOExportReader
	{
		private const uint MH_MAGIC_32 = 0xFEEDFACE;
		private const uint MH_CIGAM_32 = 0xCEFAEDFE;
		private const uint MH_MAGIC_64 = 0xFEEDFACF;
		private const uint MH_CIGAM_64 = 0xCFFAEDFE;
		private const uint FAT_MAGIC = 0xCAFEBABE;   // fat header is always big-endian on disk
		private const uint FAT_CIGAM = 0xBEBAFECA;

		private const uint LC_SYMTAB = 0x2;

		private const byte N_STAB = 0xE0;            // debug symbol mask - skip these
		private const byte N_TYPE = 0x0E;
		private const byte N_EXT = 0x01;
		private const byte N_UNDF = 0x00;

		public static SortedSet<string> ReadExportedNames( string path )
		{
			var bytes = File.ReadAllBytes( path );
			var names = new SortedSet<string>( StringComparer.Ordinal );

			if ( bytes.Length < 8 )
				throw new InvalidDataException( $"{path}: file is too small to be Mach-O." );

			var magic = ReadUInt32BigEndian( bytes, 0 );
			if ( magic is FAT_MAGIC or FAT_CIGAM )
			{
				// struct fat_header { uint32 magic; uint32 nfat_arch; }  (big-endian)
				// struct fat_arch   { cputype, cpusubtype, offset, size, align }  (5 x uint32)
				var archCount = ReadUInt32BigEndian( bytes, 4 );
				for ( uint i = 0; i < archCount; i++ )
				{
					var arch = 8 + (int)(i * 20);
					if ( arch + 20 > bytes.Length ) break;

					var sliceOffset = (int)ReadUInt32BigEndian( bytes, arch + 8 );
					if ( sliceOffset <= 0 || sliceOffset >= bytes.Length ) continue;

					ReadSlice( bytes, sliceOffset, names );
				}

				return names;
			}

			ReadSlice( bytes, 0, names );
			return names;
		}

		private static void ReadSlice( byte[] bytes, int baseOffset, SortedSet<string> names )
		{
			if ( baseOffset + 32 > bytes.Length ) return;

			var magic = BitConverter.ToUInt32( bytes, baseOffset );
			bool is64;

			switch ( magic )
			{
				case MH_MAGIC_64:
				case MH_CIGAM_64: is64 = true; break;
				case MH_MAGIC_32:
				case MH_CIGAM_32: is64 = false; break;
				default: return;                       // not a Mach-O slice; ignore
			}

			// struct mach_header[_64] { magic, cputype, cpusubtype, filetype, ncmds,
			//                           sizeofcmds, flags [, reserved(64-bit only)] }
			var commandCount = BitConverter.ToUInt32( bytes, baseOffset + 16 );
			var cursor = baseOffset + (is64 ? 32 : 28);

			for ( uint i = 0; i < commandCount; i++ )
			{
				if ( cursor + 8 > bytes.Length ) return;

				var command = BitConverter.ToUInt32( bytes, cursor );
				var commandSize = (int)BitConverter.ToUInt32( bytes, cursor + 4 );
				if ( commandSize <= 0 ) return;

				if ( command == LC_SYMTAB )
				{
					// struct symtab_command { cmd, cmdsize, symoff, nsyms, stroff, strsize }
					var symbolOffset = baseOffset + (int)BitConverter.ToUInt32( bytes, cursor + 8 );
					var symbolCount = BitConverter.ToUInt32( bytes, cursor + 12 );
					var stringOffset = baseOffset + (int)BitConverter.ToUInt32( bytes, cursor + 16 );

					var entrySize = is64 ? 16 : 12;
					for ( uint s = 0; s < symbolCount; s++ )
					{
						var sym = symbolOffset + (int)(s * entrySize);
						if ( sym + entrySize > bytes.Length ) break;

						// struct nlist[_64] { uint32 n_strx; uint8 n_type; uint8 n_sect;
						//                     uint16 n_desc; uintptr n_value; }
						var stringIndex = BitConverter.ToUInt32( bytes, sym );
						var type = bytes[sym + 4];

						if ( (type & N_STAB) != 0 ) continue;             // debug entry
						if ( (type & N_EXT) == 0 ) continue;             // not external
						if ( (type & N_TYPE) == N_UNDF ) continue;       // imported, not defined here
						if ( stringIndex == 0 ) continue;

						var start = stringOffset + (int)stringIndex;
						if ( start < 0 || start >= bytes.Length ) continue;

						var end = start;
						while ( end < bytes.Length && bytes[end] != 0 ) end++;

						var name = Encoding.ASCII.GetString( bytes, start, end - start );

						// Mach-O decorates C symbols with a leading underscore.
						if ( name.Length > 1 && name[0] == '_' )
							name = name.Substring( 1 );

						names.Add( name );
					}
				}

				cursor += commandSize;
			}
		}

		private static uint ReadUInt32BigEndian( byte[] b, int offset ) =>
			(uint)((b[offset] << 24) | (b[offset + 1] << 16) | (b[offset + 2] << 8) | b[offset + 3]);
	}
}
