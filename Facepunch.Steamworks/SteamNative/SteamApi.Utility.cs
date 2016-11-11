using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public static T WebPost<T>( string url, System.Collections.Generic.Dictionary<string, object> variables )
        {
            var www = new System.Net.WebClient();

            var nvc = new NameValueCollection();

            if ( Config.Key != null )
                variables.Add( "key", Config.Key );

            foreach ( var v in variables )
            {
                nvc.Add( v.Key, v.Value.ToString() );
            }

            var bytes = www.UploadValues( url, "POST", nvc );
            var data = System.Text.Encoding.UTF8.GetString( bytes );

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
