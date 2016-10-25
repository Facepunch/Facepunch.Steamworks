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
        public bool IsRef;
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
                return ManagedType.EndsWith( "*" ) && ManagedType.Contains( "_t" ) && Name.StartsWith( "p" ) && !Name.StartsWith( "pvec" );
            }
        }

        bool ShouldBePassedAsOut
        {
            get
            {
                return ManagedType.EndsWith( "*" ) && !ManagedType.Contains( "_t" ) && !ManagedType.Contains( "char" );
            }
        }

        bool ShouldBeIntPtr
        {
            get
            {
                if ( ManagedType.Contains( "SteamUGCDetails_t" ) )
                    return false;

                if ( Name == "pOutItemsArray" )
                    return true;

                if ( Name.Contains( "Dest" ) && ManagedType.EndsWith( "*" ) )
                    return true;

                if ( Name.EndsWith( "s" ) && ManagedType.EndsWith( "*" ) )
                    return true;

                return false;
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

            var refv = IsRef ? "ref " : "";
            return $"{refv}{ManagedType} {Name} /*{NativeType}*/";
        }

        internal string InteropVariable()
        {
            if ( ShouldBeIntPtr )
                return $"{Name}";

            if ( IsStructShouldBePassedAsRef )
                return $"ref {Name}";

            if ( ShouldBePassedAsOut )
                return $"out {Name}";

            return $"{Name}";
        }

        internal string InteropParameter( bool LargePack )
        {
            var ps = LargePack ? "" : ".PackSmall";

            if ( !NativeType.Contains( "_t" ) )
                ps = string.Empty;

            if ( TypeDef != null )
                ps = string.Empty;

            if ( ShouldBeIntPtr )
                return $"IntPtr /*{NativeType}*/ {Name}";

            if ( IsStructShouldBePassedAsRef )
                return $"ref {ManagedType.Trim( '*', ' ' )}{ps} /*{NativeType}*/ {Name}";

            if ( ShouldBePassedAsOut )
                return $"out {ManagedType.Trim( '*', ' ' )} /*{NativeType}*/ {Name}";

            if ( NativeType == "char *"  || NativeType == "char **" )
            {
                return $"System.Text.StringBuilder /*{NativeType}*/ {Name}";
            }

            if ( TypeDef != null )
            {
                if ( NativeType.EndsWith( "*" ) )
                {
                    return $"IntPtr /*{NativeType}*/ {Name}";
                }
                else
                {
                    return $"{TypeDef.Name} /*{NativeType}*/ {Name}";
                }                
            }

            if ( NativeType.EndsWith( "*" ) && ManagedType.Contains( "_t" ) )
            {
                return $"IntPtr /*{NativeType}*/ {Name} ";
            }

            return $"{ManagedType} /*{NativeType}*/ {Name} ";
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
