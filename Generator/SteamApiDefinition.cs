using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Generator
{
    public class SteamApiDefinition
    {
        public class Interface
        {
            [JsonProperty( PropertyName = "classname" )]
            public string Name { get; set; }

            [JsonProperty( PropertyName = "version_string" )]
            public string VersionString { get; set; }

            public class Method
            {
                public string Desc { get; set; }
                public string ReturnType { get; set; }
                public string CallResult { get; set; }

                public class Param
                {
                    public string ParamType { get; set; }
                    public string ParamName { get; set; }
                }

                public Param[] Params { get; set; }
                [JsonProperty( PropertyName = "methodname" )]
                public string Name { get; set; }
                [JsonProperty( PropertyName = "methodname_flat" )]
                public string FlatName { get; set; }

            }

            public Method[] Methods { get; set; }


            public class Accessor
            {
                public string Kind { get; set; }
                public string Name { get; set; }
                public string Name_Flat { get; set; }
            }

            public Accessor[] Accessors { get; set; }

        }

        public Interface[] Interfaces { get; set; }


        public class EnumDef
        {
            public class EnumValue
            {
                [JsonProperty( PropertyName = "name" )]
                public string Name { get; set; }
                [JsonProperty( PropertyName = "value" )]
                public string Value { get; set; }
            }

            [JsonProperty( PropertyName = "enumname" )]
            public string Name { get; set; }
            [JsonProperty( PropertyName = "values" )]
            public EnumValue[] Values { get; set; }
        }

        public EnumDef[] enums { get; set; }


        public class TypeDef
        {
            [JsonProperty( PropertyName = "typedef" )]
            public string Name { get; set; }
            [JsonProperty( PropertyName = "type" )]
            public string Type { get; set; }
        }

        public List<TypeDef> typedefs { get; set; }

        public class StructDef
        {
            public class StructFields
            {
                [JsonProperty( PropertyName = "fieldname" )]
                public string Name { get; set; }
                [JsonProperty( PropertyName = "fieldtype" )]
                public string Type { get; set; }
            }

            [JsonProperty( PropertyName = "struct" )]
            public string Name { get; set; }
            [JsonProperty( PropertyName = "fields" )]
            public StructFields[] Fields { get; set; }
            public Interface.Method[] Methods { get; set; }

            public bool IsPack4OnWindows
            {
                get
                {
                    // 4/8 packing is irrevant to these classes
                    if ( Name.Contains( "MatchMakingKeyValuePair_t" ) ) return true;

                    if ( Fields.Skip( 1 ).Any( x => x.Type.Contains( "CSteamID" ) ) )
                        return true;

                    if ( Fields.Skip( 1 ).Any( x => x.Type.Contains( "CGameID" ) ) )
                        return true;

                    return false;
                }
            }

            public EnumDef[] Enums { get; set; }

        }

        public List<StructDef> structs { get; set; }

        public class CallbackStructDef : StructDef
        {
            [JsonProperty( PropertyName = "callback_id" )]
            public int CallbackId { get; set; }
        }

        public List<CallbackStructDef> callback_structs { get; set; }

        public class Const
        {
            [JsonProperty( PropertyName = "consttype" )]
            public string Type { get; set; }

            [JsonProperty( PropertyName = "constname" )]
            public string Name { get; set; }


            [JsonProperty( PropertyName = "constval" )]
            public string Val { get; set; }
        }

        public List<Const> Consts { get; set; }
    }


}
