using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks.ServerList
{
	public abstract class Base : IDisposable
	{

		#region ISteamMatchmakingServers
		internal static ISteamMatchmakingServers Internal => SteamMatchmakingServers.Internal;
		#endregion


		/// <summary>
		/// Which app we're querying. Defaults to the current app.
		/// </summary>
		public AppId AppId { get; set; }

		/// <summary>
		/// When a new server is added, this function will get called
		/// </summary>
		public event Action OnChanges;

		/// <summary>
		/// Called for every responsive server
		/// </summary>
		public event Action<ServerInfo> OnResponsiveServer;

		/// <summary>
		/// A list of servers that responded. If you're only interested in servers that responded since you
		/// last updated, then simply clear this list.
		/// </summary>
		public List<ServerInfo> Responsive = new List<ServerInfo>();

		/// <summary>
		/// A list of servers that were in the master list but didn't respond. 
		/// </summary>
		public List<ServerInfo> Unresponsive = new List<ServerInfo>();


		public Base()
		{
			AppId = SteamClient.AppId; // Default AppId is this 
		}

		/// <summary>
		/// Query the server list. Task result will be true when finished
		/// </summary>
		/// <returns></returns>
		public virtual async Task<bool> RunQueryAsync( float timeoutSeconds = 10 )
		{
			var stopwatch = System.Diagnostics.Stopwatch.StartNew();

			Reset();
			LaunchQuery();

			var thisRequest = request;

			while ( IsRefreshing )
			{
				await Task.Delay( 33 );

				//
				// The request has been cancelled or changed in some way
				//
				if ( request.Value == IntPtr.Zero || thisRequest.Value != request.Value )
					return false;

				if ( !SteamClient.IsValid )
					return false;

				var r = Responsive.Count;

				UpdatePending();
				UpdateResponsive();

				if ( r != Responsive.Count )
				{
					InvokeChanges();
				}

				if ( stopwatch.Elapsed.TotalSeconds > timeoutSeconds )
					break;
			}

			MovePendingToUnresponsive();
			InvokeChanges();

			return true;
		}

		public virtual void Cancel() => Internal.CancelQuery( request );

		// Overrides
		internal abstract void LaunchQuery();

		internal HServerListRequest request;

		#region Filters

		internal List<MatchMakingKeyValuePair> filters = new List<MatchMakingKeyValuePair>();
		internal virtual MatchMakingKeyValuePair[] GetFilters() => filters.ToArray();

		public void AddFilter( string key, string value )
		{
			filters.Add( new MatchMakingKeyValuePair { Key = key, Value = value } );
		}

		#endregion

		internal int Count => Internal.GetServerCount( request );
		internal bool IsRefreshing => request.Value != IntPtr.Zero && Internal.IsRefreshing( request );
		internal List<int> watchList = new List<int>();
		internal int LastCount = 0;

		void Reset()
		{
			ReleaseQuery();
			LastCount = 0;
			watchList.Clear();
		}

		void ReleaseQuery()
		{
			if ( request.Value != IntPtr.Zero )
			{
				Cancel();
				Internal.ReleaseRequest( request );
				request = IntPtr.Zero;
			}
		}

		public void Dispose()
		{
			ReleaseQuery();
		}

		internal void InvokeChanges()
		{
			OnChanges?.Invoke();
		}

		void UpdatePending()
		{
			var count = Count;
			if ( count == LastCount ) return;
			
			for ( int i = LastCount; i < count; i++ )
			{
				watchList.Add( i );
			}
			
			LastCount = count;
		}

		public void UpdateResponsive()
		{
			watchList.RemoveAll( x =>
			{
				var info = Internal.GetServerDetails( request, x );
				if ( info.HadSuccessfulResponse )
				{
					OnServer( ServerInfo.From( info ), info.HadSuccessfulResponse );
					return true;
				}

				return false;
			} );
		}

		void MovePendingToUnresponsive()
		{
			watchList.RemoveAll( x =>
			{
				var info = Internal.GetServerDetails( request, x );
				OnServer( ServerInfo.From( info ), info.HadSuccessfulResponse );
				return true;
			} );
		}

		private void OnServer( ServerInfo serverInfo, bool responded )
		{
			if ( responded )
			{
				Responsive.Add( serverInfo );
				OnResponsiveServer?.Invoke( serverInfo );
				return;
			}
			
			Unresponsive.Add( serverInfo );
		}
	}
}