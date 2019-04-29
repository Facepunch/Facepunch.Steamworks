using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	internal abstract class SteamInterface
	{
		public IntPtr Self;
		public IntPtr VTable;

		public virtual string InterfaceName => null;

		public SteamInterface( bool server = false )
		{
			//
			// If teh client isn't initialized but the server is, 
			// try to open this interface in server mode
			//
			if ( !SteamClient.IsValid && SteamServer.IsValid ) server = true;

			var hUser = server ?
						SteamGameServer.GetHSteamUser() : 
						SteamAPI.GetHSteamUser();

			if ( hUser == 0 )
				throw new System.Exception( "Steamworks is uninitialized" );

			Self = server ?
					SteamInternal.FindOrCreateGameServerInterface( hUser, InterfaceName ) :
					SteamInternal.FindOrCreateUserInterface( hUser, InterfaceName );

			if ( Self == IntPtr.Zero )
				throw new System.Exception( $"Couldn't find interface {InterfaceName} (server:{server})" );


			VTable = Marshal.ReadIntPtr( Self, 0 );
			if ( Self == IntPtr.Zero )
				throw new System.Exception( $"Invalid VTable for {InterfaceName}" );

			InitInternals();
		}

		public abstract void InitInternals();


		//
		// Dedicated string conversion buffer
		//
		static byte[] stringbuffer = new byte[1024 * 128];

		internal string GetString( IntPtr p )
		{
			if ( p == IntPtr.Zero )
				return null;

			// return Marshal.PtrToStringUTF8( p );
			lock ( stringbuffer )
			{
				int len = 0;
				while ( Marshal.ReadByte( p, len ) != 0 && len < stringbuffer.Length ) { ++len; }
				Marshal.Copy( p, stringbuffer, 0, len );
				return Encoding.UTF8.GetString( stringbuffer, 0, len );
			}
		}
	}
}