using System;
using System.Collections.Generic;
using SteamNative;

namespace Facepunch.Steamworks
{
    public partial class Workshop
    {
        public class Editor
        {
            internal Workshop workshop;

            internal CallbackHandle CreateItem;
            internal CallbackHandle SubmitItemUpdate;
            internal SteamNative.UGCUpdateHandle_t UpdateHandle;

            public ulong Id { get; internal set; }
            public string Title { get; set; } = null;
            public string Description { get; set; } = null;
            public string Folder { get; set; } = null;
            public string PreviewImage { get; set; } = null;
            public List<string> Tags { get; set; } = new List<string>();
            public Dictionary<string, string> KeyValueTags { get; set; } = new Dictionary<string, string>();
            public bool Publishing { get; internal set; }
            public ItemType? Type { get; set; }
            public string Error { get; internal set; } = null;
            public string ChangeNote { get; set; } = "";

            public enum VisibilityType : int
            {
                Public = 0,
                FriendsOnly = 1,
                Private = 2
            }

            public VisibilityType ? Visibility { get; set; }

            public bool NeedToAgreeToWorkshopLegal { get; internal set; }

            public event Action<Editor> PublishSucceeded;
            public event Action<Editor> PublishFailed;

            private PublishStatus _publishStatus;
            public PublishStatus PublishStatus
            {
                get
                {
                    UpdatePublishProgress();
                    return _publishStatus;
                }
            }

            public double Progress
            {
                get
                {
                    if ( CreateItem != null ) return 0d;
                    if ( SubmitItemUpdate == null ) return 1d;
                    if ( !Publishing ) return 1d;

                    UpdatePublishProgress();
                    return _bytesTotal > 0 ? _bytesUploaded / (double) _bytesTotal : 0d;
                }
            }

            private ulong _bytesUploaded;
            public int BytesUploaded
            {
                get
                {
                    UpdatePublishProgress();
                    return (int) _bytesUploaded;
                }
            }

            private ulong _bytesTotal;
            public int BytesTotal
            {
                get
                {
                    UpdatePublishProgress();
                    return (int) _bytesTotal;
                }
            }

            private void UpdatePublishProgress()
            {
                if ( SubmitItemUpdate == null )
                {
                    _publishStatus = PublishStatus.Invalid;
                    return;
                }

                _publishStatus = (PublishStatus) workshop.steamworks.native.ugc.GetItemUpdateProgress( UpdateHandle, out _bytesUploaded, out _bytesTotal );
            }

            public void Publish()
            {
                Publishing = true;
                Error = null;

                if ( Id == 0 )
                {
                    StartCreatingItem();
                    return;
                }

                PublishChanges();
            }

            private void StartCreatingItem()
            {
                if ( !Type.HasValue )
                    throw new System.Exception( "Editor.Type must be set when creating a new item!" );

                System.Diagnostics.Debug.WriteLine( "StartCreatingItem()" );
                CreateItem = workshop.ugc.CreateItem( workshop.steamworks.AppId, (SteamNative.WorkshopFileType)(uint)Type, OnItemCreated );
            }

            private void OnItemCreated( SteamNative.CreateItemResult_t obj, bool Failed )
            {
                System.Diagnostics.Debug.WriteLine( $"OnItemCreated({obj.PublishedFileId}, {Failed})" );

                NeedToAgreeToWorkshopLegal = obj.UserNeedsToAcceptWorkshopLegalAgreement;
                CreateItem.Dispose();
                CreateItem = null;

                if ( obj.Result == SteamNative.Result.OK && !Failed )
                {
                    Id = obj.PublishedFileId;
                    PublishChanges();
                    return;
                }

                Error = "Error creating new file: " + obj.Result.ToString() + "("+ obj.PublishedFileId+ ")";
                Publishing = false;

                PublishFailed?.Invoke( this );
            }

