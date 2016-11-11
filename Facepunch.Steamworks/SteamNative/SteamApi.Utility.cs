using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace Facepunch.SteamApi
{
	public class Utility
	{
        public static T WebGet<T>( string url )
        {
            var www = new System.Net.WebClient();
            var data = www.DownloadString( url );
            www.Dispose();

            if ( typeof( T ) == typeof( string ) )
                return (T)(object)data;

            var response = DeserializeJson<ApiResponse<T>>( data );



            if ( response.Response == null )
                return response.Result;

            return response.Response;
        }

        private static T DeserializeJson<T>( string data )
        {
            return (T)Facepunch.SteamApi.Config.DeserializeJson( data, typeof( T ) );
        }




	}
}
