using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Steamworks.Data;

using QueryType = Steamworks.Ugc.Query;

namespace Steamworks.Ugc
{
	/// <summary>
	/// A fluent builder that publishes content to the Steam Workshop, or updates content already
	/// published. It covers both halves of the job &#8212; creating the Workshop entry and uploading
	/// the files &#8212; behind one awaited call.
	/// </summary>
	/// <remarks>
	/// <para>
	/// There are two modes. Starting from one of the static factories
	/// (<see cref="NewCommunityFile"/>, <see cref="NewCollection"/>, ...) <b>creates a new item</b>
	/// each time you submit. Starting from the <see cref="Editor(Steamworks.Data.PublishedFileId)"/>
	/// constructor, or from <see cref="Item.Edit"/>, updates that existing item.
	/// </para>
	/// <para>
	/// <b>Only the fields you set are sent.</b> Anything you leave alone is untouched on Steam rather
	/// than cleared, so a partial update is safe. The corollary is that there is no way to clear a
	/// title or description through this builder &#8212; you can only replace it.
	/// </para>
	/// <para>
	/// <b>The Workshop legal agreement.</b> A user who has not accepted it can create items, but they
	/// stay invisible to everyone else until they do. Submitting reports this through
	/// <see cref="PublishResult.NeedsWorkshopAgreement"/>; if you ignore it your users will publish
	/// items that silently never appear. This is the single most common Workshop integration bug.
	/// </para>
	/// <para>
	/// <b>Same struct-aliasing trap as <see cref="Query"/>.</b> This is a mutable struct whose tag and
	/// preview collections are reference types, so a copy taken from an editor shares those
	/// collections with the original. Build each publish from a fresh factory call rather than
	/// branching or reusing one.
	/// </para>
	/// </remarks>
	/// <example>
	/// Publishing a new Workshop item with content, a thumbnail and tags:
	/// <code>
	/// var result = await Ugc.Editor.NewCommunityFile
	///                              .WithTitle( "My Map" )
	///                              .WithDescription( "A map." )
	///                              .WithContent( "C:/mymap/content" )   // folder, must not be empty
	///                              .WithPreviewFile( "C:/mymap/thumb.jpg" )  // under 1MB
	///                              .WithTag( "Map" )
	///                              .WithPublicVisibility()
	///                              .SubmitAsync( new Progress&lt;float&gt;( f =&gt; Console.WriteLine( $"{f:P0}" ) ) );
	///
	/// if ( !result.Success )
	///     Console.WriteLine( $"publish failed: {result.Result}" );
	/// else if ( result.NeedsWorkshopAgreement )
	///     Console.WriteLine( "Published, but the user must accept the Workshop legal agreement "
	///                      + "before anyone else can see it." );
	/// else
	///     Console.WriteLine( $"published as {result.FileId}" );
	/// </code>
	/// </example>
	public struct Editor
	{
		PublishedFileId fileId;

		bool creatingNew;
		WorkshopFileType creatingType;
		AppId consumerAppId;

		internal Editor( WorkshopFileType filetype ) : this()
		{
			this.creatingNew = true;
			this.creatingType = filetype;
		}

		/// <summary>
		/// Start editing an item that already exists. Submitting updates that item rather than
		/// creating a new one.
		/// </summary>
		/// <param name="fileId">The published file id to update. Must be owned by the local user.</param>
		/// <remarks>
		/// The editor starts empty, not pre-filled with the item's current values. Set only the fields
		/// you want to change.
		/// </remarks>
		public Editor( PublishedFileId fileId ) : this()
		{
			this.fileId = fileId;
		}

		/// <summary>
		/// Create a Normal Workshop item that can be subscribed to
		/// </summary>
		public static Editor NewCommunityFile => new Editor( WorkshopFileType.Community );

		/// <summary>
		/// Create a Collection
		/// Add items using Item.AddDependency()
		/// </summary>
		public static Editor NewCollection => new Editor( WorkshopFileType.Collection );

		/// <summary>
		/// Workshop item that is meant to be voted on for the purpose of selling in-game
		/// </summary>
		public static Editor NewMicrotransactionFile => new Editor( WorkshopFileType.Microtransaction );

		/// <summary>
		/// Workshop item that is meant to be managed by the game. It is queryable by the API, but isn't visible on the web browser.
		/// </summary>
		public static Editor NewGameManagedFile => new Editor(WorkshopFileType.GameManagedItem);

