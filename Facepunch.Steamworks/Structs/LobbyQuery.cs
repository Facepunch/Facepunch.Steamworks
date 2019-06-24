using System.Threading.Tasks;

namespace Steamworks.Data
{
	public struct LobbyQuery
	{
		// TODO FILTERS
		// AddRequestLobbyListStringFilter
		// - WithKeyValue, WithoutKeyValue
		// AddRequestLobbyListNumericalFilter
		// - WithLower, WithHigher, WithEqual, WithNotEqual
		// AddRequestLobbyListNearValueFilter
		// - OrderByNear

		#region Distance Filter
		internal LobbyDistanceFilter? distance;

		/// <summary>
		/// only lobbies in the same immediate region will be returned
		/// </summary>
		public LobbyQuery FilterDistanceClose()
		{
			distance = LobbyDistanceFilter.Close;
			return this;
		}

		/// <summary>
		/// only lobbies in the same immediate region will be returned
		/// </summary>
		public LobbyQuery FilterDistanceFar()
		{
			distance = LobbyDistanceFilter.Far;
			return this;
		}

		/// <summary>
		/// only lobbies in the same immediate region will be returned
		/// </summary>
		public LobbyQuery FilterDistanceWorldwide()
		{
			distance = LobbyDistanceFilter.Worldwide;
			return this;
		}
		#endregion

		#region String key/value filter
		internal MatchMakingKeyValuePair_t? stringFilter;

		/// <summary>
		/// Filter by specified key/value pair; string parameters
		/// </summary>
		public LobbyQuery FilterStringKeyValue( string nKey, string nValue )
		{
			if ( string.IsNullOrEmpty( nKey ) )
				throw new System.ArgumentException( "Key string provided for LobbyQuery filter is null or empty", nameof( nKey ) );

			if ( nKey.Length <= SteamMatchmaking.MaxLobbyKeyLength )
			{
				stringFilter = new MatchMakingKeyValuePair_t
				{
					Key = nKey,
					Value = nValue
				};
			}
			else
				throw new System.ArgumentException( "Key length is longer than MaxLobbyKeyLength", nameof( nKey ) );

			return this;
		}
		#endregion

		#region Slots Filter
		internal int? slotsAvailable;

		/// <summary>
		/// returns only lobbies with the specified number of slots available
		/// </summary>
		public LobbyQuery WithSlotsAvailable( int minSlots )
		{
			slotsAvailable = minSlots;
			return this;
		}

		#endregion

		#region Max results filter
		internal int? maxResults;

		/// <summary>
		/// sets how many results to return, the lower the count the faster it is to download the lobby results
		/// </summary>
		public LobbyQuery WithMaxResults( int max )
		{
			maxResults = max;
			return this;
		}

		#endregion


		void ApplyFilters()
		{
			if ( distance.HasValue )
			{
				SteamMatchmaking.Internal.AddRequestLobbyListDistanceFilter( distance.Value );
			}

			if ( slotsAvailable.HasValue )
			{
				SteamMatchmaking.Internal.AddRequestLobbyListFilterSlotsAvailable( slotsAvailable.Value );
			}

			if ( maxResults.HasValue )
			{
				SteamMatchmaking.Internal.AddRequestLobbyListResultCountFilter( maxResults.Value );
			}

			if( stringFilter.HasValue )
			{
				SteamMatchmaking.Internal.AddRequestLobbyListStringFilter( stringFilter.Value.Key, stringFilter.Value.Value, LobbyComparison.Equal );
			}
		}

		/// <summary>
		/// Run the query, get the matching lobbies
		/// </summary>
		public async Task<Lobby[]> RequestAsync()
		{
			ApplyFilters();

			LobbyMatchList_t? list = await SteamMatchmaking.Internal.RequestLobbyList();
			if ( !list.HasValue || list.Value.LobbiesMatching == 0 )
			{
				return null;
			}

			Lobby[] lobbies = new Lobby[list.Value.LobbiesMatching];

			for ( int i = 0; i < list.Value.LobbiesMatching; i++ )
			{
				lobbies[i] = new Lobby { Id = SteamMatchmaking.Internal.GetLobbyByIndex( i ) };
			}

			return lobbies;
		}
	}
}