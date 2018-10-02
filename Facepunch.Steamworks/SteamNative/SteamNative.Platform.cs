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

    public enum Architecture
    {
        Unset,
        x86,
        x64
    }
}

namespace SteamNative
{
    internal static partial class Platform
    {
        private static Facepunch.Steamworks.OperatingSystem _os;
        private static Facepunch.Steamworks.Architecture _arch;

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

        internal static Facepunch.Steamworks.Architecture Arch
        {
            get
            {
                //
                // Work out whether we're 64bit or 32bit
                //
                if ( _arch == Facepunch.Steamworks.Architecture.Unset )
                {
                    if ( IntPtr.Size == 8 )
                        _arch = Facepunch.Steamworks.Architecture.x64;
                    else if ( IntPtr.Size == 4 )
                        _arch = Facepunch.Steamworks.Architecture.x86;
                    else
                        throw new System.Exception( "Unsupported Architecture!" );
                }

                return _arch;
            }

            set
            {
                _arch = value;
            }
        }

        public static bool IsWindows { get { return Os == Facepunch.Steamworks.OperatingSystem.Windows; } }
        public static bool IsWindows64 { get { return Arch == Facepunch.Steamworks.Architecture.x64 && IsWindows; } }
        public static bool IsWindows32 { get { return Arch == Facepunch.Steamworks.Architecture.x86 && IsWindows; } }
        public static bool IsLinux64 { get { return Arch == Facepunch.Steamworks.Architecture.x64 && Os == Facepunch.Steamworks.OperatingSystem.Linux; } }
        public static bool IsLinux32 { get { return Arch == Facepunch.Steamworks.Architecture.x86 && Os == Facepunch.Steamworks.OperatingSystem.Linux; } }
        public static bool IsOsx { get { return Os == Facepunch.Steamworks.OperatingSystem.macOS; } }


        /// <summary>
        /// We're only Pack = 8 on Windows
        /// </summary>
        public static bool PackSmall { get { return Os != Facepunch.Steamworks.OperatingSystem.Windows; } }
    }
}