		/// <summary>
		/// Publish to a different app's Workshop than the one currently running. Almost no game needs
		/// this; it exists for tools and launchers that publish on behalf of another app.
		/// </summary>
		/// <param name="id">The consumer app id to publish under. Defaults to <see cref="SteamClient.AppId"/>.</param>
		/// <returns>The editor, for chaining.</returns>
		/// <remarks>
		/// The logged in user needs publishing rights on the target app. Valve does not document what
		/// is returned when they do not; expect a non-OK result rather than an exception.
		/// </remarks>
		public Editor ForAppId( AppId id ) { this.consumerAppId = id; return this; }

		string Title;

		/// <summary>
		/// Set the item's title &#8212; the name players see in the Workshop and in your in-game
		/// browser.
		/// </summary>
		/// <param name="t">
		/// The title. Valve caps it at <c>k_cchPublishedDocumentTitleMax</c>; the header does not say
		/// whether an over-long title is rejected or truncated.
		/// </param>
		/// <returns>The editor, for chaining.</returns>
		public Editor WithTitle( string t ) { this.Title = t; return this; }

		string Description;

		/// <summary>
		/// Set the item's long description, shown on its Workshop page. Supports Steam's BBCode-style
		/// markup.
		/// </summary>
		/// <param name="t">The description text, capped by Valve at <c>k_cchPublishedDocumentDescriptionMax</c>.</param>
		/// <returns>The editor, for chaining.</returns>
		public Editor WithDescription( string t ) { this.Description = t; return this; }

		string MetaData;

		/// <summary>
		/// Attach a private blob of developer metadata to the item. This is not shown to players; it
		/// is for your game to read back, and is typically JSON describing the item &#8212; a schema
		/// version, required features, a content manifest.
		/// </summary>
		/// <param name="t">
		/// The metadata. Valve caps this at <c>k_cchDeveloperMetadataMax = 5000</c> bytes; the header
		/// does not say whether exceeding it truncates or fails.
		/// </param>
		/// <returns>The editor, for chaining.</returns>
		/// <remarks>
		/// Read it back by querying with <see cref="Query.WithMetadata"/> and looking at
		/// <see cref="Item.Metadata"/>. It is not returned by default.
		/// </remarks>
		public Editor WithMetaData( string t ) { this.MetaData = t; return this; }

		string ChangeLog;

		/// <summary>
		/// Set the change note attached to this update, shown in the item's public changelog. Good
		/// practice for any update to an existing item.
		/// </summary>
		/// <param name="t">The change note. If you never call this, an empty note is submitted.</param>
		/// <returns>The editor, for chaining.</returns>
		public Editor WithChangeLog( string t ) { this.ChangeLog = t; return this; }

		string Language;

		/// <summary>
		/// Declare which language the title and description you are setting are written in, so Steam
		/// can store them as that language's translation rather than overwriting the default.
		/// </summary>
		/// <param name="t">
		/// An API language code as used elsewhere in Steamworks &#8212; <c>"english"</c>,
		/// <c>"french"</c>, <c>"schinese"</c> and so on, not an ISO code.
		/// </param>
		/// <returns>The editor, for chaining.</returns>
		public Editor InLanguage( string t ) { this.Language = t; return this; }

		string PreviewFile;

		/// <summary>
		/// Set the item's main thumbnail &#8212; the image shown in Workshop listings.
		/// </summary>
		/// <param name="t">
		/// Absolute path to a <b>local image file</b>, not a URL. Valve requires it to be
		/// <b>under 1MB</b>. The header does not list the accepted formats; JPG and PNG are what the
		/// Workshop accepts in practice.
		/// </param>
		/// <returns>The editor, for chaining.</returns>
		/// <remarks>
		/// An oversized or missing file does not throw here. The path is only handed to Steam when you
		/// submit, and a rejection surfaces as a non-OK <see cref="PublishResult.Result"/>.
		/// </remarks>
		public Editor WithPreviewFile( string t ) { this.PreviewFile = t; return this; }

		Dictionary<string, ItemPreviewType> AdditionalPreviewFiles;

