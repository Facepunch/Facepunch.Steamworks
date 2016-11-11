using System;
using System.Runtime.InteropServices;

namespace Facepunch.SteamApi
{
	/// <summary>
	/// IGameInventory
	/// </summary>
	public partial class IGameInventory
	{
		private const string Url = "http://api.steampowered.com/IGameInventory/";
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="steamid">(uint64)The steam ID of the account to operate on</param>
		/// <param name="command">(string)The command to run on that asset</param>
		/// <param name="contextid">(uint64)The context to fetch history for</param>
		/// <param name="arguments">(string)The arguments that were provided with the command in the first place</param>
		public static string GetHistoryCommandDetails(uint appid, ulong steamid, string command, ulong contextid, string arguments)
		{
			return Utility.WebGet<string>( $"{Url}GetHistoryCommandDetails/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&steamid={steamid}&command={command}&contextid={contextid}&arguments={arguments}" );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="steamid">(uint64)The Steam ID to fetch history for</param>
		/// <param name="contextid">(uint64)The context to fetch history for</param>
		/// <param name="starttime">(uint32)Start time of the history range to collect</param>
		/// <param name="endtime">(uint32)End time of the history range to collect</param>
		public static string GetUserHistory(uint appid, ulong steamid, ulong contextid, uint starttime, uint endtime)
		{
			return Utility.WebGet<string>( $"{Url}GetUserHistory/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&steamid={steamid}&contextid={contextid}&starttime={starttime}&endtime={endtime}" );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="steamid">(uint64)The asset ID to operate on</param>
		/// <param name="contextid">(uint64)The context to fetch history for</param>
		/// <param name="actorid">(uint32)A unique 32 bit ID for the support person executing the command</param>
		public static string HistoryExecuteCommands(uint appid, ulong steamid, ulong contextid, uint actorid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "steamid", steamid },
				{ "contextid", contextid },
				{ "actorid", actorid },
			};
			
			return Utility.WebPost<string>( $"{Url}/HistoryExecuteCommands/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="assetid">(uint64)The asset ID to operate on</param>
		/// <param name="contextid">(uint64)The context to fetch history for</param>
		public static string SupportGetAssetHistory(uint appid, ulong assetid, ulong contextid)
		{
			return Utility.WebGet<string>( $"{Url}SupportGetAssetHistory/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&assetid={assetid}&contextid={contextid}" );
		}
		
	}
	
	/// <summary>
	/// ISteamApps
	/// </summary>
	public partial class ISteamApps
	{
		private const string Url = "http://api.steampowered.com/ISteamApps/";
		
		/// <param name="appid">(uint32)AppID of game</param>
		public static GetAppBetasResponse GetAppBetas(uint appid)
		{
			return Utility.WebGet<GetAppBetasResponse>( $"{Url}GetAppBetas/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}" );
		}
		
		/// <param name="appid">(uint32)AppID of game</param>
		/// <param name="count">(uint32)# of builds to retrieve (default 10)</param>
		public static GetAppBuildsResponse GetAppBuilds(uint appid, uint count)
		{
			return Utility.WebGet<GetAppBuildsResponse>( $"{Url}GetAppBuilds/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&count={count}" );
		}
		
		/// <param name="appid">(uint32)AppID of depot</param>
		public static string GetAppDepotVersions(uint appid)
		{
			return Utility.WebGet<string>( $"{Url}GetAppDepotVersions/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}" );
		}
		
		public static string GetAppList()
		{
			return Utility.WebGet<string>( $"{Url}GetAppList/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&" );
		}
		
		/// <param name="appid">(uint32)AppID of game</param>
		/// <param name="timebegin">(uint32)Time range begin</param>
		/// <param name="timeend">(uint32)Time range end</param>
		/// <param name="includereports">(bool)include reports that were not bans</param>
		/// <param name="includebans">(bool)include reports that were bans</param>
		/// <param name="reportidmin">(uint64)minimum report id</param>
		public static string GetCheatingReports(uint appid, uint timebegin, uint timeend, bool includereports, bool includebans, ulong reportidmin)
		{
			return Utility.WebGet<string>( $"{Url}GetCheatingReports/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&timebegin={timebegin}&timeend={timeend}&includereports={includereports}&includebans={includebans}&reportidmin={reportidmin}" );
		}
		
		/// <param name="appid">(uint32)AppID of game</param>
		public static string GetPlayersBanned(uint appid)
		{
			return Utility.WebGet<string>( $"{Url}GetPlayersBanned/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}" );
		}
		
		/// <param name="filter">(string)Query filter string</param>
		/// <param name="limit">(uint32)Limit number of servers in the response</param>
		public static string GetServerList(string filter, uint limit)
		{
			return Utility.WebGet<string>( $"{Url}GetServerList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&filter={filter}&limit={limit}" );
		}
		
		/// <param name="addr">(string)IP or IP:queryport to list</param>
		public static string GetServersAtAddress(string addr)
		{
			return Utility.WebGet<string>( $"{Url}GetServersAtAddress/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&addr={addr}" );
		}
		
		/// <param name="appid">(uint32)AppID of game</param>
		/// <param name="buildid">(uint32)BuildID</param>
		/// <param name="betakey">(string)beta key, required. Use public for default branch</param>
		/// <param name="description">(string)optional description for this build</param>
		public static string SetAppBuildLive(uint appid, uint buildid, string betakey, string description)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "buildid", buildid },
				{ "betakey", betakey },
				{ "description", description },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetAppBuildLive/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)AppID of game</param>
		/// <param name="version">(uint32)The installed version of the game</param>
		public static string UpToDateCheck(uint appid, uint version)
		{
			return Utility.WebGet<string>( $"{Url}UpToDateCheck/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&version={version}" );
		}
		
	}
	
	/// <summary>
	/// ISteamBitPay
	/// </summary>
	public partial class ISteamBitPay
	{
		private const string Url = "http://api.steampowered.com/ISteamBitPay/";
		
		public static string BitPayPaymentNotification()
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
			};
			
			return Utility.WebPost<string>( $"{Url}/BitPayPaymentNotification/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamCDN
	/// </summary>
	public partial class ISteamCDN
	{
		private const string Url = "http://api.steampowered.com/ISteamCDN/";
		
		/// <param name="cdnname">(string)Steam name of CDN property</param>
		/// <param name="allowedipblocks">(string)comma-separated list of allowed IP address blocks in CIDR format - blank for not used</param>
		/// <param name="allowedasns">(string)comma-separated list of allowed client network AS numbers - blank for not used</param>
		/// <param name="allowedipcountries">(string)comma-separated list of allowed client IP country codes in ISO 3166-1 format - blank for not used</param>
		public static string SetClientFilters(string cdnname, string allowedipblocks, string allowedasns, string allowedipcountries)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "cdnname", cdnname },
				{ "allowedipblocks", allowedipblocks },
				{ "allowedasns", allowedasns },
				{ "allowedipcountries", allowedipcountries },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetClientFilters/v0001/", headers );
		}
		
