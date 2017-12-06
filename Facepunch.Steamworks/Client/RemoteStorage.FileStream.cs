using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    /// <summary>
    /// Stream used to write to a <see cref="RemoteFile"/>.
    /// </summary>
    public class RemoteFileWriteStream : Stream
    {
        internal readonly RemoteStorage remoteStorage;

        private readonly UGCFileWriteStreamHandle_t _handle;
        private readonly RemoteFile _file;

        private int _written;
        private bool _closed;

        internal RemoteFileWriteStream( RemoteStorage r, RemoteFile file )
        {
            remoteStorage = r;

            _handle = remoteStorage.native.FileWriteStreamOpen( file.FileName );
            _file = file;
        }

        public override void Flush() { }

        public override int Read( byte[] buffer, int offset, int count )
        {
            throw new NotImplementedException();
        }

        public override long Seek( long offset, SeekOrigin origin )
        {
            throw new NotImplementedException();
        }

        public override void SetLength( long value )
        {
            throw new NotImplementedException();
        }

        public override unsafe void Write( byte[] buffer, int offset, int count )
        {
            if ( _closed ) throw new ObjectDisposedException( ToString() );

            fixed ( byte* bufferPtr = buffer )
            {
                if ( remoteStorage.native.FileWriteStreamWriteChunk( _handle, (IntPtr)(bufferPtr + offset), count ) )
                {
                    _written += count;
                }
            }
        }

        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => _written;
        public override long Position { get { return _written; } set { throw new NotImplementedException(); } }

        /// <summary>
        /// Close the stream without saving the file to remote storage.
        /// </summary>
        public void Cancel()
        {
            if ( _closed ) return;

            _closed = true;
            remoteStorage.native.FileWriteStreamCancel( _handle );
        }

        public override void Close()
        {
            if ( _closed ) return;

            _closed = true;
            remoteStorage.native.FileWriteStreamClose( _handle );

            _file.remoteStorage.OnWrittenNewFile( _file );
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing ) Close();
            base.Dispose( disposing );
        }
    }
}