            private void PublishChanges()
            {
                UpdateHandle = workshop.ugc.StartItemUpdate( workshop.steamworks.AppId, Id );

                if ( Title != null )
                    workshop.ugc.SetItemTitle( UpdateHandle, Title );

                if ( Description != null )
                    workshop.ugc.SetItemDescription( UpdateHandle, Description );

                if ( Folder != null )
                {
                    var info = new System.IO.DirectoryInfo( Folder );

                    if ( !info.Exists )
                        throw new System.Exception( $"Folder doesn't exist ({Folder})" );
                    
                    workshop.ugc.SetItemContent( UpdateHandle, Folder );
                }

                if ( Tags != null && Tags.Count > 0 )
                    workshop.ugc.SetItemTags( UpdateHandle, Tags.ToArray() );

                if ( KeyValueTags != null )
                {
                    foreach ( var keyValue in KeyValueTags )
                    {
                        workshop.ugc.AddItemKeyValueTag( UpdateHandle, keyValue.Key, keyValue.Value );
                    }
                }

                if ( Visibility.HasValue )
                    workshop.ugc.SetItemVisibility( UpdateHandle, (SteamNative.RemoteStoragePublishedFileVisibility)(uint)Visibility.Value );

                if ( PreviewImage != null )
                {
                    var info = new System.IO.FileInfo( PreviewImage );

                    if ( !info.Exists )
                        throw new System.Exception( $"PreviewImage doesn't exist ({PreviewImage})" );

                    if ( info.Length >= 1024 * 1024 )
                        throw new System.Exception( $"PreviewImage should be under 1MB ({info.Length})" );

                    workshop.ugc.SetItemPreview( UpdateHandle, PreviewImage );
                }

                /*
                    workshop.ugc.SetItemUpdateLanguage( UpdateId, const char *pchLanguage ) = 0; // specify the language of the title or description that will be set
                    workshop.ugc.SetItemMetadata( UpdateId, const char *pchMetaData ) = 0; // change the metadata of an UGC item (max = k_cchDeveloperMetadataMax)
                    workshop.ugc.RemoveItemKeyValueTags( UpdateId, const char *pchKey ) = 0; // remove any existing key-value tags with the specified key
                    workshop.ugc.AddItemKeyValueTag( UpdateId, const char *pchKey, const char *pchValue ) = 0; // add new key-value tags for the item. Note that there can be multiple values for a tag.
                    workshop.ugc.AddItemPreviewFile( UpdateId, const char *pszPreviewFile, EItemPreviewType type ) = 0; //  add preview file for this item. pszPreviewFile points to local file, which must be under 1MB in size
                    workshop.ugc.AddItemPreviewVideo( UpdateId, const char *pszVideoID ) = 0; //  add preview video for this item
                    workshop.ugc.UpdateItemPreviewFile( UpdateId, uint32 index, const char *pszPreviewFile ) = 0; //  updates an existing preview file for this item. pszPreviewFile points to local file, which must be under 1MB in size
                    workshop.ugc.UpdateItemPreviewVideo( UpdateId, uint32 index, const char *pszVideoID ) = 0; //  updates an existing preview video for this item
                    workshop.ugc.RemoveItemPreview( UpdateId, uint32 index ) = 0; // remove a preview by index starting at 0 (previews are sorted)
                 */

                SubmitItemUpdate = workshop.ugc.SubmitItemUpdate( UpdateHandle, ChangeNote, OnChangesSubmitted );
            }

            private void OnChangesSubmitted( SteamNative.SubmitItemUpdateResult_t obj, bool Failed )
            {
                if ( Failed )
                {
                    if ( PublishFailed != null ) PublishFailed( this );
                    else throw new System.Exception( "CreateItemResult_t Failed" );
                }

                SubmitItemUpdate = null;
                NeedToAgreeToWorkshopLegal = obj.UserNeedsToAcceptWorkshopLegalAgreement;
                Publishing = false;

                if ( obj.Result == SteamNative.Result.OK )
                {
                    PublishSucceeded?.Invoke( this );
                    return;
                }

                Error = "Error publishing changes: " + obj.Result.ToString() + " ("+ NeedToAgreeToWorkshopLegal + ")";
                PublishFailed?.Invoke( this );
            }

            public void Delete()
            {
                workshop.remoteStorage.DeletePublishedFile( Id );
                Id = 0;
            }
        }

        public enum PublishStatus : int
        {
            Invalid = 0,
            PreparingConfig = 1,
            PreparingContent = 2,
            UploadingContent = 3,
            UploadingPreviewFile = 4,
            CommittingChanges = 5,
        }
    }
}
