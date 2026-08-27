using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks
{
	/// <summary>
	/// Represents the ID of a Steam application &#8212; the number in a store page URL, and the value
	/// you pass to <see cref="SteamClient.Init"/>. Every app, DLC, demo and tool has its own.
	/// </summary>
	/// <remarks>
	/// <para>
	/// During development this is also what goes in the <c>steam_appid.txt</c> file next to your
	/// executable, which is how the Steam client knows which app an unlaunched build belongs to.
	/// </para>
	/// <para>
	/// Do not confuse this with <see cref="Steamworks.Data.GameId"/>, which packs an app id together
	/// with a type and mod id and is what the friends API reports as "currently playing". Nor with
	/// <see cref="Steamworks.Data.DepotId"/>, which identifies a content bundle within an app rather
	/// than the app itself.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// SteamClient.Init( 252490 );                 // implicit from int
	/// AppId self = SteamClient.AppId;
	/// Console.WriteLine( $"running as {self}" );  // running as 252490
	/// </code>
	/// </example>
	public struct AppId
	{
		/// <summary>
		/// The raw application id. Steam app ids are dense small integers, so this comfortably fits
		/// well below the 32-bit ceiling for any real app.
		/// </summary>
		public uint Value;

		/// <summary>
		/// The application id rendered as plain decimal digits, matching how Steam shows it in store
		/// URLs and on the partner site.
		/// </summary>
		/// <returns>The decimal form of <see cref="Value"/>.</returns>
		public override string ToString() => Value.ToString();

		/// <summary>
		/// Wraps a raw application id. Implicit and unvalidated &#8212; nothing checks that the app
		/// exists.
		/// </summary>
		/// <param name="value">The application id.</param>
		/// <returns>An <see cref="AppId"/> carrying that value.</returns>
		public static implicit operator AppId( uint value )
		{
			return new AppId{ Value = value };
		}

		/// <summary>
		/// Wraps a raw application id given as a signed integer, so that literals like
		/// <c>252490</c> convert without a cast.
		/// </summary>
		/// <param name="value">
		/// The application id. Reinterpreted as unsigned, so a negative value silently becomes a very
		/// large app id rather than being rejected.
		/// </param>
		/// <returns>An <see cref="AppId"/> carrying that value.</returns>
		public static implicit operator AppId( int value )
		{
			return new AppId { Value = (uint) value };
		}

		/// <summary>
		/// Unwraps to the raw application id.
		/// </summary>
		/// <param name="value">The id to unwrap.</param>
		/// <returns>The underlying <see cref="Value"/>.</returns>
		public static implicit operator uint( AppId value )
		{
			return value.Value;
		}
	}
}