		/// <summary>
		/// Attach an extra preview image or video beyond the main thumbnail, for the gallery on the
		/// item's Workshop page.
		/// </summary>
		/// <param name="f">
		/// Absolute path to a local file, which Valve requires to be <b>under 1MB</b>. For video
		/// preview types this is a video id rather than a path.
		/// </param>
		/// <param name="t">What kind of preview this is &#8212; a plain image, a video, a cubemap and so on.</param>
		/// <returns>The editor, for chaining.</returns>
		/// <exception cref="System.ArgumentException">
		/// Thrown if the same path has already been added to this editor; previews are held in a
		/// dictionary keyed by path.
		/// </exception>
		public Editor AddAdditionalPreviewFile( string f, ItemPreviewType t )
		{
			AdditionalPreviewFiles ??= new Dictionary<string, ItemPreviewType>();
			AdditionalPreviewFiles.Add(f, t);
			return this;
		}

		List<int> RemovePreviewFiles;

		/// <summary>
		/// Remove one of the item's existing additional previews.
		/// </summary>
		/// <param name="i">
		/// The preview's <b>index</b>, starting at 0, in the item's current sorted preview list
		/// &#8212; not an id. Read the current previews from
		/// <see cref="Item.AdditionalPreviews"/> (via <see cref="Query.WithAdditionalPreviews"/>) to
		/// find it.
		/// </param>
		/// <returns>The editor, for chaining.</returns>
		/// <remarks>
		/// Indices are positional, so removing several in one submit is fragile: Valve does not
		/// document whether the removals are applied against the original ordering or re-index as they
		/// go. Removing one at a time is the safe pattern.
		/// </remarks>
		public Editor RemoveAdditionalPreviewFile( int i )
		{
			RemovePreviewFiles ??= new List<int>();
			RemovePreviewFiles.Add(i);
			return this;
		}

		System.IO.DirectoryInfo ContentFolder;

		/// <summary>
		/// Set the folder whose contents become the item's payload. Everything under it, recursively,
		/// is uploaded and is what subscribers get on disk.
		/// </summary>
		/// <param name="t">
		/// The content folder. It must exist and must not be empty, or
		/// <see cref="SubmitAsync"/> throws.
		/// </param>
		/// <returns>The editor, for chaining.</returns>
		/// <remarks>
		/// This replaces the item's whole payload rather than merging &#8212; files that exist on
		/// Steam but not in this folder are removed from the item. Omit it entirely to update
		/// metadata without touching content.
		/// </remarks>
		public Editor WithContent( System.IO.DirectoryInfo t ) { this.ContentFolder = t; return this; }

		/// <summary>
		/// Set the content folder by path. Equivalent to
		/// <see cref="WithContent(System.IO.DirectoryInfo)"/>.
		/// </summary>
		/// <param name="folderName">Path to the content folder. Must exist and be non-empty at submit time.</param>
		/// <returns>The editor, for chaining.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown immediately if <paramref name="folderName"/> is null.</exception>
		public Editor WithContent( string folderName ) { return WithContent( new System.IO.DirectoryInfo( folderName ) ); }

		RemoteStoragePublishedFileVisibility? Visibility;

		/// <summary>
		/// Make the item visible to everyone. Note that a public item is still hidden from other users
		/// until its author has accepted the Workshop legal agreement &#8212; see
		/// <see cref="PublishResult.NeedsWorkshopAgreement"/>.
		/// </summary>
		/// <returns>The editor, for chaining.</returns>
		/// <remarks>
		/// If you never set a visibility, the item keeps whatever it already had; newly created items
		/// take Steam's default, which the header does not state.
		/// </remarks>
		public Editor WithPublicVisibility() { Visibility = RemoteStoragePublishedFileVisibility.Public; return this; }

		/// <summary>
		/// Restrict the item to the author's Steam friends. Useful for letting a creator share a
		/// work in progress without publishing it.
		/// </summary>
		/// <returns>The editor, for chaining.</returns>
		public Editor WithFriendsOnlyVisibility() { Visibility = RemoteStoragePublishedFileVisibility.FriendsOnly; return this; }

		/// <summary>
		/// Restrict the item to its author. The usual choice while iterating on an item before
		/// releasing it.
		/// </summary>
		/// <returns>The editor, for chaining.</returns>
		public Editor WithPrivateVisibility() { Visibility = RemoteStoragePublishedFileVisibility.Private; return this; }

