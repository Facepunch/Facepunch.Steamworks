using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Steam Cloud: a per-user, per-app file store that Steam synchronises across every machine the
	/// user plays on. This is how save games, settings and profiles follow a player between their
	/// desktop, their laptop and Steam Deck without you running a server.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>It is a flat key/value store, not a filesystem.</b> Names may contain <c>/</c> and it will
	/// look like a path, but there are no real directories, no enumeration by prefix, and no rename.
	/// Treat the name as an opaque key.
	/// </para>
	/// <para>
	/// <b>Cloud must be enabled on the partner site before any of this syncs.</b> An app with no
	/// Cloud quota configured still lets you write and read files locally &#8212; they simply never
	/// leave the machine. Nothing reports an error. Check <see cref="IsCloudEnabled"/> before
	/// promising the player their saves are backed up.
	/// </para>
	/// <para>
	/// <b>Writes are synchronous and unbuffered.</b> <see cref="FileWrite"/> blocks while the whole
	/// array is handed to Steam, so writing a large save on the main thread will hitch. Steam uploads
	/// in the background afterwards; <see cref="FilePersisted"/> is how you find out whether that has
	/// happened yet.
	/// </para>
	/// <para>
	/// Valve's <c>isteamremotestorage.h</c> is the least documented header in the SDK &#8212; 48 of
	/// its 59 methods carry no comment at all &#8212; so much of the detail below is inferred from
	/// this binding's behaviour and is labelled where that is the case. The header's publish and
	/// enumerate block is also superseded by <see cref="SteamUGC"/> with no cross-reference; if you
	/// are looking for Workshop functionality it is there, not here.
	/// </para>
	/// </remarks>
	/// <example>
	/// A save/load pair with the checks that matter:
	/// <code>
	/// // Save
	/// if ( !SteamRemoteStorage.IsCloudEnabled )
	///     Console.WriteLine( "Cloud is off - this write stays on this machine." );
	///
	/// var bytes = Serialize( saveGame );
	/// if ( bytes.Length &gt; (long)SteamRemoteStorage.QuotaRemainingBytes )
	///     throw new IOException( "Not enough Steam Cloud quota." );
	///
	/// if ( !SteamRemoteStorage.FileWrite( "save0.dat", bytes ) )
	///     Console.WriteLine( "Write failed." );
	///
	/// // Load
	/// if ( SteamRemoteStorage.FileExists( "save0.dat" ) )
	/// {
	///     var loaded = SteamRemoteStorage.FileRead( "save0.dat" );
	///     if ( loaded != null )
	///         saveGame = Deserialize( loaded );
	/// }
	/// </code>
	/// </example>
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
		/// <param name="filename">
		/// The name to store under. Flat key rather than a real path &#8212; see the remarks on
		/// <see cref="SteamRemoteStorage"/>. Case sensitivity is not documented by Valve; assume it
		/// is significant, because the same file has to resolve on Linux and Windows.
		/// </param>
		/// <param name="data">
		/// The bytes to write. Written in full; there is no append and no partial write. Steam copies
		/// the buffer during the call, so you may reuse the array immediately afterwards.
		/// </param>
		/// <returns>
		/// <see langword="true"/> on success. <see langword="false"/> covers every failure without
		/// distinguishing them &#8212; most often the write would exceed the user's remaining quota,
		/// but a file larger than Valve's per-file limit and a disk error look the same.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Success here means the file is on local disk and queued for upload. It does <b>not</b>
		/// mean it reached Steam's servers; poll <see cref="FilePersisted"/> for that.
		/// </para>
		/// <para>
		/// Check <see cref="QuotaRemainingBytes"/> before writing anything large. A quota failure is
		/// indistinguishable from any other failure once it has happened.
		/// </para>
		/// <para>
		/// A null <paramref name="data"/> throws <see cref="NullReferenceException"/> rather than
		/// writing an empty file.
		/// </para>
		/// </remarks>
		public unsafe static bool FileWrite( string filename, byte[] data )
		{
			fixed ( byte* ptr = data )
			{
				return Internal.FileWrite( filename, (IntPtr) ptr, data.Length );
			}
		}

		/// <summary>
		/// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
		/// Reads come from the local copy, so this does not wait on the network.
		/// </summary>
		/// <param name="filename">The name the file was written under.</param>
		/// <returns>
		/// The file's contents, or <see langword="null"/>. Null means one of three things this API
		/// cannot tell apart: the file does not exist, the file exists but is empty (because
		/// <see cref="FileSize"/> returns 0 for both), or the read returned fewer bytes than expected.
		/// Use <see cref="FileExists"/> first if the distinction matters.
		/// </returns>
		/// <remarks>
		/// Allocates a fresh array sized to the whole file every call, and there is no streaming or
		/// partial-read overload here, so a large save is a large allocation on the caller's thread.
		/// </remarks>
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
		/// Checks whether the specified file exists in this app's Cloud store, locally or remotely.
		/// </summary>
		/// <param name="filename">The name the file was written under.</param>
		/// <returns>
		/// <see langword="true"/> if the file exists. This says nothing about whether it has been
		/// uploaded &#8212; see <see cref="FilePersisted"/> for that.
		/// </returns>
		public static bool FileExists( string filename ) => Internal.FileExists( filename );

		/// <summary>
		/// Checks if a specific file has finished uploading to the Steam Cloud, as opposed to merely
		/// existing on this machine. This is how you tell the player their save is safely backed up.
		/// </summary>
		/// <param name="filename">The name the file was written under.</param>
		/// <returns>
		/// <see langword="true"/> once Steam has the file. <see langword="false"/> while an upload is
		/// still pending, if Cloud is disabled, and for a file that does not exist &#8212; the three
		/// are not distinguishable here.
		/// </returns>
		/// <remarks>
		/// Newly written files are not persisted immediately. Valve does not document how long upload
		/// takes or guarantee it completes before the process exits, so do not treat a
		/// <see langword="false"/> right after <see cref="FileWrite"/> as a failure.
		/// </remarks>
		public static bool FilePersisted( string filename ) => Internal.FilePersisted( filename );

		/// <summary>
		/// Gets the specified file's last modified date/time. Useful for picking the newest of several
		/// save slots, or for noticing that another machine wrote a newer save than the one you hold.
		/// </summary>
		/// <param name="filename">The name the file was written under.</param>
		/// <returns>
		/// A <see cref="DateTime"/> describing when the file was modified last, in UTC. A file that
		/// does not exist yields the Unix epoch (1970-01-01) rather than an error, because the
		/// underlying call reports 0 and this converts it literally.
		/// </returns>
		public static DateTime FileTime( string filename ) => Epoch.ToDateTime( Internal.GetFileTimestamp( filename ) );

		/// <summary>
		/// Returns the specified file's size in bytes, or <c>0</c> if the file does not exist.
		/// Read this before <see cref="FileRead"/> if you need to refuse an unreasonably large file
		/// rather than allocating it.
		/// </summary>
		/// <param name="filename">The name the file was written under.</param>
		/// <returns>
		/// The size of the file in bytes, or <c>0</c> if the file doesn't exist &#8212; which is
		/// indistinguishable from a file that exists and is empty. This is also why
		/// <see cref="FileRead"/> returns <see langword="null"/> for both cases.
		/// </returns>
		public static int FileSize( string filename ) => Internal.GetFileSize( filename );

		/// <summary>
		/// Deletes the file from remote storage, but leaves it on the local disk and remains accessible from the API.
		/// Use this to stop a large or machine-specific file consuming the user's Cloud quota without
		/// taking it away from them here.
		/// </summary>
		/// <param name="filename">The name the file was written under.</param>
		/// <returns>
		/// <see langword="true"/> on success. Valve does not document the failure conditions; a file
		/// that was never in the cloud is the obvious candidate.
		/// </returns>
		/// <remarks>
		/// Forgetting is not deleting. The file stays on this machine and
		/// <see cref="FileExists"/> keeps returning <see langword="true"/> &#8212; it simply will not
		/// appear on the user's other machines. Writing to the same name again re-uploads it.
		/// </remarks>
		public static bool FileForget( string filename ) => Internal.FileForget( filename );

		/// <summary>
		/// Deletes a file from the local disk, and propagates that delete to the cloud. This is the
		/// destructive one &#8212; use <see cref="FileForget"/> if you only want to reclaim quota.
		/// </summary>
		/// <param name="filename">The name the file was written under.</param>
		/// <returns>
		/// <see langword="true"/> on success. Deleting a file that does not exist is not
		/// distinguished from a genuine failure.
		/// </returns>
		/// <remarks>
		/// There is no undo and no recycle bin. The delete propagates to every machine the user syncs
		/// on, so this destroys the save everywhere, not just here.
		/// </remarks>
		public static bool FileDelete( string filename ) => Internal.FileDelete( filename );


		/// <summary>
		/// The user's total Steam Cloud allowance for this app, in bytes, as configured on the
		/// Steamworks partner site. Quota is per app and per user, not shared across a library.
		/// </summary>
		/// <remarks>
		/// This and <see cref="QuotaUsedBytes"/> and <see cref="QuotaRemainingBytes"/> each make their
		/// own native call, so reading all three is three round trips and they are not guaranteed to
		/// be consistent with one another. Read <see cref="QuotaRemainingBytes"/> alone if that is all
		/// you need. Zero here usually means Cloud is not configured for the app at all.
		/// </remarks>
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
		/// How much of the user's Cloud allowance this app is currently consuming, in bytes.
		/// Computed as total minus available rather than read directly.
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
		/// How many more bytes this app may store for the user before writes start failing. Check
		/// this before a large <see cref="FileWrite"/> &#8212; once a write has failed there is no
		/// way to tell a quota failure apart from any other.
		/// </summary>
		/// <remarks>
		/// A file you overwrite frees its old size, so the space you need is the delta, not the whole
		/// new file. Valve does not document whether pending deletes are reflected here immediately.
		/// </remarks>
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
		/// How many files this app has in the user's Cloud store. Counts every file Steam is tracking
		/// for the app, not only the ones this session wrote.
		/// </summary>
		public static int FileCount => Internal.GetFileCount();

		/// <summary>
		/// Every filename this app has stored, which is the only way to discover files &#8212; there
		/// is no pattern matching or prefix search. Use it to migrate or clean up saves written by an
		/// older version of your game.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Enumerated lazily, and <see cref="FileCount"/> is re-read on every iteration, so this is
		/// one native call per file plus one per step. Materialise it with <c>ToList()</c> if you are
		/// going to walk it more than once.
		/// </para>
		/// <para>
		/// Because the count is re-read each step, writing or deleting files while enumerating can
		/// skip entries or repeat them. Snapshot first if you intend to modify.
		/// </para>
		/// </remarks>
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

	}
}
