using System;
using System.Collections.Generic;

namespace Steamworks.Data
{
	/// <summary>
	/// One Steam Remote Play session &#8212; a player streaming this game from the machine it is
	/// actually running on to a phone, tablet, TV or another PC. Use it to find out what a streaming
	/// player is playing on, so you can adapt UI scale or input hints for a couch or a touchscreen.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The game still runs locally; only video and input are streamed. Multiple sessions can exist
	/// at once (Remote Play Together), so do not assume there is only one.
	/// </para>
	/// <para>
	/// This is a handle, not a snapshot &#8212; every property queries Steam live using
	/// <see cref="Id"/>. Once the session ends those queries return nothing, while
	/// <see cref="IsValid"/> keeps reporting <see langword="true"/>. Watch
	/// <c>SteamRemotePlay.OnSessionDisconnected</c> to know when to stop using one.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// for ( int i = 0; i &lt; SteamRemotePlay.SessionCount; i++ )
	/// {
	///     var session = SteamRemotePlay.GetSession( i );
	///     if ( session.FormFactor == SteamDeviceFormFactor.Phone )
	///         UseTouchFriendlyUi();
	/// }
	/// </code>
	/// </example>
	public struct RemotePlaySession
	{
		/// <summary>
		/// The opaque session handle Steam assigned. Zero means "no session"; see
		/// <see cref="IsValid"/>.
		/// </summary>
		public uint Id { get; set; }

		/// <summary>
		/// The session handle as decimal digits. Only useful for logging &#8212; it is not a user
		/// identity, and it is not stable between runs.
		/// </summary>
		/// <returns>The decimal form of <see cref="Id"/>.</returns>
		public override string ToString() => Id.ToString();

		/// <summary>
		/// Wraps a raw session handle. Implicit and unvalidated.
		/// </summary>
		/// <param name="value">The session handle.</param>
		/// <returns>A <see cref="RemotePlaySession"/> carrying that handle.</returns>
		public static implicit operator RemotePlaySession( uint value ) => new RemotePlaySession() { Id = value };

		/// <summary>
		/// Unwraps to the raw session handle.
		/// </summary>
		/// <param name="value">The session to unwrap.</param>
		/// <returns>The underlying <see cref="Id"/>.</returns>
		public static implicit operator uint( RemotePlaySession value ) => value.Id;

		/// <summary>
		/// Returns true if this session was valid when created. This will stay true even 
		/// after disconnection - so be sure to watch SteamRemotePlay.OnSessionDisconnected
		/// </summary>
		public bool IsValid => Id > 0;

		/// <summary>
		/// Which Steam user is on the far end of this stream. For Remote Play Together this is the
		/// guest, who may not own the game.
		/// </summary>
		/// <remarks>
		/// Zero once the session has ended, or if the handle was never valid.
		/// </remarks>
		public SteamId SteamId => SteamRemotePlay.Internal.GetSessionSteamID( Id );

		/// <summary>
		/// The device name the streaming client reports &#8212; the phone or TV's own name, as the
		/// user set it. A display string only; do not parse it or use it to identify a device.
		/// </summary>
		/// <remarks>
		/// Empty once the session has ended. Valve returns a <c>const char *</c> here and documents
		/// nothing about its lifetime; this binding copies it into a managed string before returning,
		/// so the value you get is yours.
		/// </remarks>
		public string ClientName => SteamRemotePlay.Internal.GetSessionClientName( Id );

		/// <summary>
		/// What kind of device the player is streaming to &#8212; phone, tablet, computer, TV or VR
		/// headset. This is the property worth branching on: a phone wants larger touch targets, a TV
		/// wants a controller-first layout and readable-from-the-couch text.
		/// </summary>
		/// <remarks>
		/// Reports <c>Unknown</c> for a device Steam cannot classify and for a session that has
		/// ended, so treat it as a hint rather than a guarantee and always have a sensible default.
		/// </remarks>
		public SteamDeviceFormFactor FormFactor => SteamRemotePlay.Internal.GetSessionClientFormFactor( Id );
	}
}
