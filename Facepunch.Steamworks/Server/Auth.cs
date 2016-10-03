using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facepunch.Steamworks.Callbacks.User;

namespace Facepunch.Steamworks
{
    public partial class Server
    {
        ServerAuth _auth;

        public ServerAuth Auth
        {
            get
            {
                if ( _auth == null )
                    _auth = new ServerAuth( this );

                return _auth;
            }
        }
    }

    public class ServerAuth
    {
        internal Server server;

        /// <summary>
        /// Steamid, Ownerid, Status
        /// </summary>
        public Action<ulong, ulong, Status> OnAuthChange;

        /// <summary>
        /// Steam authetication statuses
        /// </summary>
        public enum Status : int
        {
            /// <summary>
            /// Steam has verified the user is online, the ticket is valid and ticket has not been reused.
            /// </summary>
            OK = 0,                           
            UserNotConnectedToSteam = 1,      // The user in question is not connected to steam
            NoLicenseOrExpired = 2,           // The license has expired.
            VACBanned = 3,                    // The user is VAC banned for this game.
            LoggedInElseWhere = 4,            // The user account has logged in elsewhere and the session containing the game instance has been disconnected.
            VACCheckTimedOut = 5,             // VAC has been unable to perform anti-cheat checks on this user
            AuthTicketCanceled = 6,           // The ticket has been canceled by the issuer
            AuthTicketInvalidAlreadyUsed = 7, // This ticket has already been used, it is not valid.
            AuthTicketInvalid = 8,            // This ticket is not from a user instance currently connected to steam.
            PublisherIssuedBan = 9,           // The user is banned for this game. The ban came via the web api and not VAC
        }

        internal ServerAuth( Server s )
        {
            server = s;

            server.CallResult<ValidateAuthTicketResponse>( OnAuthTicketValidate, ValidateAuthTicketResponse.CallbackId );
        }

        void OnAuthTicketValidate( ValidateAuthTicketResponse data )
        {
            if ( OnAuthChange != null )
                OnAuthChange( data.SteamID, data.OwnerSteamID, (Status) data.AuthResponse );
        }

        /// <summary>
        /// Start authorizing a ticket. This user isn't authorized yet. Wait for a call to OnAuthChange.
        /// </summary>
        public unsafe bool StartSession( byte[] data, ulong steamid )
        {
            fixed ( byte* p = data )
            {
                var result = (Valve.Steamworks.EBeginAuthSessionResult) server.native.gameServer.BeginAuthSession( (IntPtr)p, data.Length, steamid );

                if ( result == Valve.Steamworks.EBeginAuthSessionResult.k_EBeginAuthSessionResultOK )
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Forget this guy. They're no longer in the game.
        /// </summary>
        public void EndSession( ulong steamid )
        {
            server.native.gameServer.EndAuthSession( steamid );
        }

    }
}