		List<string> Tags;
		Dictionary<string, List<string>> KeyValueTags;
		HashSet<string> KeyValueTagsToRemove;

		/// <summary>
		/// Add a Workshop tag to the item, which is how players filter and browse content. Call
		/// repeatedly for several tags.
		/// </summary>
		/// <param name="tag">
		/// The tag text. For a tag to be usable as a filter it must be one of the tags configured for
		/// your app on the Steamworks partner site.
		/// </param>
		/// <returns>The editor, for chaining.</returns>
		/// <remarks>
		/// Submitting <b>replaces</b> the item's whole tag list with the tags on this editor rather
		/// than adding to it, so when updating an existing item you must re-add the tags you want to
		/// keep. Tags accumulate into a list shared by every copy of the editor &#8212; see the
		/// remarks on <see cref="Editor"/>.
		/// </remarks>
		public Editor WithTag( string tag )
		{
			if ( Tags == null ) Tags = new List<string>();

			Tags.Add( tag );

			return this;
		}

		/// <summary>
		/// Adds a key-value tag pair to an item. 
		/// Keys can map to multiple different values (1-to-many relationship). 
		/// Key names are restricted to alpha-numeric characters and the '_' character. 
		/// Both keys and values cannot exceed 255 characters in length. Key-value tags are searchable by exact match only.
		/// To replace all values associated to one key use RemoveKeyValueTags then AddKeyValueTag.
		/// </summary>
		/// <param name="key">
		/// The key. Restricted to alphanumerics and <c>_</c>, and to 255 characters.
		/// </param>
		/// <param name="value">
		/// The value. Also capped at 255 characters. Searchable by exact match only &#8212; there is
		/// no prefix, wildcard or range matching.
		/// </param>
		/// <returns>The editor, for chaining.</returns>
		/// <remarks>
		/// Query these back with <see cref="Query.AddRequiredKeyValueTag"/> to filter, and
		/// <see cref="Query.WithKeyValueTags"/> to have them returned on results. Unlike plain tags,
		/// submitting <b>adds</b> to the item's existing key/value pairs rather than replacing them,
		/// which is why <see cref="RemoveKeyValueTags"/> exists.
		/// </remarks>
		public Editor AddKeyValueTag(string key, string value)
		{
			if (KeyValueTags == null) 
				KeyValueTags = new Dictionary<string, List<string>>();

			if ( KeyValueTags.TryGetValue( key, out var list ) )
				list.Add( value );
			else
				KeyValueTags[key] = new List<string>() { value };

			return this;
		}

		/// <summary>
		/// Removes a key and all values associated to it. 
		/// You can remove up to 100 keys per item update. 
		/// If you need remove more tags than that you'll need to make subsequent item updates.
		/// </summary>
		/// <param name="key">The key to remove, along with every value stored under it.</param>
		/// <returns>The editor, for chaining.</returns>
		/// <remarks>
		/// Removals are applied before additions when you submit, so removing and re-adding the same
		/// key in one editor is the supported way to replace all of a key's values.
		/// </remarks>
		public Editor RemoveKeyValueTags( string key )
		{
			if ( KeyValueTagsToRemove == null )
				KeyValueTagsToRemove = new HashSet<string>();

			KeyValueTagsToRemove.Add( key );
			return this;
		}

