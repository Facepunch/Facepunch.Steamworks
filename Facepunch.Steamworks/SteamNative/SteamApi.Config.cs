using System;
using System.Runtime.InteropServices;

namespace Facepunch
{
    namespace SteamApi
    {
        /// <summary>
        /// Static class to configure SteamApi
        /// </summary>
        public static class Config
        {
            /// <summary>
            /// Your Steam Api Key is secret. 
            /// Don't ship it with your game. 
            /// Treat it like a password.
            /// </summary>
            public static string Key;

            /// <summary>
            /// Plug your json deserializer in here. We don't force one on you, this is left
            /// up to the implementation. We've only tested Newtonsoft.Json though. Here's an example of
            /// how to set it up.
            /// </summary>
            /// <example>
            ///
            /// Facepunch.SteamApi.Config.DeserializeJson = ( str, type ) =>
            /// {
            ///     return Newtonsoft.Json.JsonConvert.DeserializeObject( str, type );
            /// };
            /// 
            /// </example>
            public static Func<string, Type, object> DeserializeJson;
        }
    }
}