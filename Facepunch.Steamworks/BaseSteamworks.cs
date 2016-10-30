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
                h.Remove( this );
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


        private List<SteamNative.Callback.Handle> CallbackHandles = new List<SteamNative.Callback.Handle>();
        internal void RegisterCallbackHandle( SteamNative.Callback.Handle handle )
        {
            CallbackHandles.Add( handle );
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
        internal void AddCallResult( CallResult call )
        {
            if ( call == null ) throw new ArgumentNullException( "call" );

            if ( FinishCallback( call ) )
                return;

            Callbacks.Add( call );
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