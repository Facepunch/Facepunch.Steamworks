using System;
using System.Linq;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	public struct Result
	{
		public PublishedFileId Id;

		public string Title;
		public string Description;
		public string[] Tags;


		//
		// TODO;
		//
		//internal Steamworks.Result Result; // m_eResult enum EResult
		internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
		internal uint CreatorAppID; // m_nCreatorAppID AppId_t
		internal uint ConsumerAppID; // m_nConsumerAppID AppId_t

		internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		internal uint TimeCreated; // m_rtimeCreated uint32
		internal uint TimeUpdated; // m_rtimeUpdated uint32
		internal uint TimeAddedToUserList; // m_rtimeAddedToUserList uint32
		internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
		internal bool Banned; // m_bBanned _Bool
		internal bool AcceptedForUse; // m_bAcceptedForUse _Bool
		internal bool TagsTruncated; // m_bTagsTruncated _Bool
		internal ulong File; // m_hFile UGCHandle_t
		internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
		internal string PchFileName; // m_pchFileName char [260]
		internal int FileSize; // m_nFileSize int32
		internal int PreviewFileSize; // m_nPreviewFileSize int32
		internal string URL; // m_rgchURL char [256]
		internal uint VotesUp; // m_unVotesUp uint32
		internal uint VotesDown; // m_unVotesDown uint32
		internal float Score; // m_flScore float
		internal uint NumChildren; // m_unNumChildren uint32

		internal static Result From( SteamUGCDetails_t details, UGCQueryHandle_t handle )
		{
			var d = new Result
			{
				Id = details.PublishedFileId,
				FileType = details.FileType,

				Title = details.Title,
				Description = details.Description,
				Tags = details.Tags.Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries )

			};

			return d;
		}

		/// <summary>
		/// A case insensitive check for tag
		/// </summary>
		public bool HasTag( string find )
		{
			if ( Tags.Length == 0 ) return false;

			return Tags.Contains( find, StringComparer.OrdinalIgnoreCase );
		}
	}
}