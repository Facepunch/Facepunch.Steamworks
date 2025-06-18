using System;

namespace Steamworks.Data
{
	/// <summary>
	/// Result from downloading UGC content
	/// </summary>
	public struct UgcDownloadResult
	{
		/// <summary>
		/// The result of the download operation
		/// </summary>
		public Result Result { get; internal set; }

		/// <summary>
		/// The UGC handle that was downloaded
		/// </summary>
		public Ugc Handle { get; internal set; }

		/// <summary>
		/// The App ID that created this content
		/// </summary>
		public AppId AppId { get; internal set; }

		/// <summary>
		/// Size of the downloaded file in bytes
		/// </summary>
		public int SizeInBytes { get; internal set; }

		/// <summary>
		/// Name of the downloaded file
		/// </summary>
		public string Filename { get; internal set; }

		/// <summary>
		/// Steam ID of the user who created this content
		/// </summary>
		public SteamId Owner { get; internal set; }
	}
}