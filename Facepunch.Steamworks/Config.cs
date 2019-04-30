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

		public static bool PackSmall => Os != OsType.Windows;

        /// <summary>
        /// Some platforms allow/need CallingConvention.ThisCall. If you're crashing with argument null
        /// errors on certain platforms, try flipping this to true.
        /// 
        /// I owe this logic to Riley Labrecque's hard work on Steamworks.net - I don't have the knowledge
        /// or patience to find this shit on my own, so massive thanks to him. And also massive thanks to him
        /// for releasing his shit open source under the MIT license so we can all learn and iterate.
        /// 
        /// </summary>
        public static bool UseThisCall { get; set; } = true;
    }

	public enum OsType
	{
		None,
		Windows,
		Posix,
	}
}
