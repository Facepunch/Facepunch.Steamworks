using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace Facepunch.SteamApi
{
    internal class ApiResponse<T>
    {
        public T Response;
        public T Result;
    }

    public partial class ISteamApps
    {
        public class GetAppBetasResponse
        {
            public class Beta
            {
                public ulong BuildId;
                public string Description;
                public bool ReqPassword;
                public bool ReqLocalCS;
            }

            public Dictionary<string, Beta> betas;
        }

        public class GetAppBuildsResponse
        {
            public class Build
            {
                public ulong BuildId;
                public string Description;
                public uint CreationTime;
                public ulong AccountIDCreator;

                public class Depot
                {
                    public ulong DepotId;
                    public ulong DepotVersionGID;
                    public ulong TotalOriginalBytes;
                    public ulong TotalCompressedBytes;
                }

                public Dictionary<string, Depot> depots;
            }

            public Dictionary<string, Build> builds;
        }
    }

    public partial class ISteamEconomy
    {
        public class GetMarketPricesResponse
        {
            public int total_count;
            public int start;
            public int count;

            public class Group
            {
                public string name;
                public string icon_url;
                public ulong sell_listings;
                public ulong buy_listings;
                public ulong listed_property_def_index;

                public class Currency
                {
                    public int ECurrencyCode;
                    public string currency;
                    public double sell_price;
                }

                public Currency[] currencies;
            }

            public Group[] groups;
        }
    }


    public partial class IWorkshopService
    {
        public class GetItemDailyRevenueResponse
        {
            public class Country
            {
                public string country_code;
                public uint date;

                /// <summary>
                /// Appears to be the dollar amount multiplied by 10000
                /// Use Revenue to get the real value as a double
                /// </summary>
                public double revenue_usd;

                /// <summary>
                /// Total dollar revenue, normalised
                /// </summary>
                public double Revenue { get { return revenue_usd / 10000.0; } }

                /// <summary>
                /// Number of units sold
                /// </summary>
                public long Units;
            }

            public Country[] country_revenue;

            public long TotalUnitsSold { get { return country_revenue.Sum( x => x.Units ); } }
            public double TotalRevenue { get { return country_revenue.Sum( x => x.Revenue ); } }
        }
    }

}
