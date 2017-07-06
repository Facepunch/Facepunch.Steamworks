using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public class App : IDisposable
    {
        internal Client client;

        internal App( Client c )
        {
            client = c;
        }

        public void Dispose()
        {
            client = null;
        }

        /// <summary>
        /// Mark the content as corrupt, so it will validate the downloaded files
        /// once the app is closed. This is good to call when you detect a crash happening
        /// or a file is missing that is meant to be there.
        /// </summary>
        public void MarkContentCorrupt( bool missingFilesOnly = false )
        {
            client.native.apps.MarkContentCorrupt( missingFilesOnly );
        }

        /// <summary>
        /// Tell steam to install the Dlc specified by the AppId
        /// </summary>
        public void InstallDlc( uint appId )
        {
            client.native.apps.InstallDLC( appId );
        }

        /// <summary>
        /// Tell steam to uninstall the Dlc specified by the AppId
        /// </summary>
        public void UninstallDlc(uint appId)
        {
            client.native.apps.UninstallDLC( appId );
        }

        /// <summary>
        /// Get the purchase time for this appid. Will return DateTime.MinValue if none.
        /// </summary>
        public DateTime PurchaseTime(uint appId)
        {
            var time = client.native.apps.GetEarliestPurchaseUnixTime(appId);
            if ( time == 0 ) return DateTime.MinValue;

            return Utility.Epoch.ToDateTime( time );
        }

        /// <summary>
        /// Checks if the active user is subscribed to a specified AppId.
        /// Only use this if you need to check ownership of another game related to yours, a demo for example.
        /// </summary>
        public bool IsSubscribed(uint appId)
        {
            return client.native.apps.BIsSubscribedApp(appId);
        }

        /// <summary>
        /// Returns true if specified app is installed.
        /// </summary>
        public bool IsInstalled(uint appId)
        {
            return client.native.apps.BIsAppInstalled(appId);
        }
    }
}
