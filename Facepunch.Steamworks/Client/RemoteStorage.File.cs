using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Facepunch.Steamworks.Callbacks;
using SteamNative;
using Result = SteamNative.Result;

namespace Facepunch.Steamworks
{
    /// <summary>
    /// Represents a file stored in a user's Steam Cloud.
    /// </summary>
    public class RemoteFile
    {
        internal readonly RemoteStorage remoteStorage;

        private readonly bool _isUgc;
        private string _fileName;
        private int _sizeInBytes = -1;
        private long _timestamp = 0;
        private UGCHandle_t _handle;
        private ulong _ownerId;

        private bool _isDownloading;
        private byte[] _downloadedData;

        /// <summary>
        /// Check if the file exists.
        /// </summary>
        public bool Exists { get; internal set; }

        public bool IsDownloading { get { return _isUgc && _isDownloading && _downloadedData == null; } }

        public bool IsDownloaded { get { return !_isUgc || _downloadedData != null; } }

        /// <summary>
        /// If true, the file is available for other users to download.
        /// </summary>
        public bool IsShared { get { return _handle.Value != 0; } }

        internal UGCHandle_t UGCHandle { get { return _handle; } }

        public ulong SharingId { get { return UGCHandle.Value; } }

        /// <summary>
        /// Name and path of the file.
        /// </summary>
        public string FileName
        {
            get
            {
                if ( _fileName != null ) return _fileName;
                GetUGCDetails();
                return _fileName;
            }
        }

        /// <summary>
        /// Steam ID of the file's owner.
        /// </summary>
        public ulong OwnerId
        {
            get
            {
                if ( _ownerId != 0 ) return _ownerId;
                GetUGCDetails();
                return _ownerId;
            }
        }

        /// <summary>
        /// Total size of the file in bytes.
        /// </summary>
        public int SizeInBytes
        {
            get
            {
                if ( _sizeInBytes != -1 ) return _sizeInBytes;
                if ( _isUgc ) throw new NotImplementedException();
                _sizeInBytes = remoteStorage.native.GetFileSize( FileName );
                return _sizeInBytes;
            }
            internal set { _sizeInBytes = value; }
        }

        /// <summary>
        /// Date modified timestamp in epoch format.
        /// </summary>
        public long FileTimestamp
        {
            get
            {
                if ( _timestamp != 0 ) return _timestamp;
                if (_isUgc) throw new NotImplementedException();
                _timestamp = remoteStorage.native.GetFileTimestamp(FileName);
                return _timestamp;
            }
            internal set { _timestamp = value; }
        }

        internal RemoteFile( RemoteStorage r, UGCHandle_t handle )
        {
            Exists = true;

            remoteStorage = r;

            _isUgc = true;
            _handle = handle;
        }

        internal RemoteFile( RemoteStorage r, string name, ulong ownerId, int sizeInBytes = -1, long timestamp = 0 )
        {
            remoteStorage = r;

            _isUgc = false;
            _fileName = name;
            _ownerId = ownerId;
            _sizeInBytes = sizeInBytes;
            _timestamp = timestamp;
        }

        /// <summary>
        /// Creates a <see cref="RemoteFileWriteStream"/> used to write to this file.
        /// </summary>
        public RemoteFileWriteStream OpenWrite()
        {
            if (_isUgc) throw new InvalidOperationException("Cannot write to a shared file.");

            return new RemoteFileWriteStream( remoteStorage, this );
        }

        /// <summary>
        /// Write a byte array to this file, replacing any existing contents.
        /// </summary>
        public void WriteAllBytes( byte[] buffer )
        {
            using ( var stream = OpenWrite() )
            {
                stream.Write( buffer, 0, buffer.Length );
            }
        }

        /// <summary>
        /// Write a string to this file, replacing any existing contents.
        /// </summary>
        public void WriteAllText( string text, Encoding encoding = null )
        {
            if ( encoding == null ) encoding = Encoding.UTF8;
            WriteAllBytes( encoding.GetBytes( text ) );
        }

        /// <summary>
        /// Callback invoked by <see cref="RemoteFile.Download"/> when a file download is complete.
        /// </summary>
        public delegate void DownloadCallback();

        /// <summary>
        /// Gets the number of bytes downloaded and the total number of bytes expected while
        /// this file is downloading.
        /// </summary>
        /// <returns>True if the file is downloading</returns>
        public bool GetDownloadProgress( out int bytesDownloaded, out int bytesExpected )
        {
            return remoteStorage.native.GetUGCDownloadProgress( _handle, out bytesDownloaded, out bytesExpected );
        }

