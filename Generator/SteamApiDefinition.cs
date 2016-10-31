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
        public class TypeDef
        {
            [JsonProperty( PropertyName = "typedef" )]
            public string Name { get; set; }
            [JsonProperty( PropertyName = "type" )]
            public string Type { get; set; }
        }

        public List<TypeDef> typedefs { get; set; }

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

            public string CallbackId { get; set; }
            public bool IsCallResult { get; set; }
        }

        public List<StructDef> structs { get; set; }

        public class MethodDef
        {
            public class ParamType
            {
                [JsonProperty( PropertyName = "paramname" )]
                public string Name { get; set; }
                [JsonProperty( PropertyName = "paramtype" )]
                public string Type { get; set; }
            }

            [JsonProperty( PropertyName = "classname" )]
            public string ClassName { get; set; }
            [JsonProperty( PropertyName = "methodname" )]
            public string Name { get; set; }
            [JsonProperty( PropertyName = "returntype" )]
            public string ReturnType { get; set; }
            [JsonProperty( PropertyName = "params" )]
            public ParamType[] Params { get; set; }

            [JsonProperty( PropertyName = "callresult" )]
            public string CallResult { get; set; }

            public bool NeedsSelfPointer = true;
        }

        public List<MethodDef> methods { get; set; }


        public Dictionary<string, int> CallbackIds { get; internal set; }
        public Dictionary<string, string> Defines { get; internal set; }
    }
}
