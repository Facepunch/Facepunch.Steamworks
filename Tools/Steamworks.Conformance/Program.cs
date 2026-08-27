using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Steamworks.Conformance
{
	/// <summary>
	/// Verifies that every native symbol this library P/Invokes actually exists in the
	/// Steamworks binary we ship.
	///
	/// <para>
	/// This closes a real hole. A <c>DllImport</c> entry point is just a string; nothing in
	/// the C# compiler checks it. If Valve renames or removes a function - which they do -
	/// the binding still compiles and only fails at the moment a game calls it, usually in
	/// a shipped build on a player's machine as an <c>EntryPointNotFoundException</c>.
	/// </para>
	///
	/// <para>
	/// The check is completely static: it reads metadata and PE export tables off disk. It
	/// needs no Steam client, no Steam account, no network, and no Steam login, so it can
	/// run on any build agent. This is the strongest correctness guarantee available for
	/// this codebase without a live Steam environment.
	/// </para>
	///
	/// <para>Exit codes: <c>0</c> conformant, <c>1</c> mismatches found, <c>2</c> bad usage or unreadable input.</para>
	/// </summary>
	internal static class Program
	{
		/// <summary>
		/// Entry points we deliberately allow to be missing from the native binary, with the
		/// reason. Keep this list SHORT and justified - every entry is a call that will throw
		/// at runtime if it is ever reached.
		/// </summary>
		private static readonly Dictionary<string, string> KnownMissing = new( StringComparer.Ordinal )
		{
			// Nothing is currently waived. ISteamAppList used to belong here; Valve removed the
			// interface from the SDK and the dead bindings were deleted rather than waived.
		};

		/// <summary>
		/// Picks the right binary-format reader from the file's magic bytes, so the same
		/// command works against a Windows <c>steam_api64.dll</c> and a Linux
		/// <c>libsteam_api.so</c>. Format is detected from content, not from the extension,
		/// because Steam's Linux libraries are not always named <c>.so</c> on disk.
		/// </summary>
		private static SortedSet<string> ReadNativeExports( string path )
		{
			var header = new byte[4];
			using ( var stream = File.OpenRead( path ) )
			{
				if ( stream.Read( header, 0, 4 ) < 4 )
					throw new InvalidDataException( $"{path}: file is too small to identify." );
			}

			if ( header[0] == 0x7F && header[1] == (byte)'E' && header[2] == (byte)'L' && header[3] == (byte)'F' )
				return ElfExportReader.ReadExportedNames( path );

			if ( header[0] == (byte)'M' && header[1] == (byte)'Z' )
				return PeExportReader.ReadExportedNames( path );

			// Mach-O (macOS .dylib): thin slices are 0xFEEDFACE / 0xFEEDFACF (either byte
			// order), fat/universal wrappers are 0xCAFEBABE stored big-endian.
			var magic = BitConverter.ToUInt32( header, 0 );
			if ( magic is 0xFEEDFACE or 0xFEEDFACF or 0xCEFAEDFE or 0xCFFAEDFE or 0xBEBAFECA or 0xCAFEBABE )
				return MachOExportReader.ReadExportedNames( path );

			throw new InvalidDataException( $"{path}: unrecognised binary format (magic 0x{magic:X8})." );
		}

		private static int Main( string[] args )
		{
			var paths = args.Where( a => !a.StartsWith( "-", StringComparison.Ordinal ) ).ToArray();
			var verbose = args.Any( a => a is "-v" or "--verbose" );

			if ( paths.Length < 2 )
			{
				Console.Error.WriteLine( "Verifies every DllImport entry point in a managed assembly exists in a native library." );
				Console.Error.WriteLine();
				Console.Error.WriteLine( "usage: Steamworks.Conformance <managed-assembly.dll> <native-steam_api.dll> [-v]" );
				Console.Error.WriteLine();
				Console.Error.WriteLine( "  -v, --verbose   also list native exports that are never bound" );
				return 2;
			}

			var managedPath = paths[0];
			var nativePath = paths[1];

			List<PInvokeBinding> bindings;
			SortedSet<string> exports;

			try
			{
				bindings = PInvokeReader.ReadBindings( managedPath );
				exports = ReadNativeExports( nativePath );
			}
			catch ( Exception e )
			{
				Console.Error.WriteLine( $"error: {e.Message}" );
				return 2;
			}

			// One entry point can be declared by several types; report each distinct symbol once,
			// but keep a declaration site so a failure is actionable.
			var declaredBy = new SortedDictionary<string, PInvokeBinding>( StringComparer.Ordinal );
			foreach ( var b in bindings )
			{
				if ( !declaredBy.ContainsKey( b.EntryPoint ) )
					declaredBy[b.EntryPoint] = b;
			}

			Console.WriteLine( $"managed assembly : {Path.GetFileName( managedPath )}" );
			Console.WriteLine( $"native library   : {Path.GetFileName( nativePath )}" );
			Console.WriteLine( $"P/Invoke declarations : {bindings.Count} ({declaredBy.Count} distinct entry points)" );
			Console.WriteLine( $"native exports        : {exports.Count}" );
			Console.WriteLine();

			var missing = declaredBy.Keys.Where( e => !exports.Contains( e ) ).ToList();
			var unwaived = missing.Where( e => !KnownMissing.ContainsKey( e ) ).ToList();
			var waived = missing.Where( KnownMissing.ContainsKey ).ToList();

			if ( waived.Count > 0 )
			{
				Console.WriteLine( $"WAIVED - missing but explicitly allowed ({waived.Count}):" );
				foreach ( var e in waived )
					Console.WriteLine( $"  {e}  // {KnownMissing[e]}" );
				Console.WriteLine();
			}

			if ( unwaived.Count > 0 )
			{
				Console.WriteLine( $"FAIL - {unwaived.Count} entry point(s) do not exist in {Path.GetFileName( nativePath )}." );
				Console.WriteLine( "These compile fine and throw EntryPointNotFoundException when called:" );
				Console.WriteLine();

				foreach ( var e in unwaived )
				{
					var b = declaredBy[e];
					Console.WriteLine( $"  {e}" );
					Console.WriteLine( $"      declared by {b.DeclaringType}.{b.MethodName}  (module \"{b.Module}\")" );
				}

				Console.WriteLine();
				Console.WriteLine( "Fix by deleting the dead binding, or - if the symbol moved - updating the entry point." );
				Console.WriteLine( "If it is genuinely expected to be absent, add it to KnownMissing with a reason." );
				return 1;
			}

			Console.WriteLine( $"PASS - all {declaredBy.Count} entry points resolve against {Path.GetFileName( nativePath )}." );

			if ( verbose )
			{
				// Informational only. Unbound exports are usually deliberate (deprecated Valve
				// APIs we chose not to wrap), but a sudden jump in this number after an SDK
				// update is a useful signal that new functionality landed.
				var unbound = exports
					.Where( e => e.StartsWith( "SteamAPI_ISteam", StringComparison.Ordinal ) )
					.Where( e => !declaredBy.ContainsKey( e ) )
					.ToList();

				Console.WriteLine();
				Console.WriteLine( $"INFO - {unbound.Count} exported ISteam* function(s) are never bound:" );

				var byInterface = unbound
					.GroupBy( e =>
					{
						var rest = e.Substring( "SteamAPI_".Length );
						var underscore = rest.IndexOf( '_' );
						return underscore < 0 ? rest : rest.Substring( 0, underscore );
					} )
					.OrderByDescending( g => g.Count() );

				foreach ( var g in byInterface )
				{
					Console.WriteLine( $"  {g.Key} ({g.Count()})" );
					foreach ( var e in g.OrderBy( x => x, StringComparer.Ordinal ) )
						Console.WriteLine( $"      {e}" );
				}
			}

			return 0;
		}
	}
}
