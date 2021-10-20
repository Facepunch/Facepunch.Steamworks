using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Class for utilizing the Steam Network API.
	/// </summary>
	public class SteamNetworking : SteamSharedClass<SteamNetworking>
	{
		internal static ISteamNetworking Internal => Interface as ISteamNetworking;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamNetworking( server ) );

			InstallEvents( server );
		}

		internal static void InstallEvents( bool server )
		{
			Dispatch.Install<P2PSessionRequest_t>( x => OnP2PSessionRequest?.Invoke( x.SteamIDRemote ), server );
			Dispatch.Install<P2PSessionConnectFail_t>( x => OnP2PConnectionFailed?.Invoke( x.SteamIDRemote, (P2PSessionError) x.P2PSessionError ), server );
		}

		/// <summary>
		/// Invoked when a <see cref="SteamId"/> wants to send the current user a message. You should respond by calling <see cref="AcceptP2PSessionWithUser(SteamId)"/>
		/// if you want to recieve their messages.
		/// </summary>
		public static Action<SteamId> OnP2PSessionRequest;

		/// <summary>
		/// Invoked when packets can't get through to the specified user.
		/// All queued packets unsent at this point will be dropped, further attempts
		/// to send will retry making the connection (but will be dropped if we fail again).
		/// </summary>
		public static Action<SteamId, P2PSessionError> OnP2PConnectionFailed;

		/// <summary>
		/// This should be called in response to a <see cref="OnP2PSessionRequest"/>.
		/// </summary>
		public static bool AcceptP2PSessionWithUser( SteamId user ) => Internal.AcceptP2PSessionWithUser( user );

		/// <summary>
		/// Allow or disallow P2P connects to fall back on Steam server relay if direct 
		/// connection or NAT traversal can't be established. Applies to connections 
		/// created after setting or old connections that need to reconnect.
		/// </summary>
		public static bool AllowP2PPacketRelay( bool allow ) => Internal.AllowP2PPacketRelay( allow );

		/// <summary>
		/// This should be called when you're done communicating with a user, as this will
		/// free up all of the resources allocated for the connection under-the-hood.
		/// If the remote user tries to send data to you again, a new <see cref="OnP2PSessionRequest"/> 
		/// callback will be posted
		/// </summary>
		public static bool CloseP2PSessionWithUser( SteamId user ) => Internal.CloseP2PSessionWithUser( user );

		/// <summary>
		/// Checks if a P2P packet is available to read.
		/// </summary>
		public static bool IsP2PPacketAvailable( int channel = 0 )
		{
			uint _ = 0;
			return Internal.IsP2PPacketAvailable( ref _, channel );
		}
		
		/// <summary>
		/// Checks if a P2P packet is available to read, and gets the size of the message if there is one.
		/// </summary>
		public static bool IsP2PPacketAvailable( out uint msgSize, int channel = 0 )
		{
			msgSize = 0;
			return Internal.IsP2PPacketAvailable( ref msgSize, channel );
		}

		/// <summary>
		/// Reads in a packet that has been sent from another user via <c>SendP2PPacket</c>.
		/// </summary>
		public unsafe static P2Packet? ReadP2PPacket( int channel = 0 )
		{
			uint size = 0;

			if ( !Internal.IsP2PPacketAvailable( ref size, channel ) )
				return null;

			var buffer = Helpers.TakeBuffer( (int) size );

			fixed ( byte* p = buffer )
			{
				SteamId steamid = 1;
				if ( !Internal.ReadP2PPacket( (IntPtr)p, (uint) buffer.Length, ref size, ref steamid, channel ) || size == 0 )
				    return null;

				var data = new byte[size];
				Array.Copy( buffer, 0, data, 0, size );

				return new P2Packet
				{
					SteamId = steamid,
					Data = data
				};
			}
		}

		/// <summary>
		/// Reads in a packet that has been sent from another user via <c>SendP2PPacket</c>.
		/// </summary>
		public unsafe static bool ReadP2PPacket( byte[] buffer, ref uint size, ref SteamId steamid, int channel = 0 )
		{
			fixed (byte* p = buffer) {
				return Internal.ReadP2PPacket( (IntPtr)p, (uint)buffer.Length, ref size, ref steamid, channel );
			}
		}

		/// <summary>
		/// Reads in a packet that has been sent from another user via <c>SendP2PPacket</c>.
		/// </summary>
		public unsafe static bool ReadP2PPacket( byte* buffer, uint cbuf, ref uint size, ref SteamId steamid, int channel = 0 )
		{
			return Internal.ReadP2PPacket( (IntPtr)buffer, cbuf, ref size, ref steamid, channel );
		}

		/// <summary>
		/// Sends a P2P packet to the specified user.
		/// This is a session-less API which automatically establishes NAT-traversing or Steam relay server connections.
		/// NOTE: The first packet send may be delayed as the NAT-traversal code runs.
		/// </summary>
		public static unsafe bool SendP2PPacket( SteamId steamid, byte[] data, int length = -1, int nChannel = 0, P2PSend sendType = P2PSend.Reliable )
		{
			if ( length <= 0 )
				length = data.Length;

			fixed ( byte* p = data )
			{
				return Internal.SendP2PPacket( steamid, (IntPtr)p, (uint)length, (P2PSend)sendType, nChannel );
			}
		}

		/// <summary>
		/// Sends a P2P packet to the specified user.
		/// This is a session-less API which automatically establishes NAT-traversing or Steam relay server connections.
		/// NOTE: The first packet send may be delayed as the NAT-traversal code runs.
		/// </summary>
		public static unsafe bool SendP2PPacket( SteamId steamid, byte* data, uint length, int nChannel = 1, P2PSend sendType = P2PSend.Reliable )
		{ 
			return Internal.SendP2PPacket( steamid, (IntPtr)data, (uint)length, (P2PSend)sendType, nChannel );
		}

	}
}
