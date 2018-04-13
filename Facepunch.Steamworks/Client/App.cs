using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    public class App : IDisposable
    {
        internal Client client;

        internal App( Client c )
        {
            client = c;

            client.RegisterCallback<SteamNative.DlcInstalled_t>( DlcInstalled );
        }

        public delegate void DlcInstalledDelegate( uint appid );

        /// <summary>
        /// Triggered after the current user gains ownership of DLC and that DLC is installed.
        /// </summary>
        public event DlcInstalledDelegate OnDlcInstalled;

        private void DlcInstalled( DlcInstalled_t data )
        {
            if ( OnDlcInstalled  != null )
            {
                OnDlcInstalled( data.AppID );
            }
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

        /// <summary>
        /// Returns true if specified app is installed.
        /// </summary>
        public bool IsDlcInstalled( uint appId )
        {
            return client.native.apps.BIsDlcInstalled( appId );
        }

        /// <summary>
        /// Returns the appid's name
        /// Returns error if the current app Id does not have permission to use this interface
        /// </summary>
        public string GetName( uint appId )
        {
            var str = client.native.applist.GetAppName( appId );
            if ( str == null ) return "error";
            return str;
        }

        /// <summary>
        /// Returns the app's install folder
        /// Returns error if the current app Id does not have permission to use this interface
        /// </summary>
        public string GetInstallFolder( uint appId )
        {
            return client.native.applist.GetAppInstallDir( appId );
        }

        /// <summary>
        /// Returns the app's current build id
        /// Returns 0 if the current app Id does not have permission to use this interface
        /// </summary>
        public int GetBuildId( uint appId )
        {
            return client.native.applist.GetAppBuildId( appId );
        }
    }
}
