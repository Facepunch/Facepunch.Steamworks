using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Class for utilizing the Steam Remote Storage API.
	/// </summary>
	public class SteamRemoteStorage : SteamClientClass<SteamRemoteStorage>
	{
		internal static ISteamRemoteStorage Internal => Interface as ISteamRemoteStorage;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamRemoteStorage( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			return true;
		}


		/// <summary>
		/// Creates a new file, writes the bytes to the file, and then closes the file.
		/// If the target file already exists, it is overwritten
		/// </summary>
		/// <param name="filename">The path of the file.</param>
		/// <param name="data">The bytes of data.</param>
		/// <returns>A boolean, detailing whether or not the operation was successful.</returns>
		public unsafe static bool FileWrite( string filename, byte[] data )
		{
			fixed ( byte* ptr = data )
			{
				return Internal.FileWrite( filename, (IntPtr) ptr, data.Length );
			}
		}

		/// <summary>
		/// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
		/// </summary>
		/// <param name="filename">The path of the file.</param>
		public unsafe static byte[] FileRead( string filename )
		{
			var size = FileSize( filename );
			if ( size <= 0 ) return null;
			var buffer = new byte[size];

			fixed ( byte* ptr = buffer )
			{
				var readsize = Internal.FileRead( filename, (IntPtr)ptr, size );
				if ( readsize != size )
				{
					return null;
				}
				return buffer;
			}
		}

		/// <summary>
		/// Checks whether the specified file exists.
		/// </summary>
		/// <param name="filename">The path of the file.</param>
		/// <returns>Whether or not the file exists.</returns>
		public static bool FileExists( string filename ) => Internal.FileExists( filename );

		/// <summary>
		/// Checks if a specific file is persisted in the steam cloud.
		/// </summary>
		/// <param name="filename">The path of the file.</param>
		/// <returns>Boolean.</returns>
		public static bool FilePersisted( string filename ) => Internal.FilePersisted( filename );

		/// <summary>
		/// Gets the specified file's last modified date/time.
		/// </summary>
		/// <param name="filename">The path of the file.</param>
		/// <returns>A <see cref="DateTime"/> describing when the file was modified last.</returns>
		public static DateTime FileTime( string filename ) => Epoch.ToDateTime( Internal.GetFileTimestamp( filename ) );

		/// <summary>
		/// Returns the specified files size in bytes, or <c>0</c> if the file does not exist.
		/// </summary>
		/// <param name="filename">The path of the file.</param>
		/// <returns>The size of the file in bytes, or <c>0</c> if the file doesn't exist.</returns>
		public static int FileSize( string filename ) => Internal.GetFileSize( filename );

		/// <summary>
		/// Deletes the file from remote storage, but leaves it on the local disk and remains accessible from the API.
		/// </summary>
		/// <returns>A boolean, detailing whether or not the operation was successful.</returns>
		public static bool FileForget( string filename ) => Internal.FileForget( filename );

		/// <summary>
		/// Deletes a file from the local disk, and propagates that delete to the cloud.
		/// </summary>
		public static bool FileDelete( string filename ) => Internal.FileDelete( filename );


		/// <summary>
		/// Gets the total number of quota bytes.
		/// </summary>
		public static ulong QuotaBytes
		{
			get
			{
				ulong t = 0, a = 0;
				Internal.GetQuota( ref t, ref a );
				return t;
			}
		}

		/// <summary>
		/// Gets the total number of quota bytes that have been used.
		/// </summary>
		public static ulong QuotaUsedBytes
		{
			get
			{
				ulong t = 0, a = 0;
				Internal.GetQuota( ref t, ref a );
				return t - a;
			}
		}

		/// <summary>
		/// Number of bytes remaining until the quota is used.
		/// </summary>
		public static ulong QuotaRemainingBytes
		{
			get
			{
				ulong t = 0, a = 0;
				Internal.GetQuota( ref t, ref a );
				return a;
			}
		}

		/// <summary>
		/// returns <see langword="true"/> if <see cref="IsCloudEnabledForAccount"/> AND <see cref="IsCloudEnabledForApp"/> are <see langword="true"/>.
		/// </summary>
		public static bool IsCloudEnabled => IsCloudEnabledForAccount && IsCloudEnabledForApp;

		/// <summary>
		/// Checks if the account wide Steam Cloud setting is enabled for this user
		/// or if they disabled it in the Settings->Cloud dialog.
		/// </summary>
		public static bool IsCloudEnabledForAccount => Internal.IsCloudEnabledForAccount();

		/// <summary>
		/// Checks if the per game Steam Cloud setting is enabled for this user
		/// or if they disabled it in the Game Properties->Update dialog.
		/// 
		/// This must only ever be set as the direct result of the user explicitly 
		/// requesting that it's enabled or not. This is typically accomplished with 
		/// a checkbox within your in-game options
		/// </summary>
		public static bool IsCloudEnabledForApp
		{
			get => Internal.IsCloudEnabledForApp();
			set => Internal.SetCloudEnabledForApp( value );
		}

		/// <summary>
		/// Gets the total number of local files synchronized by Steam Cloud.
		/// </summary>
		public static int FileCount => Internal.GetFileCount();

		/// <summary>
		/// Gets a list of filenames synchronized by Steam Cloud.
		/// </summary>
		public static IEnumerable<string> Files
		{
			get
			{
				int _ = 0;
				for( int i=0; i<FileCount; i++ )
				{
					var filename = Internal.GetFileNameAndSize( i, ref _ );
					yield return filename;
				}
			}
		}

		/// <summary>
		/// Share a file with the Steam community.
		/// The file must be in the user's Steam Cloud storage.
		/// </summary>
		/// <param name="filename">The name of the file to share</param>
		/// <returns>A task that returns the shared file UGC handle on success</returns>
		public static async Task<Data.Ugc?> FileShareAsync( string filename )
		{
			var result = await Internal.FileShare( filename );
			if ( !result.HasValue || result.Value.Result != Result.OK )
				return null;

			return new Data.Ugc { Handle = result.Value.File };
		}

		/// <summary>
		/// Downloads UGC content to Steam's local cache.
		/// </summary>
		/// <param name="ugc">The UGC handle to download</param>
		/// <param name="priority">Download priority (higher values get downloaded first)</param>
		/// <returns>A task that returns the download result on completion</returns>
		public static async Task<UgcDownloadResult?> UgcDownloadAsync( Data.Ugc ugc, uint priority = 0 )
		{
			var result = await Internal.UGCDownload( ugc.Handle, priority );
			if ( !result.HasValue || result.Value.Result != Result.OK )
				return null;

			return new UgcDownloadResult
			{
				Result = result.Value.Result,
				Handle = new Data.Ugc { Handle = result.Value.File },
				AppId = result.Value.AppID,
				SizeInBytes = result.Value.SizeInBytes,
				Filename = result.Value.PchFileNameUTF8(),
				Owner = result.Value.SteamIDOwner
			};
		}

		/// <summary>
		/// Downloads UGC content to a specific location on disk.
		/// </summary>
		/// <param name="ugc">The UGC handle to download</param>
		/// <param name="location">The local file path to download to</param>
		/// <param name="priority">Download priority (higher values get downloaded first)</param>
		/// <returns>A task that returns the download result on completion</returns>
		public static async Task<UgcDownloadResult?> UgcDownloadToLocationAsync( Data.Ugc ugc, string location, uint priority = 0 )
		{
			var result = await Internal.UGCDownloadToLocation( ugc.Handle, location, priority );
			if ( !result.HasValue || result.Value.Result != Result.OK )
				return null;

			return new UgcDownloadResult
			{
				Result = result.Value.Result,
				Handle = new Data.Ugc { Handle = result.Value.File },
				AppId = result.Value.AppID,
				SizeInBytes = result.Value.SizeInBytes,
				Filename = result.Value.PchFileNameUTF8(),
				Owner = result.Value.SteamIDOwner
			};
		}

		/// <summary>
		/// Reads the contents of a UGC file into a byte array.
		/// </summary>
		/// <param name="ugc">The UGC handle to read from</param>
		/// <param name="maxSize">Maximum size to read (default 10MB for safety)</param>
		/// <returns>The data read from the UGC file, or null if failed</returns>
		public static unsafe byte[] UgcRead( Data.Ugc ugc, int maxSize = 10 * 1024 * 1024 )
		{
			// Try to get size from download progress first
			var progress = GetUgcDownloadProgress( ugc );
			var size = progress?.BytesExpected ?? maxSize;
			
			if ( size <= 0 || size > maxSize )
				size = maxSize;

			var buffer = new byte[size];

			fixed ( byte* ptr = buffer )
			{
				// Use Close action (2) to close the file handle after reading
				var bytesRead = Internal.UGCRead( ugc.Handle, (IntPtr)ptr, size, 0, (UGCReadAction)2 );
				if ( bytesRead <= 0 )
					return null;

				// Trim buffer to actual bytes read
				if ( bytesRead != size )
				{
					var trimmedBuffer = new byte[bytesRead];
					Array.Copy( buffer, trimmedBuffer, bytesRead );
					return trimmedBuffer;
				}

				return buffer;
			}
		}

		/// <summary>
		/// Gets the download progress for a UGC file.
		/// </summary>
		/// <param name="ugc">The UGC handle to check progress for</param>
		/// <returns>Download progress information, or null if not downloading</returns>
		public static UgcDownloadProgress? GetUgcDownloadProgress( Data.Ugc ugc )
		{
			var bytesDownloaded = 0;
			var bytesExpected = 0;

			if ( !Internal.GetUGCDownloadProgress( ugc.Handle, ref bytesDownloaded, ref bytesExpected ) )
				return null;

			return new UgcDownloadProgress
			{
				Handle = ugc,
				BytesDownloaded = bytesDownloaded,
				BytesExpected = bytesExpected,
				Progress = bytesExpected > 0 ? (float)bytesDownloaded / bytesExpected : 0f
			};
		}

	}
}
