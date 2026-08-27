using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Steamworks.LayoutBaseline
{
	/// <summary>
	/// Records and enforces the marshalled memory layout of every struct in the binding.
	///
	/// <para>
	/// <b>Why this exists.</b> These structs are written by native Steam code directly into
	/// memory that managed code then reads with <c>Marshal.PtrToStructure</c>. If a managed
	/// layout disagrees with the C++ one, nothing throws — you silently read the wrong
	/// fields, or read past the end of Steam's buffer. It is the worst failure mode in the
	/// codebase: invisible, non-deterministic, and impossible to attribute later.
	/// </para>
	///
	/// <para>
	/// The generator currently picks <c>StructLayout.Pack</c> with a heuristic that cannot
	/// be correct in general (see <c>docs/audit/01-marshaling-abi.md</c>), so 20 structs are
	/// known to be laid out wrongly today. Fixing that means changing how every generated
	/// struct is emitted — which is only safe if we can see, precisely, what changed.
	/// </para>
	///
	/// <para>
	/// This tool provides that. <c>record</c> writes a baseline of every struct's size,
	/// pack and field offsets; <c>check</c> re-measures and diffs against it. A layout
	/// change then shows up as an explicit, reviewable diff instead of a silent behaviour
	/// change. Run <c>record</c> deliberately when a layout change is intended, and commit
	/// the resulting file as part of that change.
	/// </para>
	///
	/// <para>
	/// Needs no Steam client: loading types and measuring layout never enters native code.
	/// </para>
	///
	/// <para>Exit codes: <c>0</c> match, <c>1</c> drift detected, <c>2</c> bad usage.</para>
	/// </summary>
	internal static class Program
	{
		private static int Main( string[] args )
		{
			if ( args.Length < 3 || (args[0] != "record" && args[0] != "check") )
			{
				Console.Error.WriteLine( "Records/enforces the marshalled layout of every struct in the binding." );
				Console.Error.WriteLine();
				Console.Error.WriteLine( "usage: Steamworks.LayoutBaseline record <assembly.dll> <baseline.txt>" );
				Console.Error.WriteLine( "       Steamworks.LayoutBaseline check  <assembly.dll> <baseline.txt>" );
				return 2;
			}

			var mode = args[0];
			var assemblyPath = Path.GetFullPath( args[1] );
			var baselinePath = Path.GetFullPath( args[2] );

			List<string> measured;
			try
			{
				measured = Measure( assemblyPath );
			}
			catch ( Exception e )
			{
				Console.Error.WriteLine( $"error: {e.Message}" );
				return 2;
			}

			if ( mode == "record" )
			{
				File.WriteAllLines( baselinePath, measured );
				Console.WriteLine( $"Recorded {measured.Count} layout lines to {baselinePath}" );
				return 0;
			}

			if ( !File.Exists( baselinePath ) )
			{
				Console.Error.WriteLine( $"error: baseline not found at {baselinePath}. Run 'record' first." );
				return 2;
			}

			var expected = File.ReadAllLines( baselinePath ).Where( l => l.Length > 0 ).ToList();
			var expectedSet = new HashSet<string>( expected, StringComparer.Ordinal );
			var measuredSet = new HashSet<string>( measured, StringComparer.Ordinal );

			// Compare by the struct/field identity (everything left of '=') so a changed
			// value reads as "changed", not as one removal plus one addition.
			string Key( string line )
			{
				var i = line.IndexOf( '=' );
				return i < 0 ? line : line.Substring( 0, i );
			}

			var expectedByKey = expected.ToDictionary( Key, l => l, StringComparer.Ordinal );
			var measuredByKey = measured.ToDictionary( Key, l => l, StringComparer.Ordinal );

			var changed = measuredByKey.Keys.Where( k => expectedByKey.ContainsKey( k ) && expectedByKey[k] != measuredByKey[k] ).OrderBy( k => k, StringComparer.Ordinal ).ToList();
			var added = measuredByKey.Keys.Where( k => !expectedByKey.ContainsKey( k ) ).OrderBy( k => k, StringComparer.Ordinal ).ToList();
			var removed = expectedByKey.Keys.Where( k => !measuredByKey.ContainsKey( k ) ).OrderBy( k => k, StringComparer.Ordinal ).ToList();

			Console.WriteLine( $"baseline : {expected.Count} lines" );
			Console.WriteLine( $"measured : {measured.Count} lines" );
			Console.WriteLine();

			if ( changed.Count == 0 && added.Count == 0 && removed.Count == 0 )
			{
				Console.WriteLine( $"PASS - layout is byte-identical to the baseline." );
				return 0;
			}

			if ( changed.Count > 0 )
			{
				Console.WriteLine( $"CHANGED ({changed.Count}) - these structs are laid out differently than when the baseline was recorded:" );
				foreach ( var k in changed )
					Console.WriteLine( $"  {k}\n      was {expectedByKey[k].Substring( k.Length + 1 )}\n      now {measuredByKey[k].Substring( k.Length + 1 )}" );
				Console.WriteLine();
			}

			if ( added.Count > 0 )
			{
				Console.WriteLine( $"ADDED ({added.Count}):" );
				foreach ( var k in added ) Console.WriteLine( $"  {measuredByKey[k]}" );
				Console.WriteLine();
			}

			if ( removed.Count > 0 )
			{
				Console.WriteLine( $"REMOVED ({removed.Count}):" );
				foreach ( var k in removed ) Console.WriteLine( $"  {expectedByKey[k]}" );
				Console.WriteLine();
			}

			Console.WriteLine( "FAIL - layout drifted from the baseline." );
			Console.WriteLine( "If the change was intended, re-run with 'record' and commit the new baseline" );
			Console.WriteLine( "alongside the change so the diff is reviewable." );
			return 1;
		}

		/// <summary>
		/// Produces one stable, sorted line per struct and per field. Text rather than JSON
		/// so that a layout change shows up as a readable line-level diff in code review.
		/// </summary>
		private static List<string> Measure( string assemblyPath )
		{
			var assembly = Assembly.LoadFrom( assemblyPath );
			var lines = new List<string>();

			foreach ( var type in assembly.GetTypes().OrderBy( t => t.FullName, StringComparer.Ordinal ) )
			{
				if ( !type.IsValueType || type.IsEnum || type.IsGenericType ) continue;
				if ( type.IsPrimitive ) continue;

				// Skip compiler-generated types such as <PrivateImplementationDetails> and
				// the fixed-size-buffer backing structs. They are an implementation detail
				// of the C# compiler, not part of the native ABI, and their names churn
				// with unrelated edits - which would make the baseline noisy and get it
				// ignored.
				var name = type.FullName ?? "";
				if ( name.Contains( '<' ) || name.Contains( "PrivateImplementationDetails" ) ) continue;

				int size;
				try
				{
					// Throws for types that are not blittable/marshalable at all - those are
					// not part of the native ABI surface, so skipping them is correct.
					size = Marshal.SizeOf( type );
				}
				catch
				{
					continue;
				}

				var layout = type.StructLayoutAttribute;
				var kind = layout?.Value.ToString() ?? "Auto";
				var pack = layout?.Pack ?? 0;
				var charSet = layout?.CharSet.ToString() ?? "None";

				lines.Add( $"{type.FullName}=size:{size} kind:{kind} pack:{pack} charset:{charSet}" );

				foreach ( var field in type.GetFields( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic )
					.OrderBy( f => f.Name, StringComparer.Ordinal ) )
				{
					if ( field.IsStatic ) continue;

					try
					{
						var offset = (int)Marshal.OffsetOf( type, field.Name );
						lines.Add( $"{type.FullName}.{field.Name}=offset:{offset}" );
					}
					catch
					{
						// Fixed-size buffers and some marshalled fields have no simple offset.
					}
				}
			}

			return lines;
		}
	}
}
