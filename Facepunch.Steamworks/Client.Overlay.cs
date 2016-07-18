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
