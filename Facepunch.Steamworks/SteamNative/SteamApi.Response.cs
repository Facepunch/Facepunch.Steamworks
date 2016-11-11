using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace Facepunch.SteamApi
{
    internal class ApiResponse<T>
    {
        public T Response { get; set; }
    }

    public partial class ISteamApps
    {
        public class GetAppBetasResponse
        {
            public class Beta
            {
                public ulong BuildId { get; set; }
                public string Description { get; set; }
                public bool ReqPassword { get; set; }
                public bool ReqLocalCS { get; set; }
            }

            public Dictionary<string, Beta> betas;
        }

        public class GetAppBuildsResponse
        {
            public class Build
            {
                public ulong BuildId { get; set; }
                public string Description { get; set; }
                public uint CreationTime { get; set; }
                public ulong AccountIDCreator { get; set; }

                public class Depot
                {
                    public ulong DepotId { get; set; }
                    public ulong DepotVersionGID { get; set; }
                    public ulong TotalOriginalBytes { get; set; }
                    public ulong TotalCompressedBytes { get; set; }
                }

                public Dictionary<string, Depot> depots;
            }

            public Dictionary<string, Build> builds;
        }
    }


}
