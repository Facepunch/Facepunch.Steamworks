using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    /// <summary>
    /// Handles Steam Cloud related actions.
    /// </summary>
    public class RemoteStorage : IDisposable
    {
        private static string NormalizePath( string path )
        {
            // TODO: DUMB HACK ALERT

            return SteamNative.Platform.IsWindows
                ? new FileInfo( $"x:/{path}" ).FullName.Substring( 3 )
                : new FileInfo( $"/x/{path}" ).FullName.Substring( 3 );
        }

        internal Client client;
        internal SteamNative.SteamRemoteStorage native;

        private bool _filesInvalid = true;
        private readonly List<RemoteFile> _files = new List<RemoteFile>();

        internal RemoteStorage( Client c )
        {
            client = c;
            native = client.native.remoteStorage;
        }

        /// <summary>
        /// True if Steam Cloud is currently enabled by the current user.
        /// </summary>
        public bool IsCloudEnabledForAccount
        {
            get { return native.IsCloudEnabledForAccount(); }
        }

        /// <summary>
        /// True if Steam Cloud is currently enabled for this app by the current user.
        /// </summary>
        public bool IsCloudEnabledForApp
        {
            get { return native.IsCloudEnabledForApp(); }
        }

        /// <summary>
        /// Gets the total number of files in the current user's remote storage for the current game.
        /// </summary>
        public int FileCount
        {
            get { return native.GetFileCount(); }
        }

        /// <summary>
        /// Gets all files in the current user's remote storage for the current game.
        /// </summary>
        public IEnumerable<RemoteFile> Files
        {
            get
            {
                UpdateFiles();
                return _files;
            }
        }

        /// <summary>
        /// Creates a new <see cref="RemoteFile"/> with the given <paramref name="path"/>.
        /// If a file exists at that path it will be overwritten.
        /// </summary>
        public RemoteFile CreateFile( string path )
        {
            path = NormalizePath( path );

            InvalidateFiles();
            var existing = Files.FirstOrDefault( x => x.FileName == path );
            return existing ?? new RemoteFile( this, path, client.SteamId, 0 );
        }

        /// <summary>
        /// Opens the file if it exists, else returns null;
        /// </summary>
        public RemoteFile OpenFile( string path )
        {
            path = NormalizePath( path );

            InvalidateFiles();
            var existing = Files.FirstOrDefault( x => x.FileName == path );
            return existing;
        }

        /// <summary>
        /// Opens a previously shared <see cref="RemoteFile"/>
        /// with the given <paramref name="sharingId"/>.
        /// </summary>
        public RemoteFile OpenSharedFile( ulong sharingId )
        {
            return new RemoteFile( this, sharingId );
        }

        /// <summary>
        /// Write all text to the file at the specified path. This
        /// overwrites the contents - it does not append.
        /// </summary>
        public bool WriteString( string path, string text, Encoding encoding = null )
        {
            var file = CreateFile( path );
            file.WriteAllText( text, encoding );
            return file.Exists;
        }

        /// <summary>
        /// Write all data to the file at the specified path. This
        /// overwrites the contents - it does not append.
        /// </summary>
        public bool WriteBytes( string path, byte[] data )
        {
            var file = CreateFile( path );
            file.WriteAllBytes( data );
            return file.Exists;
        }

        /// <summary>
        /// Read the entire contents of the file as a string.
        /// Returns null if the file isn't found.
        /// </summary>
        public string ReadString( string path, Encoding encoding = null )
        {
            var file = OpenFile( path );
            if ( file == null ) return null;
            return file.ReadAllText( encoding );
        }

        /// <summary>
        /// Read the entire contents of the file as raw data.
        /// Returns null if the file isn't found.
        /// </summary>
        public byte[] ReadBytes( string path )
        {
            var file = OpenFile( path );
            if ( file == null ) return null;
            return file.ReadAllBytes();
        }

        internal void OnWrittenNewFile( RemoteFile file )
        {
            if ( _files.Any( x => x.FileName == file.FileName ) ) return;

            _files.Add( file );
            file.Exists = true;

            InvalidateFiles();
        }

        internal void InvalidateFiles()
        {
            _filesInvalid = true;
        }

        private void UpdateFiles()
        {
            if ( !_filesInvalid ) return;
            _filesInvalid = false;

            foreach ( var file in _files )
            {
                file.Exists = false;
            }

            var count = FileCount;
            for ( var i = 0; i < count; ++i )
            {
                int size;
                var name = NormalizePath( native.GetFileNameAndSize( i, out size ) );
                var timestamp = native.GetFileTimestamp(name);

                var existing = _files.FirstOrDefault( x => x.FileName == name );
                if ( existing == null )
                {
                    existing = new RemoteFile( this, name, client.SteamId, size, timestamp );
                    _files.Add( existing );
                }
                else
                {
                    existing.SizeInBytes = size;
                    existing.FileTimestamp = timestamp;
                }

                existing.Exists = true;
            }

            for ( var i = _files.Count - 1; i >= 0; --i )
            {
                if ( !_files[i].Exists ) _files.RemoveAt( i );
            }
        }

        /// <summary>
        /// Gets whether a file exists in remote storage at the given <paramref name="path"/>.
        /// </summary>
        public bool FileExists( string path )
        {
            return native.FileExists( path );
        }

        public void Dispose()
        {
            client = null;
            native = null;
        }

        /// <summary>
        /// Number of bytes used out of the user's total quota
        /// </summary>
        public ulong QuotaUsed
        {
            get
            {
                ulong totalBytes = 0;
                ulong availableBytes = 0;

                if ( !native.GetQuota( out totalBytes, out availableBytes ) )
                    return 0;

                return totalBytes - availableBytes;
            }
        }

        /// <summary>
        /// Total quota size in bytes
        /// </summary>
        public ulong QuotaTotal
        {
            get
            {
                ulong totalBytes = 0;
                ulong availableBytes = 0;

                if ( !native.GetQuota( out totalBytes, out availableBytes ) )
                    return 0;

                return totalBytes;
            }
        }


        /// <summary>
        /// Number of bytes remaining out of the user's total quota
        /// </summary>
        public ulong QuotaRemaining
        {
            get
            {
                ulong totalBytes = 0;
                ulong availableBytes = 0;

                if ( !native.GetQuota( out totalBytes, out availableBytes ) )
                    return 0;

                return availableBytes;
            }
        }
    }
}
