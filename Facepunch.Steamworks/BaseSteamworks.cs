using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public class BaseSteamworks : IDisposable
    {
        public virtual void Dispose()
        {
            foreach ( var d in Disposables )
            {
                d.Dispose();
            }
            Disposables.Clear();

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
        }

        public bool IsValid
        {
            get { return native != null; }
        }

        public Networking Networking { get; internal set; }
        public Inventory Inventory { get; internal set; }

        internal Interop.NativeInterface native;
        internal virtual bool IsGameServer { get { return false; } }

        private List<IDisposable> Disposables = new List<IDisposable>();

        public enum MessageType : int
        {
            Message = 0,
            Warning = 1
        }

        /// <summary>
        /// Called with a message from Steam
        /// </summary>
        public Action<MessageType, string> OnMessage;

        internal void AddCallback<T>( Action<T> Callback, int id )
        {
            var callback = new Facepunch.Steamworks.Interop.Callback<T>( IsGameServer, id, Callback );
            Disposables.Add( callback );
        }

        public Action OnUpdate;


        public virtual void Update()
        {
            Inventory.Update();
            Networking.Update();

            if ( OnUpdate != null )
                OnUpdate();
        }
    }
}