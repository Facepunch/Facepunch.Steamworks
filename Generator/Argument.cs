using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    class Argument
    {
        public string Name;
        public string NativeType;
        public string ManagedType;
        public TypeDef TypeDef;

        public Argument( string Name, string ManagedType, Dictionary<string, TypeDef> typeDefs )
        {
            this.Name = Name;
            this.NativeType = ManagedType;

            Build( typeDefs );
        }

        private void Build( Dictionary<string, TypeDef> typeDefs )
        {
            var cleanNative = NativeType.Trim( '*', ' ' ).Replace( "class ", "" ).Replace( "const ", "" );

            if ( typeDefs.ContainsKey( cleanNative ) )
            {
                TypeDef = typeDefs[cleanNative];
            }

            ManagedType = ToManagedType( NativeType );

            if ( ManagedType.EndsWith( "*" ) )
            {
                ManagedType = ToManagedType( ManagedType.Trim( '*', ' ' ) ) + "*";
            }
        }


        bool IsStructShouldBePassedAsRef
        {
            get
            {
                if ( ManagedType.Contains( "bool*" ) )
                    return true;

                return ManagedType.EndsWith( "*" ) && ManagedType.Contains( "_t" ) && Name.StartsWith( "p" ) && !Name.StartsWith( "pvec" );
            }
        }

        bool ShouldBePassedAsOut
        {
            get
            {
                

                return ManagedType.EndsWith( "*" ) && !ManagedType.Contains( "_t" ) && !ManagedType.Contains( "char" ) && !ManagedType.Contains( "bool" );
            }
        }

        bool ShouldBeIntPtr
        {
            get
            {
                if ( ManagedType.Contains( "SteamUGCDetails_t" ) || ManagedType.Contains( "SteamParamStringArray_t" ) )
                    return false;

                if ( Name == "pOutItemsArray" || Name == "handlesOut" )
                    return true;

                if ( Name.Contains( "Dest" ) && ManagedType.EndsWith( "*" ) )
                    return true;

                if ( Name.EndsWith( "s" ) && ManagedType.EndsWith( "*" ) )
                    return true;

                return false;
            }
        }

        bool PassedToNativeAsValue
        {
           get
            {
                if ( Name.StartsWith( "pvec" ) ) return false;
                if ( TypeDef == null ) return false;
                if ( ManagedType.Contains( "IntPtr" ) ) return false;
                if ( Name.Contains( "IntPtr" ) ) return false;

                return true;
            }
        }


        private static string ToManagedType( string type )
        {
            type = type.Replace( "ISteamHTMLSurface::", "" );
            type = type.Replace( "class ", "" );
            type = type.Replace( "struct ", "" );
            type = type.Replace( "const void", "void" );

            switch ( type )
            {
                case "uint64": return "ulong";
                case "uint32": return "uint";
                case "int32": return "int";
                case "int64": return "long";
                case "void *": return "IntPtr";
                case "int16": return "short";
                case "uint8": return "byte";
                case "int8": return "char";
                case "unsigned short": return "ushort";
                case "unsigned int": return "uint";
                case "uint16": return "ushort";
                case "const char *": return "string";

                case "SteamAPIWarningMessageHook_t": return "IntPtr";
            }

            //type = type.Trim( '*', ' ' );

            // Enums - skip the 'E'
            if ( type[0] == 'E' )
            {
                return type.Substring( 1 );
            }

            if ( type.StartsWith( "const " ) )
                return ToManagedType( type.Replace( "const ", "" ) );

            if ( type.StartsWith( "ISteamMatchmak" ) )
                return "IntPtr";

            return type;
        }

        internal string ManagedParameter()
        {
            if ( ShouldBeIntPtr )
                return $"IntPtr {Name} /*{NativeType}*/";

            if ( IsStructShouldBePassedAsRef )
                return $"ref {ManagedType.Trim( '*', ' ' )} {Name} /*{NativeType}*/";

            if ( ShouldBePassedAsOut )
                return $"out {ManagedType.Trim( '*', ' ' )} {Name} /*{NativeType}*/";

            return $"{ManagedType} {Name} /*{NativeType}*/";
        }

        internal string InteropVariable( bool AsRawValues )
        {
            if ( ShouldBeIntPtr )
                return $"{Name}";

            var value = (PassedToNativeAsValue && AsRawValues) ? ".Value" : "";

            if ( IsStructShouldBePassedAsRef )
                return $"ref {Name}{value}";

            if ( ShouldBePassedAsOut )
                return $"out {Name}{value}";

            return $"{Name}{value}";
        }

        internal string InteropParameter( bool LargePack, bool includeMarshalling = false )
        {
            var ps = LargePack ? "" : ".PackSmall";
            var marshalling = "";
            if ( !NativeType.Contains( "_t" ) )
                ps = string.Empty;

            if ( TypeDef != null )
                ps = string.Empty;

            if ( includeMarshalling )
            {
                if ( NativeType == "bool" ) marshalling = "[MarshalAs(UnmanagedType.U1)]";
                if ( NativeType == "bool *" ) marshalling = "[MarshalAs(UnmanagedType.U1)]";

                if ( PassedToNativeAsValue && !ShouldBeIntPtr )
                {
                    if ( IsStructShouldBePassedAsRef )
                        return $"ref {TypeDef.ManagedType} {Name}";
                    else if ( ShouldBePassedAsOut )
                        return $"out {TypeDef.ManagedType} {Name}";
                    else
                        return $"{TypeDef.ManagedType} {Name}";
                }
            }

            if ( ShouldBeIntPtr )
            return $"IntPtr /*{NativeType}*/ {Name}".Trim();

            if ( IsStructShouldBePassedAsRef )
                return $"{marshalling} ref {ManagedType.Trim( '*', ' ' )}{ps} /*{NativeType}*/ {Name}".Trim();

            if ( ShouldBePassedAsOut )
                return $"{marshalling} out {ManagedType.Trim( '*', ' ' )} /*{NativeType}*/ {Name}".Trim();

            if ( NativeType == "char *"  || NativeType == "char **" )
            {
                return $"System.Text.StringBuilder /*{NativeType}*/ {Name}".Trim();
            }

            if ( TypeDef != null )
            {
                if ( NativeType.EndsWith( "*" ) )
                {
                    return $"IntPtr /*{NativeType}*/ {Name}".Trim();
                }
                else
                {
                    return $"{marshalling} {TypeDef.Name} /*{NativeType}*/ {Name}".Trim();
                }                
            }


            if ( NativeType.EndsWith( "*" ) && ManagedType.Contains( "_t" ) )
            {
                return $"IntPtr /*{NativeType}*/ {Name} ".Trim();
            }

            return $"{marshalling} {ManagedType} /*{NativeType}*/ {Name} ".Trim();
        }

        internal string Return()
        {
            if ( ManagedType.EndsWith( "*" ) )
            {
                return $"IntPtr /*{NativeType}*/";
            }

            if ( TypeDef != null )
            {
                return $"{TypeDef.Name} /*({NativeType})*/";
            }

            if ( ManagedType == "string" )
                return "IntPtr";

            return $"{ManagedType} /*{NativeType}*/";
        }


    }
}
