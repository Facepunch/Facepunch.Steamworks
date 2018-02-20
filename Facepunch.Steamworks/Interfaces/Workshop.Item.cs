using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SteamNative;

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
                item.Tags = details.Tags.Split( ',' ).Select( x=> x.ToLower() ).ToArray();
                item.Score = details.Score;
                item.VotesUp = details.VotesUp;
                item.VotesDown = details.VotesDown;
                item.Modified = Utility.Epoch.ToDateTime( details.TimeUpdated );
                item.Created = Utility.Epoch.ToDateTime( details.TimeCreated );

                return item;
            }

            public bool Download( bool highPriority = true )
            {
                if ( Installed ) return true;
                if ( Downloading ) return true;

                if ( !workshop.ugc.DownloadItem( Id, highPriority ) )
                {
                    Console.WriteLine( "Download Failed" );
                    return false;
                }

                workshop.OnFileDownloaded += OnFileDownloaded;
                workshop.OnItemInstalled += OnItemInstalled;
                return true;
            }

            public void Subscribe()
            {
                workshop.ugc.SubscribeItem(Id);
                SubscriptionCount++;
            }

            public void UnSubscribe()
            {
                workshop.ugc.UnsubscribeItem(Id);
                SubscriptionCount--;
            }


            private void OnFileDownloaded( ulong fileid, Callbacks.Result result )
            {
                if ( fileid != Id ) return;

                workshop.OnFileDownloaded -= OnFileDownloaded;
            }

            private void OnItemInstalled( ulong fileid )
            {
                if ( fileid != Id ) return;

                workshop.OnItemInstalled -= OnItemInstalled;
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

            public bool Installed { get { return ( State & ItemState.Installed ) != 0; } }
            public bool Downloading { get { return ( State & ItemState.Downloading ) != 0; } }
            public bool DownloadPending { get { return ( State & ItemState.DownloadPending ) != 0; } }
            public bool Subscribed { get { return ( State & ItemState.Subscribed ) != 0; } }
            public bool NeedsUpdate { get { return ( State & ItemState.NeedsUpdate ) != 0; } }

            private SteamNative.ItemState State { get { return ( SteamNative.ItemState) workshop.ugc.GetItemState( Id ); } }


            private DirectoryInfo _directory;

            public DirectoryInfo Directory
            {
                get
                {
                    if ( _directory != null )
                        return _directory;

                    if ( !Installed )
                        return null;

                    ulong sizeOnDisk;
                    string folder;
                    uint timestamp;

                    if ( workshop.ugc.GetItemInstallInfo( Id, out sizeOnDisk, out folder, out timestamp ) )
                    {
                        _directory = new DirectoryInfo( folder );
                        Size = sizeOnDisk;

                        if ( !_directory.Exists )
                        {
                         //   Size = 0;
                         //   _directory = null;
                        }
                    }

                    return _directory;
                }
            }

            public ulong Size { get; private set; }

            private ulong _BytesDownloaded, _BytesTotal;

            internal void UpdateDownloadProgress()
            {
               workshop.ugc.GetItemDownloadInfo( Id, out _BytesDownloaded, out _BytesTotal );
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
