using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        private Overlay _overlay;

        public Overlay Overlay
        {
            get
            {
                if ( _overlay == null )
                    _overlay = new Overlay { client = this };

                return _overlay;
            }
        }
    }

    public class Overlay
    {
        internal Client client;

        /// <summary>
        /// Returns true if the Steam Overlay is actually available, false if it was not injected properly.
        /// Note that it may take some time after Steam_Init() until the overlay injection is complete.
        /// Also note that the overlay will only work if the Steam API is initialized before the rendering device is initialized. That means that 
        /// for Unity3D games the overlay is only available if the game was launched directly through Steam. Calling Steam_Init() from Mono
        /// code is too late.  
        /// </summary>
        public bool Enabled
        {
            get { return client.native != null ? client.native.utils.IsOverlayEnabled() : false; }
        }

        public void OpenUserPage( string name, ulong steamid ) { client.native.friends.ActivateGameOverlayToUser( name, steamid ); }

        public void OpenProfile( ulong steamid ) { OpenUserPage( "steamid", steamid ); }
        public void OpenChat( ulong steamid ){ OpenUserPage( "chat", steamid ); }
        public void OpenTrade( ulong steamid ) { OpenUserPage( "jointrade", steamid ); }
        public void OpenStats( ulong steamid ) { OpenUserPage( "stats", steamid ); }
        public void OpenAchievements( ulong steamid ) { OpenUserPage( "achievements", steamid ); }
        public void AddFriend( ulong steamid ) { OpenUserPage( "friendadd", steamid ); }
        public void RemoveFriend( ulong steamid ) { OpenUserPage( "friendremove", steamid ); }
        public void AcceptFriendRequest( ulong steamid ) { OpenUserPage( "friendrequestaccept", steamid ); }
        public void IgnoreFriendRequest( ulong steamid ) { OpenUserPage( "friendrequestignore", steamid ); }

        public void OpenUrl( string url ) { client.native.friends.ActivateGameOverlayToWebPage( url ); }
    }
}
