using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	public struct Screenshot
	{
		internal ScreenshotHandle Value;

		/// <summary>
		/// Tags a user as being visible in the screenshot
		/// </summary>
		public bool TagUser( SteamId user )
		{
			return SteamScreenshots.Internal.TagUser( Value, user );
		}

		/// <summary>
		/// Tags a user as being visible in the screenshot
		/// </summary>
		public bool SetLocation( string location )
		{
			return SteamScreenshots.Internal.SetLocation( Value, location );
		}

		/// <summary>
		/// Tags a user as being visible in the screenshot
		/// </summary>
		public bool TagPublishedFile( PublishedFileId file )
		{
			return SteamScreenshots.Internal.TagPublishedFile( Value, file );
		}
	}
}