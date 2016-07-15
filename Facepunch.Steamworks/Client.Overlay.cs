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
    
        public void OpenProfile( ulong steamid ) { client.native.friends.ActivateGameOverlayToUser( "steamid", steamid ); }
        public void OpenChat( ulong steamid ){ client.native.friends.ActivateGameOverlayToUser( "chat", steamid ); }
        public void OpenTrade( ulong steamid ) { client.native.friends.ActivateGameOverlayToUser( "jointrade", steamid ); }
        public void OpenStats( ulong steamid ) { client.native.friends.ActivateGameOverlayToUser( "stats", steamid ); }
        public void OpenAchievements( ulong steamid ) { client.native.friends.ActivateGameOverlayToUser( "achievements", steamid ); }

        public void AddFriend( ulong steamid ) { client.native.friends.ActivateGameOverlayToUser( "friendadd", steamid ); }
        public void RemoveFriend( ulong steamid ) { client.native.friends.ActivateGameOverlayToUser( "friendremove", steamid ); }
        public void AcceptFriendRequest( ulong steamid ) { client.native.friends.ActivateGameOverlayToUser( "friendrequestaccept", steamid ); }
        public void IgnoreFriendRequest( ulong steamid ) { client.native.friends.ActivateGameOverlayToUser( "friendrequestignore", steamid ); }

        public void OpenUrl( string url ) { client.native.friends.ActivateGameOverlayToWebPage( url ); }
    }
}
