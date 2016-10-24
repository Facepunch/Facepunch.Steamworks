using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks.Callbacks.Networking;
using Facepunch.Steamworks.Callbacks.Workshop;

namespace Facepunch.Steamworks
{
    public partial class Workshop
    {
        public class Item
        {
            internal Workshop workshop;

            public string Description { get; private set; }
            public ulong Id { get; private set; }
            public ulong OwnerId { get; private set; }
            public float Score { get; private set; }
            public string[] Tags { get; private set; }
            public string Title { get; private set; }
            public uint VotesDown { get; private set; }
            public uint VotesUp { get; private set; }
            public DateTime Modified { get; private set; }
            public DateTime Created { get; private set; }

            public Item( ulong Id, Workshop workshop )
            {
                this.Id = Id;
                this.workshop = workshop;
            }

            internal static Item From( SteamNative.SteamUGCDetails_t details, Workshop workshop )
            {
                var item = new Item( details.PublishedFileId, workshop);

                item.Title = details.Title;
                item.Description = details.Description;
                item.OwnerId = details.SteamIDOwner;
                item.Tags = details.Tags.Split( ',' );
                item.Score = details.Score;
                item.VotesUp = details.VotesUp;
                item.VotesDown = details.VotesDown;
                item.Modified = new DateTime( details.TimeUpdated );
                item.Created = new DateTime( details.TimeCreated );
                item.UpdateState();

                return item;
            }

            internal static Item FromId( ulong id, Workshop workshop )
            {
                var item = new Item();

                item.workshop = workshop;
                item.Id = id;
                item.UpdateState();

                return item;
            }

            public void Download( bool highPriority = true )
            {
                UpdateState();

                if ( Installed ) return;
                if ( Downloading ) return;

                if ( !workshop.ugc.DownloadItem( Id, highPriority ) )
                {
                    Console.WriteLine( "Download Failed" );
                    return;
                }

                workshop.OnFileDownloaded += OnFileDownloaded;
                workshop.OnItemInstalled += OnItemInstalled;
                UpdateState();
                Downloading = true;
            }

            private void OnFileDownloaded( ulong fileid, Callbacks.Result result )
            {
                if ( fileid != Id ) return;

                workshop.OnFileDownloaded -= OnFileDownloaded;
                UpdateState();

                if ( result == Callbacks.Result.OK )
                    Downloading = false;
            }

            private void OnItemInstalled( ulong fileid )
            {
                if ( fileid != Id ) return;

                workshop.OnItemInstalled -= OnItemInstalled;
                UpdateState();

                Downloading = false;
                Installed = true;
            }

            public ulong BytesDownloaded { get { UpdateDownloadProgress(); return _BytesDownloaded; } }
            public ulong BytesTotalDownload { get { UpdateDownloadProgress(); return _BytesTotal; } }

            public double DownloadProgress
            {
                get
                {
                    UpdateDownloadProgress();
                    if ( _BytesTotal == 0 ) return 0;
                    return (double)_BytesDownloaded / (double)_BytesTotal;
                }
            }

            public bool Installed { get; private set; }
            public bool Downloading { get; private set; }
            public bool DownloadPending { get; private set; }
            public bool Subscribed { get; private set; }
            public bool NeedsUpdate { get; private set; }

            public DirectoryInfo Directory { get; private set; }

            public ulong Size { get; private set; }

            private ulong _BytesDownloaded, _BytesTotal;

            internal void UpdateDownloadProgress()
            {
               workshop.ugc.GetItemDownloadInfo( Id, out _BytesDownloaded, out _BytesTotal );
            }

            internal void UpdateState()
            {
                var state = workshop.ugc.GetItemState( Id );

                Installed = ( state & (uint)SteamNative.ItemState.Installed ) != 0;
                Downloading = ( state & (uint)SteamNative.ItemState.Downloading ) != 0;
                DownloadPending = ( state & (uint)SteamNative.ItemState.DownloadPending ) != 0;
                Subscribed = ( state & (uint)SteamNative.ItemState.Subscribed ) != 0;
                NeedsUpdate = ( state & (uint)SteamNative.ItemState.NeedsUpdate ) != 0;

                if ( Installed && Directory == null )
                {
                    Size = 0;
                    Directory = null;

                    ulong sizeOnDisk;
                    string folder;
                    uint timestamp;
                    if ( workshop.ugc.GetItemInstallInfo( Id, out sizeOnDisk, out folder, out timestamp ) )
                    {
                        Directory = new DirectoryInfo( folder );
                        Size = sizeOnDisk;

                        if ( !Directory.Exists )
                        {
                            Size = 0;
                            Directory = null;
                            Installed = false;
                        }
                    }
                }
            }


            private int YourVote = 0;


            public void VoteUp()
            {
                if ( YourVote == 1 ) return;
                if ( YourVote == -1 ) VotesDown--;

                VotesUp++;
                workshop.ugc.SetUserItemVote( Id, true );
                YourVote = 1;
            }

            public void VoteDown()
            {
                if ( YourVote == -1 ) return;
                if ( YourVote == 1 ) VotesUp--;

                VotesDown++;
                workshop.ugc.SetUserItemVote( Id, false );
                YourVote = -1;
            }

            public Editor Edit()
            {
                return workshop.EditItem( Id );
            }


            /// <summary>
            /// Return a URL to view this item online
            /// </summary>
            public string Url { get { return string.Format( "http://steamcommunity.com/sharedfiles/filedetails/?source=Facepunch.Steamworks&id={0}", Id ); } }

            public string ChangelogUrl { get { return string.Format( "http://steamcommunity.com/sharedfiles/filedetails/changelog/{0}", Id ); } }

            public string CommentsUrl { get { return string.Format( "http://steamcommunity.com/sharedfiles/filedetails/comments/{0}", Id ); } }

            public string DiscussUrl { get { return string.Format( "http://steamcommunity.com/sharedfiles/filedetails/discussions/{0}", Id ); } }

            public string StartsUrl { get { return string.Format( "http://steamcommunity.com/sharedfiles/filedetails/stats/{0}", Id ); } }

            public int SubscriptionCount { get; internal set; }
            public int FavouriteCount { get; internal set; }
            public int FollowerCount { get; internal set; }
            public int WebsiteViews { get; internal set; }
            public int ReportScore { get; internal set; }
            public string PreviewImageUrl { get; internal set; }

            string _ownerName = null;



            public string OwnerName
            {
                get
                {
                    if ( _ownerName == null && workshop.friends != null )
                    {
                        _ownerName = workshop.friends.GetName( OwnerId );
                        if ( _ownerName == "[unknown]" )
                        {
                            _ownerName = null;
                            return string.Empty;
                        }
                    }

                    if ( _ownerName == null )
                        return string.Empty;

                    return _ownerName;
                }
            }
        }
    }
}