		/// <param name="cdnname">(string)Steam name of CDN property</param>
		/// <param name="mbps_sent">(uint32)Outgoing network traffic in Mbps</param>
		/// <param name="mbps_recv">(uint32)Incoming network traffic in Mbps</param>
		/// <param name="cpu_percent">(uint32)Percent CPU load</param>
		/// <param name="cache_hit_percent">(uint32)Percent cache hits</param>
		public static string SetPerformanceStats(string cdnname, uint mbps_sent, uint mbps_recv, uint cpu_percent, uint cache_hit_percent)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "cdnname", cdnname },
				{ "mbps_sent", mbps_sent },
				{ "mbps_recv", mbps_recv },
				{ "cpu_percent", cpu_percent },
				{ "cache_hit_percent", cache_hit_percent },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetPerformanceStats/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamCommunity
	/// </summary>
	public partial class ISteamCommunity
	{
		private const string Url = "http://api.steampowered.com/ISteamCommunity/";
		
		/// <param name="steamidActor">(uint64)SteamID of user doing the reporting</param>
		/// <param name="steamidTarget">(uint64)SteamID of the entity being accused of abuse</param>
		/// <param name="appid">(uint32)AppID to check for ownership</param>
		/// <param name="abuseType">(uint32)Abuse type code (see EAbuseReportType enum)</param>
		/// <param name="contentType">(uint32)Content type code (see ECommunityContentType enum)</param>
		/// <param name="description">(string)Narrative from user</param>
		/// <param name="gid">(uint64)GID of related record (depends on content type)</param>
		public static string ReportAbuse(ulong steamidActor, ulong steamidTarget, uint appid, uint abuseType, uint contentType, string description, ulong gid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamidActor", steamidActor },
				{ "steamidTarget", steamidTarget },
				{ "appid", appid },
				{ "abuseType", abuseType },
				{ "contentType", contentType },
				{ "description", description },
				{ "gid", gid },
			};
			
			return Utility.WebPost<string>( $"{Url}/ReportAbuse/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamDirectory
	/// </summary>
	public partial class ISteamDirectory
	{
		private const string Url = "http://api.steampowered.com/ISteamDirectory/";
		
		/// <param name="cellid">(uint32)Client's Steam cell ID</param>
		/// <param name="maxcount">(uint32)Max number of servers to return</param>
		public static string GetCMList(uint cellid, uint maxcount)
		{
			return Utility.WebGet<string>( $"{Url}GetCMList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&cellid={cellid}&maxcount={maxcount}" );
		}
		
	}
	
	/// <summary>
	/// ISteamEconomy
	/// </summary>
	public partial class ISteamEconomy
	{
		private const string Url = "http://api.steampowered.com/ISteamEconomy/";
		
		/// <param name="appid">(uint32)That the key is associated with. Must be a steam economy app.</param>
		/// <param name="steamid">(uint64)SteamID of user attempting to initiate a trade</param>
		/// <param name="targetid">(uint64)SteamID of user that is the target of the trade invitation</param>
		public static string CanTrade(uint appid, ulong steamid, ulong targetid)
		{
			return Utility.WebGet<string>( $"{Url}CanTrade/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&steamid={steamid}&targetid={targetid}" );
		}
		
		/// <param name="appid">(uint32)The app ID the user is buying assets for</param>
		/// <param name="steamid">(uint64)SteamID of the user making a purchase</param>
		/// <param name="txnid">(string)The transaction ID</param>
		/// <param name="language">(string)The local language for the user</param>
		public static string FinalizeAssetTransaction(uint appid, ulong steamid, string txnid, string language)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "steamid", steamid },
				{ "txnid", txnid },
				{ "language", language },
			};
			
			return Utility.WebPost<string>( $"{Url}/FinalizeAssetTransaction/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)Must be a steam economy app.</param>
		/// <param name="language">(string)The user's local language</param>
		/// <param name="class_count">(uint32)Number of classes requested. Must be at least one.</param>
		/// <param name="classid0">(uint64)Class ID of the nth class.</param>
		/// <param name="instanceid0">(uint64)Instance ID of the nth class.</param>
		public static string GetAssetClassInfo(uint appid, string language, uint class_count, ulong classid0, ulong instanceid0)
		{
			return Utility.WebGet<string>( $"{Url}GetAssetClassInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&language={language}&class_count={class_count}&classid0={classid0}&instanceid0={instanceid0}" );
		}
		
		/// <param name="appid">(uint32)Must be a steam economy app.</param>
		/// <param name="currency">(string)The currency to filter for</param>
		/// <param name="language">(string)The user's local language</param>
		public static string GetAssetPrices(uint appid, string currency, string language)
		{
			return Utility.WebGet<string>( $"{Url}GetAssetPrices/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&currency={currency}&language={language}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)The app to get exported items from.</param>
		/// <param name="contextid">(uint64)The context in the app to get exported items from.</param>
		public static string GetExportedAssetsForUser(ulong steamid, uint appid, ulong contextid)
		{
			return Utility.WebGet<string>( $"{Url}GetExportedAssetsForUser/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}&contextid={contextid}" );
		}
		
		/// <param name="appid">(uint32)Must be a steam economy app.</param>
		public static GetMarketPricesResponse GetMarketPrices(uint appid)
		{
			return Utility.WebGet<GetMarketPricesResponse>( $"{Url}GetMarketPrices/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}" );
		}
		
		/// <param name="appid">(uint32)The app ID the user is buying assets for</param>
		/// <param name="steamid">(uint64)SteamID of user making a purchase</param>
		/// <param name="assetid0">(string)The ID of the first asset the user is buying - there must be at least one</param>
		/// <param name="assetquantity0">(uint32)The quantity of assetid0's the the user is buying</param>
		/// <param name="currency">(string)The local currency for the user</param>
		/// <param name="language">(string)The local language for the user</param>
		/// <param name="ipaddress">(string)The user's IP address</param>
		/// <param name="referrer">(string)The referring URL</param>
		/// <param name="clientauth">(bool)If true (default is false), the authorization will appear in the user's steam client overlay, rather than as a web page - useful for stores that are embedded in products.</param>
		public static string StartAssetTransaction(uint appid, ulong steamid, string assetid0, uint assetquantity0, string currency, string language, string ipaddress, string referrer, bool clientauth)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "steamid", steamid },
				{ "assetid0", assetid0 },
				{ "assetquantity0", assetquantity0 },
				{ "currency", currency },
				{ "language", language },
				{ "ipaddress", ipaddress },
				{ "referrer", referrer },
				{ "clientauth", clientauth },
			};
			
			return Utility.WebPost<string>( $"{Url}/StartAssetTransaction/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)That the key is associated with. Must be a steam economy app.</param>
		/// <param name="partya">(uint64)SteamID of first user in the trade</param>
		/// <param name="partyb">(uint64)SteamID of second user in the trade</param>
		public static string StartTrade(uint appid, ulong partya, ulong partyb)
		{
			return Utility.WebGet<string>( $"{Url}StartTrade/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&partya={partya}&partyb={partyb}" );
		}
		
	}
	
	/// <summary>
	/// ISteamEnvoy
	/// </summary>
	public partial class ISteamEnvoy
	{
		private const string Url = "http://api.steampowered.com/ISteamEnvoy/";
		
		public static string PaymentOutNotification()
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
			};
			
			return Utility.WebPost<string>( $"{Url}/PaymentOutNotification/v0001/", headers );
		}
		
		public static string PaymentOutReversalNotification()
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
			};
			
			return Utility.WebPost<string>( $"{Url}/PaymentOutReversalNotification/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamGameServerStats
	/// </summary>
	public partial class ISteamGameServerStats
	{
		private const string Url = "http://api.steampowered.com/ISteamGameServerStats/";
		
		/// <param name="gameid">(uint64)game id to get stats for, if not a mod, it's safe to use appid here</param>
		/// <param name="appid">(uint32)appID of the game</param>
		/// <param name="rangestart">(string)range start date/time (Format: YYYY-MM-DD HH:MM:SS, seattle local time</param>
		/// <param name="rangeend">(string)range end date/time (Format: YYYY-MM-DD HH:MM:SS, seattle local time</param>
		/// <param name="maxresults">(uint32)Max number of results to return (up to 1000)</param>
		public static string GetGameServerPlayerStatsForGame(ulong gameid, uint appid, string rangestart, string rangeend, uint maxresults)
		{
			return Utility.WebGet<string>( $"{Url}GetGameServerPlayerStatsForGame/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&gameid={gameid}&appid={appid}&rangestart={rangestart}&rangeend={rangeend}&maxresults={maxresults}" );
		}
		
	}
	
	/// <summary>
	/// ISteamLeaderboards
	/// </summary>
	public partial class ISteamLeaderboards
	{
		private const string Url = "http://api.steampowered.com/ISteamLeaderboards/";
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="name">(string)name of the leaderboard to delete</param>
		public static string DeleteLeaderboard(uint appid, string name)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "name", name },
			};
			
			return Utility.WebPost<string>( $"{Url}/DeleteLeaderboard/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="name">(string)name of the leaderboard to create</param>
		/// <param name="sortmethod">(string)sort method to use for this leaderboard (defaults to Ascending)</param>
		/// <param name="displaytype">(string)display type for this leaderboard (defaults to Numeric)</param>
		/// <param name="createifnotfound">(bool)if this is true the leaderboard will be created if it doesn't exist. Defaults to true.</param>
		/// <param name="onlytrustedwrites">(bool)if this is true the leaderboard scores cannot be set by clients, and can only be set by publisher via SetLeaderboardScore WebAPI. Defaults to false.</param>
		/// <param name="onlyfriendsreads">(bool)if this is true the leaderboard scores can only be read for friends by clients, scores can always be read by publisher. Defaults to false.</param>
		public static string FindOrCreateLeaderboard(uint appid, string name, string sortmethod, string displaytype, bool createifnotfound, bool onlytrustedwrites, bool onlyfriendsreads)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "name", name },
				{ "sortmethod", sortmethod },
				{ "displaytype", displaytype },
				{ "createifnotfound", createifnotfound },
				{ "onlytrustedwrites", onlytrustedwrites },
				{ "onlyfriendsreads", onlyfriendsreads },
			};
			
			return Utility.WebPost<string>( $"{Url}/FindOrCreateLeaderboard/v0002/", headers );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="rangestart">(int32)range start or 0</param>
		/// <param name="rangeend">(int32)range end or max LB entries</param>
		/// <param name="steamid">(uint64)SteamID used for friend & around user requests</param>
		/// <param name="leaderboardid">(int32)ID of the leaderboard to view</param>
		/// <param name="datarequest">(uint32)type of request: RequestGlobal, RequestAroundUser, RequestFriends</param>
		public static string GetLeaderboardEntries(uint appid, int rangestart, int rangeend, ulong steamid, int leaderboardid, uint datarequest)
		{
			return Utility.WebGet<string>( $"{Url}GetLeaderboardEntries/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&rangestart={rangestart}&rangeend={rangeend}&steamid={steamid}&leaderboardid={leaderboardid}&datarequest={datarequest}" );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		public static string GetLeaderboardsForGame(uint appid)
		{
			return Utility.WebGet<string>( $"{Url}GetLeaderboardsForGame/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}" );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="leaderboardid">(uint32)numeric ID of the target leaderboard. Can be retrieved from GetLeaderboardsForGame</param>
		public static string ResetLeaderboard(uint appid, uint leaderboardid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "leaderboardid", leaderboardid },
			};
			
			return Utility.WebPost<string>( $"{Url}/ResetLeaderboard/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="leaderboardid">(uint32)numeric ID of the target leaderboard. Can be retrieved from GetLeaderboardsForGame</param>
		/// <param name="steamid">(uint64)steamID to set the score for</param>
		/// <param name="score">(int32)the score to set for this user</param>
		/// <param name="scoremethod">(string)update method to use. Can be "KeepBest" or "ForceUpdate"</param>
		/// <param name="details">(rawbinary)game-specific details for how the score was earned. Up to 256 bytes.</param>
		public static string SetLeaderboardScore(uint appid, uint leaderboardid, ulong steamid, int score, string scoremethod, byte[] details)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "leaderboardid", leaderboardid },
				{ "steamid", steamid },
				{ "score", score },
				{ "scoremethod", scoremethod },
				{ "details", details },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetLeaderboardScore/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamMicroTxn
	/// </summary>
	public partial class ISteamMicroTxn
	{
		private const string Url = "http://api.steampowered.com/ISteamMicroTxn/";
		
		/// <param name="steamid">(uint64)SteamID of user with the agreement</param>
		/// <param name="agreementid">(uint64)ID of agreement</param>
		/// <param name="appid">(uint32)AppID of game</param>
		/// <param name="nextprocessdate">(string)Date for next process</param>
		public static string AdjustAgreement(ulong steamid, ulong agreementid, uint appid, string nextprocessdate)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "agreementid", agreementid },
				{ "appid", appid },
				{ "nextprocessdate", nextprocessdate },
			};
			
			return Utility.WebPost<string>( $"{Url}/AdjustAgreement/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)SteamID of user with the agreement</param>
		/// <param name="agreementid">(uint64)ID of agreement</param>
		/// <param name="appid">(uint32)AppID of game</param>
		public static string CancelAgreement(ulong steamid, ulong agreementid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "agreementid", agreementid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/CancelAgreement/v0001/", headers );
		}
		
		/// <param name="orderid">(uint64)3rd party ID for transaction</param>
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		public static string FinalizeTxn(ulong orderid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "orderid", orderid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/FinalizeTxn/v0002/", headers );
		}
		
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		/// <param name="type">(string)Report type (GAMESALES, STEAMSTORE, SETTLEMENT)</param>
		/// <param name="time">(string)Beginning time to start report from (RFC 3339 UTC format)</param>
		/// <param name="maxresults">(uint32)Max number of results to return (up to 1000)</param>
		public static string GetReport(uint appid, string type, string time, uint maxresults)
		{
			return Utility.WebGet<string>( $"{Url}GetReport/v0003/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&type={type}&time={time}&maxresults={maxresults}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user making purchase</param>
		/// <param name="appid">(uint32)AppID of game</param>
		public static string GetUserAgreementInfo(ulong steamid, uint appid)
		{
			return Utility.WebGet<string>( $"{Url}GetUserAgreementInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user making purchase</param>
		/// <param name="ipaddress">(string)ip address of user in string format (xxx.xxx.xxx.xxx). Only required if usersession=web</param>
		public static string GetUserInfo(ulong steamid, string ipaddress)
		{
			return Utility.WebGet<string>( $"{Url}GetUserInfo/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&ipaddress={ipaddress}" );
		}
		
		/// <param name="orderid">(uint64)3rd party ID for transaction</param>
		/// <param name="steamid">(uint64)SteamID of user making purchase</param>
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		/// <param name="itemcount">(uint32)Number of items in cart</param>
		/// <param name="language">(string)ISO 639-1 language code of description</param>
		/// <param name="currency">(string)ISO 4217 currency code</param>
		/// <param name="usersession">(string)session where user will authorize the transaction. client or web (defaults to client)</param>
		/// <param name="ipaddress">(string)ip address of user in string format (xxx.xxx.xxx.xxx). Only required if usersession=web</param>
		/// <param name="itemid[]">(uint32)3rd party ID for item</param>
		/// <param name="qty[]">(uint32)Quantity of this item</param>
		/// <param name="amount[]">(int32)Total cost (in cents) of item(s)</param>
		/// <param name="description[]">(string)Description of item</param>
		/// <param name="category[]">(string)Optional category grouping for item</param>
		/// <param name="associated_bundle[]">(uint32)Optional bundleid of associated bundle</param>
		/// <param name="billingtype[]">(string)Optional recurring billing type</param>
		/// <param name="startdate[]">(string)Optional start date for recurring billing</param>
		/// <param name="enddate[]">(string)Optional end date for recurring billing</param>
		/// <param name="period[]">(string)Optional period for recurring billing</param>
		/// <param name="frequency[]">(uint32)Optional frequency for recurring billing</param>
		/// <param name="recurringamt[]">(string)Optional recurring billing amount</param>
		/// <param name="bundlecount">(uint32)Number of bundles in cart</param>
		/// <param name="bundleid[]">(uint32)3rd party ID of the bundle. This shares the same ID space as 3rd party items.</param>
		/// <param name="bundle_qty[]">(uint32)Quantity of this bundle</param>
		/// <param name="bundle_desc[]">(string)Description of bundle</param>
		/// <param name="bundle_category[]">(string)Optional category grouping for bundle</param>
		public static string InitTxn(ulong orderid, ulong steamid, uint appid, uint itemcount, string language, string currency, string usersession, string ipaddress, uint[] itemid, uint[] qty, int[] amount, string[] description, string[] category, uint[] associated_bundle, string[] billingtype, string[] startdate, string[] enddate, string[] period, uint[] frequency, string[] recurringamt, uint bundlecount, uint[] bundleid, uint[] bundle_qty, string[] bundle_desc, string[] bundle_category)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "orderid", orderid },
				{ "steamid", steamid },
				{ "appid", appid },
				{ "itemcount", itemcount },
				{ "language", language },
				{ "currency", currency },
				{ "usersession", usersession },
				{ "ipaddress", ipaddress },
				{ "itemid[0]", itemid[0] },
				{ "qty[0]", qty[0] },
				{ "amount[0]", amount[0] },
				{ "description[0]", description[0] },
				{ "category[0]", category[0] },
				{ "associated_bundle[0]", associated_bundle[0] },
				{ "billingtype[0]", billingtype[0] },
				{ "startdate[0]", startdate[0] },
				{ "enddate[0]", enddate[0] },
				{ "period[0]", period[0] },
				{ "frequency[0]", frequency[0] },
				{ "recurringamt[0]", recurringamt[0] },
				{ "bundlecount", bundlecount },
				{ "bundleid[0]", bundleid[0] },
				{ "bundle_qty[0]", bundle_qty[0] },
				{ "bundle_desc[0]", bundle_desc[0] },
				{ "bundle_category[0]", bundle_category[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/InitTxn/v0003/", headers );
		}
		
		/// <param name="orderid">(uint64)3rd party ID for transaction</param>
		/// <param name="steamid">(uint64)SteamID of user with the agreement</param>
		/// <param name="agreementid">(uint64)ID of agreement</param>
		/// <param name="appid">(uint32)AppID of game</param>
		/// <param name="amount">(int32)Total cost (in cents) to charge</param>
		/// <param name="currency">(string)ISO 4217 currency code</param>
		public static string ProcessAgreement(ulong orderid, ulong steamid, ulong agreementid, uint appid, int amount, string currency)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "orderid", orderid },
				{ "steamid", steamid },
				{ "agreementid", agreementid },
				{ "appid", appid },
				{ "amount", amount },
				{ "currency", currency },
			};
			
			return Utility.WebPost<string>( $"{Url}/ProcessAgreement/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		/// <param name="orderid">(uint64)3rd party ID for transaction</param>
		/// <param name="transid">(uint64)Steam transaction ID</param>
		public static string QueryTxn(uint appid, ulong orderid, ulong transid)
		{
			return Utility.WebGet<string>( $"{Url}QueryTxn/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&orderid={orderid}&transid={transid}" );
		}
		
		/// <param name="orderid">(uint64)3rd party ID for transaction</param>
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		public static string RefundTxn(ulong orderid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "orderid", orderid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/RefundTxn/v0002/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamMicroTxnSandbox
	/// </summary>
	public partial class ISteamMicroTxnSandbox
	{
		private const string Url = "http://api.steampowered.com/ISteamMicroTxnSandbox/";
		
		/// <param name="steamid">(uint64)SteamID of user with the agreement</param>
		/// <param name="agreementid">(uint64)ID of agreement</param>
		/// <param name="appid">(uint32)AppID of game</param>
		/// <param name="nextprocessdate">(string)Date for next process</param>
		public static string AdjustAgreement(ulong steamid, ulong agreementid, uint appid, string nextprocessdate)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "agreementid", agreementid },
				{ "appid", appid },
				{ "nextprocessdate", nextprocessdate },
			};
			
			return Utility.WebPost<string>( $"{Url}/AdjustAgreement/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)SteamID of user with the agreement</param>
		/// <param name="agreementid">(uint64)ID of agreement</param>
		/// <param name="appid">(uint32)AppID of game</param>
		public static string CancelAgreement(ulong steamid, ulong agreementid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "agreementid", agreementid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/CancelAgreement/v0001/", headers );
		}
		
		/// <param name="orderid">(uint64)3rd party ID for transaction</param>
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		public static string FinalizeTxn(ulong orderid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "orderid", orderid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/FinalizeTxn/v0002/", headers );
		}
		
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		/// <param name="type">(string)Report type (GAMESALES, STEAMSTORE, SETTLEMENT)</param>
		/// <param name="time">(string)Beginning time to start report from (RFC 3339 UTC format)</param>
		/// <param name="maxresults">(uint32)Max number of results to return (up to 1000)</param>
		public static string GetReport(uint appid, string type, string time, uint maxresults)
		{
			return Utility.WebGet<string>( $"{Url}GetReport/v0003/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&type={type}&time={time}&maxresults={maxresults}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user making purchase</param>
		/// <param name="appid">(uint32)AppID of game</param>
		public static string GetUserAgreementInfo(ulong steamid, uint appid)
		{
			return Utility.WebGet<string>( $"{Url}GetUserAgreementInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user making purchase</param>
		/// <param name="ipaddress">(string)ip address of user in string format (xxx.xxx.xxx.xxx). Only required if usersession=web</param>
		public static string GetUserInfo(ulong steamid, string ipaddress)
		{
			return Utility.WebGet<string>( $"{Url}GetUserInfo/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&ipaddress={ipaddress}" );
		}
		
		/// <param name="orderid">(uint64)3rd party ID for transaction</param>
		/// <param name="steamid">(uint64)SteamID of user making purchase</param>
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		/// <param name="itemcount">(uint32)Number of items in cart</param>
		/// <param name="language">(string)ISO 639-1 language code of description</param>
		/// <param name="currency">(string)ISO 4217 currency code</param>
		/// <param name="itemid[]">(uint32)3rd party ID for item</param>
		/// <param name="qty[]">(uint32)Quantity of this item</param>
		/// <param name="amount[]">(int32)Total cost (in cents) of item(s)</param>
		/// <param name="description[]">(string)Description of item</param>
		/// <param name="category[]">(string)Optional category grouping for item</param>
		/// <param name="billingtype[]">(string)Optional recurring billing type</param>
		/// <param name="startdate[]">(string)Optional start date for recurring billing</param>
		/// <param name="enddate[]">(string)Optional end date for recurring billing</param>
		/// <param name="period[]">(string)Optional period for recurring billing</param>
		/// <param name="frequency[]">(uint32)Optional frequency for recurring billing</param>
		/// <param name="recurringamt[]">(string)Optional recurring billing amount</param>
		public static string InitTxn(ulong orderid, ulong steamid, uint appid, uint itemcount, string language, string currency, uint[] itemid, uint[] qty, int[] amount, string[] description, string[] category, string[] billingtype, string[] startdate, string[] enddate, string[] period, uint[] frequency, string[] recurringamt)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "orderid", orderid },
				{ "steamid", steamid },
				{ "appid", appid },
				{ "itemcount", itemcount },
				{ "language", language },
				{ "currency", currency },
				{ "itemid[0]", itemid[0] },
				{ "qty[0]", qty[0] },
				{ "amount[0]", amount[0] },
				{ "description[0]", description[0] },
				{ "category[0]", category[0] },
				{ "billingtype[0]", billingtype[0] },
				{ "startdate[0]", startdate[0] },
				{ "enddate[0]", enddate[0] },
				{ "period[0]", period[0] },
				{ "frequency[0]", frequency[0] },
				{ "recurringamt[0]", recurringamt[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/InitTxn/v0003/", headers );
		}
		
		/// <param name="steamid">(uint64)SteamID of user with the agreement</param>
		/// <param name="agreementid">(uint64)ID of agreement</param>
		/// <param name="appid">(uint32)AppID of game</param>
		/// <param name="amount">(int32)Total cost (in cents) to charge</param>
		/// <param name="currency">(string)ISO 4217 currency code</param>
		public static string ProcessAgreement(ulong steamid, ulong agreementid, uint appid, int amount, string currency)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "agreementid", agreementid },
				{ "appid", appid },
				{ "amount", amount },
				{ "currency", currency },
			};
			
			return Utility.WebPost<string>( $"{Url}/ProcessAgreement/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		/// <param name="orderid">(uint64)3rd party ID for transaction</param>
		/// <param name="transid">(uint64)Steam transaction ID</param>
		public static string QueryTxn(uint appid, ulong orderid, ulong transid)
		{
			return Utility.WebGet<string>( $"{Url}QueryTxn/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&orderid={orderid}&transid={transid}" );
		}
		
		/// <param name="orderid">(uint64)3rd party ID for transaction</param>
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		public static string RefundTxn(ulong orderid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "orderid", orderid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/RefundTxn/v0002/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamNews
	/// </summary>
	public partial class ISteamNews
	{
		private const string Url = "http://api.steampowered.com/ISteamNews/";
		
		/// <param name="appid">(uint32)AppID to retrieve news for</param>
		/// <param name="maxlength">(uint32)Maximum length for the content to return, if this is 0 the full content is returned, if it's less then a blurb is generated to fit.</param>
		/// <param name="enddate_dt">(uint32)Retrieve posts earlier than this date (unix epoch timestamp)</param>
		/// <param name="count">(uint32)# of posts to retrieve (default 20)</param>
		/// <param name="feeds">(string)Comma-seperated list of feed names to return news for</param>
		public static string GetNewsForApp(uint appid, uint maxlength, DateTime enddate_dt, uint count, string feeds)
		{
			uint enddate = Facepunch.Steamworks.Utility.Epoch.FromDateTime( enddate_dt );
			
			return Utility.WebGet<string>( $"{Url}GetNewsForApp/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&maxlength={maxlength}&enddate={enddate}&count={count}&feeds={feeds}" );
		}
		
		/// <param name="appid">(uint32)AppID to retrieve news for</param>
		/// <param name="maxlength">(uint32)Maximum length for the content to return, if this is 0 the full content is returned, if it's less then a blurb is generated to fit.</param>
		/// <param name="enddate_dt">(uint32)Retrieve posts earlier than this date (unix epoch timestamp)</param>
		/// <param name="count">(uint32)# of posts to retrieve (default 20)</param>
		/// <param name="feeds">(string)Comma-seperated list of feed names to return news for</param>
		public static string GetNewsForAppAuthed(uint appid, uint maxlength, DateTime enddate_dt, uint count, string feeds)
		{
			uint enddate = Facepunch.Steamworks.Utility.Epoch.FromDateTime( enddate_dt );
			
			return Utility.WebGet<string>( $"{Url}GetNewsForAppAuthed/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&maxlength={maxlength}&enddate={enddate}&count={count}&feeds={feeds}" );
		}
		
	}
	
	/// <summary>
	/// ISteamPayPalPaymentsHub
	/// </summary>
	public partial class ISteamPayPalPaymentsHub
	{
		private const string Url = "http://api.steampowered.com/ISteamPayPalPaymentsHub/";
		
		public static string PayPalPaymentsHubPaymentNotification()
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
			};
			
			return Utility.WebPost<string>( $"{Url}/PayPalPaymentsHubPaymentNotification/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamPublishedItemSearch
	/// </summary>
	public partial class ISteamPublishedItemSearch
	{
		private const string Url = "http://api.steampowered.com/ISteamPublishedItemSearch/";
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)appID of product</param>
		/// <param name="startidx">(uint32)Starting index in the result set (0 based)</param>
		/// <param name="count">(uint32)Number Requested</param>
		/// <param name="tagcount">(uint32)Number of Tags Specified</param>
		/// <param name="usertagcount">(uint32)Number of User specific tags requested</param>
		/// <param name="hasappadminaccess">(bool)Whether the user making the request is an admin for the app and can see private files</param>
		/// <param name="fileType">(uint32)EPublishedFileInfoMatchingFileType, defaults to k_PFI_MatchingFileType_Items</param>
		/// <param name="tag[]">(string)Tag to filter result set</param>
		/// <param name="usertag[]">(string)A user specific tag</param>
		public static string RankedByPublicationOrder(ulong steamid, uint appid, uint startidx, uint count, uint tagcount, uint usertagcount, bool hasappadminaccess, uint fileType, string[] tag, string[] usertag)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "startidx", startidx },
				{ "count", count },
				{ "tagcount", tagcount },
				{ "usertagcount", usertagcount },
				{ "hasappadminaccess", hasappadminaccess },
				{ "fileType", fileType },
				{ "tag[0]", tag[0] },
				{ "usertag[0]", usertag[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/RankedByPublicationOrder/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)appID of product</param>
		/// <param name="startidx">(uint32)Starting index in the result set (0 based)</param>
		/// <param name="count">(uint32)Number Requested</param>
		/// <param name="tagcount">(uint32)Number of Tags Specified</param>
		/// <param name="usertagcount">(uint32)Number of User specific tags requested</param>
		/// <param name="hasappadminaccess">(bool)Whether the user making the request is an admin for the app and can see private files</param>
		/// <param name="fileType">(uint32)EPublishedFileInfoMatchingFileType, defaults to k_PFI_MatchingFileType_Items</param>
		/// <param name="days">(uint32)[1,7] number of days for the trend period, including today</param>
		/// <param name="tag[]">(string)Tag to filter result set</param>
		/// <param name="usertag[]">(string)A user specific tag</param>
		public static string RankedByTrend(ulong steamid, uint appid, uint startidx, uint count, uint tagcount, uint usertagcount, bool hasappadminaccess, uint fileType, uint days, string[] tag, string[] usertag)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "startidx", startidx },
				{ "count", count },
				{ "tagcount", tagcount },
				{ "usertagcount", usertagcount },
				{ "hasappadminaccess", hasappadminaccess },
				{ "fileType", fileType },
				{ "days", days },
				{ "tag[0]", tag[0] },
				{ "usertag[0]", usertag[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/RankedByTrend/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)appID of product</param>
		/// <param name="startidx">(uint32)Starting index in the result set (0 based)</param>
		/// <param name="count">(uint32)Number Requested</param>
		/// <param name="tagcount">(uint32)Number of Tags Specified</param>
		/// <param name="usertagcount">(uint32)Number of User specific tags requested</param>
		/// <param name="hasappadminaccess">(bool)Whether the user making the request is an admin for the app and can see private files</param>
		/// <param name="fileType">(uint32)EPublishedFileInfoMatchingFileType, defaults to k_PFI_MatchingFileType_Items</param>
		/// <param name="tag[]">(string)Tag to filter result set</param>
		/// <param name="usertag[]">(string)A user specific tag</param>
		public static string RankedByVote(ulong steamid, uint appid, uint startidx, uint count, uint tagcount, uint usertagcount, bool hasappadminaccess, uint fileType, string[] tag, string[] usertag)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "startidx", startidx },
				{ "count", count },
				{ "tagcount", tagcount },
				{ "usertagcount", usertagcount },
				{ "hasappadminaccess", hasappadminaccess },
				{ "fileType", fileType },
				{ "tag[0]", tag[0] },
				{ "usertag[0]", usertag[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/RankedByVote/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint64)appID relevant to all subsequent tags</param>
		/// <param name="tagcount">(uint32)Number of Tags Specified</param>
		/// <param name="usertagcount">(uint32)Number of User specific tags requested</param>
		/// <param name="hasappadminaccess">(bool)Whether the user making the request is an admin for the app and can see private files</param>
		/// <param name="fileType">(uint32)EPublishedFileInfoMatchingFileType, defaults to k_PFI_MatchingFileType_Items</param>
		/// <param name="tag[]">(string)Tag to filter result set</param>
		/// <param name="usertag[]">(string)A user specific tag</param>
		public static string ResultSetSummary(ulong steamid, ulong appid, uint tagcount, uint usertagcount, bool hasappadminaccess, uint fileType, string[] tag, string[] usertag)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "tagcount", tagcount },
				{ "usertagcount", usertagcount },
				{ "hasappadminaccess", hasappadminaccess },
				{ "fileType", fileType },
				{ "tag[0]", tag[0] },
				{ "usertag[0]", usertag[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/ResultSetSummary/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamPublishedItemVoting
	/// </summary>
	public partial class ISteamPublishedItemVoting
	{
		private const string Url = "http://api.steampowered.com/ISteamPublishedItemVoting/";
		
		/// <param name="steamid">(uint64)Steam ID of user</param>
		/// <param name="appid">(uint32)appID of product</param>
		/// <param name="count">(uint32)Count of how many items we are querying</param>
		/// <param name="publishedfileid[]">(uint64)The Published File ID who's vote details are required</param>
		public static string ItemVoteSummary(ulong steamid, uint appid, uint count, ulong[] publishedfileid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "count", count },
				{ "publishedfileid[0]", publishedfileid[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/ItemVoteSummary/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)Steam ID of user</param>
		/// <param name="count">(uint32)Count of how many items we are querying</param>
		/// <param name="publishedfileid[]">(uint64)A Specific Published Item</param>
		public static string UserVoteSummary(ulong steamid, uint count, ulong[] publishedfileid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "count", count },
				{ "publishedfileid[0]", publishedfileid[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/UserVoteSummary/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamRemoteStorage
	/// </summary>
	public partial class ISteamRemoteStorage
	{
		private const string Url = "http://api.steampowered.com/ISteamRemoteStorage/";
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)appID of product</param>
		public static string EnumerateUserPublishedFiles(ulong steamid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/EnumerateUserPublishedFiles/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)appID of product</param>
		/// <param name="listtype">(uint32)EUCMListType</param>
		public static string EnumerateUserSubscribedFiles(ulong steamid, uint appid, uint listtype)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "listtype", listtype },
			};
			
			return Utility.WebPost<string>( $"{Url}/EnumerateUserSubscribedFiles/v0001/", headers );
		}
		
		/// <param name="collectioncount">(uint32)Number of collections being requested</param>
		/// <param name="publishedfileids[]">(uint64)collection ids to get the details for</param>
		public static string GetCollectionDetails(uint collectioncount, ulong[] publishedfileids)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "collectioncount", collectioncount },
				{ "publishedfileids[0]", publishedfileids[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/GetCollectionDetails/v0001/", headers );
		}
		
		/// <param name="itemcount">(uint32)Number of items being requested</param>
		/// <param name="publishedfileids[]">(uint64)published file id to look up</param>
		public static string GetPublishedFileDetails(uint itemcount, ulong[] publishedfileids)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "itemcount", itemcount },
				{ "publishedfileids[0]", publishedfileids[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/GetPublishedFileDetails/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)If specified, only returns details if the file is owned by the SteamID specified</param>
		/// <param name="ugcid">(uint64)ID of UGC file to get info for</param>
		/// <param name="appid">(uint32)appID of product</param>
		public static string GetUGCFileDetails(ulong steamid, ulong ugcid, uint appid)
		{
			return Utility.WebGet<string>( $"{Url}GetUGCFileDetails/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&ugcid={ugcid}&appid={appid}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="ugcid">(uint64)ID of UGC file whose bits are being fiddled with</param>
		/// <param name="appid">(uint32)appID of product to change updating state for</param>
		/// <param name="used">(bool)New state of flag</param>
		public static string SetUGCUsedByGC(ulong steamid, ulong ugcid, uint appid, bool used)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "ugcid", ugcid },
				{ "appid", appid },
				{ "used", used },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetUGCUsedByGC/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)appID of product</param>
		/// <param name="publishedfileid">(uint64)published file id to subscribe to</param>
		public static string SubscribePublishedFile(ulong steamid, uint appid, ulong publishedfileid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "publishedfileid", publishedfileid },
			};
			
			return Utility.WebPost<string>( $"{Url}/SubscribePublishedFile/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)appID of product</param>
		/// <param name="publishedfileid">(uint64)published file id to unsubscribe from</param>
		public static string UnsubscribePublishedFile(ulong steamid, uint appid, ulong publishedfileid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "publishedfileid", publishedfileid },
			};
			
			return Utility.WebPost<string>( $"{Url}/UnsubscribePublishedFile/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamSpecialSurvey
	/// </summary>
	public partial class ISteamSpecialSurvey
	{
		private const string Url = "http://api.steampowered.com/ISteamSpecialSurvey/";
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="surveyid">(uint32)ID of the survey being taken</param>
		/// <param name="steamid">(uint64)SteamID of the user taking the survey</param>
		/// <param name="token">(string)Survey identity verification token for the user</param>
		public static string CheckUserStatus(uint appid, uint surveyid, ulong steamid, string token)
		{
			return Utility.WebGet<string>( $"{Url}CheckUserStatus/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&surveyid={surveyid}&steamid={steamid}&token={token}" );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="surveyid">(uint32)ID of the survey being taken</param>
		/// <param name="steamid">(uint64)SteamID of the user taking the survey</param>
		/// <param name="token">(string)Survey identity verification token for the user</param>
		public static string SetUserFinished(uint appid, uint surveyid, ulong steamid, string token)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "surveyid", surveyid },
				{ "steamid", steamid },
				{ "token", token },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetUserFinished/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamUser
	/// </summary>
	public partial class ISteamUser
	{
		private const string Url = "http://api.steampowered.com/ISteamUser/";
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)AppID to check for ownership</param>
		public static string CheckAppOwnership(ulong steamid, uint appid)
		{
			return Utility.WebGet<string>( $"{Url}CheckAppOwnership/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appids">(string)Comma-delimited list of appids (max: 100)</param>
		public static string GetAppPriceInfo(ulong steamid, string appids)
		{
			return Utility.WebGet<string>( $"{Url}GetAppPriceInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appids={appids}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="relationship">(string)relationship type (ex: friend)</param>
		public static string GetFriendList(ulong steamid, string relationship)
		{
			return Utility.WebGet<string>( $"{Url}GetFriendList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&relationship={relationship}" );
		}
		
		/// <param name="steamids">(string)Comma-delimited list of SteamIDs</param>
		public static string GetPlayerBans(string steamids)
		{
			return Utility.WebGet<string>( $"{Url}GetPlayerBans/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamids={steamids}" );
		}
		
		/// <param name="steamids">(string)Comma-delimited list of SteamIDs (max: 100)</param>
		public static string GetPlayerSummaries(string steamids)
		{
			return Utility.WebGet<string>( $"{Url}GetPlayerSummaries/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&steamids={steamids}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		public static string GetPublisherAppOwnership(ulong steamid)
		{
			return Utility.WebGet<string>( $"{Url}GetPublisherAppOwnership/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		public static string GetUserGroupList(ulong steamid)
		{
			return Utility.WebGet<string>( $"{Url}GetUserGroupList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="packageid">(uint32)PackageID to grant</param>
		/// <param name="ipaddress">(string)ip address of user in string format (xxx.xxx.xxx.xxx).</param>
		/// <param name="thirdpartykey">(string)Optionally associate third party key during grant. 'thirdpartyappid' will have to be set.</param>
		/// <param name="thirdpartyappid">(uint32)Has to be set if 'thirdpartykey' is set. The appid associated with the 'thirdpartykey'.</param>
		public static string GrantPackage(ulong steamid, uint packageid, string ipaddress, string thirdpartykey, uint thirdpartyappid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "packageid", packageid },
				{ "ipaddress", ipaddress },
				{ "thirdpartykey", thirdpartykey },
				{ "thirdpartyappid", thirdpartyappid },
			};
			
			return Utility.WebPost<string>( $"{Url}/GrantPackage/v0001/", headers );
		}
		
		/// <param name="vanityurl">(string)The vanity URL to get a SteamID for</param>
		/// <param name="url_type">(int32)The type of vanity URL. 1 (default): Individual profile, 2: Group, 3: Official game group</param>
		public static string ResolveVanityURL(string vanityurl, int url_type)
		{
			return Utility.WebGet<string>( $"{Url}ResolveVanityURL/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&vanityurl={vanityurl}&url_type={url_type}" );
		}
		
	}
	
	/// <summary>
	/// ISteamUserAuth
	/// </summary>
	public partial class ISteamUserAuth
	{
		private const string Url = "http://api.steampowered.com/ISteamUserAuth/";
		
		/// <param name="steamid">(uint64)Should be the users steamid, unencrypted.</param>
		/// <param name="sessionkey">(rawbinary)Should be a 32 byte random blob of data, which is then encrypted with RSA using the Steam system's public key.  Randomness is important here for security.</param>
		/// <param name="encrypted_loginkey">(rawbinary)Should be the users hashed loginkey, AES encrypted with the sessionkey.</param>
		public static string AuthenticateUser(ulong steamid, byte[] sessionkey, byte[] encrypted_loginkey)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "sessionkey", sessionkey },
				{ "encrypted_loginkey", encrypted_loginkey },
			};
			
			return Utility.WebPost<string>( $"{Url}/AuthenticateUser/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="ticket">(string)Ticket from GetAuthSessionTicket.</param>
		public static string AuthenticateUserTicket(uint appid, string ticket)
		{
			return Utility.WebGet<string>( $"{Url}AuthenticateUserTicket/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&ticket={ticket}" );
		}
		
	}
	
	/// <summary>
	/// ISteamUserOAuth
	/// </summary>
	public partial class ISteamUserOAuth
	{
		private const string Url = "http://api.steampowered.com/ISteamUserOAuth/";
		
		/// <param name="access_token">(string)OAuth2 token for which to return details</param>
		public static string GetTokenDetails(string access_token)
		{
			return Utility.WebGet<string>( $"{Url}GetTokenDetails/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&access_token={access_token}" );
		}
		
	}
	
	/// <summary>
	/// ISteamUserStats
	/// </summary>
	public partial class ISteamUserStats
	{
		private const string Url = "http://api.steampowered.com/ISteamUserStats/";
		
		/// <param name="gameid">(uint64)GameID to retrieve the achievement percentages for</param>
		public static string GetGlobalAchievementPercentagesForApp(ulong gameid)
		{
			return Utility.WebGet<string>( $"{Url}GetGlobalAchievementPercentagesForApp/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&gameid={gameid}" );
		}
		
		/// <param name="appid">(uint32)AppID that we're getting global stats for</param>
		/// <param name="count">(uint32)Number of stats get data for</param>
		/// <param name="name[]">(string)Names of stat to get data for</param>
		/// <param name="startdate_dt">(uint32)Start date for daily totals (unix epoch timestamp)</param>
		/// <param name="enddate_dt">(uint32)End date for daily totals (unix epoch timestamp)</param>
		public static string GetGlobalStatsForGame(uint appid, uint count, string[] name, DateTime startdate_dt, DateTime enddate_dt)
		{
			uint startdate = Facepunch.Steamworks.Utility.Epoch.FromDateTime( startdate_dt );
			uint enddate = Facepunch.Steamworks.Utility.Epoch.FromDateTime( enddate_dt );
			
			return Utility.WebGet<string>( $"{Url}GetGlobalStatsForGame/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&count={count}&name[0]={name[0]}&startdate={startdate}&enddate={enddate}" );
		}
		
		/// <param name="appid">(uint32)AppID that we're getting user count for</param>
		public static string GetNumberOfCurrentPlayers(uint appid)
		{
			return Utility.WebGet<string>( $"{Url}GetNumberOfCurrentPlayers/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)AppID to get achievements for</param>
		/// <param name="l">(string)Language to return strings for</param>
		public static string GetPlayerAchievements(ulong steamid, uint appid, string l)
		{
			return Utility.WebGet<string>( $"{Url}GetPlayerAchievements/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}&l={l}" );
		}
		
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="l">(string)localized language to return (english, french, etc.)</param>
		public static string GetSchemaForGame(uint appid, string l)
		{
			return Utility.WebGet<string>( $"{Url}GetSchemaForGame/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&l={l}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)appid of game</param>
		public static string GetUserStatsForGame(ulong steamid, uint appid)
		{
			return Utility.WebGet<string>( $"{Url}GetUserStatsForGame/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}" );
		}
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)appid of game</param>
		/// <param name="count">(uint32)Number of stats and achievements to set a value for (name/value param pairs)</param>
		/// <param name="name[]">(string)Name of stat or achievement to set</param>
		/// <param name="value[]">(uint32)Value to set</param>
		public static string SetUserStatsForGame(ulong steamid, uint appid, uint count, string[] name, uint[] value)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "count", count },
				{ "name[0]", name[0] },
				{ "value[0]", value[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetUserStatsForGame/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamVideo
	/// </summary>
	public partial class ISteamVideo
	{
		private const string Url = "http://api.steampowered.com/ISteamVideo/";
		
		/// <param name="steamid">(uint64)SteamID of user</param>
		/// <param name="appid">(uint32)appID of the video</param>
		/// <param name="videoid">(string)ID of the video on the provider's site</param>
		/// <param name="accountname">(string)Account name of the video's owner on the provider's site</param>
		public static string AddVideo(ulong steamid, uint appid, string videoid, string accountname)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "videoid", videoid },
				{ "accountname", accountname },
			};
			
			return Utility.WebPost<string>( $"{Url}/AddVideo/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamWebAPIUtil
	/// </summary>
	public partial class ISteamWebAPIUtil
	{
		private const string Url = "http://api.steampowered.com/ISteamWebAPIUtil/";
		
		public static string GetServerInfo()
		{
			return Utility.WebGet<string>( $"{Url}GetServerInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&" );
		}
		
		public static string GetSupportedAPIList()
		{
			return Utility.WebGet<string>( $"{Url}GetSupportedAPIList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&" );
		}
		
	}
	
	/// <summary>
	/// ISteamWebUserPresenceOAuth
	/// </summary>
	public partial class ISteamWebUserPresenceOAuth
	{
		private const string Url = "http://api.steampowered.com/ISteamWebUserPresenceOAuth/";
		
		/// <param name="steamid">(string)Steam ID of the user</param>
		/// <param name="umqid">(uint64)UMQ Session ID</param>
		/// <param name="message">(uint32)Message that was last known to the user</param>
		/// <param name="pollid">(uint32)Caller-specific poll id</param>
		/// <param name="sectimeout">(uint32)Long-poll timeout in seconds</param>
		/// <param name="secidletime">(uint32)How many seconds is client considering itself idle, e.g. screen is off</param>
		/// <param name="use_accountids">(uint32)Boolean, 0 (default): return steamid_from in output, 1: return accountid_from</param>
		public static string PollStatus(string steamid, ulong umqid, uint message, uint pollid, uint sectimeout, uint secidletime, uint use_accountids)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "umqid", umqid },
				{ "message", message },
				{ "pollid", pollid },
				{ "sectimeout", sectimeout },
				{ "secidletime", secidletime },
				{ "use_accountids", use_accountids },
			};
			
			return Utility.WebPost<string>( $"{Url}/PollStatus/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ISteamWorkshop
	/// </summary>
	public partial class ISteamWorkshop
	{
		private const string Url = "http://api.steampowered.com/ISteamWorkshop/";
		
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		/// <param name="itemcount">(uint32)Number of items to associate</param>
		/// <param name="publishedfileid[]">(uint64)the workshop published file id</param>
		/// <param name="gameitemid[]">(uint32)3rd party ID for item</param>
		/// <param name="revenuepercentage[]">(float)Percentage of revenue the owners of the workshop item will get from the sale of the item [0.0, 100.0].  For bundle-like items, send over an entry for each item in the bundle (gameitemid = bundle id) with different publishedfileids and with the revenue percentage pre-split amongst the items in the bundle (i.e. 30% / 10 items in the bundle)</param>
		/// <param name="gameitemdescription[]">(string)Game's description of the game item</param>
		public static string AssociateWorkshopItems(uint appid, uint itemcount, ulong[] publishedfileid, uint[] gameitemid, float[] revenuepercentage, string[] gameitemdescription)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "itemcount", itemcount },
				{ "publishedfileid[0]", publishedfileid[0] },
				{ "gameitemid[0]", gameitemid[0] },
				{ "revenuepercentage[0]", revenuepercentage[0] },
				{ "gameitemdescription[0]", gameitemdescription[0] },
			};
			
			return Utility.WebPost<string>( $"{Url}/AssociateWorkshopItems/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)AppID of game this transaction is for</param>
		public static string GetContributors(uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/GetContributors/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// IGameServersService
	/// </summary>
	public partial class IGameServersService
	{
		private const string Url = "http://api.steampowered.com/IGameServersService/";
		
		/// <param name="appid">(uint32)The app to use the account for</param>
		/// <param name="memo">(string)The memo to set on the new account</param>
		public static string CreateAccount(uint appid, string memo)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "memo", memo },
			};
			
			return Utility.WebPost<string>( $"{Url}/CreateAccount/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)The SteamID of the game server account to delete</param>
		public static string DeleteAccount(ulong steamid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
			};
			
			return Utility.WebPost<string>( $"{Url}/DeleteAccount/v0001/", headers );
		}
		
		public static string GetAccountList()
		{
			return Utility.WebGet<string>( $"{Url}GetAccountList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&" );
		}
		
		/// <param name="steamid">(uint64)The SteamID of the game server to get info on</param>
		public static string GetAccountPublicInfo(ulong steamid)
		{
			return Utility.WebGet<string>( $"{Url}GetAccountPublicInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}" );
		}
		
		/// <param name="server_steamids">(uint64)</param>
		public static string GetServerIPsBySteamID(ulong server_steamids)
		{
			return Utility.WebGet<string>( $"{Url}GetServerIPsBySteamID/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&server_steamids={server_steamids}" );
		}
		
		/// <param name="server_ips">(string)</param>
		public static string GetServerSteamIDsByIP(string server_ips)
		{
			return Utility.WebGet<string>( $"{Url}GetServerSteamIDsByIP/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&server_ips={server_ips}" );
		}
		
		/// <param name="login_token">(string)Login token to query</param>
		public static string QueryLoginToken(string login_token)
		{
			return Utility.WebGet<string>( $"{Url}QueryLoginToken/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&login_token={login_token}" );
		}
		
		/// <param name="steamid">(uint64)The SteamID of the game server to reset the login token of</param>
		public static string ResetLoginToken(ulong steamid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
			};
			
			return Utility.WebPost<string>( $"{Url}/ResetLoginToken/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)</param>
		/// <param name="banned">(bool)</param>
		/// <param name="ban_seconds">(uint32)</param>
		public static string SetBanStatus(ulong steamid, bool banned, uint ban_seconds)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "banned", banned },
				{ "ban_seconds", ban_seconds },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetBanStatus/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)The SteamID of the game server to set the memo on</param>
		/// <param name="memo">(string)The memo to set on the new account</param>
		public static string SetMemo(ulong steamid, string memo)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "memo", memo },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetMemo/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// IBroadcastService
	/// </summary>
	public partial class IBroadcastService
	{
		private const string Url = "http://api.steampowered.com/IBroadcastService/";
		
		/// <param name="appid">(uint32)</param>
		/// <param name="steamid">(uint64)</param>
		/// <param name="broadcast_id">(uint64)</param>
		/// <param name="frame_data">(string)</param>
		public static string PostGameDataFrame(uint appid, ulong steamid, ulong broadcast_id, string frame_data)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "steamid", steamid },
				{ "broadcast_id", broadcast_id },
				{ "frame_data", frame_data },
			};
			
			return Utility.WebPost<string>( $"{Url}/PostGameDataFrame/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// IPublishedFileService
	/// </summary>
	public partial class IPublishedFileService
	{
		private const string Url = "http://api.steampowered.com/IPublishedFileService/";
		
		/// <param name="query_type">(uint32)enumeration EPublishedFileQueryType in clientenums.h</param>
		/// <param name="page">(uint32)Current page</param>
		/// <param name="numperpage">(uint32)(Optional) The number of results, per page to return.</param>
		/// <param name="creator_appid">(uint32)App that created the files</param>
		/// <param name="appid">(uint32)App that consumes the files</param>
		/// <param name="requiredtags">(string)Tags to match on. See match_all_tags parameter below</param>
		/// <param name="excludedtags">(string)(Optional) Tags that must NOT be present on a published file to satisfy the query.</param>
		/// <param name="match_all_tags">(bool)If true, then items must have all the tags specified, otherwise they must have at least one of the tags.</param>
		/// <param name="required_flags">(string)Required flags that must be set on any returned items</param>
		/// <param name="omitted_flags">(string)Flags that must not be set on any returned items</param>
		/// <param name="search_text">(string)Text to match in the item's title or description</param>
		/// <param name="filetype">(uint32)EPublishedFileInfoMatchingFileType</param>
		/// <param name="child_publishedfileid">(uint64)Find all items that reference the given item.</param>
		/// <param name="days">(uint32)If query_type is k_PublishedFileQueryType_RankedByTrend, then this is the number of days to get votes for [1,7].</param>
		/// <param name="include_recent_votes_only">(bool)If query_type is k_PublishedFileQueryType_RankedByTrend, then limit result set just to items that have votes within the day range given</param>
		/// <param name="cache_max_age_seconds">(uint32)Allow stale data to be returned for the specified number of seconds.</param>
		/// <param name="language">(int32)Language to search in and also what gets returned. Defaults to English.</param>
		/// <param name="required_kv_tags">({message})Required key-value tags to match on.</param>
		/// <param name="totalonly">(bool)(Optional) If true, only return the total number of files that satisfy this query.</param>
		/// <param name="ids_only">(bool)(Optional) If true, only return the published file ids of files that satisfy this query.</param>
		/// <param name="return_vote_data">(bool)Return vote data</param>
		/// <param name="return_tags">(bool)Return tags in the file details</param>
		/// <param name="return_kv_tags">(bool)Return key-value tags in the file details</param>
		/// <param name="return_previews">(bool)Return preview image and video details in the file details</param>
		/// <param name="return_children">(bool)Return child item ids in the file details</param>
		/// <param name="return_short_description">(bool)Populate the short_description field instead of file_description</param>
		/// <param name="return_for_sale_data">(bool)Return pricing information, if applicable</param>
		/// <param name="return_metadata">(bool)Populate the metadata</param>
		public static string QueryFiles(uint query_type, uint page, uint numperpage, uint creator_appid, uint appid, string requiredtags, string excludedtags, bool match_all_tags, string required_flags, string omitted_flags, string search_text, uint filetype, ulong child_publishedfileid, uint days, bool include_recent_votes_only, uint cache_max_age_seconds, int language, string required_kv_tags, bool totalonly, bool ids_only, bool return_vote_data, bool return_tags, bool return_kv_tags, bool return_previews, bool return_children, bool return_short_description, bool return_for_sale_data, bool return_metadata)
		{
			return Utility.WebGet<string>( $"{Url}QueryFiles/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&query_type={query_type}&page={page}&numperpage={numperpage}&creator_appid={creator_appid}&appid={appid}&requiredtags={requiredtags}&excludedtags={excludedtags}&match_all_tags={match_all_tags}&required_flags={required_flags}&omitted_flags={omitted_flags}&search_text={search_text}&filetype={filetype}&child_publishedfileid={child_publishedfileid}&days={days}&include_recent_votes_only={include_recent_votes_only}&cache_max_age_seconds={cache_max_age_seconds}&language={language}&required_kv_tags={required_kv_tags}&totalonly={totalonly}&ids_only={ids_only}&return_vote_data={return_vote_data}&return_tags={return_tags}&return_kv_tags={return_kv_tags}&return_previews={return_previews}&return_children={return_children}&return_short_description={return_short_description}&return_for_sale_data={return_for_sale_data}&return_metadata={return_metadata}" );
		}
		
		/// <param name="publishedfileid">(uint64)</param>
		/// <param name="appid">(uint32)</param>
		/// <param name="metadata">(string)</param>
		public static string SetDeveloperMetadata(ulong publishedfileid, uint appid, string metadata)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "publishedfileid", publishedfileid },
				{ "appid", appid },
				{ "metadata", metadata },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetDeveloperMetadata/v0001/", headers );
		}
		
		/// <param name="publishedfileid">(uint64)</param>
		/// <param name="appid">(uint32)</param>
		/// <param name="add_tags">(string)</param>
		/// <param name="remove_tags">(string)</param>
		public static string UpdateTags(ulong publishedfileid, uint appid, string add_tags, string remove_tags)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "publishedfileid", publishedfileid },
				{ "appid", appid },
				{ "add_tags", add_tags },
				{ "remove_tags", remove_tags },
			};
			
			return Utility.WebPost<string>( $"{Url}/UpdateTags/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// IEconService
	/// </summary>
	public partial class IEconService
	{
		private const string Url = "http://api.steampowered.com/IEconService/";
		
		/// <param name="tradeofferid">(uint64)</param>
		public static string CancelTradeOffer(ulong tradeofferid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "tradeofferid", tradeofferid },
			};
			
			return Utility.WebPost<string>( $"{Url}/CancelTradeOffer/v0001/", headers );
		}
		
		/// <param name="tradeofferid">(uint64)</param>
		public static string DeclineTradeOffer(ulong tradeofferid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "tradeofferid", tradeofferid },
			};
			
			return Utility.WebPost<string>( $"{Url}/DeclineTradeOffer/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)</param>
		public static string FlushAssetAppearanceCache(uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/FlushAssetAppearanceCache/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)User to clear cache for.</param>
		/// <param name="appid">(uint32)App to clear cache for.</param>
		/// <param name="contextid">(uint64)Context to clear cache for.</param>
		public static string FlushInventoryCache(ulong steamid, uint appid, ulong contextid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "contextid", contextid },
			};
			
			return Utility.WebPost<string>( $"{Url}/FlushInventoryCache/v0001/", headers );
		}
		
		/// <param name="max_trades">(uint32)The number of trades to return information for</param>
		/// <param name="start_after_time">(uint32)The time of the last trade shown on the previous page of results, or the time of the first trade if navigating back</param>
		/// <param name="start_after_tradeid">(uint64)The tradeid shown on the previous page of results, or the ID of the first trade if navigating back</param>
		/// <param name="navigating_back">(bool)The user wants the previous page of results, so return the previous max_trades trades before the start time and ID</param>
		/// <param name="get_descriptions">(bool)If set, the item display data for the items included in the returned trades will also be returned</param>
		/// <param name="language">(string)The language to use when loading item display data</param>
		/// <param name="include_failed">(bool)</param>
		/// <param name="include_total">(bool)If set, the total number of trades the account has participated in will be included in the response</param>
		public static string GetTradeHistory(uint max_trades, uint start_after_time, ulong start_after_tradeid, bool navigating_back, bool get_descriptions, string language, bool include_failed, bool include_total)
		{
			return Utility.WebGet<string>( $"{Url}GetTradeHistory/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&max_trades={max_trades}&start_after_time={start_after_time}&start_after_tradeid={start_after_tradeid}&navigating_back={navigating_back}&get_descriptions={get_descriptions}&language={language}&include_failed={include_failed}&include_total={include_total}" );
		}
		
		/// <param name="tradeofferid">(uint64)</param>
		/// <param name="language">(string)</param>
		public static string GetTradeOffer(ulong tradeofferid, string language)
		{
			return Utility.WebGet<string>( $"{Url}GetTradeOffer/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&tradeofferid={tradeofferid}&language={language}" );
		}
		
		/// <param name="get_sent_offers">(bool)Request the list of sent offers.</param>
		/// <param name="get_received_offers">(bool)Request the list of received offers.</param>
		/// <param name="get_descriptions">(bool)If set, the item display data for the items included in the returned trade offers will also be returned.</param>
		/// <param name="language">(string)The language to use when loading item display data.</param>
		/// <param name="active_only">(bool)Indicates we should only return offers which are still active, or offers that have changed in state since the time_historical_cutoff</param>
		/// <param name="historical_only">(bool)Indicates we should only return offers which are not active.</param>
		/// <param name="time_historical_cutoff">(uint32)When active_only is set, offers updated since this time will also be returned</param>
		public static string GetTradeOffers(bool get_sent_offers, bool get_received_offers, bool get_descriptions, string language, bool active_only, bool historical_only, uint time_historical_cutoff)
		{
			return Utility.WebGet<string>( $"{Url}GetTradeOffers/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&get_sent_offers={get_sent_offers}&get_received_offers={get_received_offers}&get_descriptions={get_descriptions}&language={language}&active_only={active_only}&historical_only={historical_only}&time_historical_cutoff={time_historical_cutoff}" );
		}
		
		/// <param name="time_last_visit">(uint32)The time the user last visited.  If not passed, will use the time the user last visited the trade offer page.</param>
		public static string GetTradeOffersSummary(uint time_last_visit)
		{
			return Utility.WebGet<string>( $"{Url}GetTradeOffersSummary/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&time_last_visit={time_last_visit}" );
		}
		
	}
	
	/// <summary>
	/// IPlayerService
	/// </summary>
	public partial class IPlayerService
	{
		private const string Url = "http://api.steampowered.com/IPlayerService/";
		
		/// <param name="steamid">(uint64)The player we're asking about</param>
		public static string GetBadges(ulong steamid)
		{
			return Utility.WebGet<string>( $"{Url}GetBadges/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}" );
		}
		
		/// <param name="steamid">(uint64)The player we're asking about</param>
		/// <param name="badgeid">(int32)The badge we're asking about</param>
		public static string GetCommunityBadgeProgress(ulong steamid, int badgeid)
		{
			return Utility.WebGet<string>( $"{Url}GetCommunityBadgeProgress/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&badgeid={badgeid}" );
		}
		
		/// <param name="steamid">(uint64)The player we're asking about</param>
		/// <param name="include_appinfo">(bool)true if we want additional details (name, icon) about each game</param>
		/// <param name="include_played_free_games">(bool)Free games are excluded by default.  If this is set, free games the user has played will be returned.</param>
		/// <param name="appids_filter">(uint32)if set, restricts result set to the passed in apps</param>
		public static string GetOwnedGames(ulong steamid, bool include_appinfo, bool include_played_free_games, uint appids_filter)
		{
			return Utility.WebGet<string>( $"{Url}GetOwnedGames/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&include_appinfo={include_appinfo}&include_played_free_games={include_played_free_games}&appids_filter={appids_filter}" );
		}
		
		/// <param name="steamid">(uint64)The player we're asking about</param>
		/// <param name="count">(uint32)The number of games to return (0/unset: all)</param>
		public static string GetRecentlyPlayedGames(ulong steamid, uint count)
		{
			return Utility.WebGet<string>( $"{Url}GetRecentlyPlayedGames/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&count={count}" );
		}
		
		/// <param name="steamid">(uint64)The player we're asking about</param>
		public static string GetSteamLevel(ulong steamid)
		{
			return Utility.WebGet<string>( $"{Url}GetSteamLevel/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}" );
		}
		
		/// <param name="steamid">(uint64)The player we're asking about</param>
		/// <param name="appid_playing">(uint32)The game player is currently playing</param>
		public static string IsPlayingSharedGame(ulong steamid, uint appid_playing)
		{
			return Utility.WebGet<string>( $"{Url}IsPlayingSharedGame/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid_playing={appid_playing}" );
		}
		
		/// <param name="steamid">(uint64)</param>
		/// <param name="ticket">(string)</param>
		/// <param name="play_sessions">({message})</param>
		public static string RecordOfflinePlaytime(ulong steamid, string ticket, string play_sessions)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "ticket", ticket },
				{ "play_sessions", play_sessions },
			};
			
			return Utility.WebPost<string>( $"{Url}/RecordOfflinePlaytime/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// IGameNotificationsService
	/// </summary>
	public partial class IGameNotificationsService
	{
		private const string Url = "http://api.steampowered.com/IGameNotificationsService/";
		
		/// <param name="appid">(uint32)The appid to create the session for.</param>
		/// <param name="context">(uint64)Game-specified context value the game can used to associate the session with some object on their backend.</param>
		/// <param name="title">({message})The title of the session to be displayed within each user's list of sessions.</param>
		/// <param name="users">({message})The initial state of all users in the session.</param>
		/// <param name="steamid">(uint64)(Optional) steamid to make the request on behalf of -- if specified, the user must be in the session and all users being added to the session must be friends with the user.</param>
		public static string CreateSession(uint appid, ulong context, string title, string users, ulong steamid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "context", context },
				{ "title", title },
				{ "users", users },
				{ "steamid", steamid },
			};
			
			return Utility.WebPost<string>( $"{Url}/CreateSession/v0001/", headers );
		}
		
		/// <param name="sessionid">(uint64)The sessionid to delete.</param>
		/// <param name="appid">(uint32)The appid of the session to delete.</param>
		/// <param name="steamid">(uint64)(Optional) steamid to make the request on behalf of -- if specified, the user must be in the session.</param>
		public static string DeleteSession(ulong sessionid, uint appid, ulong steamid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "sessionid", sessionid },
				{ "appid", appid },
				{ "steamid", steamid },
			};
			
			return Utility.WebPost<string>( $"{Url}/DeleteSession/v0001/", headers );
		}
		
		/// <param name="sessionid">(uint64)The sessionid to delete.</param>
		/// <param name="appid">(uint32)The appid of the session to delete.</param>
		public static string DeleteSessionBatch(ulong sessionid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "sessionid", sessionid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/DeleteSessionBatch/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)The sessionid to request details for. Optional. If not specified, all the user's sessions will be returned.</param>
		/// <param name="steamid">(uint64)The user whose sessions are to be enumerated.</param>
		/// <param name="include_all_user_messages">(bool)(Optional) Boolean determining whether the message for all users should be included. Defaults to false.</param>
		/// <param name="include_auth_user_message">(bool)(Optional) Boolean determining whether the message for the authenticated user should be included. Defaults to false.</param>
		/// <param name="language">(string)(Optional) Language to localize the text in.</param>
		public static string EnumerateSessionsForApp(uint appid, ulong steamid, bool include_all_user_messages, bool include_auth_user_message, string language)
		{
			return Utility.WebGet<string>( $"{Url}EnumerateSessionsForApp/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&steamid={steamid}&include_all_user_messages={include_all_user_messages}&include_auth_user_message={include_auth_user_message}&language={language}" );
		}
		
		/// <param name="sessions">({message})</param>
		/// <param name="appid">(uint32)The appid for the sessions.</param>
		/// <param name="language">(string)Language to localize the text in.</param>
		public static string GetSessionDetailsForApp(string sessions, uint appid, string language)
		{
			return Utility.WebGet<string>( $"{Url}GetSessionDetailsForApp/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&sessions={sessions}&appid={appid}&language={language}" );
		}
		
		/// <param name="steamid">(uint64)The steamid to request notifications for.</param>
		/// <param name="appid">(uint32)The appid to request notifications for.</param>
		public static string RequestNotifications(ulong steamid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/RequestNotifications/v0001/", headers );
		}
		
		/// <param name="sessionid">(uint64)The sessionid to update.</param>
		/// <param name="appid">(uint32)The appid of the session to update.</param>
		/// <param name="title">({message})(Optional) The new title of the session.  If not specified, the title will not be changed.</param>
		/// <param name="users">({message})(Optional) A list of users whose state will be updated to reflect the given state. If the users are not already in the session, they will be added to it.</param>
		/// <param name="steamid">(uint64)(Optional) steamid to make the request on behalf of -- if specified, the user must be in the session and all users being added to the session must be friends with the user.</param>
		public static string UpdateSession(ulong sessionid, uint appid, string title, string users, ulong steamid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "sessionid", sessionid },
				{ "appid", appid },
				{ "title", title },
				{ "users", users },
				{ "steamid", steamid },
			};
			
			return Utility.WebPost<string>( $"{Url}/UpdateSession/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// IInventoryService
	/// </summary>
	public partial class IInventoryService
	{
		private const string Url = "http://api.steampowered.com/IInventoryService/";
		
		/// <param name="appid">(uint32)</param>
		/// <param name="itemdefid">(uint64)</param>
		/// <param name="itempropsjson">(string)</param>
		/// <param name="steamid">(uint64)</param>
		/// <param name="notify">(bool)Should notify the user that the item was added to their Steam Inventory.</param>
		public static string AddItem(uint appid, ulong itemdefid, string itempropsjson, ulong steamid, bool notify)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "itemdefid", itemdefid },
				{ "itempropsjson", itempropsjson },
				{ "steamid", steamid },
				{ "notify", notify },
			};
			
			return Utility.WebPost<string>( $"{Url}/AddItem/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)</param>
		/// <param name="itemdefid">(uint64)</param>
		/// <param name="itempropsjson">(string)</param>
		/// <param name="steamid">(uint64)</param>
		/// <param name="notify">(bool)Should notify the user that the item was added to their Steam Inventory.</param>
		public static string AddPromoItem(uint appid, ulong itemdefid, string itempropsjson, ulong steamid, bool notify)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "itemdefid", itemdefid },
				{ "itempropsjson", itempropsjson },
				{ "steamid", steamid },
				{ "notify", notify },
			};
			
			return Utility.WebPost<string>( $"{Url}/AddPromoItem/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)</param>
		/// <param name="steamid">(uint64)</param>
		/// <param name="materialsitemid">(uint64)</param>
		/// <param name="materialsquantity">(uint32)</param>
		/// <param name="outputitemdefid">(uint64)</param>
		public static string ExchangeItem(uint appid, ulong steamid, ulong materialsitemid, uint materialsquantity, ulong outputitemdefid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "steamid", steamid },
				{ "materialsitemid", materialsitemid },
				{ "materialsquantity", materialsquantity },
				{ "outputitemdefid", outputitemdefid },
			};
			
			return Utility.WebPost<string>( $"{Url}/ExchangeItem/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)</param>
		/// <param name="steamid">(uint64)</param>
		public static string GetInventory(uint appid, ulong steamid)
		{
			return Utility.WebGet<string>( $"{Url}GetInventory/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&steamid={steamid}" );
		}
		
		/// <param name="appid">(uint32)</param>
		/// <param name="modifiedsince">(string)</param>
		/// <param name="itemdefids">(uint64)</param>
		/// <param name="workshopids">(uint64)</param>
		/// <param name="cache_max_age_seconds">(uint32)Allow stale data to be returned for the specified number of seconds.</param>
		public static string GetItemDefs(uint appid, string modifiedsince, ulong itemdefids, ulong workshopids, uint cache_max_age_seconds)
		{
			return Utility.WebGet<string>( $"{Url}GetItemDefs/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&modifiedsince={modifiedsince}&itemdefids={itemdefids}&workshopids={workshopids}&cache_max_age_seconds={cache_max_age_seconds}" );
		}
		
		/// <param name="ecurrency">(int32)</param>
		public static string GetPriceSheet(int ecurrency)
		{
			return Utility.WebGet<string>( $"{Url}GetPriceSheet/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&ecurrency={ecurrency}" );
		}
		
	}
	
	/// <summary>
	/// IEconMarketService
	/// </summary>
	public partial class IEconMarketService
	{
		private const string Url = "http://api.steampowered.com/IEconMarketService/";
		
		/// <param name="appid">(uint32)The app making the request</param>
		/// <param name="steamid">(uint64)The SteamID of the user whose listings should be canceled</param>
		/// <param name="synchronous">(bool)Whether or not to wait until all listings have been canceled before returning the response</param>
		public static string CancelAppListingsForUser(uint appid, ulong steamid, bool synchronous)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "steamid", steamid },
				{ "synchronous", synchronous },
			};
			
			return Utility.WebPost<string>( $"{Url}/CancelAppListingsForUser/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)The app that's asking. Must match the app of the listing and must belong to the publisher group that owns the API key making the request</param>
		/// <param name="listingid">(uint64)The identifier of the listing to get information for</param>
		public static string GetAssetID(uint appid, ulong listingid)
		{
			return Utility.WebGet<string>( $"{Url}GetAssetID/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&listingid={listingid}" );
		}
		
		/// <param name="steamid">(uint64)The SteamID of the user to check</param>
		public static string GetMarketEligibility(ulong steamid)
		{
			return Utility.WebGet<string>( $"{Url}GetMarketEligibility/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}" );
		}
		
		/// <param name="language">(string)The language to use in item descriptions</param>
		/// <param name="rows">(uint32)Number of rows per page</param>
		/// <param name="start">(uint32)The result number to start at</param>
		/// <param name="filter_appid">(uint32)If present, the app ID to limit results to</param>
		/// <param name="ecurrency">(uint32)If present, prices returned will be represented in this currency</param>
		public static string GetPopular(string language, uint rows, uint start, uint filter_appid, uint ecurrency)
		{
			return Utility.WebGet<string>( $"{Url}GetPopular/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&language={language}&rows={rows}&start={start}&filter_appid={filter_appid}&ecurrency={ecurrency}" );
		}
		
	}
	
	/// <summary>
	/// ITestExternalPrivilegeService
	/// </summary>
	public partial class ITestExternalPrivilegeService
	{
		private const string Url = "http://api.steampowered.com/ITestExternalPrivilegeService/";
		
		public static string CallPublisherKey()
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
			};
			
			return Utility.WebPost<string>( $"{Url}/CallPublisherKey/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)</param>
		public static string CallPublisherKeyOwnsApp(uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/CallPublisherKeyOwnsApp/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// ICheatReportingService
	/// </summary>
	public partial class ICheatReportingService
	{
		private const string Url = "http://api.steampowered.com/ICheatReportingService/";
		
		/// <param name="steamid">(uint64)steamid of the user.</param>
		/// <param name="appid">(uint32)The appid the user is playing.</param>
		/// <param name="session_id">(uint64)session id</param>
		public static string EndSecureMultiplayerSession(ulong steamid, uint appid, ulong session_id)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "session_id", session_id },
			};
			
			return Utility.WebPost<string>( $"{Url}/EndSecureMultiplayerSession/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)The appid.</param>
		/// <param name="timeend">(uint32)The beginning of the time range .</param>
		/// <param name="timebegin">(uint32)The end of the time range.</param>
		/// <param name="reportidmin">(uint64)Minimum reportID to include</param>
		/// <param name="includereports">(bool)(Optional) Include reports.</param>
		/// <param name="includebans">(bool)(Optional) Include ban requests.</param>
		/// <param name="steamid">(uint64)(Optional) Query just for this steamid.</param>
		public static string GetCheatingReports(uint appid, uint timeend, uint timebegin, ulong reportidmin, bool includereports, bool includebans, ulong steamid)
		{
			return Utility.WebGet<string>( $"{Url}GetCheatingReports/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&timeend={timeend}&timebegin={timebegin}&reportidmin={reportidmin}&includereports={includereports}&includebans={includebans}&steamid={steamid}" );
		}
		
		/// <param name="steamid">(uint64)steamid of the user who is reported as cheating.</param>
		/// <param name="appid">(uint32)The appid.</param>
		public static string RemovePlayerGameBan(ulong steamid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/RemovePlayerGameBan/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)steamid of the user running and reporting the cheat.</param>
		/// <param name="appid">(uint32)The appid.</param>
		/// <param name="pathandfilename">(string)path and file name of the cheat executable.</param>
		/// <param name="webcheaturl">(string)web url where the cheat was found and downloaded.</param>
		/// <param name="time_now">(uint64)local system time now.</param>
		/// <param name="time_started">(uint64)local system time when cheat process started. ( 0 if not yet run )</param>
		/// <param name="time_stopped">(uint64)local system time when cheat process stopped. ( 0 if still running )</param>
		/// <param name="cheatname">(string)descriptive name for the cheat.</param>
		/// <param name="game_process_id">(uint32)process ID of the running game.</param>
		/// <param name="cheat_process_id">(uint32)process ID of the cheat process that ran</param>
		/// <param name="cheat_param_1">(uint64)cheat param 1</param>
		/// <param name="cheat_param_2">(uint64)cheat param 2</param>
		public static string ReportCheatData(ulong steamid, uint appid, string pathandfilename, string webcheaturl, ulong time_now, ulong time_started, ulong time_stopped, string cheatname, uint game_process_id, uint cheat_process_id, ulong cheat_param_1, ulong cheat_param_2)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "pathandfilename", pathandfilename },
				{ "webcheaturl", webcheaturl },
				{ "time_now", time_now },
				{ "time_started", time_started },
				{ "time_stopped", time_stopped },
				{ "cheatname", cheatname },
				{ "game_process_id", game_process_id },
				{ "cheat_process_id", cheat_process_id },
				{ "cheat_param_1", cheat_param_1 },
				{ "cheat_param_2", cheat_param_2 },
			};
			
			return Utility.WebPost<string>( $"{Url}/ReportCheatData/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)steamid of the user who is reported as cheating.</param>
		/// <param name="appid">(uint32)The appid.</param>
		/// <param name="steamidreporter">(uint64)(Optional) steamid of the user or game server who is reporting the cheating.</param>
		/// <param name="appdata">(uint64)(Optional) App specific data about the cheating.</param>
		/// <param name="heuristic">(bool)(Optional) extra information about the source of the cheating - was it a heuristic.</param>
		/// <param name="detection">(bool)(Optional) extra information about the source of the cheating - was it a detection.</param>
		/// <param name="playerreport">(bool)(Optional) extra information about the source of the cheating - was it a player report.</param>
		/// <param name="noreportid">(bool)(Optional) dont return report id</param>
		/// <param name="gamemode">(uint32)(Optional) extra information about state of game - was it a specific type of game play (0 = generic)</param>
		/// <param name="suspicionstarttime">(uint32)(Optional) extra information indicating how far back the game thinks is interesting for this user</param>
		/// <param name="severity">(uint32)(Optional) level of severity of bad action being reported</param>
		public static string ReportPlayerCheating(ulong steamid, uint appid, ulong steamidreporter, ulong appdata, bool heuristic, bool detection, bool playerreport, bool noreportid, uint gamemode, uint suspicionstarttime, uint severity)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "steamidreporter", steamidreporter },
				{ "appdata", appdata },
				{ "heuristic", heuristic },
				{ "detection", detection },
				{ "playerreport", playerreport },
				{ "noreportid", noreportid },
				{ "gamemode", gamemode },
				{ "suspicionstarttime", suspicionstarttime },
				{ "severity", severity },
			};
			
			return Utility.WebPost<string>( $"{Url}/ReportPlayerCheating/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)steamid of the user who is reported as cheating.</param>
		/// <param name="appid">(uint32)The appid.</param>
		/// <param name="reportid">(uint64)The reportid originally used to report cheating.</param>
		/// <param name="cheatdescription">(string)Text describing cheating infraction.</param>
		/// <param name="duration">(uint32)Ban duration requested in seconds.</param>
		/// <param name="delayban">(bool)Delay the ban according to default ban delay rules.</param>
		/// <param name="flags">(uint32)Additional information about the ban request.</param>
		public static string RequestPlayerGameBan(ulong steamid, uint appid, ulong reportid, string cheatdescription, uint duration, bool delayban, uint flags)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "reportid", reportid },
				{ "cheatdescription", cheatdescription },
				{ "duration", duration },
				{ "delayban", delayban },
				{ "flags", flags },
			};
			
			return Utility.WebPost<string>( $"{Url}/RequestPlayerGameBan/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)steamid of the user.</param>
		/// <param name="appid">(uint32)The appid the user is playing.</param>
		/// <param name="session_id">(uint64)session id</param>
		public static string RequestVacStatusForUser(ulong steamid, uint appid, ulong session_id)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
				{ "session_id", session_id },
			};
			
			return Utility.WebPost<string>( $"{Url}/RequestVacStatusForUser/v0001/", headers );
		}
		
		/// <param name="steamid">(uint64)steamid of the user.</param>
		/// <param name="appid">(uint32)The appid the user is playing.</param>
		public static string StartSecureMultiplayerSession(ulong steamid, uint appid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "steamid", steamid },
				{ "appid", appid },
			};
			
			return Utility.WebPost<string>( $"{Url}/StartSecureMultiplayerSession/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// IAccountRecoveryService
	/// </summary>
	public partial class IAccountRecoveryService
	{
		private const string Url = "http://api.steampowered.com/IAccountRecoveryService/";
		
		/// <param name="loginuser_list">(string)</param>
		/// <param name="install_config">(string)</param>
		/// <param name="shasentryfile">(string)</param>
		/// <param name="machineid">(string)</param>
		public static string ReportAccountRecoveryData(string loginuser_list, string install_config, string shasentryfile, string machineid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "loginuser_list", loginuser_list },
				{ "install_config", install_config },
				{ "shasentryfile", shasentryfile },
				{ "machineid", machineid },
			};
			
			return Utility.WebPost<string>( $"{Url}/ReportAccountRecoveryData/v0001/", headers );
		}
		
		/// <param name="requesthandle">(string)</param>
		public static string RetrieveAccountRecoveryData(string requesthandle)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "requesthandle", requesthandle },
			};
			
			return Utility.WebPost<string>( $"{Url}/RetrieveAccountRecoveryData/v0001/", headers );
		}
		
	}
	
	/// <summary>
	/// IWorkshopService
	/// </summary>
	public partial class IWorkshopService
	{
		private const string Url = "http://api.steampowered.com/IWorkshopService/";
		
		/// <param name="appid">(uint32)</param>
		/// <param name="gameitemid">(uint32)</param>
		public static string GetFinalizedContributors(uint appid, uint gameitemid)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "gameitemid", gameitemid },
			};
			
			return Utility.WebPost<string>( $"{Url}/GetFinalizedContributors/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)</param>
		/// <param name="item_id">(uint32)</param>
		/// <param name="date_start_dt">(uint32)</param>
		/// <param name="date_end_dt">(uint32)</param>
		public static GetItemDailyRevenueResponse GetItemDailyRevenue(uint appid, uint item_id, DateTime date_start_dt, DateTime date_end_dt)
		{
			uint date_start = Facepunch.Steamworks.Utility.Epoch.FromDateTime( date_start_dt );
			uint date_end = Facepunch.Steamworks.Utility.Epoch.FromDateTime( date_end_dt );
			
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "item_id", item_id },
				{ "date_start", date_start },
				{ "date_end", date_end },
			};
			
			return Utility.WebPost<GetItemDailyRevenueResponse>( $"{Url}/GetItemDailyRevenue/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)</param>
		/// <param name="languages">({message})</param>
		public static string PopulateItemDescriptions(uint appid, string languages)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "languages", languages },
			};
			
			return Utility.WebPost<string>( $"{Url}/PopulateItemDescriptions/v0001/", headers );
		}
		
		/// <param name="appid">(uint32)</param>
		/// <param name="gameitemid">(uint32)</param>
		/// <param name="associated_workshop_files">({message})</param>
		/// <param name="partner_accounts">({message})</param>
		/// <param name="validate_only">(bool)Only validates the rules and does not persist them.</param>
		/// <param name="make_workshop_files_subscribable">(bool)</param>
		public static string SetItemPaymentRules(uint appid, uint gameitemid, string associated_workshop_files, string partner_accounts, bool validate_only, bool make_workshop_files_subscribable)
		{
			var headers = new System.Collections.Generic.Dictionary<string, object>()
			{
				{ "appid", appid },
				{ "gameitemid", gameitemid },
				{ "associated_workshop_files", associated_workshop_files },
				{ "partner_accounts", partner_accounts },
				{ "validate_only", validate_only },
				{ "make_workshop_files_subscribable", make_workshop_files_subscribable },
			};
			
			return Utility.WebPost<string>( $"{Url}/SetItemPaymentRules/v0001/", headers );
		}
		
	}
	
}
