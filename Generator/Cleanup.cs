using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


public static class Cleanup
{
	public static string ConvertType( string type )
	{
		type = type.Replace( "class ", "" );
		type = type.Replace( "struct ", "" );

		type = type.Replace( "unsigned long long", "uint64" );
		type = type.Replace( "unsigned int", "uint" );
		type = type.Replace( "uint32", "uint" );

		type = type.Replace( "CSteamID", "SteamId" );
		type = type.Replace( "CGameID", "GameId" );
		type = type.Replace( "PersonaState", "FriendState" );
		type = type.Replace( "AudioPlayback_Status", "MusicStatus" );
		type = type.Replace( "AuthSessionResponse", "AuthResponse" );
		type = type.Replace( "FriendRelationship", "Relationship" );
		type = type.Replace( "BeginAuthSessionResult", "BeginAuthResult" );
		type = type.Replace( "PublishedFileId_t", "PublishedFileId" );
		type = type.Replace( "PublishedFileId_t", "PublishedFileId" );
		type = type.Replace( "AppId_t", "AppId" );
		type = type.Replace( "LeaderboardSortMethod", "LeaderboardSort" );
		type = type.Replace( "LeaderboardDisplayType", "LeaderboardDisplay" );
		type = type.Replace( "UGCMatchingUGCType", "UgcType" );
		type = type.Replace( "SteamItemInstanceID_t", "InventoryItemId" );
		type = type.Replace( "SteamItemDef_t", "InventoryDefId" );
		type = type.Replace( "ChatRoomEnterResponse", "RoomEnter" );
		type = type.Replace( "SteamNetworkPingLocation_t", "PingLocation" );
		type = type.Replace( "SteamNetworkingConfigValue_t", "NetKeyValue" );
		type = type.Replace( "SteamNetworkingConfigValue", "NetConfig" );
		type = type.Replace( "SteamNetworkingConfigScope", "NetConfigScope" );
		type = type.Replace( "SteamNetworkingConfigDataType", "NetConfigType" );
		type = type.Replace( "HSteamNetConnection", "Connection" );
		type = type.Replace( "HSteamListenSocket", "Socket" );
		type = type.Replace( "SteamNetworkingIPAddr", "NetAddress" );
		type = type.Replace( "SteamNetworkingIdentity", "NetIdentity" );
		type = type.Replace( "SteamNetConnectionInfo_t", "ConnectionInfo" );
		type = type.Replace( "SteamNetworkingConnectionState", "ConnectionState" );
		type = type.Replace( "SteamNetworkingMicroseconds", "long" );
		type = type.Replace( "SteamNetworkingSocketsDebugOutputType", "DebugOutputType" );
		type = type.Replace( "SteamNetworkingGetConfigValueResult", "NetConfigResult" );
		type = type.Replace( "SteamInputType", "InputType" );
		type = type.Replace( "InputDigitalActionData_t", "DigitalState" );
		type = type.Replace( "InputAnalogActionData_t", "AnalogState" );
		type = type.Replace( "InputMotionData_t", "MotionState" );
		type = type.Replace( "MatchMakingKeyValuePair_t", "MatchMakingKeyValuePair" );
		type = type.Replace( "ISteamNetworkingMessage", "NetMsg" );
		type = type.Replace( "SteamNetworkingMessage_t", "NetMsg" );

		type = type.Replace( "::", "." );

		return type;
	}

	public static bool ShouldCreate( string type )
	{
		if ( type == "SteamId" ) return false;
		if ( type == "LeaderboardSort" ) return false;
		if ( type == "LeaderboardDisplay" ) return false;
		if ( type == "AppId" ) return false;
		if ( type == "AnalogState" ) return false;
		if ( type == "DigitalState" ) return false;
		if ( type == "MotionState" ) return false;
		if ( type == "MatchMakingKeyValuePair" ) return false;
		if ( type == "Connection" ) return false;
		if ( type == "Socket" ) return false;
		if ( type == "SteamNetworkingMicroseconds" ) return false;
		if ( type == "FSteamNetworkingSocketsDebugOutput" ) return false;
		if ( type == "NetMsg" ) return false;
		if ( type == "SteamDatagramErrMsg" ) return false;
		if ( type == "ConnectionInfo" ) return false;
		if ( type == "SteamNetworkingIPAddr" ) return false;
		if ( type == "NetAddress" ) return false;
		if ( type == "NetIdentity" ) return false;
		if ( type == "SteamNetworkingQuickConnectionStatus" ) return false;
		if ( type == "SteamNetworkingErrMsg" ) return false;
		if ( type == "NetKeyValue" ) return false;

		return true;
	}

	internal static string Expose( string name )
	{
		if ( name == "FriendState" ) return "public";
		if (name == "FriendFlags") return "public";
		if ( name == "MusicStatus" ) return "public";
		if ( name == "ParentalFeature" ) return "public";
		if ( name == "AuthResponse" ) return "public";
		if ( name == "Relationship" ) return "public";
		if ( name == "BeginAuthResult" ) return "public";
		if ( name == "Universe" ) return "public";
		if ( name == "NotificationPosition" ) return "public";
		if ( name == "GamepadTextInputMode" ) return "public";
		if ( name == "GamepadTextInputLineMode" ) return "public";
		if ( name == "CheckFileSignature" ) return "public";
		if ( name == "BroadcastUploadResult" ) return "public";
		if ( name == "PublishedFileId" ) return "public";
		if ( name == "Result" ) return "public";
		if ( name == "UgcType" ) return "public";
		if ( name == "InventoryItemId" ) return "public";
		if ( name == "InventoryDefId" ) return "public";
		if ( name == "P2PSend" ) return "public";
		if ( name == "RoomEnter" ) return "public";
		if ( name == "P2PSessionError" ) return "public";
		if ( name == "InputType" ) return "public";
		if ( name == "InputSourceMode" ) return "public";
		if ( name == "UserHasLicenseForAppResult" ) return "public";
		if ( name == "PingLocation" ) return "public";
		if ( name == "ConnectionState" ) return "public";

		return "internal";
	}

	internal static bool IsDeprecated( string name )
	{
		if ( name.StartsWith( "ISteamRemoteStorage." ) )
		{
			if ( name.Contains( "Publish" ) ) return true;
			if ( name.Contains( "ResetFileRequestState" ) ) return true;
			if ( name.Contains( "EnumerateUserSubscribedFiles" ) ) return true;
			if ( name.Contains( "EnumerateUserSharedWorkshopFile" ) ) return true;
		}

		return false;
	}
}
