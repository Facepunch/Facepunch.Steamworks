namespace Steamworks.Data
{ 
	/// <summary>
	/// A server query packet.
	/// </summary>
	public struct OutgoingPacket
	{
		/// <summary>
		/// Target IP address
		/// </summary>
		public uint Address { get; internal set; }

		/// <summary>
		/// Target port
		/// </summary>
		public ushort Port { get; internal set; }

		/// <summary>
		/// This data is pooled. Make a copy if you don't use it immediately.
		/// This buffer is also quite large - so pay attention to Size.
		/// </summary>
		public byte[] Data { get; internal set; }

		/// <summary>
		/// Size of the data
		/// </summary>
		public int Size { get; internal set; }
	}
	
}