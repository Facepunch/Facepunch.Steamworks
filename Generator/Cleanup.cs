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


		return type;
	}

	public static bool ShouldCreate( string type )
	{
		if ( type == "SteamId" ) return false;


		return true;
	}
}
