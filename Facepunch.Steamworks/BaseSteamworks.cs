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
            Workshop = new Steamworks.Workshop( this, native.ugc );
        }

        public bool IsValid
        {
            get { return native != null; }
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

        /// <summary>
        /// Global callback type
        /// </summary>
        internal void AddCallback<T>( Action<T> Callback, int id )
        {
            var callback = new Facepunch.Steamworks.Interop.Callback<T>( IsGameServer, id, Callback );
            Disposables.Add( callback );
        }

        public Action OnUpdate;


        public virtual void Update()
        {
            RunCallbackQueue();

            Inventory.Update();
            Networking.Update();

            if ( OnUpdate != null )
                OnUpdate();
        }

        List<CallResult> Callbacks = new List<CallResult>();

        /// <summary>
        /// Call results are results to specific actions
        /// </summary>
        internal void AddCallResult( CallResult c )
        {
            if ( FinishCallback( c ) )
                return;

            Callbacks.Add( c );
        }

        void RunCallbackQueue()
        {
            for ( int i=0; i< Callbacks.Count(); i++ )
            {
                if ( !FinishCallback( Callbacks[i] ) )
                    continue;
                
                Callbacks.RemoveAt( i );
                i--;
            }
        }

        bool FinishCallback( CallResult call )
        {
            bool failed = true;

            if ( !native.utils.IsAPICallCompleted( call.Handle, ref failed ) )
                return false;

            if ( failed )
            {
                //
                // TODO - failure reason?
                //
                return true;
            }

            call.Run( native.utils );
            return true;
        }
    }
}