using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facepunch.Steamworks.Interop;

namespace Facepunch.Steamworks
{
    /// <summary>
    /// Implements shared functionality between Steamworks.Client and Steamworks.Server
    /// </summary>
    public class BaseSteamworks : IDisposable
    {
        /// <summary>
        /// Current running program's AppId
        /// </summary>
        public uint AppId { get; internal set; }

        public Networking Networking { get; internal set; }
        public Inventory Inventory { get; internal set; }
        public Workshop Workshop { get; internal set; }

        internal event Action OnUpdate;

        internal Interop.NativeInterface native;

        private List<SteamNative.CallbackHandle> CallbackHandles = new List<SteamNative.CallbackHandle>();

        public virtual void Dispose()
        {
            foreach ( var h in CallbackHandles )
            {
                h.Dispose();
            }
            CallbackHandles.Clear();

            if ( Workshop != null )
            {
                Workshop.Dispose();
                Workshop = null;
            }

            if ( Inventory != null )
            {
                Inventory.Dispose();
                Inventory = null;
            }

            if ( Networking != null )
            {
                Networking.Dispose();
                Networking = null;
            }

            if ( native != null )
            {
                native.Dispose();
                native = null;
            }
        }

        protected void SetupCommonInterfaces()
        {
            Networking = new Steamworks.Networking( this, native.networking );
            Inventory = new Steamworks.Inventory( native.inventory, IsGameServer );
            Workshop = new Steamworks.Workshop( this, native.ugc, native.remoteStorage );
        }

        /// <summary>
        /// Returns true if this instance has initialized properly.
        /// If this returns false you should Dispose and throw an error.
        /// </summary>
        public bool IsValid
        {
            get { return native != null; }
        }

        
        internal virtual bool IsGameServer { get { return false; } }

        internal void RegisterCallbackHandle( SteamNative.CallbackHandle handle )
        {
            CallbackHandles.Add( handle );
        }

        public virtual void Update()
        {
            Inventory.Update();

            Networking.Update();

            if ( OnUpdate != null )
                OnUpdate();
        }

        /// <summary>
        /// Run Update until func returns false.
        /// This will cause your program to lock up until it finishes.
        /// This is useful for things like tests or command line utilities etc.
        /// </summary>
        public void UpdateWhile( Func<bool> func )
        {
            while ( func() )
            {
                Update();
                System.Threading.Thread.Sleep( 1 );
            }
        }
    }
}