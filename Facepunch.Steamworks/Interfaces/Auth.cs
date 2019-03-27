using System;
using System.Collections.Generic;
using System.Text;

namespace Facepunch.Steamworks
{
    /// <summary>
    /// Common authentication functionality for servers and clients
    /// </summary>
    public abstract class Auth : IDisposable
    {
        public enum StartAuthResult : int
        {
            OK = SteamNative.BeginAuthSessionResult.OK,
            InvalidTicket = SteamNative.BeginAuthSessionResult.InvalidTicket,
            DuplicateRequest = SteamNative.BeginAuthSessionResult.DuplicateRequest,
            InvalidVersion = SteamNative.BeginAuthSessionResult.InvalidVersion,
            GameMismatch = SteamNative.BeginAuthSessionResult.GameMismatch,
            ExpiredTicket = SteamNative.BeginAuthSessionResult.ExpiredTicket
        }
        
        public enum AuthStatus : int
        {
            OK = SteamNative.AuthSessionResponse.OK,
            UserNotConnectedToSteam = SteamNative.AuthSessionResponse.UserNotConnectedToSteam,
            NoLicenseOrExpired = SteamNative.AuthSessionResponse.NoLicenseOrExpired,
            VACBanned = SteamNative.AuthSessionResponse.VACBanned,
            LoggedInElseWhere = SteamNative.AuthSessionResponse.LoggedInElseWhere,
            VACCheckTimedOut = SteamNative.AuthSessionResponse.VACCheckTimedOut,
            AuthTicketCanceled = SteamNative.AuthSessionResponse.AuthTicketCanceled,
            AuthTicketInvalidAlreadyUsed = SteamNative.AuthSessionResponse.AuthTicketInvalidAlreadyUsed,
            AuthTicketInvalid = SteamNative.AuthSessionResponse.AuthTicketInvalid,
            PublisherIssuedBan = SteamNative.AuthSessionResponse.PublisherIssuedBan
        }

        /// <summary>
        /// This is ran whenever the status of an ongoing session changes
        /// SteamId, OwnerSteamId, Status
        /// </summary>
        public Action<ulong, ulong, AuthStatus> OnAuthChange;

        internal void OnAuthTicketValidate( SteamNative.ValidateAuthTicketResponse_t data )
        {
            if ( OnAuthChange != null )
                OnAuthChange( data.SteamID, data.OwnerSteamID, (AuthStatus) data.AuthSessionResponse );
        }

        /// <summary>
        /// An auth ticket for the local client/server (never remote)
        /// You should not cancel this ticket until you are disconnected from the remote user
        /// </summary>
        public class Ticket : IDisposable
        {
            private Auth _auth;
            public uint Handle { get; }
            public byte[] Data { get; }

            public Ticket( Auth auth, uint handle, byte[] data )
            {
                _auth = auth;
                Handle = handle;
                Data = data;
            }

            public void Cancel()
            {
                _auth.CancelAuthTicket( this );
            }

            public void Dispose()
            {
                Cancel();
            }
        }

        public abstract Ticket GetAuthSessionTicket();
        public abstract void CancelAuthTicket( Ticket ticket );

        /// <summary>
        /// Start authorizing a ticket. This user isn't authorized yet. Wait for a call to OnAuthChange.
        /// </summary>
        public abstract StartAuthResult StartSession( byte[] data, ulong steamid );

        /// <summary>
        /// Forget this guy. They're no longer in the game.
        /// </summary>
        public abstract void EndSession( ulong steamid );

        public abstract void Dispose();
    }
}
