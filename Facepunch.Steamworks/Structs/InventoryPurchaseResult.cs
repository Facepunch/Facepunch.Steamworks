using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	/// <summary>
	/// The acknowledgement Steam returns when a microtransaction has been <i>opened</i> — not when
	/// it has been paid for. Returned by <c>SteamInventory.StartPurchaseAsync</c>, it tells you
	/// whether Steam managed to set the transaction up and gives you the identifiers for it.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>Receiving this does not mean the player bought anything.</b> Steam still has to show its
	/// authorization overlay, and the player can decline or abandon it. The real outcome arrives on
	/// <c>SteamUser.OnMicroTxnAuthorizationResponse</c>, and the resulting items show up through
	/// <c>SteamInventory.OnInventoryUpdated</c>. Never grant an item off the back of this value.
	/// </para>
	/// <para>
	/// There is no native handle here and nothing to dispose.
	/// </para>
	/// </remarks>
	public struct InventoryPurchaseResult
	{
		/// <summary>
		/// Whether Steam was able to open the transaction. Check this before trusting the ID
		/// fields — anything other than OK means no purchase flow was started, and the IDs are
		/// meaningless.
		/// </summary>
		public Result Result;

		/// <summary>
		/// Steam's identifier for the order. This is the value to log, and the one to correlate
		/// against your own records or against the Steam microtransaction Web API if you reconcile
		/// purchases on a backend.
		/// </summary>
		public ulong OrderID;

		/// <summary>
		/// Steam's identifier for the underlying transaction, distinct from the order. Also
		/// primarily useful for logging and backend reconciliation.
		/// </summary>
		public ulong TransID;
	}
}