using System;

namespace Steamworks.Data
{
	/// <summary>
	/// Download progress information for a UGC file
	/// </summary>
	public struct UgcDownloadProgress
	{
		/// <summary>
		/// The UGC handle being downloaded
		/// </summary>
		public Ugc Handle { get; internal set; }

		/// <summary>
		/// Number of bytes downloaded so far
		/// </summary>
		public int BytesDownloaded { get; internal set; }

		/// <summary>
		/// Total number of bytes expected to download
		/// </summary>
		public int BytesExpected { get; internal set; }

		/// <summary>
		/// Download progress as a value between 0.0 and 1.0
		/// </summary>
		public float Progress { get; internal set; }

		/// <summary>
		/// Whether the download is complete
		/// </summary>
		public bool IsComplete => BytesDownloaded >= BytesExpected && BytesExpected > 0;
	}
}