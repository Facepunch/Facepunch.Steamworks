using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    public class MicroTransactions : IDisposable
    {
        internal Client client;

        public delegate void AuthorizationResponse( bool authorized, int appId, ulong orderId );

        /// <summary>
        /// Called on the MicroTxnAuthorizationResponse_t event
        /// </summary>
        public event AuthorizationResponse OnAuthorizationResponse;

        internal MicroTransactions( Client c )
        {
            client = c;

            SteamNative.MicroTxnAuthorizationResponse_t.RegisterCallback( client, onMicroTxnAuthorizationResponse );
        }

        private void onMicroTxnAuthorizationResponse( MicroTxnAuthorizationResponse_t arg1, bool arg2 )
        {
            if ( OnAuthorizationResponse  != null )
            {
                OnAuthorizationResponse( arg1.Authorized == 1, (int) arg1.AppID, arg1.OrderID );
            }
        }

        public void Dispose()
        {
            client = null;
        }
    }
}
