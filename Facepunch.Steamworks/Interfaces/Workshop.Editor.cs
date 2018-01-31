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
            public bool Publishing { get; internal set; }
            public ItemType? Type { get; set; }
            public string Error { get; internal set; } = null;
            public string ChangeNote { get; set; } = "";
            public uint WorkshopUploadAppId { get; set; }

            public enum VisibilityType : int
            {
                Public = 0,
                FriendsOnly = 1,
                Private = 2
            }

            public VisibilityType ? Visibility { get; set; }

            public bool NeedToAgreeToWorkshopLegal { get; internal set; }



            public double Progress
            {
                get
                {
                    var bt = BytesTotal;
                    if (bt == 0) return 0;

                    return (double)BytesUploaded / (double)bt;
                }
            }

            private int bytesUploaded = 0;

            public int BytesUploaded
            {
                get
                {
                    if ( !Publishing ) return bytesUploaded;
                    if (UpdateHandle == 0) return bytesUploaded;

                    ulong b = 0;
                    ulong t = 0;

                    workshop.steamworks.native.ugc.GetItemUpdateProgress( UpdateHandle, out b, out t );
                    bytesUploaded = Math.Max( bytesUploaded, (int) b );
                    return (int)bytesUploaded;
                }
            }

            private int bytesTotal = 0;

            public int BytesTotal
            {
                get
                {
                    if ( !Publishing ) return bytesTotal;
                    if (UpdateHandle == 0 ) return bytesTotal;

                    ulong b = 0;
                    ulong t = 0;

                    workshop.steamworks.native.ugc.GetItemUpdateProgress( UpdateHandle, out b, out t );
                    bytesTotal = Math.Max(bytesTotal, (int)t);
                    return (int)bytesTotal;
                }
            }

            public void Publish()
            {
                bytesUploaded = 0;
                bytesTotal = 0;

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

                CreateItem = workshop.ugc.CreateItem( WorkshopUploadAppId, (SteamNative.WorkshopFileType)(uint)Type, OnItemCreated );
            }

            private void OnItemCreated( SteamNative.CreateItemResult_t obj, bool Failed )
            {
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
            }

            private void PublishChanges()
            {
                UpdateHandle = workshop.ugc.StartItemUpdate(WorkshopUploadAppId, Id );

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
                    throw new System.Exception( "CreateItemResult_t Failed" );

                UpdateHandle = 0;
                SubmitItemUpdate = null;
                NeedToAgreeToWorkshopLegal = obj.UserNeedsToAcceptWorkshopLegalAgreement;
                Publishing = false;

                if ( obj.Result == SteamNative.Result.OK )
                {
                    return;
                }

                Error = "Error publishing changes: " + obj.Result.ToString() + " ("+ NeedToAgreeToWorkshopLegal + ")";
            }

            public void Delete()
            {
                workshop.ugc.DeleteItem( Id );
                Id = 0;
            }
        }
    }
}