		/// <summary>
		/// Run the publish. Creates the Workshop item if this editor came from one of the
		/// <c>New*</c> factories, then uploads the content and applies every field you set, and waits
		/// for Steam to finish committing it.
		/// </summary>
		/// <param name="progress">
		/// Optional receiver for upload progress as a fraction between 0 and 1. The scale is shaped
		/// for UI rather than exact: roughly 0.1 while preparing configuration, 0.2 while preparing
		/// content, 0.2-0.8 during the actual upload, 0.8 while the preview image uploads, and 1 while
		/// committing. Progress is only sampled when this is non-null.
		/// </param>
		/// <param name="onItemCreated">
		/// Optional callback fired as soon as a <b>new</b> item has been assigned its
		/// <see cref="PublishedFileId"/>, before the content upload begins. Use it to persist the id
		/// so a failed or interrupted upload can be resumed against the same item instead of
		/// orphaning it. Never called when updating an existing item.
		/// </param>
		/// <returns>
		/// A <see cref="PublishResult"/>. Check <see cref="PublishResult.Success"/> first, then
		/// <see cref="PublishResult.NeedsWorkshopAgreement"/> &#8212; a submit can succeed and still
		/// leave the item invisible to other users.
		/// </returns>
		/// <exception cref="System.Exception">
		/// Thrown before anything is sent if a content folder was set but does not exist, or exists
		/// but contains no files. Both are checked eagerly so you fail fast rather than creating an
		/// empty Workshop item.
		/// </exception>
		/// <remarks>
		/// <para>
		/// <b>An interrupted submit can leave an orphan.</b> Creating the item and uploading its
		/// content are two separate steps. If the upload fails after creation, the (empty) Workshop
		/// item still exists and this returns a failure without telling you its id unless you supplied
		/// <paramref name="onItemCreated"/>. That is exactly what that callback is for.
		/// </para>
		/// <para>
		/// This polls for completion roughly 60 times a second and has <b>no timeout</b>. A submit
		/// that Steam never completes waits forever, and there is no cancellation token. Uploads of
		/// large content folders legitimately take minutes, so do not treat a long wait as a hang.
		/// </para>
		/// <para>
		/// Callbacks must be pumped for this to complete &#8212; automatic with
		/// <c>SteamClient.Init( appid, asyncCallbacks: true )</c>, otherwise call
		/// <c>SteamClient.RunCallbacks()</c> regularly.
		/// </para>
		/// <para>
		/// Failures from the individual field setters are not surfaced. Each <c>SetItem*</c> call
		/// returns a bool that this method discards, so a rejected title or an oversized preview file
		/// can leave you with a successful-looking submit that did not apply everything you asked for.
		/// </para>
		/// </remarks>
		public async Task<PublishResult> SubmitAsync( IProgress<float> progress = null, Action<PublishResult> onItemCreated = null )
		{
			var result = default( PublishResult );

			progress?.Report( 0 );

			if ( consumerAppId == 0 )
				consumerAppId = SteamClient.AppId;

			//
			// Checks
			//
			if ( ContentFolder != null )
			{
				if ( !System.IO.Directory.Exists( ContentFolder.FullName ) )
					throw new System.Exception( $"UgcEditor - Content Folder doesn't exist ({ContentFolder.FullName})" );

				if ( !ContentFolder.EnumerateFiles( "*", System.IO.SearchOption.AllDirectories ).Any() )
					throw new System.Exception( $"UgcEditor - Content Folder is empty" );
			}


			//
			// Item Create
			//
			if ( creatingNew )
			{
				result.Result = Steamworks.Result.Fail;

				var created = await SteamUGC.Internal.CreateItem( consumerAppId, creatingType );
				if ( !created.HasValue ) return result;

				result.Result = created.Value.Result;

				if ( result.Result != Steamworks.Result.OK )
					return result;

				fileId = created.Value.PublishedFileId;
				result.NeedsWorkshopAgreement = created.Value.UserNeedsToAcceptWorkshopLegalAgreement;
				result.FileId = fileId;

				if ( onItemCreated != null )
					onItemCreated( result );
			}


			result.FileId = fileId;

			//
			// Item Update
			//
			{
				var handle = SteamUGC.Internal.StartItemUpdate( consumerAppId, fileId );
				if ( handle == 0xffffffffffffffff )
					return result;

				if ( Title != null ) SteamUGC.Internal.SetItemTitle( handle, Title );
				if ( Description != null ) SteamUGC.Internal.SetItemDescription( handle, Description );
				if ( MetaData != null ) SteamUGC.Internal.SetItemMetadata( handle, MetaData );
				if ( Language != null ) SteamUGC.Internal.SetItemUpdateLanguage( handle, Language );
				if ( ContentFolder != null ) SteamUGC.Internal.SetItemContent( handle, ContentFolder.FullName );
				if ( PreviewFile != null ) SteamUGC.Internal.SetItemPreview( handle, PreviewFile );
				if ( Visibility.HasValue ) SteamUGC.Internal.SetItemVisibility( handle, Visibility.Value );
				if ( Tags != null && Tags.Count > 0 )
				{
					using ( var a = SteamParamStringArray.From( Tags.ToArray() ) )
					{
						var val = a.Value;
						SteamUGC.Internal.SetItemTags( handle, ref val, false );
					}
				}

				if ( KeyValueTagsToRemove != null)
				{
					foreach ( var key in KeyValueTagsToRemove )
						SteamUGC.Internal.RemoveItemKeyValueTags( handle, key );
				}

				if ( KeyValueTags != null )
				{
					foreach ( var keyWithValues in KeyValueTags )
					{
						var key = keyWithValues.Key;
						foreach ( var value in keyWithValues.Value )
							SteamUGC.Internal.AddItemKeyValueTag( handle, key, value );
					}
				}

				if ( AdditionalPreviewFiles != null )
				{
					foreach ( var file in AdditionalPreviewFiles )
						SteamUGC.Internal.AddItemPreviewFile( handle, file.Key, file.Value );
				}

				if ( RemovePreviewFiles != null )
				{
					foreach ( var fileIndex in RemovePreviewFiles )
						SteamUGC.Internal.RemoveItemPreview( handle, (uint) fileIndex );
				}
				
				result.Result = Steamworks.Result.Fail;

				if ( ChangeLog == null )
					ChangeLog = "";

			    var updating = SteamUGC.Internal.SubmitItemUpdate( handle, ChangeLog );

				while ( !updating.IsCompleted )
				{
					if ( progress != null )
					{
						ulong total = 0;
						ulong processed = 0;

						var r = SteamUGC.Internal.GetItemUpdateProgress( handle, ref processed, ref total );

						switch ( r )
						{
							case ItemUpdateStatus.PreparingConfig:
								{
									progress?.Report( 0.1f );
									break;
								}

							case ItemUpdateStatus.PreparingContent:
								{
									progress?.Report( 0.2f );
									break;
								}
							case ItemUpdateStatus.UploadingContent:
								{
									var uploaded = total > 0 ? ((float)processed / (float)total) : 0.0f;
									progress?.Report( 0.2f + uploaded * 0.6f );
									break;
								}
							case ItemUpdateStatus.UploadingPreviewFile:
								{
									progress?.Report( 0.8f );
									break;
								}
							case ItemUpdateStatus.CommittingChanges:
								{
									progress?.Report( 1 );
									break;
								}
						}
					}

					await Task.Delay( 1000 / 60 );
				}

				progress?.Report( 1 );

				var updated = updating.GetResult();

				if ( !updated.HasValue ) return result;

				result.Result = updated.Value.Result;

				if ( result.Result != Steamworks.Result.OK )
					return result;

				result.NeedsWorkshopAgreement = updated.Value.UserNeedsToAcceptWorkshopLegalAgreement;
				result.FileId = fileId;

			}

			return result;
		}
	}

