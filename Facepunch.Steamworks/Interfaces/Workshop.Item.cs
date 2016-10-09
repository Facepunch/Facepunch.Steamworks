using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks.Callbacks.Networking;
using Facepunch.Steamworks.Callbacks.Workshop;
using Facepunch.Steamworks.Interop;
using Valve.Steamworks;

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

            internal static Item From( SteamUGCDetails_t details, Workshop workshop )
            {
                var item = new Item();

                item.workshop = workshop;
                item.Id = details.m_nPublishedFileId;
                item.Title = details.m_rgchTitle;
                item.Description = details.m_rgchDescription;
                item.OwnerId = details.m_ulSteamIDOwner;
                item.Tags = details.m_rgchTags.Split( ',' );
                item.Score = details.m_flScore;
                item.VotesUp = details.m_unVotesUp;
                item.VotesDown = details.m_unVotesDown;
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

                workshop.OnFileDownloaded -= OnFileDownloaded;
                UpdateState();

                if ( result == Callbacks.Result.OK )
                    Downloading = false;
            }

            private void OnItemInstalled( ulong fileid )
            {

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

                Installed = ( state & (uint) EItemState.k_EItemStateInstalled ) != 0;
                Downloading = ( state & (uint) EItemState.k_EItemStateDownloading ) != 0;
                DownloadPending = ( state & (uint) EItemState.k_EItemStateDownloadPending ) != 0;
                Subscribed = ( state & (uint) EItemState.k_EItemStateSubscribed ) != 0;
                NeedsUpdate = ( state & (uint) EItemState.k_EItemStateNeedsUpdate ) != 0;

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


            public void VoteUp()
            {
                workshop.ugc.SetUserItemVote( Id, true );
            }

            public void VoteDown()
            {
                workshop.ugc.SetUserItemVote( Id, false );
            }
        }
    }
}
