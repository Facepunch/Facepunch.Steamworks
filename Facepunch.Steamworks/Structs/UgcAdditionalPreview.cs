using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	public struct UgcAdditionalPreview
	{
		internal UgcAdditionalPreview( string urlOrVideoID, string originalFileName, ItemPreviewType itemPreviewType )
		{
			this.UrlOrVideoID = urlOrVideoID;
			this.OriginalFileName = originalFileName;
			this.ItemPreviewType = itemPreviewType;
		}

		public string UrlOrVideoID { get; private set; }
		public string OriginalFileName { get; private set; }
		internal ItemPreviewType ItemPreviewType { get; private set; }

		/// <summary>
		/// Flags that specify the type of preview an item has:
		/// Image = 0,
		/// YouTubeVideo = 1,
		/// Sketchfab = 2,
		/// EnvironmentMap_HorizontalCross = 3,
		/// EnvironmentMap_LatLong = 4,
		/// ReservedMax = 255
		/// </summary>
		public int GetItemPreviewType() => (int)ItemPreviewType;
	}
}
