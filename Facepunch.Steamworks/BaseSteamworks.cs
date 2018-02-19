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
        private List<SteamNative.CallResult> CallResults = new List<SteamNative.CallResult>();
        protected bool disposed = false;


        protected BaseSteamworks( uint appId )
        {
            AppId = appId;

            //
            // No need for the "steam_appid.txt" file any more
            //
            System.Environment.SetEnvironmentVariable("SteamAppId", AppId.ToString());
            System.Environment.SetEnvironmentVariable("SteamGameId", AppId.ToString());
        }

        ~BaseSteamworks()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            if ( disposed ) return;

            Callbacks.Clear();

            foreach ( var h in CallbackHandles )
            {
                h.Dispose();
            }
            CallbackHandles.Clear();

            foreach ( var h in CallResults )
            {
                h.Dispose();
            }
            CallResults.Clear();

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

            System.Environment.SetEnvironmentVariable("SteamAppId", null );
            System.Environment.SetEnvironmentVariable("SteamGameId", null );
            disposed = true;
        }

        protected void SetupCommonInterfaces()
        {
            Networking = new Steamworks.Networking( this, native.networking );
            Inventory = new Steamworks.Inventory( this, native.inventory, IsGameServer );
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

        internal void RegisterCallResult( SteamNative.CallResult handle )
        {
            CallResults.Add( handle );
        }

        internal void UnregisterCallResult( SteamNative.CallResult handle )
        {
            CallResults.Remove( handle );
        }

        public virtual void Update()
        {
            Inventory.Update();

            Networking.Update();

            RunUpdateCallbacks();
        }

        /// <summary>
        /// This gets called automatically in Update. Only call it manually if you know why you're doing it.
        /// </summary>
        public void RunUpdateCallbacks()
        {
            if ( OnUpdate != null )
                OnUpdate();

            for( int i=0; i < CallResults.Count; i++ )
            {
                CallResults[i].Try();
            }
        }

        /// <summary>
        /// Run Update until func returns false.
        /// This will cause your program to lock up until it finishes.
        /// This is useful for things like tests or command line utilities etc.
        /// </summary>
        public void UpdateWhile( Func<bool> func )
        {
            const int sleepMs = 1;

            while ( func() )
            {
                Update();
#if NET_CORE
                System.Threading.Tasks.Task.Delay( sleepMs ).Wait();
#else
                System.Threading.Thread.Sleep( sleepMs );
#endif
            }
        }

        Dictionary<Type, List<Action<object>>> Callbacks = new Dictionary<Type, List<Action<object>>>();

        internal List<Action<object>> CallbackList( Type T )
        {
            List<Action<object>> list = null;

            if ( !Callbacks.TryGetValue( T, out list ) )
            {
                list = new List<Action<object>>();
                Callbacks[T] = list;
            }

            return list;
        }

        internal void OnCallback<T>( T data )
        {
            var list = CallbackList( typeof( T ) );

            foreach ( var i in list )
            {
                i( data );
            }
        }

        internal void RegisterCallback<T>( Action<T> func )
        {
            var list = CallbackList( typeof( T ) );
            list.Add( ( o ) => func( (T) o ) );
        }

    }
}