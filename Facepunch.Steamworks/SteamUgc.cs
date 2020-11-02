﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Functions for accessing and manipulating Steam user information.
	/// This is also where the APIs for Steam Voice are exposed.
	/// </summary>
	public class SteamUGC : SteamSharedClass<SteamUGC>
	{
		internal static ISteamUGC Internal => Interface as ISteamUGC;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamUGC( server ) );
			InstallEvents( server );
		}

		internal static void InstallEvents( bool server )
		{
			Dispatch.Install<DownloadItemResult_t>( x => OnDownloadItemResult?.Invoke( x.Result ), server );
		}

		/// <summary>
		/// Posted after Download call
		/// </summary>
		public static event Action<Result> OnDownloadItemResult;

		public static async Task<bool> DeleteFileAsync( PublishedFileId fileId )
		{
			var r = await Internal.DeleteItem( fileId );
			return r?.Result == Result.OK;
		}

		/// <summary>
		/// Start downloading this item. You'll get notified of completion via OnDownloadItemResult.
		/// </summary>
		/// <param name="fileId">The ID of the file you want to download</param>
		/// <param name="highPriority">If true this should go straight to the top of the download list</param>
		/// <returns>true if nothing went wrong and the download is started</returns>
		public static bool Download( PublishedFileId fileId, bool highPriority = false )
		{
			return Internal.DownloadItem( fileId, highPriority );
		}

		/// <summary>
		/// Will attempt to download this item asyncronously - allowing you to instantly react to its installation
		/// </summary>
		/// <param name="fileId">The ID of the file you want to download</param>
		/// <param name="progress">An optional callback</param>
		/// <param name="ct">Allows you to send a message to cancel the download anywhere during the process</param>
		/// <param name="milisecondsUpdateDelay">How often to call the progress function</param>
		/// <returns>true if downloaded and installed correctly</returns>
		public static async Task<bool> DownloadAsync( PublishedFileId fileId, Action<float> progress = null, int milisecondsUpdateDelay = 60, CancellationToken ct = default )
		{
			var item = new Steamworks.Ugc.Item( fileId );

			if ( ct == default )
				ct = new CancellationTokenSource( TimeSpan.FromSeconds( 60 ) ).Token;

			progress?.Invoke( 0.0f );

			if ( Download( fileId, true ) == false )
				return item.IsInstalled;

			// Steam docs about Download:
			// If the return value is true then register and wait
			// for the Callback DownloadItemResult_t before calling 
			// GetItemInstallInfo or accessing the workshop item on disk.

			// Wait for DownloadItemResult_t
			{
				Action<Result> onDownloadStarted = null;

				try
				{
					var downloadStarted = false;
					
					onDownloadStarted = r => downloadStarted = true;
					OnDownloadItemResult += onDownloadStarted;

					while ( downloadStarted == false )
					{
						if ( ct.IsCancellationRequested )
							break;

						await Task.Delay( milisecondsUpdateDelay );
					}
				}
				finally
				{
					OnDownloadItemResult -= onDownloadStarted;
				}
			}

			progress?.Invoke( 0.2f );
			await Task.Delay( milisecondsUpdateDelay );

			//Wait for downloading completion
			{
				while ( true )
				{
					if ( ct.IsCancellationRequested )
						break;

					progress?.Invoke( 0.2f + item.DownloadAmount * 0.8f );

					if ( !item.IsDownloading && item.IsInstalled )
						break;

					await Task.Delay( milisecondsUpdateDelay );
				}
			}

			progress?.Invoke( 1.0f );

			return item.IsInstalled;
		}

		/// <summary>
		/// Utility function to fetch a single item. Internally this uses Ugc.FileQuery -
		/// which you can use to query multiple items if you need to.
		/// </summary>
		public static async Task<Ugc.Item?> QueryFileAsync( PublishedFileId fileId )
		{
			var result = await Ugc.Query.All
									.WithFileId( fileId )
									.GetPageAsync( 1 );

			if ( !result.HasValue || result.Value.ResultCount != 1 )
				return null;

			var item = result.Value.Entries.First();

			result.Value.Dispose();

			return item;
		}

		public static async Task<bool> StartPlaytimeTracking(PublishedFileId fileId)
		{
			var result = await Internal.StartPlaytimeTracking(new[] {fileId}, 1);
			return result.Value.Result == Result.OK;
		}
		
		public static async Task<bool> StopPlaytimeTracking(PublishedFileId fileId)
		{
			var result = await Internal.StopPlaytimeTracking(new[] {fileId}, 1);
			return result.Value.Result == Result.OK;
		}
		
		public static async Task<bool> StopPlaytimeTrackingForAllItems()
		{
			var result = await Internal.StopPlaytimeTrackingForAllItems();
			return result.Value.Result == Result.OK;
		}
	}
}