        /// <summary>
        /// Attempts to start downloading a shared file.
        /// </summary>
        /// <returns>True if the download has successfully started</returns>
        public bool Download( DownloadCallback onSuccess = null, FailureCallback onFailure = null )
        {
            if ( !_isUgc ) return false;
            if ( _isDownloading ) return false;
            if ( IsDownloaded ) return false;

            _isDownloading = true;

            remoteStorage.native.UGCDownload( _handle, 1000, ( result, error ) =>
            {
                _isDownloading = false;

                if ( error || result.Result != Result.OK )
                {
                    onFailure?.Invoke( result.Result == 0 ? Callbacks.Result.IOFailure : (Callbacks.Result) result.Result );
                    return;
                }

                _ownerId = result.SteamIDOwner;
                _sizeInBytes = result.SizeInBytes;
                _fileName = result.PchFileName;

                unsafe
                {
                    _downloadedData = new byte[_sizeInBytes];
                    fixed ( byte* bufferPtr = _downloadedData )
                    {
                        remoteStorage.native.UGCRead( _handle, (IntPtr) bufferPtr, _sizeInBytes, 0, UGCReadAction.ontinueReading );
                    }
                }

                onSuccess?.Invoke();
            } );

            return true;
        }

        /// <summary>
        /// Opens a stream used to read from this file.
        /// </summary>
        /// <returns></returns>
        public Stream OpenRead()
        {
            return new MemoryStream( ReadAllBytes(), false );
        }

        /// <summary>
        /// Reads the entire contents of the file as a byte array.
        /// </summary>
        public unsafe byte[] ReadAllBytes()
        {
            if ( _isUgc )
            {
                if ( !IsDownloaded ) throw new Exception( "Cannot read a file that hasn't been downloaded." );
                return _downloadedData;
            }

            var size = SizeInBytes;
            var buffer = new byte[size];

            fixed ( byte* bufferPtr = buffer )
            {
                remoteStorage.native.FileRead( FileName, (IntPtr) bufferPtr, size );
            }

            return buffer;
        }

        /// <summary>
        /// Reads the entire contents of the file as a string.
        /// </summary>
        public string ReadAllText( Encoding encoding = null )
        {
            if ( encoding == null ) encoding = Encoding.UTF8;
            return encoding.GetString( ReadAllBytes() );
        }

        /// <summary>
        /// Callback invoked by <see cref="RemoteFile.Share"/> when file sharing is complete.
        /// </summary>
        public delegate void ShareCallback();

        /// <summary>
        /// Attempt to publish this file for other users to download.
        /// </summary>
        /// <returns>True if we have started attempting to share</returns>
        public bool Share( ShareCallback onSuccess = null, FailureCallback onFailure = null )
        {
            if ( _isUgc ) return false;

            // Already shared
            if ( _handle.Value != 0 ) return false;

            remoteStorage.native.FileShare( FileName, ( result, error ) =>
            {
                if ( !error && result.Result == Result.OK )
                {
                    _handle.Value = result.File;
                    onSuccess?.Invoke();
                }
                else
                {
                    onFailure?.Invoke( result.Result == 0 ? Callbacks.Result.IOFailure : (Callbacks.Result) result.Result );
                }
            } );

            return true;
        }

        /// <summary>
        /// Delete this file from remote storage.
        /// </summary>
        /// <returns>True if the file could be deleted</returns>
        public bool Delete()
        {
            if ( !Exists ) return false;
            if ( _isUgc ) return false;
            if ( !remoteStorage.native.FileDelete( FileName ) ) return false;

            Exists = false;
            remoteStorage.InvalidateFiles();

            return true;
        }

        /// <summary>
        /// Remove this file from remote storage, while keeping a local copy.
        /// Writing to this file again will re-add it to the cloud.
        /// </summary>
        /// <returns>True if the file was forgotten</returns>
        public bool Forget()
        {
            if ( !Exists ) return false;
            if ( _isUgc ) return false;

            return remoteStorage.native.FileForget( FileName );
        }

        private void GetUGCDetails()
        {
            if ( !_isUgc ) throw new InvalidOperationException();

            var appId = new AppId_t { Value = remoteStorage.native.steamworks.AppId };

            CSteamID ownerId;
            remoteStorage.native.GetUGCDetails( _handle, ref appId, out _fileName, out ownerId );

            _ownerId = ownerId.Value;
        }
    }
}
