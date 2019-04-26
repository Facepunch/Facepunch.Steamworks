using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steamworks.Data;

using QueryType = Steamworks.Ugc.Query;

namespace Steamworks.Ugc
{
	public struct Editor
	{
		PublishedFileId fileId;

		bool creatingNew;
		WorkshopFileType creatingType;
		AppId consumerAppId;

		internal Editor( WorkshopFileType filetype ) : this()
		{
			creatingNew = true;
			creatingType = filetype;
		}

		/// <summary>
		/// Create a Normal Workshop item that can be subscribed to
		/// </summary>
		public static Editor NewCommunityFile => new Editor( WorkshopFileType.Community );

		/// <summary>
		/// Workshop item that is meant to be voted on for the purpose of selling in-game
		/// </summary>
		public static Editor NewMicrotransactionFile => new Editor( WorkshopFileType.Microtransaction );

		public Editor ForAppId( AppId id ) { this.consumerAppId = id; return this; }


		string Title;
		public Editor WithTitle( string t ) { this.Title = t; return this; }

		string Description;
		public Editor WithDescription( string t ) { this.Description = t; return this; }


		public async Task<PublishResult> SubmitAsync()
		{
			var result = default( PublishResult );
			
			//
			// Item Create
			//
			if ( creatingNew )
			{
				if ( consumerAppId == 0 )
					consumerAppId = SteamClient.AppId;

				result.Result = Steamworks.Result.Fail;

				var created = await SteamUGC.Internal.CreateItem( consumerAppId, creatingType );
				if ( !created.HasValue ) return result;

				result.Result = created.Value.Result;

				if ( result.Result != Steamworks.Result.OK )
					return result;

				fileId = created.Value.PublishedFileId;
				result.NeedsWorkshopAgreement = created.Value.UserNeedsToAcceptWorkshopLegalAgreement;
				result.FileId = fileId;

				await Task.Delay( 500 );
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

				result.Result = Steamworks.Result.Fail;

				var updated = await SteamUGC.Internal.SubmitItemUpdate( handle, "" );
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

	public struct PublishResult
	{
		public bool Success => Result == Steamworks.Result.OK;

		public Steamworks.Result Result;
		public PublishedFileId FileId;

		/// <summary>
		/// https://partner.steamgames.com/doc/features/workshop/implementation#Legal
		/// </summary>
		public bool NeedsWorkshopAgreement;
	}
}