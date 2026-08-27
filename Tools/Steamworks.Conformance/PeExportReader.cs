using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Steamworks.Conformance
{
	/// <summary>
	/// Reads the exported symbol names out of a Windows PE image (a .dll).
	///
	/// <para>
	/// We parse the file by hand instead of calling <c>dumpbin</c> or the Win32 loader for
	/// three reasons: it needs no Visual Studio install, it never loads or executes the
	/// target DLL, and it works when the checking process is a different architecture than
	/// the DLL being checked (we can inspect the 32-bit <c>steam_api.dll</c> from a 64-bit
	/// process).
	/// </para>
	///
	/// <para>
	/// Layout walked here, all of it documented in the Microsoft PE/COFF specification:
	/// <c>MZ header</c> → <c>e_lfanew</c> → <c>PE\0\0</c> → <c>COFF header</c> →
	/// <c>optional header</c> → <c>data directory[0]</c> (the export directory) →
	/// <c>IMAGE_EXPORT_DIRECTORY</c> → <c>AddressOfNames</c> → an array of RVAs, each
	/// pointing at a NUL-terminated ASCII symbol name.
	/// </para>
	/// </summary>
	internal static class PeExportReader
	{
		/// <summary>
		/// Returns every symbol name in the image's export name table.
		/// Returns an empty set for a valid PE image that exports nothing.
		/// </summary>
		/// <exception cref="InvalidDataException">The file is not a well-formed PE image.</exception>
		public static SortedSet<string> ReadExportedNames( string path )
		{
			var bytes = File.ReadAllBytes( path );
			var names = new SortedSet<string>( StringComparer.Ordinal );

			// --- MZ (DOS) header -------------------------------------------------------
			if ( bytes.Length < 0x40 || bytes[0] != (byte)'M' || bytes[1] != (byte)'Z' )
				throw new InvalidDataException( $"{path}: not an MZ/PE image." );

			var peOffset = BitConverter.ToInt32( bytes, 0x3C );
			if ( peOffset <= 0 || peOffset + 24 > bytes.Length || BitConverter.ToUInt32( bytes, peOffset ) != 0x00004550 )
				throw new InvalidDataException( $"{path}: bad PE signature." );

			// --- COFF header -----------------------------------------------------------
			var coff = peOffset + 4;
			int sectionCount = BitConverter.ToUInt16( bytes, coff + 2 );
			int optionalHeaderSize = BitConverter.ToUInt16( bytes, coff + 16 );
			var optionalHeader = coff + 20;

			// --- Optional header -------------------------------------------------------
			// 0x10B = PE32 (x86), 0x20B = PE32+ (x64). The only thing that differs for us
			// is where the data directories start, because PE32+ widens several fields.
			var magic = BitConverter.ToUInt16( bytes, optionalHeader );
			var isPe32Plus = magic == 0x20B;
			var dataDirectories = optionalHeader + (isPe32Plus ? 112 : 96);

			// Data directory index 0 is always the export table.
			var exportRva = BitConverter.ToUInt32( bytes, dataDirectories + 0 );
			var exportSize = BitConverter.ToUInt32( bytes, dataDirectories + 4 );
			if ( exportRva == 0 || exportSize == 0 )
				return names;

			// --- Section table, used to translate RVAs into file offsets ---------------
			var sections = new (uint VirtualAddress, uint VirtualSize, uint RawPointer, uint RawSize)[sectionCount];
			var sectionTable = optionalHeader + optionalHeaderSize;
			for ( int i = 0; i < sectionCount; i++ )
			{
				var s = sectionTable + i * 40;
				sections[i] = (
					BitConverter.ToUInt32( bytes, s + 12 ),   // VirtualAddress
					BitConverter.ToUInt32( bytes, s + 8 ),    // VirtualSize
					BitConverter.ToUInt32( bytes, s + 20 ),   // PointerToRawData
					BitConverter.ToUInt32( bytes, s + 16 ) ); // SizeOfRawData
			}

			int ToFileOffset( uint rva )
			{
				foreach ( var s in sections )
				{
					// A section's on-disk size and in-memory size can differ; accept the larger
					// span so we do not reject RVAs that land in the zero-filled tail.
					var span = Math.Max( s.VirtualSize, s.RawSize );
					if ( rva >= s.VirtualAddress && rva < s.VirtualAddress + span )
						return (int)(s.RawPointer + (rva - s.VirtualAddress));
				}

				throw new InvalidDataException( $"{path}: RVA 0x{rva:X} falls outside every section." );
			}

			// --- IMAGE_EXPORT_DIRECTORY ------------------------------------------------
			var exportDirectory = ToFileOffset( exportRva );
			var nameCount = BitConverter.ToUInt32( bytes, exportDirectory + 24 );      // NumberOfNames
			var nameTableRva = BitConverter.ToUInt32( bytes, exportDirectory + 32 );   // AddressOfNames
			if ( nameCount == 0 || nameTableRva == 0 )
				return names;

			var nameTable = ToFileOffset( nameTableRva );
			for ( uint i = 0; i < nameCount; i++ )
			{
				var nameRva = BitConverter.ToUInt32( bytes, nameTable + (int)(i * 4) );
				var start = ToFileOffset( nameRva );

				var end = start;
				while ( end < bytes.Length && bytes[end] != 0 )
					end++;

				names.Add( Encoding.ASCII.GetString( bytes, start, end - start ) );
			}

			return names;
		}
	}
}
