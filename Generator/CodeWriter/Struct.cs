using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        public class TypeDef
        {
            public string Name;
            public string NativeType;
            public string ManagedType;
        }

        private readonly Dictionary<string, TypeDef> TypeDefs = new Dictionary<string, TypeDef>();

        //
        // A native fixed size array member - char[8000], uint8[2560], PublishedFileId[50] -
        // is emitted as a C# fixed size buffer of the element type listed here.
        //
        // These used to be [MarshalAs(UnmanagedType.ByValArray)] managed arrays. That
        // marshals to the correct bytes, but the marshaller has to allocate a brand new
        // managed array for every such field on every single marshal. For a callback struct
        // that is once per delivery, for the lifetime of the process; SteamUGCDetails_t alone
        // cost ~9.8 KB of garbage per workshop item. A fixed size buffer occupies exactly the
        // same inline storage, needs no marshalling at all, and makes the whole struct
        // blittable - so sizes and offsets are unchanged (Tools/baselines/ proves it) while
        // the allocation goes to zero.
        //
        // AppId and PublishedFileId are single field wrappers over uint/ulong. `fixed` only
        // accepts primitives, so they widen to the underlying primitive - identical bytes,
        // identical offsets.
        //
        private readonly static Dictionary<string, string> FixedBufferElements = new Dictionary<string, string>
        {
            { "byte", "byte" },
            { "char", "byte" },
            { "uint8", "byte" },
            { "ushort", "ushort" },
            { "uint", "uint" },
            { "uint32", "uint" },
            { "AppId", "uint" },
            { "float", "float" },
            { "PublishedFileId", "ulong" },
        };

        //
        // What the old ByValArray form used, kept for the few members that still have to be
        // managed arrays - see KeepAsManagedArray.
        //
        private readonly static Dictionary<string, string> ManagedArrayForms = new Dictionary<string, string>
        {
            { "byte", "byte[]|" },
            { "ushort", "ushort[]|, ArraySubType = UnmanagedType.U2" },
            { "uint", "uint[]|, ArraySubType = UnmanagedType.U4" },
            { "float", "float[]|, ArraySubType = UnmanagedType.R4" },
            { "ulong", "ulong[]|, ArraySubType = UnmanagedType.U8" },
        };

        //
        // Members that must stay managed arrays, keyed by "NativeStruct.nativeMember".
        //
        // Only add to this when hand written code outside Generated/ takes the member as an
        // array and keeps the reference - a fixed size buffer cannot be assigned to a byte[].
        // Every entry here is a struct that keeps paying the per marshal allocation, so each
        // one wants removing as soon as its consumer can be reworked.
        //
        private readonly static string[] KeepAsManagedArray = new string[]
        {
            // Networking/HostedServerAddress.cs copies m_data straight into a byte[] field of
            // its own and holds it. Converting this member means reworking that type too.
            "SteamDatagramHostedAddress.m_data",
        };

        void Structs()
        {
            foreach ( var c in def.structs )
            {
				var name = Cleanup.ConvertType( c.Name );

				if ( !Cleanup.ShouldCreate( name ) )
					continue;

                if ( name.Contains( "::" ) )
                    continue;

                var partial = "";
                if ( c.Methods != null ) partial = " partial";

                //
                // Main struct
                //
                WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]" );
                StartBlock( $"{Cleanup.Expose( name )}{UnsafeModifier( c )}{partial} struct {name}" );
                {
					//
					// The fields
					//
					StructFields( c.Name, c.Fields );
					WriteLine();

                    if ( c.Enums != null )
                    {
                        foreach ( var e in c.Enums )
                        {
                            WriteEnum( e, e.Name );
                        }
                    }

                }
                EndBlock();
                WriteLine();
            }
        }

        /// <summary>
        /// A fixed size buffer can only be declared in an unsafe context, so any struct that
        /// gets one has to carry the modifier.
        /// </summary>
        private string UnsafeModifier( SteamApiDefinition.StructDef c )
        {
            if ( c.Fields == null )
                return "";

            return c.Fields.Any( m => IsFixedBuffer( c.Name, m, out _, out _ ) ) ? " unsafe" : "";
        }

        //
        // Valve's two id classes are declared inside "#pragma pack( push, 1 )"
        // (steamclientpublic.h:475, CSteamID at :480 and CGameID at :922, popped at :1108),
        // so both are 8 bytes with ALIGNMENT 1. A C# ulong wants alignment 8, which is what
        // shifted 16 generated structs. They are emitted as PackedId - a Pack = 1 wrapper
        // over a ulong - so the managed field has the native alignment and the enclosing struct
        // can carry the header's own pack value with no heuristic. See
        // Facepunch.Steamworks/Structs/PackedId.cs.
        //
        private const string NativeIdType = "PackedId";

        /// <summary>
        /// True when this member is a native <c>CSteamID</c>/<c>CGameID</c>.
        /// <paramref name="arrayLength"/> is the element count for an array member, 0 for a
        /// scalar one.
        /// </summary>
        private static bool IsNativeIdMember( string nativeType, out int arrayLength )
        {
            arrayLength = 0;

            var t = nativeType.Replace( "class ", "" ).Replace( "struct ", "" ).Trim();

            var bracket = t.IndexOf( '[' );
            if ( bracket >= 0 )
            {
                if ( !int.TryParse( t.Substring( bracket ).Trim( '[', ']', ' ' ), out arrayLength ) )
                    return false;

                t = t.Substring( 0, bracket ).Trim();
            }

            return t == "CSteamID" || t == "CGameID";
        }

        /// <summary>
        /// The managed type a member ends up as, before any array handling.
        /// </summary>
        private string FieldType( SteamApiDefinition.StructDef.StructFields m )
        {
            if ( IsNativeIdMember( m.Type, out var idCount ) )
            {
                //
                // A fixed size buffer takes its alignment from its element type and C# only
                // allows primitives there, so an array of ids cannot be `fixed PackedId[N]`.
                // `fixed byte[N * 8]` occupies the same bytes and, crucially, has the same
                // alignment as the native array: 1.
                //
                return idCount > 0 ? $"byte [{idCount * 8}]" : NativeIdType;
            }

            var t = Cleanup.ConvertType( ToManagedType( m.Type ) );

            if ( TypeDefs.ContainsKey( t ) )
                t = TypeDefs[t].ManagedType;

            return t;
        }

        /// <summary>
        /// True when this member is a native fixed size array we can emit inline.
        /// </summary>
        private bool IsFixedBuffer( string structName, SteamApiDefinition.StructDef.StructFields m, out string element, out string length )
        {
            element = null;
            length = null;

            if ( !IsArrayMember( FieldType( m ), out element, out length ) )
                return false;

            return !KeepAsManagedArray.Contains( $"{structName}.{m.Name}" );
        }

        /// <summary>
        /// Splits "char [8000]" into its fixed buffer element type and its length.
        /// </summary>
        private bool IsArrayMember( string type, out string element, out string length )
        {
            element = null;
            length = null;

            var bracket = type.IndexOf( '[' );
            if ( bracket < 0 )
                return false;

            if ( !FixedBufferElements.TryGetValue( type.Substring( 0, bracket ).Trim(), out element ) )
                return false;

            length = type.Substring( bracket ).Trim( '[', ']', ' ' );
            return true;
        }

        private void StructFields( string structName, SteamApiDefinition.StructDef.StructFields[] fields )
        {
            foreach ( var m in fields )
            {
                var t = FieldType( m );
				var name = CleanMemberName( m.Name );

                if ( t == "bool" )
                {
                    WriteLine( "[MarshalAs(UnmanagedType.I1)]" );
                }

                if ( IsFixedBuffer( structName, m, out var element, out var length ) )
                {
					//
					// A fixed size buffer carries no length of its own, so the decoder has to
					// be told how far it may scan. Steam is not obliged to terminate a buffer
					// it filled completely.
					//
					if ( t.StartsWith( "char" ) )
						WriteLine( $"internal string {name}UTF8() {{ fixed ( byte* b = {name} ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, {length} ); }}" );

					WriteLine( $"internal fixed {element} {name}[{length}]; // {m.Name} {m.Type}" );
					continue;
                }

                if ( IsArrayMember( t, out element, out length ) )
                {
					//
					// Held back from the fixed buffer treatment by KeepAsManagedArray, so it
					// keeps the old marshalled-array form - and its per marshal allocation.
					//
					if ( t.StartsWith( "char" ) )
						WriteLine( $"internal string {name}UTF8() => Steamworks.Utility.Utf8NoBom.GetString( {name}, 0, System.Array.IndexOf<byte>( {name}, 0 ) );" );

					var form = ManagedArrayForms[element].Split( '|' );

					WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {length}{form[1]})]" );
					WriteLine( $"internal {form[0]} {name}; // {m.Name} {m.Type}" );
					continue;
                }

                if ( t == "const char **" )
                {
                    t = "IntPtr";
                }

                if ( t == "SteamInputActionEvent_t.AnalogAction_t" )
                {
                    Write( "// " );
                }

                WriteLine( $"internal {t} {name}; // {m.Name} {m.Type}" );
            }
        }
    }
}
