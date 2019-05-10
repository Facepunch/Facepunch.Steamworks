namespace Steamworks.Data
{
	public struct Socket
	{
		internal uint Id;
		public override string ToString() => Id.ToString();

		/// <summary>
		/// Destroy a listen socket.  All the connections that were accepting on the listen
		/// socket are closed ungracefully.
		/// </summary>
		public bool Close()
		{
			return SteamNetworkingSockets.Internal.CloseListenSocket( this );
		}

		public SocketInterface Interface
		{
			get => SteamNetworkingSockets.GetSocketInterface( Id );
			set => SteamNetworkingSockets.SetSocketInterface( Id, value );
		}
	}
}