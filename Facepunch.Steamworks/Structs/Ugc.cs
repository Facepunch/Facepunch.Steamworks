using System.Linq;

#pragma warning disable 649 

namespace Steamworks.Data
{
	public struct Ugc
	{
		internal UGCHandle_t Handle;

		/// <summary>
		/// Create a Ugc from a ulong handle value
		/// </summary>
		public Ugc( ulong handle )
		{
			Handle = handle;
		}

		/// <summary>
		/// Implicit conversion from ulong
		/// </summary>
		public static implicit operator Ugc( ulong handle ) => new Ugc( handle );

		/// <summary>
		/// Get the underlying handle value
		/// </summary>
		public ulong Value => Handle.Value;
	}
}

#pragma warning restore 649
