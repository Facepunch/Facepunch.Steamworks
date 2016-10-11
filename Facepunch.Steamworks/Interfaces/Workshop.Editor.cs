using System;
using System.Collections.Generic;
using Facepunch.Steamworks.Callbacks.Workshop;

namespace Facepunch.Steamworks
{
    public partial class Workshop
    {
        public class Editor
        {
            internal Workshop workshop;

            internal CreateItem CreateItem;
            internal SubmitItemUpdate SubmitItemUpdate;

            public ulong Id { get; internal set; }
            public string Title { get; set; } = null;
            public string Description { get; set; } = null;
            public string Folder { get; set; } = null;
            public List<string> Tags { get; set; } = new List<string>();
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



            public double Progress
            {
                get
                {
                    if ( !Publishing ) return 1.0;
                    if ( CreateItem != null ) return 0.0;
                    if ( SubmitItemUpdate == null ) return 1.0;

                    ulong b = 0;
                    ulong t = 0;

                    workshop.steamworks.native.ugc.GetItemUpdateProgress( SubmitItemUpdate.Handle, ref b, ref t );

                    if ( t == 0 )
                        return 0;

                    return (double)b / (double) t;
                }
            }

            public int BytesUploaded
            {
                get
                {
                    if ( !Publishing ) return 0;
                    if ( CreateItem != null ) return 0;
                    if ( SubmitItemUpdate == null ) return 0;

                    ulong b = 0;
                    ulong t = 0;

                    workshop.steamworks.native.ugc.GetItemUpdateProgress( SubmitItemUpdate.Handle, ref b, ref t );
                    return (int) b;
                }
            }

            public int BytesTotal
            {
                get
                {
                    if ( !Publishing ) return 0;
                    if ( CreateItem != null ) return 0;
                    if ( SubmitItemUpdate == null ) return 0;

                    ulong b = 0;
                    ulong t = 0;

                    workshop.steamworks.native.ugc.GetItemUpdateProgress( SubmitItemUpdate.Handle, ref b, ref t );
                    return (int)t;
                }
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

                CreateItem = new CreateItem();
                CreateItem.Handle = workshop.ugc.CreateItem( workshop.steamworks.AppId, (uint)Type );
                CreateItem.OnResult = OnItemCreated;
                workshop.steamworks.AddCallResult( CreateItem );
            }

            private void OnItemCreated( CreateItem.Data obj )
            {
                NeedToAgreeToWorkshopLegal = obj.NeedsLegalAgreement;
                CreateItem = null;

                if ( obj.Result == Callbacks.Result.OK )
                {
                    Id = obj.FileId;
                    PublishChanges();
                    return;
                }

                Error = "Error creating new file: " + obj.Result.ToString();
                Publishing = false;
            }

            private void PublishChanges()
            {
                ulong UpdateId = workshop.ugc.StartItemUpdate( workshop.steamworks.AppId, Id );

                if ( Title != null )
                    workshop.ugc.SetItemTitle( UpdateId, Title );

                if ( Description != null )
                    workshop.ugc.SetItemDescription( UpdateId, Description );

                if ( Folder != null )
                    workshop.ugc.SetItemContent( UpdateId, Folder );

                if ( Tags != null && Tags.Count > 0 )
                    workshop.ugc.SetItemTags( UpdateId, Tags.ToArray() );

                if ( Visibility.HasValue )
                    workshop.ugc.SetItemVisibility( UpdateId, (uint)Visibility.Value );

                /*
                    workshop.ugc.SetItemUpdateLanguage( UpdateId, const char *pchLanguage ) = 0; // specify the language of the title or description that will be set
                    workshop.ugc.SetItemMetadata( UpdateId, const char *pchMetaData ) = 0; // change the metadata of an UGC item (max = k_cchDeveloperMetadataMax)
                    workshop.ugc.SetItemPreview( UpdateId, const char *pszPreviewFile ) = 0; //  change preview image file for this item. pszPreviewFile points to local image file, which must be under 1MB in size
                    workshop.ugc.RemoveItemKeyValueTags( UpdateId, const char *pchKey ) = 0; // remove any existing key-value tags with the specified key
                    workshop.ugc.AddItemKeyValueTag( UpdateId, const char *pchKey, const char *pchValue ) = 0; // add new key-value tags for the item. Note that there can be multiple values for a tag.
                    workshop.ugc.AddItemPreviewFile( UpdateId, const char *pszPreviewFile, EItemPreviewType type ) = 0; //  add preview file for this item. pszPreviewFile points to local file, which must be under 1MB in size
                    workshop.ugc.AddItemPreviewVideo( UpdateId, const char *pszVideoID ) = 0; //  add preview video for this item
                    workshop.ugc.UpdateItemPreviewFile( UpdateId, uint32 index, const char *pszPreviewFile ) = 0; //  updates an existing preview file for this item. pszPreviewFile points to local file, which must be under 1MB in size
                    workshop.ugc.UpdateItemPreviewVideo( UpdateId, uint32 index, const char *pszVideoID ) = 0; //  updates an existing preview video for this item
                    workshop.ugc.RemoveItemPreview( UpdateId, uint32 index ) = 0; // remove a preview by index starting at 0 (previews are sorted)
                 */

                SubmitItemUpdate = new SubmitItemUpdate();
                SubmitItemUpdate.Handle = workshop.ugc.SubmitItemUpdate( UpdateId, ChangeNote );
                SubmitItemUpdate.OnResult = OnChangesSubmitted;
                workshop.steamworks.AddCallResult( SubmitItemUpdate );
            }

            private void OnChangesSubmitted( SubmitItemUpdate.Data obj )
            {
                SubmitItemUpdate = null;
                NeedToAgreeToWorkshopLegal = obj.NeedsLegalAgreement;
                Publishing = false;

                if ( obj.Result == Callbacks.Result.OK )
                {
                    return;
                }

                Error = "Error publishing changes: " + obj.Result.ToString();
            }

            public void Delete()
            {
                workshop.remoteStorage.DeletePublishedFile( Id );
                Id = 0;
            }
        }
    }
}
