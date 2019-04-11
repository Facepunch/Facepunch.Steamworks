using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Facepunch.Steamworks
{
    public enum OperatingSystem
    {
        Unset,
        Windows,
        Linux,
        macOS,
    }
}

namespace SteamNative
{
    internal static partial class Platform
    {
        private static Facepunch.Steamworks.OperatingSystem _os;

        public static Facepunch.Steamworks.OperatingSystem RunningPlatform()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:                    
                    // macOS sometimes reports to .NET as Unix. Fix is to check against macOS root folders
                    if (Directory.Exists("/Applications") && Directory.Exists("/System") && Directory.Exists("/Users") && Directory.Exists("/Volumes"))
                        return Facepunch.Steamworks.OperatingSystem.macOS;
                    else
                        return Facepunch.Steamworks.OperatingSystem.Linux;
                case PlatformID.MacOSX:
                    return Facepunch.Steamworks.OperatingSystem.macOS;

                default:
                    return Facepunch.Steamworks.OperatingSystem.Windows;
            }
        }

        internal static Facepunch.Steamworks.OperatingSystem Os
        {
            get
            {
                //
                // Work out our platform
                //
                if ( _os == Facepunch.Steamworks.OperatingSystem.Unset )
                {
                    _os = Facepunch.Steamworks.OperatingSystem.Windows;

#if !NET_CORE
                    // Fixed Bet
                    _os = RunningPlatform();                   
#endif
                }

                return _os;
            }

            set
            {
                _os = value;
            }
        }

		public static bool IsWindows => Os == Facepunch.Steamworks.OperatingSystem.Windows;
		public static bool IsLinux => Os == Facepunch.Steamworks.OperatingSystem.Linux;
		public static bool IsOsx => Os == Facepunch.Steamworks.OperatingSystem.macOS;


		/// <summary>
		/// We're only Pack = 8 on Windows
		/// </summary>
		public static bool PackSmall => Os != Facepunch.Steamworks.OperatingSystem.Windows;
    }
}
