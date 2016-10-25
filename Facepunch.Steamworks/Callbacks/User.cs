using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks.Callbacks.User
{
    [StructLayout( LayoutKind.Sequential, Pack = 4 )]
    internal struct ValidateAuthTicketResponse
    {
        public ulong SteamID;
        public int AuthResponse;
        public ulong OwnerSteamID;

        public const int CallbackId = Index.User + 43;

        public enum Response : int
        {
            Okay = 0,                         // Steam has verified the user is online, the ticket is valid and ticket has not been reused.
            UserNotConnectedToSteam = 1,      // The user in question is not connected to steam
            NoLicenseOrExpired = 2,           // The license has expired.
            VACBanned = 3,                    // The user is VAC banned for this game.
            LoggedInElseWhere = 4,            // The user account has logged in elsewhere and the session containing the game instance has been disconnected.
            VACCheckTimedOut = 5,             // VAC has been unable to perform anti-cheat checks on this user
            AuthTicketCanceled = 6,           // The ticket has been canceled by the issuer
            AuthTicketInvalidAlreadyUsed = 7, // This ticket has already been used, it is not valid.
            AuthTicketInvalid = 8,            // This ticket is not from a user instance currently connected to steam.
            PublisherIssuedBan = 9,           // The user is banned for this game. The ban came via the web api and not VAC
        };
    };


}
