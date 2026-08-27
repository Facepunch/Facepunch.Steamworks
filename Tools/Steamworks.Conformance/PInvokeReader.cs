using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

namespace Steamworks.Conformance
{
	/// <summary>One P/Invoke declaration found in a managed assembly.</summary>
	internal readonly struct PInvokeBinding
	{
		/// <summary>The native symbol the CLR will look up, e.g. <c>SteamAPI_ISteamApps_BIsSubscribed</c>.</summary>
		public string EntryPoint { get; }

		/// <summary>The native module name from the <c>DllImport</c>, e.g. <c>steam_api64</c>.</summary>
		public string Module { get; }

		/// <summary>Fully-qualified declaring type, for reporting.</summary>
		public string DeclaringType { get; }

		/// <summary>The C# method name, for reporting.</summary>
		public string MethodName { get; }

		public PInvokeBinding( string entryPoint, string module, string declaringType, string methodName )
		{
			EntryPoint = entryPoint;
			Module = module;
			DeclaringType = declaringType;
			MethodName = methodName;
		}
	}

	/// <summary>
	/// Extracts every <c>DllImport</c> declaration from a managed assembly by reading its
	/// metadata directly.
	///
	/// <para>
	/// Note that <c>DllImportAttribute</c> is a <i>pseudo-custom attribute</i>: the C#
	/// compiler does not emit it into the custom-attribute blob, it emits rows into the
	/// <c>ImplMap</c> metadata table. That means <c>GetCustomAttributes()</c> under a
	/// <see cref="System.Reflection.MetadataLoadContext"/> will not see it. Reading
	/// <see cref="MethodImport"/> off each <see cref="MethodDefinition"/> is the reliable
	/// way, and it has the further advantage of never loading or running the assembly.
	/// </para>
	/// </summary>
	internal static class PInvokeReader
	{
		public static List<PInvokeBinding> ReadBindings( string assemblyPath )
		{
			var results = new List<PInvokeBinding>();

			using var stream = File.OpenRead( assemblyPath );
			using var pe = new PEReader( stream );

			if ( !pe.HasMetadata )
				throw new InvalidDataException( $"{assemblyPath}: no managed metadata (is this a native DLL?)." );

			var reader = pe.GetMetadataReader();

			foreach ( var handle in reader.MethodDefinitions )
			{
				var method = reader.GetMethodDefinition( handle );

				// Only methods flagged PinvokeImpl carry an ImplMap row.
				if ( (method.Attributes & System.Reflection.MethodAttributes.PinvokeImpl) == 0 )
					continue;

				var import = method.GetImport();
				if ( import.Module.IsNil )
					continue;

				var moduleRef = reader.GetModuleReference( import.Module );
				var module = reader.GetString( moduleRef.Name );

				// When DllImport omits EntryPoint, the CLR falls back to the method name.
				// Model that here so the comparison matches real runtime behaviour.
				var methodName = reader.GetString( method.Name );
				var entryPoint = import.Name.IsNil ? methodName : reader.GetString( import.Name );

				var declaringType = "<global>";
				var typeHandle = method.GetDeclaringType();
				if ( !typeHandle.IsNil )
				{
					var type = reader.GetTypeDefinition( typeHandle );
					var ns = reader.GetString( type.Namespace );
					var name = reader.GetString( type.Name );
					declaringType = string.IsNullOrEmpty( ns ) ? name : $"{ns}.{name}";
				}

				results.Add( new PInvokeBinding( entryPoint, module, declaringType, methodName ) );
			}

			return results;
		}
	}
}
