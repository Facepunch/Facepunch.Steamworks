using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	/// <summary>
	/// Identifies a depot &#8212; one bundle of downloadable content inside a Steam application.
	/// An app is made of one or more depots, typically split by platform or by language, and Steam
	/// downloads them independently according to what the user owns and what platform they are on.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Depot ids are configured on the Steamworks partner site and are distinct from
	/// <see cref="Steamworks.AppId"/>, though by convention an app's first depot is often its app id
	/// plus one. Do not rely on that convention.
	/// </para>
	/// <para>
	/// In this library a depot id is mainly what you hand to the game-server Workshop initialiser to
	/// say which depot Workshop content should be installed under.
	/// </para>
	/// </remarks>
	public struct DepotId
	{
		/// <summary>
		/// The raw depot id, as configured on the Steamworks partner site.
		/// </summary>
		public uint Value;

		/// <summary>
		/// Wraps a raw depot id. Implicit and unvalidated.
		/// </summary>
		/// <param name="value">The depot id.</param>
		/// <returns>A <see cref="DepotId"/> carrying that value.</returns>
		public static implicit operator DepotId( uint value )
		{
			return new DepotId { Value = value };
		}

		/// <summary>
		/// Wraps a raw depot id given as a signed integer, so literals convert without a cast.
		/// </summary>
		/// <param name="value">
		/// The depot id. Reinterpreted as unsigned, so a negative value silently becomes a very large
		/// depot id rather than being rejected.
		/// </param>
		/// <returns>A <see cref="DepotId"/> carrying that value.</returns>
		public static implicit operator DepotId( int value )
		{
			return new DepotId { Value = (uint) value };
		}

		/// <summary>
		/// Unwraps to the raw depot id.
		/// </summary>
		/// <param name="value">The id to unwrap.</param>
		/// <returns>The underlying <see cref="Value"/>.</returns>
		public static implicit operator uint( DepotId value )
		{
			return value.Value;
		}

		/// <summary>
		/// The depot id rendered as plain decimal digits.
		/// </summary>
		/// <returns>The decimal form of <see cref="Value"/>.</returns>
		public override string ToString() => Value.ToString();
	}
}