using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	/// <summary>
	/// What Steam believes one of your installed game files should be: its size and its SHA1 hash,
	/// as recorded in the depot manifest. Compare against the file on disk to detect tampering or a
	/// corrupted install without shipping your own manifest.
	/// </summary>
	/// <remarks>
	/// <para>
	/// These are Steam's <b>expected</b> values, not measurements of the file as it currently exists.
	/// A file that has been modified still reports the original size and hash here &#8212; that is
	/// precisely what makes the comparison useful.
	/// </para>
	/// <para>
	/// This is not an anti-cheat mechanism on its own. Anything that can modify the game file can
	/// also modify the code that performs this check.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// var details = await SteamApps.GetFileDetailsAsync( "mygame.exe" );
	/// if ( details.HasValue )
	/// {
	///     var onDisk = new FileInfo( path );
	///     if ( (ulong)onDisk.Length != details.Value.SizeInBytes )
	///         Console.WriteLine( "File size does not match the depot manifest." );
	/// }
	/// </code>
	/// </example>
	public struct FileDetails
	{
		/// <summary>
		/// The size of the file in bytes, as recorded in the depot manifest.
		/// </summary>
		public ulong SizeInBytes;

		/// <summary>
		/// The file's SHA1 hash from the depot manifest, as a hex string. Compare case-insensitively;
		/// this binding does not guarantee a particular casing.
		/// </summary>
		/// <remarks>
		/// SHA1 is not collision resistant. It is fine for spotting an accidentally corrupted or
		/// casually patched file, and worthless against a deliberate attacker.
		/// </remarks>
		public string Sha1;

		/// <summary>
		/// Undocumented flags. Valve's <c>FileDetailsResult_t.m_unFlags</c> in <c>isteamapps.h</c>
		/// carries an empty comment and the SDK defines no constants for it, so there is no
		/// supported way to interpret this value. Ignore it.
		/// </summary>
		public uint Flags;
	}
}
