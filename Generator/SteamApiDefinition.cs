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
                    
                    [JsonProperty( PropertyName = "out_string_count" )]
                    public string OutStringCount { get; set; }
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

            //
            // There was an IsPack4OnWindows property here. It forced a struct's whole
            // StructLayout.Pack to 4 when any field after the first mentioned CSteamID/CGameID,
            // as a way of expressing that Valve declares those two classes inside
            // "#pragma pack( push, 1 )" and they are therefore 1-aligned.
            //
            // The intent was right, the mechanism could not work: Pack is a struct-wide switch,
            // so it also dragged genuine uint64 members off their natural 8-byte boundary, and
            // Skip(1) missed the case where the id IS the first field. It was wrong in both
            // directions and mis-laid-out 16 structs.
            //
            // The alignment now lives on the field's type (PackedId, see
            // CodeWriter/Struct.cs::FieldType), which is where it belongs, so every struct just
            // carries the pack the header declares - Platform.StructPlatformPackSize.
            //

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
