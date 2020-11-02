using System;
using System.Collections.Generic;

namespace Steamworks.Data
{
	/// <summary>
	/// Represents a RemotePlaySession from the SteamRemotePlay interface
	/// </summary>
	public struct RemotePlaySession
	{
		public uint Id { get; set; }

		public override string ToString() => Id.ToString();
		public static implicit operator RemotePlaySession( uint value ) => new RemotePlaySession() { Id = value };
		public static implicit operator uint( RemotePlaySession value ) => value.Id;

		/// <summary>
		/// Returns true if this session was valid when created. This will stay true even 
		/// after disconnection - so be sure to watch SteamRemotePlay.OnSessionDisconnected
		/// </summary>
		public bool IsValid => Id > 0;

		/// <summary>
		/// Get the SteamID of the connected user
		/// </summary>
		public SteamId SteamId => SteamRemotePlay.Internal.GetSessionSteamID( Id );

		/// <summary>
		/// Get the name of the session client device
		/// </summary>
		public string ClientName => SteamRemotePlay.Internal.GetSessionClientName( Id );

		/// <summary>
		/// Get the name of the session client device
		/// </summary>
		public SteamDeviceFormFactor FormFactor => SteamRemotePlay.Internal.GetSessionClientFormFactor( Id );
	}
}
