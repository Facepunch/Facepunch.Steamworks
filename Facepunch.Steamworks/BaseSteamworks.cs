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
    }
}