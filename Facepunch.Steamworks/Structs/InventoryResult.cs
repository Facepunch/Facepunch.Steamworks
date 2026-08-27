using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// A point-in-time snapshot of some part of a user's inventory, returned by every asynchronous
	/// inventory operation. It is both the answer to "what does the player own" and the receipt for
	/// a change you just made — after a consume or a craft, the items in here carry the flags
	/// saying what actually happened.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>This owns a native handle and must be disposed.</b> Valve requires the underlying result
	/// to be destroyed; if you drop one of these on the floor the handle is leaked for the lifetime
	/// of the process. There is no finalizer to save you. Wrap it in <c>using</c>.
	/// </para>
	/// <para>
	/// It is a struct, so copying it copies the handle without duplicating ownership. Two copies
	/// refer to the same native result: disposing either invalidates both, and disposing both is a
	/// double free. Pass it by reference or keep exactly one owner.
	/// </para>
	/// <para>
	/// Reading is lazy — nothing is materialized until you call <see cref="GetItems(bool)"/> or
	/// read <see cref="ItemCount"/>, and those go back to the native result each time. Do it once
	/// and keep the array rather than re-querying, and do it before disposing.
	/// </para>
	/// <para>
	/// A result containing zero items is a perfectly normal success. It means the player owns
	/// nothing matching, or that the app has no item schema configured on the Steamworks partner
	/// site. It does not mean the call failed — failure is signalled by the <c>null</c> the
	/// producing method returns instead of a result.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// var result = await SteamInventory.GetAllItemsAsync();
	/// if ( !result.HasValue )
	///     return; // request rejected
	///
	/// using ( var snapshot = result.Value )
	/// {
	///     var items = snapshot.GetItems();
	///     if ( items == null )
	///         return; // no items — note this is null, not an empty array
	///
	///     foreach ( var item in items )
	///         Console.WriteLine( $"{item.Def?.Name} x{item.Quantity}" );
	///
	///     // Prove ownership to a server without exposing the whole inventory.
	///     byte[] proof = snapshot.Serialize();
	/// }
	/// </code>
	/// </example>
	public struct InventoryResult : IDisposable
	{
		internal SteamInventoryResult_t _id;

		/// <summary>
		/// Whether this snapshot is flagged as possibly out of date. Only meaningful for results
		/// produced by <see cref="SteamInventory.DeserializeAsync"/>, where a snapshot can verify
		/// successfully and still be stale.
		/// </summary>
		/// <value>
		/// <c>true</c> if Steam reported the result as expired. The data is still readable — an
		/// expired result is a soft failure, not a hard one.
		/// </value>
		/// <remarks>
		/// Expired means either that the snapshot's built-in one hour timestamp has elapsed, or
		/// that an item in it has been traded or consumed since it was generated. Deciding what to
		/// do is your call: accept it, or ask the sending player for a fresh snapshot. Do not treat
		/// it as proof of cheating — an honest client sitting in a long match will produce expired
		/// snapshots routinely.
		/// </remarks>
		public bool Expired { get; internal set; }

		internal InventoryResult( SteamInventoryResult_t id, bool expired )
		{
			_id = id;
			Expired = expired;
		}

		/// <summary>
		/// How many item entries this snapshot holds. Cheap way to check whether there is anything
		/// worth reading before paying for a full <see cref="GetItems(bool)"/>.
		/// </summary>
		/// <value>
		/// The number of entries, or <c>0</c> if the result is empty or the underlying query
		/// failed. Note that stacked items count as one entry regardless of their quantity, so this
		/// is not the total number of units the player owns.
		/// </value>
		/// <remarks>
		/// Each read makes a native call, so cache the value rather than using it as a loop bound.
		/// A failure to read and a genuinely empty result both report zero here.
		/// </remarks>
		public int ItemCount
		{
			get
			{
				uint cnt = 0;

				if ( !SteamInventory.Internal.GetResultItems( _id, null, ref cnt ) )
					return 0;

				return (int) cnt;
			}
		}

		/// <summary>
		/// Confirms this snapshot really describes the inventory of the user you think it does.
		/// This is the check that makes <see cref="SteamInventory.DeserializeAsync"/> trustworthy:
		/// Steam's signature proves the snapshot is genuine, but only this proves whose it is.
		/// </summary>
		/// <param name="steamId">
		/// The user you expect to own these items — the identity you established through your own
		/// authentication, not one the sender told you.
		/// </param>
		/// <returns>
		/// <c>true</c> if the snapshot belongs to <paramref name="steamId"/>, <c>false</c> if it
		/// belongs to someone else.
		/// </returns>
		/// <remarks>
		/// Skipping this is the classic hole in an item-verification flow: without it, a player can
		/// capture another user's legitimately signed snapshot and replay it as their own to claim
		/// items they do not have. Always call it on anything that arrived over the network, and
		/// reject the snapshot when it returns <c>false</c>.
		/// </remarks>
		public bool BelongsTo( SteamId steamId )
		{
			return SteamInventory.Internal.CheckResultSteamID( _id, steamId );
		}

		/// <summary>
		/// Materializes the snapshot into an array of items you can actually read. This is how you
		/// get from a result handle to "the player owns these three hats".
		/// </summary>
		/// <param name="includeProperties">
		/// Whether to also fetch each item's dynamic properties, which is what makes
		/// <see cref="InventoryItem.Origin"/> and <see cref="InventoryItem.Acquired"/> return real
		/// values. Off by default because it costs a native call per property per item — on a large
		/// inventory that is genuinely slow. Prefer the overload taking explicit property names
		/// when you only need one or two.
		/// </param>
		/// <returns>
		/// The items, or <c>null</c> if the snapshot is empty or the underlying read failed.
		/// <b>Note this is null and not an empty array</b>, so guard before enumerating.
		/// </returns>
		/// <remarks>
		/// Builds a fresh array on every call, so call it once and keep the result. Must be called
		/// before the result is disposed. <see cref="InventoryItem.Def"/> on the returned items is
		/// null unless item definitions were loaded first.
		/// </remarks>
		public InventoryItem[] GetItems( bool includeProperties = false )
		{
			uint cnt = (uint) ItemCount;
			if ( cnt <= 0 ) return null;

			var pOutItemsArray = new SteamItemDetails_t[cnt];

			if ( !SteamInventory.Internal.GetResultItems( _id, pOutItemsArray, ref cnt ) )
				return null;

			var items = new InventoryItem[cnt];

			for( int i=0; i< cnt; i++ )
			{
				var item = InventoryItem.From( pOutItemsArray[i] );

				if ( includeProperties )
					item._properties = InventoryItem.GetProperties( _id, i );

				items[i] = item;
			}


			return items;
		}

		/// <summary>
		/// Materializes the snapshot, fetching only the item properties you name. This is the
		/// version to use in practice: it skips the "list every property name" round trip and
		/// avoids reading fields you are going to ignore, which matters a lot on a big inventory.
		/// </summary>
		/// <param name="withProperties">
		/// The property keys to fetch for each item, for example <c>"origin"</c> and
		/// <c>"acquired"</c>. Pass <c>null</c> or an empty array to skip properties entirely, which
		/// leaves <see cref="InventoryItem.Properties"/> null on every item.
		/// </param>
		/// <returns>
		/// The items, or <c>null</c> if the snapshot is empty or the underlying read failed — null,
		/// not an empty array.
		/// </returns>
		/// <remarks>
		/// Still one native call per property per item, so the cost scales with items multiplied by
		/// the number of keys you ask for. Ask for the fewest keys you need. Properties that do not
		/// exist on an item are simply absent from the dictionary rather than present as null.
		/// </remarks>
		public InventoryItem[] GetItems( string[] withProperties )
		{
			uint cnt = (uint) ItemCount;
			if ( cnt <= 0 ) return null;

			var pOutItemsArray = new SteamItemDetails_t[cnt];

			if ( !SteamInventory.Internal.GetResultItems( _id, pOutItemsArray, ref cnt ) )
				return null;

			var items = new InventoryItem[cnt];

			for( int i=0; i< cnt; i++ )
			{
				var item = InventoryItem.From( pOutItemsArray[i] );

				if ( withProperties != null && withProperties.Length > 0 )
					item._properties = InventoryItem.GetProperties( _id, i, withProperties );

				items[i] = item;
			}

			return items;
		}

		/// <summary>
		/// Releases the native inventory result and the memory Steam allocated for it. Valve
		/// requires every result handle to be destroyed, so this is mandatory, not an
		/// optimization — prefer a <c>using</c> block over calling it by hand.
		/// </summary>
		/// <remarks>
		/// <para>
		/// After disposal the snapshot is dead: do not call <see cref="GetItems(bool)"/>,
		/// <see cref="Serialize"/>, <see cref="BelongsTo"/> or read <see cref="ItemCount"/>. Item
		/// arrays you already materialized stay valid, because they are plain managed objects.
		/// </para>
		/// <para>
		/// Because <see cref="InventoryResult"/> is a struct, every copy carries the same handle
		/// and this is not reference counted. Dispose exactly once, from whichever copy you treat
		/// as the owner. It is a no-op only on the specific invalid-handle sentinel, so disposing
		/// twice, or disposing a default-constructed value, is not protected against.
		/// </para>
		/// </remarks>
		public void Dispose()
		{
			if ( _id.Value == -1 ) return;

			SteamInventory.Internal.DestroyResult( _id );
		}

		internal static async Task<InventoryResult?> GetAsync( SteamInventoryResult_t sresult )
		{
			var _result = Result.Pending;
			while ( _result == Result.Pending )
			{
				_result = SteamInventory.Internal.GetResultStatus( sresult );
				await Task.Delay( 10 );
			}

			if ( _result != Result.OK && _result != Result.Expired )
				return null;

			return new InventoryResult( sresult, _result == Result.Expired );
		}

		/// <summary>
		/// Packages this snapshot into a signed blob you can send to other machines as proof of
		/// what the player owns. This is the sending half of Steam's anti-forgery mechanism: the
		/// receiver calls <see cref="SteamInventory.DeserializeAsync"/>, which verifies Steam's
		/// signature, so they can trust the item list without trusting the client that sent it.
		/// The typical use is a client proving its equipped cosmetics to your game server on join.
		/// </summary>
		/// <returns>
		/// The signed blob, ready to put on the wire, or <c>null</c> if serialization failed. Size
		/// scales with the number of items in the snapshot.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The signature cannot be forged, and cannot be replayed into a different game session.
		/// It does not, however, say who the items belong to — the receiver must still call
		/// <see cref="BelongsTo"/> with the expected Steam ID, or a player can relay someone else's
		/// blob as their own.
		/// </para>
		/// <para>
		/// The blob carries a timestamp and is treated as expired an hour after it was generated;
		/// the receiver sees this as <see cref="Expired"/>. Regenerate rather than caching one for
		/// a long session.
		/// </para>
		/// <para>
		/// Valve recommends narrowing the snapshot to just the items you need to prove before
		/// serializing, using their GetItemsByID call, so you neither leak the player's whole
		/// inventory nor pay to transmit it. This binding does not expose that call, so a snapshot
		/// from <see cref="SteamInventory.GetAllItemsAsync"/> serializes the full inventory — bear
		/// that in mind for both size and privacy.
		/// </para>
		/// </remarks>
		public unsafe byte[] Serialize()
		{
			uint size = 0;

			if ( !SteamInventory.Internal.SerializeResult( _id, IntPtr.Zero, ref size ) )
				return null;

			var data = new byte[size];

			fixed ( byte* ptr = data )
			{
				if ( !SteamInventory.Internal.SerializeResult( _id, (IntPtr)ptr, ref size ) )
					return null;
			}

			return data;
		}
	}
}