	/// <summary>
	/// The outcome of an <see cref="Editor.SubmitAsync"/> call: whether the publish worked, the item's
	/// id, and whether the user still has to accept the Workshop legal agreement before anyone else
	/// can see what they just published.
	/// </summary>
	public struct PublishResult
	{
		/// <summary>
		/// Whether the publish completed. Check this first &#8212; but do not stop here, because a
		/// successful publish can still be invisible to other users. See
		/// <see cref="NeedsWorkshopAgreement"/>.
		/// </summary>
		public bool Success => Result == Steamworks.Result.OK;

		/// <summary>
		/// The result code Steam returned. <see cref="Steamworks.Result.Fail"/> is also used by this
		/// binding as the generic "we never got a usable answer" value, so it covers both an explicit
		/// rejection and the call failing or timing out.
		/// </summary>
		public Steamworks.Result Result;

		/// <summary>
		/// The item's published file id. Set for a successful publish, and also set for a create that
		/// succeeded before a later step failed &#8212; so a failed result can still carry a real id
		/// pointing at a half-published item worth cleaning up or retrying against.
		/// </summary>
		public PublishedFileId FileId;

		/// <summary>
		/// <see langword="true"/> if the user has not yet accepted this app's Workshop legal agreement.
		/// The item exists and the upload succeeded, but <b>nobody else can see it</b> until they
		/// accept.
		/// </summary>
		/// <remarks>
		/// Handle this or your users will silently publish items that never appear. Send them to the
		/// item's Workshop page, or show the agreement with
		/// <see cref="Steamworks.SteamUGC.ShowWorkshopEula"/>.
		/// See https://partner.steamgames.com/doc/features/workshop/implementation#Legal
		/// </remarks>
		public bool NeedsWorkshopAgreement;
	}
}
