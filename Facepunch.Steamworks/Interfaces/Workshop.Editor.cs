using System;
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
            public string Title { get; set; }
            public string Description { get; set; }
            public bool Publishing { get; internal set; }
            public ItemType? Type { get; set; }

            public string ChangeNote { get; set; } = "";

            public bool NeedToAgreeToWorkshopLegal { get; internal set; }

            public void Publish()
            {
                Publishing = true;

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

                Console.WriteLine( "File publish error: " + obj );
                Publishing = false;
            }

            private void PublishChanges()
            {
                Publishing = false;

                ulong UpdateId = workshop.ugc.StartItemUpdate( workshop.steamworks.AppId, Id );

                if ( Title != null )
                    workshop.ugc.SetItemTitle( UpdateId, Title );

                if ( Description != null )
                    workshop.ugc.SetItemDescription( UpdateId, Description );

                /*
                                workshop.ugc.SetItemUpdateLanguage( UpdateId, const char *pchLanguage ) = 0; // specify the language of the title or description that will be set
                                workshop.ugc.SetItemMetadata( UpdateId, const char *pchMetaData ) = 0; // change the metadata of an UGC item (max = k_cchDeveloperMetadataMax)
                                workshop.ugc.SetItemVisibility( UpdateId, ERemoteStoragePublishedFileVisibility eVisibility ) = 0; // change the visibility of an UGC item
                                workshop.ugc.SetItemTags( UpdateId, const SteamParamStringArray_t *pTags ) = 0; // change the tags of an UGC item
                                workshop.ugc.SetItemContent( UpdateId, const char *pszContentFolder ) = 0; // update item content from this local folder
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

                if ( obj.Result == Callbacks.Result.OK )
                {
                    Publishing = false;
                    return;
                }
            }

            public void Delete()
            {
                workshop.remoteStorage.DeletePublishedFile( Id );
                Id = 0;
            }
        }
    }
}
