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
		type = type.Replace( "int32_t", "int" );
		type = type.Replace( "int64_t", "long" );
		type = type.Replace( "uint16", "ushort" );

		type = type.Replace( "CSteamID", "SteamId" );
		type = type.Replace( "CGameID", "GameId" );
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
		type = type.Replace( "SteamNetworkPingLocation_t", "NetPingLocation" );
		type = type.Replace( "SteamNetworkingConfigValue_t", "NetKeyValue" );
		type = type.Replace( "SteamNetworkingConfigValue", "NetConfig" );
		type = type.Replace( "SteamNetworkingConfigScope", "NetConfigScope" );
		type = type.Replace( "SteamNetworkingConfigDataType", "NetConfigType" );
		type = type.Replace( "FSteamNetworkingSocketsDebugOutput", "NetDebugFunc" );
		type = type.Replace( "SteamNetworkingErrMsg", "NetErrorMessage" );
		type = type.Replace( "SteamNetConnectionEnd", "NetConnectionEnd" );
		type = type.Replace( "HSteamNetConnection", "Connection" );
		type = type.Replace( "HSteamListenSocket", "Socket" );
		type = type.Replace( "SteamNetworkingIPAddr", "NetAddress" );
		type = type.Replace( "SteamNetworkingIdentity", "NetIdentity" );
		type = type.Replace( "SteamNetConnectionInfo_t", "ConnectionInfo" );
		type = type.Replace( "SteamNetworkingConnectionState", "ConnectionState" );
		type = type.Replace( "SteamNetworkingMicroseconds", "long" );
		type = type.Replace( "SteamNetworkingSocketsDebugOutputType", "NetDebugOutput" );
		type = type.Replace( "SteamNetworkingGetConfigValueResult", "NetConfigResult" );
		type = type.Replace( "SteamInputType", "InputType" );
		type = type.Replace( "InputDigitalActionData_t", "DigitalState" );
		type = type.Replace( "InputAnalogActionData_t", "AnalogState" );
		type = type.Replace( "InputMotionData_t", "MotionState" );
		type = type.Replace( "MatchMakingKeyValuePair_t", "MatchMakingKeyValuePair" );
		type = type.Replace( "ISteamNetworkingMessage", "NetMsg" );
		type = type.Replace( "SteamNetworkingMessage_t", "NetMsg" );
		type = type.Replace( "SteamIPAddress_t", "SteamIPAddress" );
		type = type.Replace( "SteamNetConnectionRealTimeStatus_t", "ConnectionStatus" );
		type = type.Replace( "SteamNetConnectionRealTimeLaneStatus_t", "ConnectionLaneStatus" );
		type = type.Replace( "SteamInputGlyphSize", "GlyphSize" );
		type = type.Replace( "FloatingGamepadTextInputMode", "TextInputMode" );

		type = type.Replace( "::", "." );

		if ( type == "EPersonaState" ) return "EFriendState";
		if ( type == "PersonaState" ) return "FriendState";

		return type;
	}

	public static bool ShouldCreate( string type )
	{
		if ( IsDeprecated( type ) ) return false;
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
		if ( type == "NetDebugFunc" ) return false;
		if ( type == "NetMsg" ) return false;
		if ( type == "SteamDatagramErrMsg" ) return false;
		if ( type == "ConnectionInfo" ) return false;
		if ( type == "SteamNetworkingIPAddr" ) return false;
		if ( type == "NetAddress" ) return false;
		if ( type == "NetIdentity" ) return false;
		if ( type == "SteamNetworkingErrMsg" ) return false;
		if ( type == "NetKeyValue" ) return false;
		if ( type == "SteamIPAddress" ) return false;
		if ( type == "NetPingLocation" ) return false;
		if ( type == "CSteamID" ) return false;
		if ( type == "CSteamAPIContext" ) return false;
		if ( type == "CCallResult" ) return false;
		if ( type == "CCallback" ) return false;
		if ( type == "ValvePackingSentinel_t" ) return false;
		if ( type == "CCallbackBase" ) return false;
		if ( type == "CSteamGameServerAPIContext" ) return false;
		if ( type == "ConnectionStatus") return false;
		if ( type == "ConnectionLaneStatus" ) return false;
		if ( type == "SteamInputActionEventCallbackPointer" ) return false;
		if ( type.StartsWith( "FnSteam" ) ) return false;

		return true;
	}

	internal static string Expose( string name )
	{
		if ( name == "FriendState" ) return "public";
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
		if ( name == "NetPingLocation" ) return "public";
		if ( name == "ConnectionState" ) return "public";
		if ( name == "SteamNetworkingAvailability" ) return "public";
		if ( name == "SteamDeviceFormFactor" ) return "public";
		if ( name == "DurationControlProgress" ) return "public";
		if ( name == "NetConnectionEnd" ) return "public";
		if ( name == "NetIdentity" ) return "public";
		if ( name == "NetAddress" ) return "public";
		if ( name == "NetDebugOutput" ) return "public";
		if ( name == "ItemPreviewType" ) return "public";
		if ( name == "OverlayToStoreFlag" ) return "public";
		if ( name == "TextFilteringContext" ) return "public";
		if ( name == "GlyphSize" ) return "public";
		if ( name == "TextInputMode" ) return "public";

		return "internal";
	}

	internal static bool IsDeprecated( string name )
	{
		if ( name.StartsWith( "PS3" ) ) return true;

		if ( name == "SocketStatusCallback_t" ) return true;
		if ( name == "SNetSocketConnectionType" ) return true;
		if ( name == "SNetSocketState" ) return true;

		
		if ( name.StartsWith( "ISteamInput." ) )
		{
			if ( name.Contains( "EnableActionEventCallbacks" ) ) return true;
		}

		if ( name.StartsWith( "ISteamRemoteStorage." ) )
		{
			if ( name.Contains( "Publish" ) ) return true;
			if ( name.Contains( "ResetFileRequestState" ) ) return true;
			if ( name.Contains( "EnumerateUserSubscribedFiles" ) ) return true;
			if ( name.Contains( "EnumerateUserSharedWorkshopFile" ) ) return true;
		}

		//
		// In ISteamNetworking everything is deprecated apart from the p2p stuff
		//
		if ( name.StartsWith( "ISteamNetworking." ) )
		{
			if ( !name.Contains( "P2P" ))
				return true;
		}

		if ( name == "ISteamUGC.RequestUGCDetails" ) return true;

		return false;
	}	
	
	//
	// If we start with E[Capital] then strip the E
	// (makes us more C# like)
	//
	internal static string CleanEnum( string name )
	{
		if ( name[0] != 'E' ) return name;
		if ( !char.IsUpper( name[1] ) ) return name;

		return name.Substring( 1 );
	}
}
