using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facepunch.Steamworks.Interop;

namespace Facepunch.Steamworks
{
    public class BaseSteamworks : IDisposable
    {
        /// <summary>
        /// Current running program's AppId
        /// </summary>
        public uint AppId { get; internal set; }

        public Networking Networking { get; internal set; }
        public Inventory Inventory { get; internal set; }
        public Workshop Workshop { get; internal set; }

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

        public void SetupCommonInterfaces()
        {
            Networking = new Steamworks.Networking( this, native.networking );
            Inventory = new Steamworks.Inventory( native.inventory, IsGameServer );
            Workshop = new Steamworks.Workshop( this, native.ugc, native.remoteStorage );
        }

        public bool IsValid
        {
            get { return native != null; }
        }

        internal Interop.NativeInterface native;
        internal virtual bool IsGameServer { get { return false; } }

        public enum MessageType : int
        {
            Message = 0,
            Warning = 1
        }

        /// <summary>
        /// Called with a message from Steam
        /// </summary>
        public Action<MessageType, string> OnMessage;


        private List<SteamNative.CallbackHandle> CallbackHandles = new List<SteamNative.CallbackHandle>();
        internal void RegisterCallbackHandle( SteamNative.CallbackHandle handle )
        {
            CallbackHandles.Add( handle );
        }

        public Action OnUpdate;


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