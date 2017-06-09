using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    public class User : IDisposable
    {
        internal Client client;
        internal Dictionary<string, string> richPresence = new Dictionary<string, string>();

        internal User( Client c )
        {
            client = c;
        }

        public void Dispose()
        {
            client = null;
        }

        public string GetRichPresence( string key )
        {
            string val = null;

            if ( richPresence.TryGetValue( key, out val ) )
                return val;

            return null;
        }

        public void SetRichPresence( string key, string value )
        {
            richPresence[key] = value;
            client.native.friends.SetRichPresence( key, value );
        }

        public void ClearRichPresence()
        {
            richPresence.Clear();
            client.native.friends.ClearRichPresence();
        }
    }
}
