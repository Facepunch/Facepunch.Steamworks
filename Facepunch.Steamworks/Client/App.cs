using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        App _app;

        public App App
        {
            get
            {
                if ( _app == null )
                    _app = new App( this );

                return _app;
            }
        }
    }

    public class App
    {
        internal Client client;

        internal App( Client c )
        {
            client = c;
        }

        public void MarkContentCorrupt( bool missingFilesOnly = false )
        {
            client.native.apps.MarkContentCorrupt( missingFilesOnly );
        }

        /// <summary>
        /// Returns the current BuildId of the game. 
        /// This is pretty useless, as it isn't guarenteed to return
        /// the build id you're playing, or the latest build id.
        /// </summary>
        public int BuildId
        {
            get
            {
                return client.native.apps.GetAppBuildId();
            }
        }

    }
}
