using System;
using System.IO;

namespace Steamworks.Data
{
	public readonly struct LegacyDownloadResult
	{
		public readonly Result Result;
		public readonly PublishedFileId File;
		public readonly AppId AppId;
		public readonly Friend Owner;
		public readonly int SizeInBytes;
		public readonly string FileName;

		internal LegacyDownloadResult( in RemoteStorageDownloadUGCResult_t result )
		{
			Result = result.Result;
			File = result.File;
			AppId = result.AppID;
			SizeInBytes = result.SizeInBytes;
			FileName = result.PchFileNameUTF8();
			Owner = new Friend( result.SteamIDOwner );
		}

		private class UgcStream : Stream
		{
			private readonly UGCHandle_t _handle;
			private readonly int _sizeInBytes;

			private uint _offset;
			private bool _closed;

			public UgcStream( UGCHandle_t handle, int sizeInBytes ) 
			{
				_handle = handle;
				_sizeInBytes = sizeInBytes;
			}

			public override bool CanRead => true;

			public override bool CanSeek => true;

			public override bool CanWrite => false;

			public override long Length => _sizeInBytes;

			public override long Position { get => _offset; set => Seek( value, SeekOrigin.Begin ); }

			public override void Flush() { }

			public override unsafe int Read( byte[] buffer, int offset, int count )
			{
				if ( _closed )
				{
					throw new ObjectDisposedException( nameof( UgcStream ) );
				}

				count = Math.Min( count, (int) (_sizeInBytes - _offset) );

				if ( offset + count > buffer.Length )
				{
					throw new ArgumentOutOfRangeException( nameof(count), "Attempting to write beyond the end of the given buffer." );
				}

				fixed ( byte* ptr = buffer )
				{
					var read = SteamRemoteStorage.Internal.UGCRead( _handle, (IntPtr) (ptr + offset), count, _offset, UGCReadAction.ontinueReading );
					_offset += (uint) read;
					return read;
				}
			}

			public override long Seek( long offset, SeekOrigin origin )
			{
				if ( _closed )
				{
					throw new ObjectDisposedException( nameof( UgcStream ) );
				}

				switch ( origin )
				{
					case SeekOrigin.Begin:
						break;

					case SeekOrigin.Current:
						offset += _offset;
						break;

					case SeekOrigin.End:
						offset += _sizeInBytes;
						break;
				}

				if ( offset < 0 )
				{
					throw new Exception( "Attempted to seek before the start of the stream." );
				}

				if ( offset > _sizeInBytes )
				{
					throw new Exception( "Attempted to seek beyond the end of the stream." );
				}

				return _offset = (uint) offset;
			}

			public override void SetLength( long value )
			{
				throw new NotImplementedException();
			}

			public override void Write( byte[] buffer, int offset, int count )
			{
				throw new NotImplementedException();
			}

			public override void Close()
			{
				if ( _closed )
				{
					return;
				}

				_closed = true;
				SteamRemoteStorage.Internal.UGCRead( _handle, IntPtr.Zero, 0, 0, UGCReadAction.lose );
			}
		}

		public Stream OpenRead()
		{
			return new UgcStream( File.Value, SizeInBytes );
		}

		public override string ToString()
		{
			return FileName;
		}
	}
}
