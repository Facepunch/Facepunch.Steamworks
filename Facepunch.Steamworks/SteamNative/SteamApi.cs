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
		
		/// <param name="appid">appid of game</param>
		/// <param name="steamid">The steam ID of the account to operate on</param>
		/// <param name="command">The command to run on that asset</param>
		/// <param name="contextid">The context to fetch history for</param>
		/// <param name="arguments">The arguments that were provided with the command in the first place</param>
		public static string GetHistoryCommandDetails(uint appid, ulong steamid, string command, ulong contextid, string arguments)
		{
			string url = $"{Url}GetHistoryCommandDetails/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&steamid={steamid}&command={command}&contextid={contextid}&arguments={arguments}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">appid of game</param>
		/// <param name="steamid">The Steam ID to fetch history for</param>
		/// <param name="contextid">The context to fetch history for</param>
		/// <param name="starttime">Start time of the history range to collect</param>
		/// <param name="endtime">End time of the history range to collect</param>
		public static string GetUserHistory(uint appid, ulong steamid, ulong contextid, uint starttime, uint endtime)
		{
			string url = $"{Url}GetUserHistory/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&steamid={steamid}&contextid={contextid}&starttime={starttime}&endtime={endtime}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">appid of game</param>
		/// <param name="steamid">The asset ID to operate on</param>
		/// <param name="contextid">The context to fetch history for</param>
		/// <param name="actorid">A unique 32 bit ID for the support person executing the command</param>
		public static string HistoryExecuteCommands(uint appid, ulong steamid, ulong contextid, uint actorid)
		{
			string url = $"{Url}/HistoryExecuteCommands/v0001/";
			return url;
		}
		
		/// <param name="appid">appid of game</param>
		/// <param name="assetid">The asset ID to operate on</param>
		/// <param name="contextid">The context to fetch history for</param>
		public static string SupportGetAssetHistory(uint appid, ulong assetid, ulong contextid)
		{
			string url = $"{Url}SupportGetAssetHistory/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&assetid={assetid}&contextid={contextid}";
			return Utility.WebGet<string>( url );
		}
		
	}
	
	/// <summary>
	/// ISteamApps
	/// </summary>
	public partial class ISteamApps
	{
		private const string Url = "http://api.steampowered.com/ISteamApps/";
		
		/// <param name="appid">AppID of game</param>
		public static GetAppBetasResponse GetAppBetas(uint appid)
		{
			string url = $"{Url}GetAppBetas/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}";
			return Utility.WebGet<GetAppBetasResponse>( url );
		}
		
		/// <param name="appid">AppID of game</param>
		/// <param name="count"># of builds to retrieve (default 10)</param>
		public static GetAppBuildsResponse GetAppBuilds(uint appid, uint count)
		{
			string url = $"{Url}GetAppBuilds/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&count={count}";
			return Utility.WebGet<GetAppBuildsResponse>( url );
		}
		
		/// <param name="appid">AppID of depot</param>
		public static string GetAppDepotVersions(uint appid)
		{
			string url = $"{Url}GetAppDepotVersions/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}";
			return Utility.WebGet<string>( url );
		}
		
		public static string GetAppList()
		{
			string url = $"{Url}GetAppList/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">AppID of game</param>
		/// <param name="timebegin">Time range begin</param>
		/// <param name="timeend">Time range end</param>
		/// <param name="includereports">include reports that were not bans</param>
		/// <param name="includebans">include reports that were bans</param>
		/// <param name="reportidmin">minimum report id</param>
		public static string GetCheatingReports(uint appid, uint timebegin, uint timeend, bool includereports, bool includebans, ulong reportidmin)
		{
			string url = $"{Url}GetCheatingReports/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&timebegin={timebegin}&timeend={timeend}&includereports={includereports}&includebans={includebans}&reportidmin={reportidmin}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">AppID of game</param>
		public static string GetPlayersBanned(uint appid)
		{
			string url = $"{Url}GetPlayersBanned/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="filter">Query filter string</param>
		/// <param name="limit">Limit number of servers in the response</param>
		public static string GetServerList(string filter, uint limit)
		{
			string url = $"{Url}GetServerList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&filter={filter}&limit={limit}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="addr">IP or IP:queryport to list</param>
		public static string GetServersAtAddress(string addr)
		{
			string url = $"{Url}GetServersAtAddress/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&addr={addr}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">AppID of game</param>
		/// <param name="buildid">BuildID</param>
		/// <param name="betakey">beta key, required. Use public for default branch</param>
		/// <param name="description">optional description for this build</param>
		public static string SetAppBuildLive(uint appid, uint buildid, string betakey, string description)
		{
			string url = $"{Url}/SetAppBuildLive/v0001/";
			return url;
		}
		
		/// <param name="appid">AppID of game</param>
		/// <param name="version">The installed version of the game</param>
		public static string UpToDateCheck(uint appid, uint version)
		{
			string url = $"{Url}UpToDateCheck/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&version={version}";
			return Utility.WebGet<string>( url );
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
			string url = $"{Url}/BitPayPaymentNotification/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamCDN
	/// </summary>
	public partial class ISteamCDN
	{
		private const string Url = "http://api.steampowered.com/ISteamCDN/";
		
		/// <param name="cdnname">Steam name of CDN property</param>
		/// <param name="allowedipblocks">comma-separated list of allowed IP address blocks in CIDR format - blank for not used</param>
		/// <param name="allowedasns">comma-separated list of allowed client network AS numbers - blank for not used</param>
		/// <param name="allowedipcountries">comma-separated list of allowed client IP country codes in ISO 3166-1 format - blank for not used</param>
		public static string SetClientFilters(string cdnname, string allowedipblocks, string allowedasns, string allowedipcountries)
		{
			string url = $"{Url}/SetClientFilters/v0001/";
			return url;
		}
		
		/// <param name="cdnname">Steam name of CDN property</param>
		/// <param name="mbps_sent">Outgoing network traffic in Mbps</param>
		/// <param name="mbps_recv">Incoming network traffic in Mbps</param>
		/// <param name="cpu_percent">Percent CPU load</param>
		/// <param name="cache_hit_percent">Percent cache hits</param>
		public static string SetPerformanceStats(string cdnname, uint mbps_sent, uint mbps_recv, uint cpu_percent, uint cache_hit_percent)
		{
			string url = $"{Url}/SetPerformanceStats/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamCommunity
	/// </summary>
	public partial class ISteamCommunity
	{
		private const string Url = "http://api.steampowered.com/ISteamCommunity/";
		
		/// <param name="steamidActor">SteamID of user doing the reporting</param>
		/// <param name="steamidTarget">SteamID of the entity being accused of abuse</param>
		/// <param name="appid">AppID to check for ownership</param>
		/// <param name="abuseType">Abuse type code (see EAbuseReportType enum)</param>
		/// <param name="contentType">Content type code (see ECommunityContentType enum)</param>
		/// <param name="description">Narrative from user</param>
		/// <param name="gid">GID of related record (depends on content type)</param>
		public static string ReportAbuse(ulong steamidActor, ulong steamidTarget, uint appid, uint abuseType, uint contentType, string description, ulong gid)
		{
			string url = $"{Url}/ReportAbuse/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamDirectory
	/// </summary>
	public partial class ISteamDirectory
	{
		private const string Url = "http://api.steampowered.com/ISteamDirectory/";
		
		/// <param name="cellid">Client's Steam cell ID</param>
		/// <param name="maxcount">Max number of servers to return</param>
		public static string GetCMList(uint cellid, uint maxcount)
		{
			string url = $"{Url}GetCMList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&cellid={cellid}&maxcount={maxcount}";
			return Utility.WebGet<string>( url );
		}
		
	}
	
	/// <summary>
	/// ISteamEconomy
	/// </summary>
	public partial class ISteamEconomy
	{
		private const string Url = "http://api.steampowered.com/ISteamEconomy/";
		
		/// <param name="appid">That the key is associated with. Must be a steam economy app.</param>
		/// <param name="steamid">SteamID of user attempting to initiate a trade</param>
		/// <param name="targetid">SteamID of user that is the target of the trade invitation</param>
		public static string CanTrade(uint appid, ulong steamid, ulong targetid)
		{
			string url = $"{Url}CanTrade/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&steamid={steamid}&targetid={targetid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">The app ID the user is buying assets for</param>
		/// <param name="steamid">SteamID of the user making a purchase</param>
		/// <param name="txnid">The transaction ID</param>
		/// <param name="language">The local language for the user</param>
		public static string FinalizeAssetTransaction(uint appid, ulong steamid, string txnid, string language)
		{
			string url = $"{Url}/FinalizeAssetTransaction/v0001/";
			return url;
		}
		
		/// <param name="appid">Must be a steam economy app.</param>
		/// <param name="language">The user's local language</param>
		/// <param name="class_count">Number of classes requested. Must be at least one.</param>
		/// <param name="classid0">Class ID of the nth class.</param>
		/// <param name="instanceid0">Instance ID of the nth class.</param>
		public static string GetAssetClassInfo(uint appid, string language, uint class_count, ulong classid0, ulong instanceid0)
		{
			string url = $"{Url}GetAssetClassInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&language={language}&class_count={class_count}&classid0={classid0}&instanceid0={instanceid0}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">Must be a steam economy app.</param>
		/// <param name="currency">The currency to filter for</param>
		/// <param name="language">The user's local language</param>
		public static string GetAssetPrices(uint appid, string currency, string language)
		{
			string url = $"{Url}GetAssetPrices/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&currency={currency}&language={language}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">The app to get exported items from.</param>
		/// <param name="contextid">The context in the app to get exported items from.</param>
		public static string GetExportedAssetsForUser(ulong steamid, uint appid, ulong contextid)
		{
			string url = $"{Url}GetExportedAssetsForUser/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}&contextid={contextid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">Must be a steam economy app.</param>
		public static string GetMarketPrices(uint appid)
		{
			string url = $"{Url}GetMarketPrices/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">The app ID the user is buying assets for</param>
		/// <param name="steamid">SteamID of user making a purchase</param>
		/// <param name="assetid0">The ID of the first asset the user is buying - there must be at least one</param>
		/// <param name="assetquantity0">The quantity of assetid0's the the user is buying</param>
		/// <param name="currency">The local currency for the user</param>
		/// <param name="language">The local language for the user</param>
		/// <param name="ipaddress">The user's IP address</param>
		/// <param name="referrer">The referring URL</param>
		/// <param name="clientauth">If true (default is false), the authorization will appear in the user's steam client overlay, rather than as a web page - useful for stores that are embedded in products.</param>
		public static string StartAssetTransaction(uint appid, ulong steamid, string assetid0, uint assetquantity0, string currency, string language, string ipaddress, string referrer, bool clientauth)
		{
			string url = $"{Url}/StartAssetTransaction/v0001/";
			return url;
		}
		
		/// <param name="appid">That the key is associated with. Must be a steam economy app.</param>
		/// <param name="partya">SteamID of first user in the trade</param>
		/// <param name="partyb">SteamID of second user in the trade</param>
		public static string StartTrade(uint appid, ulong partya, ulong partyb)
		{
			string url = $"{Url}StartTrade/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&partya={partya}&partyb={partyb}";
			return Utility.WebGet<string>( url );
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
			string url = $"{Url}/PaymentOutNotification/v0001/";
			return url;
		}
		
		public static string PaymentOutReversalNotification()
		{
			string url = $"{Url}/PaymentOutReversalNotification/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamGameServerStats
	/// </summary>
	public partial class ISteamGameServerStats
	{
		private const string Url = "http://api.steampowered.com/ISteamGameServerStats/";
		
		/// <param name="gameid">game id to get stats for, if not a mod, it's safe to use appid here</param>
		/// <param name="appid">appID of the game</param>
		/// <param name="rangestart">range start date/time (Format: YYYY-MM-DD HH:MM:SS, seattle local time</param>
		/// <param name="rangeend">range end date/time (Format: YYYY-MM-DD HH:MM:SS, seattle local time</param>
		/// <param name="maxresults">Max number of results to return (up to 1000)</param>
		public static string GetGameServerPlayerStatsForGame(ulong gameid, uint appid, string rangestart, string rangeend, uint maxresults)
		{
			string url = $"{Url}GetGameServerPlayerStatsForGame/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&gameid={gameid}&appid={appid}&rangestart={rangestart}&rangeend={rangeend}&maxresults={maxresults}";
			return Utility.WebGet<string>( url );
		}
		
	}
	
	/// <summary>
	/// ISteamLeaderboards
	/// </summary>
	public partial class ISteamLeaderboards
	{
		private const string Url = "http://api.steampowered.com/ISteamLeaderboards/";
		
		/// <param name="appid">appid of game</param>
		/// <param name="name">name of the leaderboard to delete</param>
		public static string DeleteLeaderboard(uint appid, string name)
		{
			string url = $"{Url}/DeleteLeaderboard/v0001/";
			return url;
		}
		
		/// <param name="appid">appid of game</param>
		/// <param name="name">name of the leaderboard to create</param>
		/// <param name="sortmethod">sort method to use for this leaderboard (defaults to Ascending)</param>
		/// <param name="displaytype">display type for this leaderboard (defaults to Numeric)</param>
		/// <param name="createifnotfound">if this is true the leaderboard will be created if it doesn't exist. Defaults to true.</param>
		/// <param name="onlytrustedwrites">if this is true the leaderboard scores cannot be set by clients, and can only be set by publisher via SetLeaderboardScore WebAPI. Defaults to false.</param>
		/// <param name="onlyfriendsreads">if this is true the leaderboard scores can only be read for friends by clients, scores can always be read by publisher. Defaults to false.</param>
		public static string FindOrCreateLeaderboard(uint appid, string name, string sortmethod, string displaytype, bool createifnotfound, bool onlytrustedwrites, bool onlyfriendsreads)
		{
			string url = $"{Url}/FindOrCreateLeaderboard/v0002/";
			return url;
		}
		
		/// <param name="appid">appid of game</param>
		/// <param name="rangestart">range start or 0</param>
		/// <param name="rangeend">range end or max LB entries</param>
		/// <param name="steamid">SteamID used for friend & around user requests</param>
		/// <param name="leaderboardid">ID of the leaderboard to view</param>
		/// <param name="datarequest">type of request: RequestGlobal, RequestAroundUser, RequestFriends</param>
		public static string GetLeaderboardEntries(uint appid, int rangestart, int rangeend, ulong steamid, int leaderboardid, uint datarequest)
		{
			string url = $"{Url}GetLeaderboardEntries/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&rangestart={rangestart}&rangeend={rangeend}&steamid={steamid}&leaderboardid={leaderboardid}&datarequest={datarequest}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">appid of game</param>
		public static string GetLeaderboardsForGame(uint appid)
		{
			string url = $"{Url}GetLeaderboardsForGame/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">appid of game</param>
		/// <param name="leaderboardid">numeric ID of the target leaderboard. Can be retrieved from GetLeaderboardsForGame</param>
		public static string ResetLeaderboard(uint appid, uint leaderboardid)
		{
			string url = $"{Url}/ResetLeaderboard/v0001/";
			return url;
		}
		
		/// <param name="appid">appid of game</param>
		/// <param name="leaderboardid">numeric ID of the target leaderboard. Can be retrieved from GetLeaderboardsForGame</param>
		/// <param name="steamid">steamID to set the score for</param>
		/// <param name="score">the score to set for this user</param>
		/// <param name="scoremethod">update method to use. Can be "KeepBest" or "ForceUpdate"</param>
		/// <param name="details">game-specific details for how the score was earned. Up to 256 bytes.</param>
		public static string SetLeaderboardScore(uint appid, uint leaderboardid, ulong steamid, int score, string scoremethod, byte[] details)
		{
			string url = $"{Url}/SetLeaderboardScore/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamMicroTxn
	/// </summary>
	public partial class ISteamMicroTxn
	{
		private const string Url = "http://api.steampowered.com/ISteamMicroTxn/";
		
		/// <param name="steamid">SteamID of user with the agreement</param>
		/// <param name="agreementid">ID of agreement</param>
		/// <param name="appid">AppID of game</param>
		/// <param name="nextprocessdate">Date for next process</param>
		public static string AdjustAgreement(ulong steamid, ulong agreementid, uint appid, string nextprocessdate)
		{
			string url = $"{Url}/AdjustAgreement/v0001/";
			return url;
		}
		
		/// <param name="steamid">SteamID of user with the agreement</param>
		/// <param name="agreementid">ID of agreement</param>
		/// <param name="appid">AppID of game</param>
		public static string CancelAgreement(ulong steamid, ulong agreementid, uint appid)
		{
			string url = $"{Url}/CancelAgreement/v0001/";
			return url;
		}
		
		/// <param name="orderid">3rd party ID for transaction</param>
		/// <param name="appid">AppID of game this transaction is for</param>
		public static string FinalizeTxn(ulong orderid, uint appid)
		{
			string url = $"{Url}/FinalizeTxn/v0002/";
			return url;
		}
		
		/// <param name="appid">AppID of game this transaction is for</param>
		/// <param name="type">Report type (GAMESALES, STEAMSTORE, SETTLEMENT)</param>
		/// <param name="time">Beginning time to start report from (RFC 3339 UTC format)</param>
		/// <param name="maxresults">Max number of results to return (up to 1000)</param>
		public static string GetReport(uint appid, string type, string time, uint maxresults)
		{
			string url = $"{Url}GetReport/v0003/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&type={type}&time={time}&maxresults={maxresults}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user making purchase</param>
		/// <param name="appid">AppID of game</param>
		public static string GetUserAgreementInfo(ulong steamid, uint appid)
		{
			string url = $"{Url}GetUserAgreementInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user making purchase</param>
		/// <param name="ipaddress">ip address of user in string format (xxx.xxx.xxx.xxx). Only required if usersession=web</param>
		public static string GetUserInfo(ulong steamid, string ipaddress)
		{
			string url = $"{Url}GetUserInfo/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&ipaddress={ipaddress}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="orderid">3rd party ID for transaction</param>
		/// <param name="steamid">SteamID of user making purchase</param>
		/// <param name="appid">AppID of game this transaction is for</param>
		/// <param name="itemcount">Number of items in cart</param>
		/// <param name="language">ISO 639-1 language code of description</param>
		/// <param name="currency">ISO 4217 currency code</param>
		/// <param name="usersession">session where user will authorize the transaction. client or web (defaults to client)</param>
		/// <param name="ipaddress">ip address of user in string format (xxx.xxx.xxx.xxx). Only required if usersession=web</param>
		/// <param name="itemid[]">3rd party ID for item</param>
		/// <param name="qty[]">Quantity of this item</param>
		/// <param name="amount[]">Total cost (in cents) of item(s)</param>
		/// <param name="description[]">Description of item</param>
		/// <param name="category[]">Optional category grouping for item</param>
		/// <param name="associated_bundle[]">Optional bundleid of associated bundle</param>
		/// <param name="billingtype[]">Optional recurring billing type</param>
		/// <param name="startdate[]">Optional start date for recurring billing</param>
		/// <param name="enddate[]">Optional end date for recurring billing</param>
		/// <param name="period[]">Optional period for recurring billing</param>
		/// <param name="frequency[]">Optional frequency for recurring billing</param>
		/// <param name="recurringamt[]">Optional recurring billing amount</param>
		/// <param name="bundlecount">Number of bundles in cart</param>
		/// <param name="bundleid[]">3rd party ID of the bundle. This shares the same ID space as 3rd party items.</param>
		/// <param name="bundle_qty[]">Quantity of this bundle</param>
		/// <param name="bundle_desc[]">Description of bundle</param>
		/// <param name="bundle_category[]">Optional category grouping for bundle</param>
		public static string InitTxn(ulong orderid, ulong steamid, uint appid, uint itemcount, string language, string currency, string usersession, string ipaddress, uint[] itemid, uint[] qty, int[] amount, string[] description, string[] category, uint[] associated_bundle, string[] billingtype, string[] startdate, string[] enddate, string[] period, uint[] frequency, string[] recurringamt, uint bundlecount, uint[] bundleid, uint[] bundle_qty, string[] bundle_desc, string[] bundle_category)
		{
			string url = $"{Url}/InitTxn/v0003/";
			return url;
		}
		
		/// <param name="orderid">3rd party ID for transaction</param>
		/// <param name="steamid">SteamID of user with the agreement</param>
		/// <param name="agreementid">ID of agreement</param>
		/// <param name="appid">AppID of game</param>
		/// <param name="amount">Total cost (in cents) to charge</param>
		/// <param name="currency">ISO 4217 currency code</param>
		public static string ProcessAgreement(ulong orderid, ulong steamid, ulong agreementid, uint appid, int amount, string currency)
		{
			string url = $"{Url}/ProcessAgreement/v0001/";
			return url;
		}
		
		/// <param name="appid">AppID of game this transaction is for</param>
		/// <param name="orderid">3rd party ID for transaction</param>
		/// <param name="transid">Steam transaction ID</param>
		public static string QueryTxn(uint appid, ulong orderid, ulong transid)
		{
			string url = $"{Url}QueryTxn/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&orderid={orderid}&transid={transid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="orderid">3rd party ID for transaction</param>
		/// <param name="appid">AppID of game this transaction is for</param>
		public static string RefundTxn(ulong orderid, uint appid)
		{
			string url = $"{Url}/RefundTxn/v0002/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamMicroTxnSandbox
	/// </summary>
	public partial class ISteamMicroTxnSandbox
	{
		private const string Url = "http://api.steampowered.com/ISteamMicroTxnSandbox/";
		
		/// <param name="steamid">SteamID of user with the agreement</param>
		/// <param name="agreementid">ID of agreement</param>
		/// <param name="appid">AppID of game</param>
		/// <param name="nextprocessdate">Date for next process</param>
		public static string AdjustAgreement(ulong steamid, ulong agreementid, uint appid, string nextprocessdate)
		{
			string url = $"{Url}/AdjustAgreement/v0001/";
			return url;
		}
		
		/// <param name="steamid">SteamID of user with the agreement</param>
		/// <param name="agreementid">ID of agreement</param>
		/// <param name="appid">AppID of game</param>
		public static string CancelAgreement(ulong steamid, ulong agreementid, uint appid)
		{
			string url = $"{Url}/CancelAgreement/v0001/";
			return url;
		}
		
		/// <param name="orderid">3rd party ID for transaction</param>
		/// <param name="appid">AppID of game this transaction is for</param>
		public static string FinalizeTxn(ulong orderid, uint appid)
		{
			string url = $"{Url}/FinalizeTxn/v0002/";
			return url;
		}
		
		/// <param name="appid">AppID of game this transaction is for</param>
		/// <param name="type">Report type (GAMESALES, STEAMSTORE, SETTLEMENT)</param>
		/// <param name="time">Beginning time to start report from (RFC 3339 UTC format)</param>
		/// <param name="maxresults">Max number of results to return (up to 1000)</param>
		public static string GetReport(uint appid, string type, string time, uint maxresults)
		{
			string url = $"{Url}GetReport/v0003/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&type={type}&time={time}&maxresults={maxresults}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user making purchase</param>
		/// <param name="appid">AppID of game</param>
		public static string GetUserAgreementInfo(ulong steamid, uint appid)
		{
			string url = $"{Url}GetUserAgreementInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user making purchase</param>
		/// <param name="ipaddress">ip address of user in string format (xxx.xxx.xxx.xxx). Only required if usersession=web</param>
		public static string GetUserInfo(ulong steamid, string ipaddress)
		{
			string url = $"{Url}GetUserInfo/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&ipaddress={ipaddress}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="orderid">3rd party ID for transaction</param>
		/// <param name="steamid">SteamID of user making purchase</param>
		/// <param name="appid">AppID of game this transaction is for</param>
		/// <param name="itemcount">Number of items in cart</param>
		/// <param name="language">ISO 639-1 language code of description</param>
		/// <param name="currency">ISO 4217 currency code</param>
		/// <param name="itemid[]">3rd party ID for item</param>
		/// <param name="qty[]">Quantity of this item</param>
		/// <param name="amount[]">Total cost (in cents) of item(s)</param>
		/// <param name="description[]">Description of item</param>
		/// <param name="category[]">Optional category grouping for item</param>
		/// <param name="billingtype[]">Optional recurring billing type</param>
		/// <param name="startdate[]">Optional start date for recurring billing</param>
		/// <param name="enddate[]">Optional end date for recurring billing</param>
		/// <param name="period[]">Optional period for recurring billing</param>
		/// <param name="frequency[]">Optional frequency for recurring billing</param>
		/// <param name="recurringamt[]">Optional recurring billing amount</param>
		public static string InitTxn(ulong orderid, ulong steamid, uint appid, uint itemcount, string language, string currency, uint[] itemid, uint[] qty, int[] amount, string[] description, string[] category, string[] billingtype, string[] startdate, string[] enddate, string[] period, uint[] frequency, string[] recurringamt)
		{
			string url = $"{Url}/InitTxn/v0003/";
			return url;
		}
		
		/// <param name="steamid">SteamID of user with the agreement</param>
		/// <param name="agreementid">ID of agreement</param>
		/// <param name="appid">AppID of game</param>
		/// <param name="amount">Total cost (in cents) to charge</param>
		/// <param name="currency">ISO 4217 currency code</param>
		public static string ProcessAgreement(ulong steamid, ulong agreementid, uint appid, int amount, string currency)
		{
			string url = $"{Url}/ProcessAgreement/v0001/";
			return url;
		}
		
		/// <param name="appid">AppID of game this transaction is for</param>
		/// <param name="orderid">3rd party ID for transaction</param>
		/// <param name="transid">Steam transaction ID</param>
		public static string QueryTxn(uint appid, ulong orderid, ulong transid)
		{
			string url = $"{Url}QueryTxn/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&orderid={orderid}&transid={transid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="orderid">3rd party ID for transaction</param>
		/// <param name="appid">AppID of game this transaction is for</param>
		public static string RefundTxn(ulong orderid, uint appid)
		{
			string url = $"{Url}/RefundTxn/v0002/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamNews
	/// </summary>
	public partial class ISteamNews
	{
		private const string Url = "http://api.steampowered.com/ISteamNews/";
		
		/// <param name="appid">AppID to retrieve news for</param>
		/// <param name="maxlength">Maximum length for the content to return, if this is 0 the full content is returned, if it's less then a blurb is generated to fit.</param>
		/// <param name="enddate">Retrieve posts earlier than this date (unix epoch timestamp)</param>
		/// <param name="count"># of posts to retrieve (default 20)</param>
		/// <param name="feeds">Comma-seperated list of feed names to return news for</param>
		public static string GetNewsForApp(uint appid, uint maxlength, uint enddate, uint count, string feeds)
		{
			string url = $"{Url}GetNewsForApp/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&maxlength={maxlength}&enddate={enddate}&count={count}&feeds={feeds}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">AppID to retrieve news for</param>
		/// <param name="maxlength">Maximum length for the content to return, if this is 0 the full content is returned, if it's less then a blurb is generated to fit.</param>
		/// <param name="enddate">Retrieve posts earlier than this date (unix epoch timestamp)</param>
		/// <param name="count"># of posts to retrieve (default 20)</param>
		/// <param name="feeds">Comma-seperated list of feed names to return news for</param>
		public static string GetNewsForAppAuthed(uint appid, uint maxlength, uint enddate, uint count, string feeds)
		{
			string url = $"{Url}GetNewsForAppAuthed/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&maxlength={maxlength}&enddate={enddate}&count={count}&feeds={feeds}";
			return Utility.WebGet<string>( url );
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
			string url = $"{Url}/PayPalPaymentsHubPaymentNotification/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamPublishedItemSearch
	/// </summary>
	public partial class ISteamPublishedItemSearch
	{
		private const string Url = "http://api.steampowered.com/ISteamPublishedItemSearch/";
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appID of product</param>
		/// <param name="startidx">Starting index in the result set (0 based)</param>
		/// <param name="count">Number Requested</param>
		/// <param name="tagcount">Number of Tags Specified</param>
		/// <param name="usertagcount">Number of User specific tags requested</param>
		/// <param name="hasappadminaccess">Whether the user making the request is an admin for the app and can see private files</param>
		/// <param name="fileType">EPublishedFileInfoMatchingFileType, defaults to k_PFI_MatchingFileType_Items</param>
		/// <param name="tag[]">Tag to filter result set</param>
		/// <param name="usertag[]">A user specific tag</param>
		public static string RankedByPublicationOrder(ulong steamid, uint appid, uint startidx, uint count, uint tagcount, uint usertagcount, bool hasappadminaccess, uint fileType, string[] tag, string[] usertag)
		{
			string url = $"{Url}/RankedByPublicationOrder/v0001/";
			return url;
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appID of product</param>
		/// <param name="startidx">Starting index in the result set (0 based)</param>
		/// <param name="count">Number Requested</param>
		/// <param name="tagcount">Number of Tags Specified</param>
		/// <param name="usertagcount">Number of User specific tags requested</param>
		/// <param name="hasappadminaccess">Whether the user making the request is an admin for the app and can see private files</param>
		/// <param name="fileType">EPublishedFileInfoMatchingFileType, defaults to k_PFI_MatchingFileType_Items</param>
		/// <param name="days">[1,7] number of days for the trend period, including today</param>
		/// <param name="tag[]">Tag to filter result set</param>
		/// <param name="usertag[]">A user specific tag</param>
		public static string RankedByTrend(ulong steamid, uint appid, uint startidx, uint count, uint tagcount, uint usertagcount, bool hasappadminaccess, uint fileType, uint days, string[] tag, string[] usertag)
		{
			string url = $"{Url}/RankedByTrend/v0001/";
			return url;
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appID of product</param>
		/// <param name="startidx">Starting index in the result set (0 based)</param>
		/// <param name="count">Number Requested</param>
		/// <param name="tagcount">Number of Tags Specified</param>
		/// <param name="usertagcount">Number of User specific tags requested</param>
		/// <param name="hasappadminaccess">Whether the user making the request is an admin for the app and can see private files</param>
		/// <param name="fileType">EPublishedFileInfoMatchingFileType, defaults to k_PFI_MatchingFileType_Items</param>
		/// <param name="tag[]">Tag to filter result set</param>
		/// <param name="usertag[]">A user specific tag</param>
		public static string RankedByVote(ulong steamid, uint appid, uint startidx, uint count, uint tagcount, uint usertagcount, bool hasappadminaccess, uint fileType, string[] tag, string[] usertag)
		{
			string url = $"{Url}/RankedByVote/v0001/";
			return url;
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appID relevant to all subsequent tags</param>
		/// <param name="tagcount">Number of Tags Specified</param>
		/// <param name="usertagcount">Number of User specific tags requested</param>
		/// <param name="hasappadminaccess">Whether the user making the request is an admin for the app and can see private files</param>
		/// <param name="fileType">EPublishedFileInfoMatchingFileType, defaults to k_PFI_MatchingFileType_Items</param>
		/// <param name="tag[]">Tag to filter result set</param>
		/// <param name="usertag[]">A user specific tag</param>
		public static string ResultSetSummary(ulong steamid, ulong appid, uint tagcount, uint usertagcount, bool hasappadminaccess, uint fileType, string[] tag, string[] usertag)
		{
			string url = $"{Url}/ResultSetSummary/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamPublishedItemVoting
	/// </summary>
	public partial class ISteamPublishedItemVoting
	{
		private const string Url = "http://api.steampowered.com/ISteamPublishedItemVoting/";
		
		/// <param name="steamid">Steam ID of user</param>
		/// <param name="appid">appID of product</param>
		/// <param name="count">Count of how many items we are querying</param>
		/// <param name="publishedfileid[]">The Published File ID who's vote details are required</param>
		public static string ItemVoteSummary(ulong steamid, uint appid, uint count, ulong[] publishedfileid)
		{
			string url = $"{Url}/ItemVoteSummary/v0001/";
			return url;
		}
		
		/// <param name="steamid">Steam ID of user</param>
		/// <param name="count">Count of how many items we are querying</param>
		/// <param name="publishedfileid[]">A Specific Published Item</param>
		public static string UserVoteSummary(ulong steamid, uint count, ulong[] publishedfileid)
		{
			string url = $"{Url}/UserVoteSummary/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamRemoteStorage
	/// </summary>
	public partial class ISteamRemoteStorage
	{
		private const string Url = "http://api.steampowered.com/ISteamRemoteStorage/";
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appID of product</param>
		public static string EnumerateUserPublishedFiles(ulong steamid, uint appid)
		{
			string url = $"{Url}/EnumerateUserPublishedFiles/v0001/";
			return url;
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appID of product</param>
		/// <param name="listtype">EUCMListType</param>
		public static string EnumerateUserSubscribedFiles(ulong steamid, uint appid, uint listtype)
		{
			string url = $"{Url}/EnumerateUserSubscribedFiles/v0001/";
			return url;
		}
		
		/// <param name="collectioncount">Number of collections being requested</param>
		/// <param name="publishedfileids[]">collection ids to get the details for</param>
		public static string GetCollectionDetails(uint collectioncount, ulong[] publishedfileids)
		{
			string url = $"{Url}/GetCollectionDetails/v0001/";
			return url;
		}
		
		/// <param name="itemcount">Number of items being requested</param>
		/// <param name="publishedfileids[]">published file id to look up</param>
		public static string GetPublishedFileDetails(uint itemcount, ulong[] publishedfileids)
		{
			string url = $"{Url}/GetPublishedFileDetails/v0001/";
			return url;
		}
		
		/// <param name="steamid">If specified, only returns details if the file is owned by the SteamID specified</param>
		/// <param name="ugcid">ID of UGC file to get info for</param>
		/// <param name="appid">appID of product</param>
		public static string GetUGCFileDetails(ulong steamid, ulong ugcid, uint appid)
		{
			string url = $"{Url}GetUGCFileDetails/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&ugcid={ugcid}&appid={appid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="ugcid">ID of UGC file whose bits are being fiddled with</param>
		/// <param name="appid">appID of product to change updating state for</param>
		/// <param name="used">New state of flag</param>
		public static string SetUGCUsedByGC(ulong steamid, ulong ugcid, uint appid, bool used)
		{
			string url = $"{Url}/SetUGCUsedByGC/v0001/";
			return url;
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appID of product</param>
		/// <param name="publishedfileid">published file id to subscribe to</param>
		public static string SubscribePublishedFile(ulong steamid, uint appid, ulong publishedfileid)
		{
			string url = $"{Url}/SubscribePublishedFile/v0001/";
			return url;
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appID of product</param>
		/// <param name="publishedfileid">published file id to unsubscribe from</param>
		public static string UnsubscribePublishedFile(ulong steamid, uint appid, ulong publishedfileid)
		{
			string url = $"{Url}/UnsubscribePublishedFile/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamSpecialSurvey
	/// </summary>
	public partial class ISteamSpecialSurvey
	{
		private const string Url = "http://api.steampowered.com/ISteamSpecialSurvey/";
		
		/// <param name="appid">appid of game</param>
		/// <param name="surveyid">ID of the survey being taken</param>
		/// <param name="steamid">SteamID of the user taking the survey</param>
		/// <param name="token">Survey identity verification token for the user</param>
		public static string CheckUserStatus(uint appid, uint surveyid, ulong steamid, string token)
		{
			string url = $"{Url}CheckUserStatus/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&surveyid={surveyid}&steamid={steamid}&token={token}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">appid of game</param>
		/// <param name="surveyid">ID of the survey being taken</param>
		/// <param name="steamid">SteamID of the user taking the survey</param>
		/// <param name="token">Survey identity verification token for the user</param>
		public static string SetUserFinished(uint appid, uint surveyid, ulong steamid, string token)
		{
			string url = $"{Url}/SetUserFinished/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamUser
	/// </summary>
	public partial class ISteamUser
	{
		private const string Url = "http://api.steampowered.com/ISteamUser/";
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">AppID to check for ownership</param>
		public static string CheckAppOwnership(ulong steamid, uint appid)
		{
			string url = $"{Url}CheckAppOwnership/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appids">Comma-delimited list of appids (max: 100)</param>
		public static string GetAppPriceInfo(ulong steamid, string appids)
		{
			string url = $"{Url}GetAppPriceInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appids={appids}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="relationship">relationship type (ex: friend)</param>
		public static string GetFriendList(ulong steamid, string relationship)
		{
			string url = $"{Url}GetFriendList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&relationship={relationship}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamids">Comma-delimited list of SteamIDs</param>
		public static string GetPlayerBans(string steamids)
		{
			string url = $"{Url}GetPlayerBans/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamids={steamids}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamids">Comma-delimited list of SteamIDs (max: 100)</param>
		public static string GetPlayerSummaries(string steamids)
		{
			string url = $"{Url}GetPlayerSummaries/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&steamids={steamids}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user</param>
		public static string GetPublisherAppOwnership(ulong steamid)
		{
			string url = $"{Url}GetPublisherAppOwnership/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user</param>
		public static string GetUserGroupList(ulong steamid)
		{
			string url = $"{Url}GetUserGroupList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="packageid">PackageID to grant</param>
		/// <param name="ipaddress">ip address of user in string format (xxx.xxx.xxx.xxx).</param>
		/// <param name="thirdpartykey">Optionally associate third party key during grant. 'thirdpartyappid' will have to be set.</param>
		/// <param name="thirdpartyappid">Has to be set if 'thirdpartykey' is set. The appid associated with the 'thirdpartykey'.</param>
		public static string GrantPackage(ulong steamid, uint packageid, string ipaddress, string thirdpartykey, uint thirdpartyappid)
		{
			string url = $"{Url}/GrantPackage/v0001/";
			return url;
		}
		
		/// <param name="vanityurl">The vanity URL to get a SteamID for</param>
		/// <param name="url_type">The type of vanity URL. 1 (default): Individual profile, 2: Group, 3: Official game group</param>
		public static string ResolveVanityURL(string vanityurl, int url_type)
		{
			string url = $"{Url}ResolveVanityURL/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&vanityurl={vanityurl}&url_type={url_type}";
			return Utility.WebGet<string>( url );
		}
		
	}
	
	/// <summary>
	/// ISteamUserAuth
	/// </summary>
	public partial class ISteamUserAuth
	{
		private const string Url = "http://api.steampowered.com/ISteamUserAuth/";
		
		/// <param name="steamid">Should be the users steamid, unencrypted.</param>
		/// <param name="sessionkey">Should be a 32 byte random blob of data, which is then encrypted with RSA using the Steam system's public key.  Randomness is important here for security.</param>
		/// <param name="encrypted_loginkey">Should be the users hashed loginkey, AES encrypted with the sessionkey.</param>
		public static string AuthenticateUser(ulong steamid, byte[] sessionkey, byte[] encrypted_loginkey)
		{
			string url = $"{Url}/AuthenticateUser/v0001/";
			return url;
		}
		
		/// <param name="appid">appid of game</param>
		/// <param name="ticket">Ticket from GetAuthSessionTicket.</param>
		public static string AuthenticateUserTicket(uint appid, string ticket)
		{
			string url = $"{Url}AuthenticateUserTicket/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&ticket={ticket}";
			return Utility.WebGet<string>( url );
		}
		
	}
	
	/// <summary>
	/// ISteamUserOAuth
	/// </summary>
	public partial class ISteamUserOAuth
	{
		private const string Url = "http://api.steampowered.com/ISteamUserOAuth/";
		
		/// <param name="access_token">OAuth2 token for which to return details</param>
		public static string GetTokenDetails(string access_token)
		{
			string url = $"{Url}GetTokenDetails/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&access_token={access_token}";
			return Utility.WebGet<string>( url );
		}
		
	}
	
	/// <summary>
	/// ISteamUserStats
	/// </summary>
	public partial class ISteamUserStats
	{
		private const string Url = "http://api.steampowered.com/ISteamUserStats/";
		
		/// <param name="gameid">GameID to retrieve the achievement percentages for</param>
		public static string GetGlobalAchievementPercentagesForApp(ulong gameid)
		{
			string url = $"{Url}GetGlobalAchievementPercentagesForApp/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&gameid={gameid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">AppID that we're getting global stats for</param>
		/// <param name="count">Number of stats get data for</param>
		/// <param name="name[]">Names of stat to get data for</param>
		/// <param name="startdate">Start date for daily totals (unix epoch timestamp)</param>
		/// <param name="enddate">End date for daily totals (unix epoch timestamp)</param>
		public static string GetGlobalStatsForGame(uint appid, uint count, string[] name, uint startdate, uint enddate)
		{
			string url = $"{Url}GetGlobalStatsForGame/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&count={count}&name[0]={name[0]}&startdate={startdate}&enddate={enddate}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">AppID that we're getting user count for</param>
		public static string GetNumberOfCurrentPlayers(uint appid)
		{
			string url = $"{Url}GetNumberOfCurrentPlayers/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">AppID to get achievements for</param>
		/// <param name="l">Language to return strings for</param>
		public static string GetPlayerAchievements(ulong steamid, uint appid, string l)
		{
			string url = $"{Url}GetPlayerAchievements/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}&l={l}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid">appid of game</param>
		/// <param name="l">localized language to return (english, french, etc.)</param>
		public static string GetSchemaForGame(uint appid, string l)
		{
			string url = $"{Url}GetSchemaForGame/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&l={l}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appid of game</param>
		public static string GetUserStatsForGame(ulong steamid, uint appid)
		{
			string url = $"{Url}GetUserStatsForGame/v0002/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid={appid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appid of game</param>
		/// <param name="count">Number of stats and achievements to set a value for (name/value param pairs)</param>
		/// <param name="name[]">Name of stat or achievement to set</param>
		/// <param name="value[]">Value to set</param>
		public static string SetUserStatsForGame(ulong steamid, uint appid, uint count, string[] name, uint[] value)
		{
			string url = $"{Url}/SetUserStatsForGame/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamVideo
	/// </summary>
	public partial class ISteamVideo
	{
		private const string Url = "http://api.steampowered.com/ISteamVideo/";
		
		/// <param name="steamid">SteamID of user</param>
		/// <param name="appid">appID of the video</param>
		/// <param name="videoid">ID of the video on the provider's site</param>
		/// <param name="accountname">Account name of the video's owner on the provider's site</param>
		public static string AddVideo(ulong steamid, uint appid, string videoid, string accountname)
		{
			string url = $"{Url}/AddVideo/v0001/";
			return url;
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
			string url = $"{Url}GetServerInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&";
			return Utility.WebGet<string>( url );
		}
		
		public static string GetSupportedAPIList()
		{
			string url = $"{Url}GetSupportedAPIList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&";
			return Utility.WebGet<string>( url );
		}
		
	}
	
	/// <summary>
	/// ISteamWebUserPresenceOAuth
	/// </summary>
	public partial class ISteamWebUserPresenceOAuth
	{
		private const string Url = "http://api.steampowered.com/ISteamWebUserPresenceOAuth/";
		
		/// <param name="steamid">Steam ID of the user</param>
		/// <param name="umqid">UMQ Session ID</param>
		/// <param name="message">Message that was last known to the user</param>
		/// <param name="pollid">Caller-specific poll id</param>
		/// <param name="sectimeout">Long-poll timeout in seconds</param>
		/// <param name="secidletime">How many seconds is client considering itself idle, e.g. screen is off</param>
		/// <param name="use_accountids">Boolean, 0 (default): return steamid_from in output, 1: return accountid_from</param>
		public static string PollStatus(string steamid, ulong umqid, uint message, uint pollid, uint sectimeout, uint secidletime, uint use_accountids)
		{
			string url = $"{Url}/PollStatus/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ISteamWorkshop
	/// </summary>
	public partial class ISteamWorkshop
	{
		private const string Url = "http://api.steampowered.com/ISteamWorkshop/";
		
		/// <param name="appid">AppID of game this transaction is for</param>
		/// <param name="itemcount">Number of items to associate</param>
		/// <param name="publishedfileid[]">the workshop published file id</param>
		/// <param name="gameitemid[]">3rd party ID for item</param>
		/// <param name="revenuepercentage[]">Percentage of revenue the owners of the workshop item will get from the sale of the item [0.0, 100.0].  For bundle-like items, send over an entry for each item in the bundle (gameitemid = bundle id) with different publishedfileids and with the revenue percentage pre-split amongst the items in the bundle (i.e. 30% / 10 items in the bundle)</param>
		/// <param name="gameitemdescription[]">Game's description of the game item</param>
		public static string AssociateWorkshopItems(uint appid, uint itemcount, ulong[] publishedfileid, uint[] gameitemid, float[] revenuepercentage, string[] gameitemdescription)
		{
			string url = $"{Url}/AssociateWorkshopItems/v0001/";
			return url;
		}
		
		/// <param name="appid">AppID of game this transaction is for</param>
		public static string GetContributors(uint appid)
		{
			string url = $"{Url}/GetContributors/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// IGameServersService
	/// </summary>
	public partial class IGameServersService
	{
		private const string Url = "http://api.steampowered.com/IGameServersService/";
		
		/// <param name="appid">The app to use the account for</param>
		/// <param name="memo">The memo to set on the new account</param>
		public static string CreateAccount(uint appid, string memo)
		{
			string url = $"{Url}/CreateAccount/v0001/";
			return url;
		}
		
		/// <param name="steamid">The SteamID of the game server account to delete</param>
		public static string DeleteAccount(ulong steamid)
		{
			string url = $"{Url}/DeleteAccount/v0001/";
			return url;
		}
		
		public static string GetAccountList()
		{
			string url = $"{Url}GetAccountList/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">The SteamID of the game server to get info on</param>
		public static string GetAccountPublicInfo(ulong steamid)
		{
			string url = $"{Url}GetAccountPublicInfo/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="server_steamids"></param>
		public static string GetServerIPsBySteamID(ulong server_steamids)
		{
			string url = $"{Url}GetServerIPsBySteamID/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&server_steamids={server_steamids}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="server_ips"></param>
		public static string GetServerSteamIDsByIP(string server_ips)
		{
			string url = $"{Url}GetServerSteamIDsByIP/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&server_ips={server_ips}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="login_token">Login token to query</param>
		public static string QueryLoginToken(string login_token)
		{
			string url = $"{Url}QueryLoginToken/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&login_token={login_token}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">The SteamID of the game server to reset the login token of</param>
		public static string ResetLoginToken(ulong steamid)
		{
			string url = $"{Url}/ResetLoginToken/v0001/";
			return url;
		}
		
		/// <param name="steamid"></param>
		/// <param name="banned"></param>
		/// <param name="ban_seconds"></param>
		public static string SetBanStatus(ulong steamid, bool banned, uint ban_seconds)
		{
			string url = $"{Url}/SetBanStatus/v0001/";
			return url;
		}
		
		/// <param name="steamid">The SteamID of the game server to set the memo on</param>
		/// <param name="memo">The memo to set on the new account</param>
		public static string SetMemo(ulong steamid, string memo)
		{
			string url = $"{Url}/SetMemo/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// IBroadcastService
	/// </summary>
	public partial class IBroadcastService
	{
		private const string Url = "http://api.steampowered.com/IBroadcastService/";
		
		/// <param name="appid"></param>
		/// <param name="steamid"></param>
		/// <param name="broadcast_id"></param>
		/// <param name="frame_data"></param>
		public static string PostGameDataFrame(uint appid, ulong steamid, ulong broadcast_id, string frame_data)
		{
			string url = $"{Url}/PostGameDataFrame/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// IPublishedFileService
	/// </summary>
	public partial class IPublishedFileService
	{
		private const string Url = "http://api.steampowered.com/IPublishedFileService/";
		
		/// <param name="query_type">enumeration EPublishedFileQueryType in clientenums.h</param>
		/// <param name="page">Current page</param>
		/// <param name="numperpage">(Optional) The number of results, per page to return.</param>
		/// <param name="creator_appid">App that created the files</param>
		/// <param name="appid">App that consumes the files</param>
		/// <param name="requiredtags">Tags to match on. See match_all_tags parameter below</param>
		/// <param name="excludedtags">(Optional) Tags that must NOT be present on a published file to satisfy the query.</param>
		/// <param name="match_all_tags">If true, then items must have all the tags specified, otherwise they must have at least one of the tags.</param>
		/// <param name="required_flags">Required flags that must be set on any returned items</param>
		/// <param name="omitted_flags">Flags that must not be set on any returned items</param>
		/// <param name="search_text">Text to match in the item's title or description</param>
		/// <param name="filetype">EPublishedFileInfoMatchingFileType</param>
		/// <param name="child_publishedfileid">Find all items that reference the given item.</param>
		/// <param name="days">If query_type is k_PublishedFileQueryType_RankedByTrend, then this is the number of days to get votes for [1,7].</param>
		/// <param name="include_recent_votes_only">If query_type is k_PublishedFileQueryType_RankedByTrend, then limit result set just to items that have votes within the day range given</param>
		/// <param name="cache_max_age_seconds">Allow stale data to be returned for the specified number of seconds.</param>
		/// <param name="language">Language to search in and also what gets returned. Defaults to English.</param>
		/// <param name="required_kv_tags">Required key-value tags to match on.</param>
		/// <param name="totalonly">(Optional) If true, only return the total number of files that satisfy this query.</param>
		/// <param name="ids_only">(Optional) If true, only return the published file ids of files that satisfy this query.</param>
		/// <param name="return_vote_data">Return vote data</param>
		/// <param name="return_tags">Return tags in the file details</param>
		/// <param name="return_kv_tags">Return key-value tags in the file details</param>
		/// <param name="return_previews">Return preview image and video details in the file details</param>
		/// <param name="return_children">Return child item ids in the file details</param>
		/// <param name="return_short_description">Populate the short_description field instead of file_description</param>
		/// <param name="return_for_sale_data">Return pricing information, if applicable</param>
		/// <param name="return_metadata">Populate the metadata</param>
		public static string QueryFiles(uint query_type, uint page, uint numperpage, uint creator_appid, uint appid, string requiredtags, string excludedtags, bool match_all_tags, string required_flags, string omitted_flags, string search_text, uint filetype, ulong child_publishedfileid, uint days, bool include_recent_votes_only, uint cache_max_age_seconds, int language, string required_kv_tags, bool totalonly, bool ids_only, bool return_vote_data, bool return_tags, bool return_kv_tags, bool return_previews, bool return_children, bool return_short_description, bool return_for_sale_data, bool return_metadata)
		{
			string url = $"{Url}QueryFiles/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&query_type={query_type}&page={page}&numperpage={numperpage}&creator_appid={creator_appid}&appid={appid}&requiredtags={requiredtags}&excludedtags={excludedtags}&match_all_tags={match_all_tags}&required_flags={required_flags}&omitted_flags={omitted_flags}&search_text={search_text}&filetype={filetype}&child_publishedfileid={child_publishedfileid}&days={days}&include_recent_votes_only={include_recent_votes_only}&cache_max_age_seconds={cache_max_age_seconds}&language={language}&required_kv_tags={required_kv_tags}&totalonly={totalonly}&ids_only={ids_only}&return_vote_data={return_vote_data}&return_tags={return_tags}&return_kv_tags={return_kv_tags}&return_previews={return_previews}&return_children={return_children}&return_short_description={return_short_description}&return_for_sale_data={return_for_sale_data}&return_metadata={return_metadata}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="publishedfileid"></param>
		/// <param name="appid"></param>
		/// <param name="metadata"></param>
		public static string SetDeveloperMetadata(ulong publishedfileid, uint appid, string metadata)
		{
			string url = $"{Url}/SetDeveloperMetadata/v0001/";
			return url;
		}
		
		/// <param name="publishedfileid"></param>
		/// <param name="appid"></param>
		/// <param name="add_tags"></param>
		/// <param name="remove_tags"></param>
		public static string UpdateTags(ulong publishedfileid, uint appid, string add_tags, string remove_tags)
		{
			string url = $"{Url}/UpdateTags/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// IEconService
	/// </summary>
	public partial class IEconService
	{
		private const string Url = "http://api.steampowered.com/IEconService/";
		
		/// <param name="tradeofferid"></param>
		public static string CancelTradeOffer(ulong tradeofferid)
		{
			string url = $"{Url}/CancelTradeOffer/v0001/";
			return url;
		}
		
		/// <param name="tradeofferid"></param>
		public static string DeclineTradeOffer(ulong tradeofferid)
		{
			string url = $"{Url}/DeclineTradeOffer/v0001/";
			return url;
		}
		
		/// <param name="appid"></param>
		public static string FlushAssetAppearanceCache(uint appid)
		{
			string url = $"{Url}/FlushAssetAppearanceCache/v0001/";
			return url;
		}
		
		/// <param name="steamid">User to clear cache for.</param>
		/// <param name="appid">App to clear cache for.</param>
		/// <param name="contextid">Context to clear cache for.</param>
		public static string FlushInventoryCache(ulong steamid, uint appid, ulong contextid)
		{
			string url = $"{Url}/FlushInventoryCache/v0001/";
			return url;
		}
		
		/// <param name="max_trades">The number of trades to return information for</param>
		/// <param name="start_after_time">The time of the last trade shown on the previous page of results, or the time of the first trade if navigating back</param>
		/// <param name="start_after_tradeid">The tradeid shown on the previous page of results, or the ID of the first trade if navigating back</param>
		/// <param name="navigating_back">The user wants the previous page of results, so return the previous max_trades trades before the start time and ID</param>
		/// <param name="get_descriptions">If set, the item display data for the items included in the returned trades will also be returned</param>
		/// <param name="language">The language to use when loading item display data</param>
		/// <param name="include_failed"></param>
		/// <param name="include_total">If set, the total number of trades the account has participated in will be included in the response</param>
		public static string GetTradeHistory(uint max_trades, uint start_after_time, ulong start_after_tradeid, bool navigating_back, bool get_descriptions, string language, bool include_failed, bool include_total)
		{
			string url = $"{Url}GetTradeHistory/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&max_trades={max_trades}&start_after_time={start_after_time}&start_after_tradeid={start_after_tradeid}&navigating_back={navigating_back}&get_descriptions={get_descriptions}&language={language}&include_failed={include_failed}&include_total={include_total}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="tradeofferid"></param>
		/// <param name="language"></param>
		public static string GetTradeOffer(ulong tradeofferid, string language)
		{
			string url = $"{Url}GetTradeOffer/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&tradeofferid={tradeofferid}&language={language}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="get_sent_offers">Request the list of sent offers.</param>
		/// <param name="get_received_offers">Request the list of received offers.</param>
		/// <param name="get_descriptions">If set, the item display data for the items included in the returned trade offers will also be returned.</param>
		/// <param name="language">The language to use when loading item display data.</param>
		/// <param name="active_only">Indicates we should only return offers which are still active, or offers that have changed in state since the time_historical_cutoff</param>
		/// <param name="historical_only">Indicates we should only return offers which are not active.</param>
		/// <param name="time_historical_cutoff">When active_only is set, offers updated since this time will also be returned</param>
		public static string GetTradeOffers(bool get_sent_offers, bool get_received_offers, bool get_descriptions, string language, bool active_only, bool historical_only, uint time_historical_cutoff)
		{
			string url = $"{Url}GetTradeOffers/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&get_sent_offers={get_sent_offers}&get_received_offers={get_received_offers}&get_descriptions={get_descriptions}&language={language}&active_only={active_only}&historical_only={historical_only}&time_historical_cutoff={time_historical_cutoff}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="time_last_visit">The time the user last visited.  If not passed, will use the time the user last visited the trade offer page.</param>
		public static string GetTradeOffersSummary(uint time_last_visit)
		{
			string url = $"{Url}GetTradeOffersSummary/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&time_last_visit={time_last_visit}";
			return Utility.WebGet<string>( url );
		}
		
	}
	
	/// <summary>
	/// IPlayerService
	/// </summary>
	public partial class IPlayerService
	{
		private const string Url = "http://api.steampowered.com/IPlayerService/";
		
		/// <param name="steamid">The player we're asking about</param>
		public static string GetBadges(ulong steamid)
		{
			string url = $"{Url}GetBadges/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">The player we're asking about</param>
		/// <param name="badgeid">The badge we're asking about</param>
		public static string GetCommunityBadgeProgress(ulong steamid, int badgeid)
		{
			string url = $"{Url}GetCommunityBadgeProgress/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&badgeid={badgeid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">The player we're asking about</param>
		/// <param name="include_appinfo">true if we want additional details (name, icon) about each game</param>
		/// <param name="include_played_free_games">Free games are excluded by default.  If this is set, free games the user has played will be returned.</param>
		/// <param name="appids_filter">if set, restricts result set to the passed in apps</param>
		public static string GetOwnedGames(ulong steamid, bool include_appinfo, bool include_played_free_games, uint appids_filter)
		{
			string url = $"{Url}GetOwnedGames/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&include_appinfo={include_appinfo}&include_played_free_games={include_played_free_games}&appids_filter={appids_filter}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">The player we're asking about</param>
		/// <param name="count">The number of games to return (0/unset: all)</param>
		public static string GetRecentlyPlayedGames(ulong steamid, uint count)
		{
			string url = $"{Url}GetRecentlyPlayedGames/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&count={count}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">The player we're asking about</param>
		public static string GetSteamLevel(ulong steamid)
		{
			string url = $"{Url}GetSteamLevel/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">The player we're asking about</param>
		/// <param name="appid_playing">The game player is currently playing</param>
		public static string IsPlayingSharedGame(ulong steamid, uint appid_playing)
		{
			string url = $"{Url}IsPlayingSharedGame/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}&appid_playing={appid_playing}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid"></param>
		/// <param name="ticket"></param>
		/// <param name="play_sessions"></param>
		public static string RecordOfflinePlaytime(ulong steamid, string ticket, string play_sessions)
		{
			string url = $"{Url}/RecordOfflinePlaytime/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// IGameNotificationsService
	/// </summary>
	public partial class IGameNotificationsService
	{
		private const string Url = "http://api.steampowered.com/IGameNotificationsService/";
		
		/// <param name="appid">The appid to create the session for.</param>
		/// <param name="context">Game-specified context value the game can used to associate the session with some object on their backend.</param>
		/// <param name="title">The title of the session to be displayed within each user's list of sessions.</param>
		/// <param name="users">The initial state of all users in the session.</param>
		/// <param name="steamid">(Optional) steamid to make the request on behalf of -- if specified, the user must be in the session and all users being added to the session must be friends with the user.</param>
		public static string CreateSession(uint appid, ulong context, string title, string users, ulong steamid)
		{
			string url = $"{Url}/CreateSession/v0001/";
			return url;
		}
		
		/// <param name="sessionid">The sessionid to delete.</param>
		/// <param name="appid">The appid of the session to delete.</param>
		/// <param name="steamid">(Optional) steamid to make the request on behalf of -- if specified, the user must be in the session.</param>
		public static string DeleteSession(ulong sessionid, uint appid, ulong steamid)
		{
			string url = $"{Url}/DeleteSession/v0001/";
			return url;
		}
		
		/// <param name="sessionid">The sessionid to delete.</param>
		/// <param name="appid">The appid of the session to delete.</param>
		public static string DeleteSessionBatch(ulong sessionid, uint appid)
		{
			string url = $"{Url}/DeleteSessionBatch/v0001/";
			return url;
		}
		
		/// <param name="appid">The sessionid to request details for. Optional. If not specified, all the user's sessions will be returned.</param>
		/// <param name="steamid">The user whose sessions are to be enumerated.</param>
		/// <param name="include_all_user_messages">(Optional) Boolean determining whether the message for all users should be included. Defaults to false.</param>
		/// <param name="include_auth_user_message">(Optional) Boolean determining whether the message for the authenticated user should be included. Defaults to false.</param>
		/// <param name="language">(Optional) Language to localize the text in.</param>
		public static string EnumerateSessionsForApp(uint appid, ulong steamid, bool include_all_user_messages, bool include_auth_user_message, string language)
		{
			string url = $"{Url}EnumerateSessionsForApp/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&steamid={steamid}&include_all_user_messages={include_all_user_messages}&include_auth_user_message={include_auth_user_message}&language={language}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="sessions"></param>
		/// <param name="appid">The appid for the sessions.</param>
		/// <param name="language">Language to localize the text in.</param>
		public static string GetSessionDetailsForApp(string sessions, uint appid, string language)
		{
			string url = $"{Url}GetSessionDetailsForApp/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&sessions={sessions}&appid={appid}&language={language}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">The steamid to request notifications for.</param>
		/// <param name="appid">The appid to request notifications for.</param>
		public static string RequestNotifications(ulong steamid, uint appid)
		{
			string url = $"{Url}/RequestNotifications/v0001/";
			return url;
		}
		
		/// <param name="sessionid">The sessionid to update.</param>
		/// <param name="appid">The appid of the session to update.</param>
		/// <param name="title">(Optional) The new title of the session.  If not specified, the title will not be changed.</param>
		/// <param name="users">(Optional) A list of users whose state will be updated to reflect the given state. If the users are not already in the session, they will be added to it.</param>
		/// <param name="steamid">(Optional) steamid to make the request on behalf of -- if specified, the user must be in the session and all users being added to the session must be friends with the user.</param>
		public static string UpdateSession(ulong sessionid, uint appid, string title, string users, ulong steamid)
		{
			string url = $"{Url}/UpdateSession/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// IInventoryService
	/// </summary>
	public partial class IInventoryService
	{
		private const string Url = "http://api.steampowered.com/IInventoryService/";
		
		/// <param name="appid"></param>
		/// <param name="itemdefid"></param>
		/// <param name="itempropsjson"></param>
		/// <param name="steamid"></param>
		/// <param name="notify">Should notify the user that the item was added to their Steam Inventory.</param>
		public static string AddItem(uint appid, ulong itemdefid, string itempropsjson, ulong steamid, bool notify)
		{
			string url = $"{Url}/AddItem/v0001/";
			return url;
		}
		
		/// <param name="appid"></param>
		/// <param name="itemdefid"></param>
		/// <param name="itempropsjson"></param>
		/// <param name="steamid"></param>
		/// <param name="notify">Should notify the user that the item was added to their Steam Inventory.</param>
		public static string AddPromoItem(uint appid, ulong itemdefid, string itempropsjson, ulong steamid, bool notify)
		{
			string url = $"{Url}/AddPromoItem/v0001/";
			return url;
		}
		
		/// <param name="appid"></param>
		/// <param name="steamid"></param>
		/// <param name="materialsitemid"></param>
		/// <param name="materialsquantity"></param>
		/// <param name="outputitemdefid"></param>
		public static string ExchangeItem(uint appid, ulong steamid, ulong materialsitemid, uint materialsquantity, ulong outputitemdefid)
		{
			string url = $"{Url}/ExchangeItem/v0001/";
			return url;
		}
		
		/// <param name="appid"></param>
		/// <param name="steamid"></param>
		public static string GetInventory(uint appid, ulong steamid)
		{
			string url = $"{Url}GetInventory/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&steamid={steamid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="appid"></param>
		/// <param name="modifiedsince"></param>
		/// <param name="itemdefids"></param>
		/// <param name="workshopids"></param>
		/// <param name="cache_max_age_seconds">Allow stale data to be returned for the specified number of seconds.</param>
		public static string GetItemDefs(uint appid, string modifiedsince, ulong itemdefids, ulong workshopids, uint cache_max_age_seconds)
		{
			string url = $"{Url}GetItemDefs/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&modifiedsince={modifiedsince}&itemdefids={itemdefids}&workshopids={workshopids}&cache_max_age_seconds={cache_max_age_seconds}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="ecurrency"></param>
		public static string GetPriceSheet(int ecurrency)
		{
			string url = $"{Url}GetPriceSheet/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&ecurrency={ecurrency}";
			return Utility.WebGet<string>( url );
		}
		
	}
	
	/// <summary>
	/// IEconMarketService
	/// </summary>
	public partial class IEconMarketService
	{
		private const string Url = "http://api.steampowered.com/IEconMarketService/";
		
		/// <param name="appid">The app making the request</param>
		/// <param name="steamid">The SteamID of the user whose listings should be canceled</param>
		/// <param name="synchronous">Whether or not to wait until all listings have been canceled before returning the response</param>
		public static string CancelAppListingsForUser(uint appid, ulong steamid, bool synchronous)
		{
			string url = $"{Url}/CancelAppListingsForUser/v0001/";
			return url;
		}
		
		/// <param name="appid">The app that's asking. Must match the app of the listing and must belong to the publisher group that owns the API key making the request</param>
		/// <param name="listingid">The identifier of the listing to get information for</param>
		public static string GetAssetID(uint appid, ulong listingid)
		{
			string url = $"{Url}GetAssetID/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&listingid={listingid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">The SteamID of the user to check</param>
		public static string GetMarketEligibility(ulong steamid)
		{
			string url = $"{Url}GetMarketEligibility/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&steamid={steamid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="language">The language to use in item descriptions</param>
		/// <param name="rows">Number of rows per page</param>
		/// <param name="start">The result number to start at</param>
		/// <param name="filter_appid">If present, the app ID to limit results to</param>
		/// <param name="ecurrency">If present, prices returned will be represented in this currency</param>
		public static string GetPopular(string language, uint rows, uint start, uint filter_appid, uint ecurrency)
		{
			string url = $"{Url}GetPopular/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&language={language}&rows={rows}&start={start}&filter_appid={filter_appid}&ecurrency={ecurrency}";
			return Utility.WebGet<string>( url );
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
			string url = $"{Url}/CallPublisherKey/v0001/";
			return url;
		}
		
		/// <param name="appid"></param>
		public static string CallPublisherKeyOwnsApp(uint appid)
		{
			string url = $"{Url}/CallPublisherKeyOwnsApp/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// ICheatReportingService
	/// </summary>
	public partial class ICheatReportingService
	{
		private const string Url = "http://api.steampowered.com/ICheatReportingService/";
		
		/// <param name="steamid">steamid of the user.</param>
		/// <param name="appid">The appid the user is playing.</param>
		/// <param name="session_id">session id</param>
		public static string EndSecureMultiplayerSession(ulong steamid, uint appid, ulong session_id)
		{
			string url = $"{Url}/EndSecureMultiplayerSession/v0001/";
			return url;
		}
		
		/// <param name="appid">The appid.</param>
		/// <param name="timeend">The beginning of the time range .</param>
		/// <param name="timebegin">The end of the time range.</param>
		/// <param name="reportidmin">Minimum reportID to include</param>
		/// <param name="includereports">(Optional) Include reports.</param>
		/// <param name="includebans">(Optional) Include ban requests.</param>
		/// <param name="steamid">(Optional) Query just for this steamid.</param>
		public static string GetCheatingReports(uint appid, uint timeend, uint timebegin, ulong reportidmin, bool includereports, bool includebans, ulong steamid)
		{
			string url = $"{Url}GetCheatingReports/v0001/?key={Facepunch.SteamApi.Config.Key}&format=json&appid={appid}&timeend={timeend}&timebegin={timebegin}&reportidmin={reportidmin}&includereports={includereports}&includebans={includebans}&steamid={steamid}";
			return Utility.WebGet<string>( url );
		}
		
		/// <param name="steamid">steamid of the user who is reported as cheating.</param>
		/// <param name="appid">The appid.</param>
		public static string RemovePlayerGameBan(ulong steamid, uint appid)
		{
			string url = $"{Url}/RemovePlayerGameBan/v0001/";
			return url;
		}
		
		/// <param name="steamid">steamid of the user running and reporting the cheat.</param>
		/// <param name="appid">The appid.</param>
		/// <param name="pathandfilename">path and file name of the cheat executable.</param>
		/// <param name="webcheaturl">web url where the cheat was found and downloaded.</param>
		/// <param name="time_now">local system time now.</param>
		/// <param name="time_started">local system time when cheat process started. ( 0 if not yet run )</param>
		/// <param name="time_stopped">local system time when cheat process stopped. ( 0 if still running )</param>
		/// <param name="cheatname">descriptive name for the cheat.</param>
		/// <param name="game_process_id">process ID of the running game.</param>
		/// <param name="cheat_process_id">process ID of the cheat process that ran</param>
		/// <param name="cheat_param_1">cheat param 1</param>
		/// <param name="cheat_param_2">cheat param 2</param>
		public static string ReportCheatData(ulong steamid, uint appid, string pathandfilename, string webcheaturl, ulong time_now, ulong time_started, ulong time_stopped, string cheatname, uint game_process_id, uint cheat_process_id, ulong cheat_param_1, ulong cheat_param_2)
		{
			string url = $"{Url}/ReportCheatData/v0001/";
			return url;
		}
		
		/// <param name="steamid">steamid of the user who is reported as cheating.</param>
		/// <param name="appid">The appid.</param>
		/// <param name="steamidreporter">(Optional) steamid of the user or game server who is reporting the cheating.</param>
		/// <param name="appdata">(Optional) App specific data about the cheating.</param>
		/// <param name="heuristic">(Optional) extra information about the source of the cheating - was it a heuristic.</param>
		/// <param name="detection">(Optional) extra information about the source of the cheating - was it a detection.</param>
		/// <param name="playerreport">(Optional) extra information about the source of the cheating - was it a player report.</param>
		/// <param name="noreportid">(Optional) dont return report id</param>
		/// <param name="gamemode">(Optional) extra information about state of game - was it a specific type of game play (0 = generic)</param>
		/// <param name="suspicionstarttime">(Optional) extra information indicating how far back the game thinks is interesting for this user</param>
		/// <param name="severity">(Optional) level of severity of bad action being reported</param>
		public static string ReportPlayerCheating(ulong steamid, uint appid, ulong steamidreporter, ulong appdata, bool heuristic, bool detection, bool playerreport, bool noreportid, uint gamemode, uint suspicionstarttime, uint severity)
		{
			string url = $"{Url}/ReportPlayerCheating/v0001/";
			return url;
		}
		
		/// <param name="steamid">steamid of the user who is reported as cheating.</param>
		/// <param name="appid">The appid.</param>
		/// <param name="reportid">The reportid originally used to report cheating.</param>
		/// <param name="cheatdescription">Text describing cheating infraction.</param>
		/// <param name="duration">Ban duration requested in seconds.</param>
		/// <param name="delayban">Delay the ban according to default ban delay rules.</param>
		/// <param name="flags">Additional information about the ban request.</param>
		public static string RequestPlayerGameBan(ulong steamid, uint appid, ulong reportid, string cheatdescription, uint duration, bool delayban, uint flags)
		{
			string url = $"{Url}/RequestPlayerGameBan/v0001/";
			return url;
		}
		
		/// <param name="steamid">steamid of the user.</param>
		/// <param name="appid">The appid the user is playing.</param>
		/// <param name="session_id">session id</param>
		public static string RequestVacStatusForUser(ulong steamid, uint appid, ulong session_id)
		{
			string url = $"{Url}/RequestVacStatusForUser/v0001/";
			return url;
		}
		
		/// <param name="steamid">steamid of the user.</param>
		/// <param name="appid">The appid the user is playing.</param>
		public static string StartSecureMultiplayerSession(ulong steamid, uint appid)
		{
			string url = $"{Url}/StartSecureMultiplayerSession/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// IAccountRecoveryService
	/// </summary>
	public partial class IAccountRecoveryService
	{
		private const string Url = "http://api.steampowered.com/IAccountRecoveryService/";
		
		/// <param name="loginuser_list"></param>
		/// <param name="install_config"></param>
		/// <param name="shasentryfile"></param>
		/// <param name="machineid"></param>
		public static string ReportAccountRecoveryData(string loginuser_list, string install_config, string shasentryfile, string machineid)
		{
			string url = $"{Url}/ReportAccountRecoveryData/v0001/";
			return url;
		}
		
		/// <param name="requesthandle"></param>
		public static string RetrieveAccountRecoveryData(string requesthandle)
		{
			string url = $"{Url}/RetrieveAccountRecoveryData/v0001/";
			return url;
		}
		
	}
	
	/// <summary>
	/// IWorkshopService
	/// </summary>
	public partial class IWorkshopService
	{
		private const string Url = "http://api.steampowered.com/IWorkshopService/";
		
		/// <param name="appid"></param>
		/// <param name="gameitemid"></param>
		public static string GetFinalizedContributors(uint appid, uint gameitemid)
		{
			string url = $"{Url}/GetFinalizedContributors/v0001/";
			return url;
		}
		
		/// <param name="appid"></param>
		/// <param name="item_id"></param>
		/// <param name="date_start"></param>
		/// <param name="date_end"></param>
		public static string GetItemDailyRevenue(uint appid, uint item_id, uint date_start, uint date_end)
		{
			string url = $"{Url}/GetItemDailyRevenue/v0001/";
			return url;
		}
		
		/// <param name="appid"></param>
		/// <param name="languages"></param>
		public static string PopulateItemDescriptions(uint appid, string languages)
		{
			string url = $"{Url}/PopulateItemDescriptions/v0001/";
			return url;
		}
		
		/// <param name="appid"></param>
		/// <param name="gameitemid"></param>
		/// <param name="associated_workshop_files"></param>
		/// <param name="partner_accounts"></param>
		/// <param name="validate_only">Only validates the rules and does not persist them.</param>
		/// <param name="make_workshop_files_subscribable"></param>
		public static string SetItemPaymentRules(uint appid, uint gameitemid, string associated_workshop_files, string partner_accounts, bool validate_only, bool make_workshop_files_subscribable)
		{
			string url = $"{Url}/SetItemPaymentRules/v0001/";
			return url;
		}
		
	}
	
}
