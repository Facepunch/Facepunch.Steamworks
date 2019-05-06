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

		public void Init()
		{
			if ( SteamClient.IsValid )
			{
				InitClient();
				return;
			}

			if ( SteamServer.IsValid )
			{
				InitServer();
				return;
			}

			throw new System.Exception( "Trying to initialize Steam Interface but Steam not initialized" );
		}

		public void InitClient()
		{
			var user = SteamAPI.GetHSteamUser();
			Self = SteamInternal.FindOrCreateUserInterface( user, InterfaceName );

			if ( Self == IntPtr.Zero )
				throw new System.Exception( $"Couldn't find interface {InterfaceName}" );

			VTable = Marshal.ReadIntPtr( Self, 0 );
			if ( Self == IntPtr.Zero )
				throw new System.Exception( $"Invalid VTable for {InterfaceName}" );

			InitInternals();
		}

		public void InitServer()
		{
			var user = SteamGameServer.GetHSteamUser();
			Self = SteamInternal.FindOrCreateGameServerInterface( user, InterfaceName );

			if ( Self == IntPtr.Zero )
				throw new System.Exception( $"Couldn't find server interface {InterfaceName}" );

			VTable = Marshal.ReadIntPtr( Self, 0 );
			if ( Self == IntPtr.Zero )
				throw new System.Exception( $"Invalid VTable for server {InterfaceName}" );

			InitInternals();
		}

		public virtual void InitUserless()
		{
			Self = SteamInternal.FindOrCreateUserInterface( 0, InterfaceName );

			if ( Self == IntPtr.Zero )
				throw new System.Exception( $"Couldn't find interface {InterfaceName}" );

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