using System;
using System.IO;

namespace Steamworks
{
    public static class Config
    {
		static OsType _os;
		public static OsType Os
		{
			get
			{
				if ( _os == OsType.None )
				{
					string windir = Environment.GetEnvironmentVariable( "windir" );
					if ( !string.IsNullOrEmpty( windir ) && windir.Contains( @"\" ) && Directory.Exists( windir ) )
					{
						_os = OsType.Windows;
					}
					else if ( File.Exists( @"/System/Library/CoreServices/SystemVersion.plist" ) )
					{
						_os = OsType.Posix;
					}
					else if ( File.Exists( @"/proc/sys/kernel/ostype" ) )
					{
						_os = OsType.Posix;
					}
					else
					{
						throw new System.Exception( "Couldn't determine operating system" );
					}
				}

				return _os;
			}

			set => _os = value;
		}
    }

	public enum OsType
	{
		None,
		Windows,
		Posix,
	}
}
