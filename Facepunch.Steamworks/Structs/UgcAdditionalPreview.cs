using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	/// <summary>
	/// One entry in a Workshop item's preview gallery &#8212; a screenshot, a video, or one of the
	/// specialised image types &#8212; beyond the single main thumbnail.
	/// </summary>
	/// <remarks>
	/// These are only populated on items returned by a query that asked for them with
	/// <c>WithAdditionalPreviews( true )</c>. Otherwise the owning item's preview array is null.
	/// </remarks>
	/// <example>
	/// <code>
	/// var page = await Ugc.Query.Items.WithAdditionalPreviews( true ).GetPageAsync( 1 );
	/// using ( var results = page.Value )
	/// {
	///     foreach ( var item in results.Entries )
	///     foreach ( var preview in item.AdditionalPreviews ?? Array.Empty&lt;UgcAdditionalPreview&gt;() )
	///     {
	///         if ( preview.ItemPreviewType == ItemPreviewType.Image )
	///             DownloadAndShow( preview.UrlOrVideoID );
	///     }
	/// }
	/// </code>
	/// </example>
	public struct UgcAdditionalPreview
	{
		internal UgcAdditionalPreview( string urlOrVideoID, string originalFileName, ItemPreviewType itemPreviewType )
		{
			this.UrlOrVideoID = urlOrVideoID;
			this.OriginalFileName = originalFileName;
			this.ItemPreviewType = itemPreviewType;
		}

		/// <summary>
		/// Either an HTTP URL to the preview image, or a video id, depending on
		/// <see cref="ItemPreviewType"/>. You must check the type before deciding how to use this
		/// &#8212; the two are not distinguishable from the string alone.
		/// </summary>
		/// <remarks>
		/// For image types this is a URL you fetch yourself; Steam does not download it for you.
		/// For video types it is an id for the hosting service, not a playable URL.
		/// </remarks>
		public string UrlOrVideoID { get; private set; }

		/// <summary>
		/// The file name the creator originally uploaded this preview as.
		/// </summary>
		/// <remarks>
		/// Valve documents nothing about this field, and this binding's own source carries a
		/// "what is this???" comment where it is read. Treat it as cosmetic: it is frequently empty,
		/// and it should not be used to identify or key a preview.
		/// </remarks>
		public string OriginalFileName { get; private set; }

		/// <summary>
		/// What kind of preview this is &#8212; a plain image, a YouTube video, a Sketchfab model, a
		/// cubemap, and so on. Decides how to interpret <see cref="UrlOrVideoID"/>.
		/// </summary>
		/// <remarks>
		/// Valve reserves values above <c>k_EItemPreviewType_ReservedMax</c> (255) for app-defined
		/// types, so an unrecognised value is not necessarily an error.
		/// </remarks>
		public ItemPreviewType ItemPreviewType { get; private set; }
	}
}
