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

		return type;
	}

	public static bool ShouldCreate( string type )
	{
		if ( type == "SteamId" ) return false;
		if ( type == "LeaderboardSort" ) return false;
		if ( type == "LeaderboardDisplay" ) return false;
		if ( type == "AppId" ) return false;

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

		return "internal";
	}
}
