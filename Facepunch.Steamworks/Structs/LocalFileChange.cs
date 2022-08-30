using System;
using System.Collections.Generic;
using System.Text;

namespace Steamworks
{
	/// <summary>
	/// References a Remote Storage file that was externally changed
	/// during the lifetime of the application.
	/// See <see cref="SteamRemoteStorage.OnLocalFileChange"/>.
	/// </summary>
	/// <param name="Filename">Name of the changed file, of type <paramref name="PathType"/></param>
	/// <param name="ChangeType">What happened to this file</param>
	/// <param name="PathType">Type of path to the file</param>
	public record struct LocalFileChange(
		string Filename,
		RemoteStorageLocalFileChange ChangeType,
		RemoteStorageFilePathType PathType );
}
