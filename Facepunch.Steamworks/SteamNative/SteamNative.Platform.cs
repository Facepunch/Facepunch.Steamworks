using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
    public enum OperatingSystem
    {
        Unset,
        Windows,
        Linux,
        Osx,
    }

    public enum Architecture
    {
        Unset,
        x86,
        x64
    }

    internal static partial class Platform
    {
        private static OperatingSystem _os;
        private static Architecture _arch;

        internal static OperatingSystem Os
        {
            get
            {
                //
                // Work out our platform
                //
                if ( _os == OperatingSystem.Unset )
                {
                    _os = OperatingSystem.Windows;

                    //
                    // These checks aren't so accurate on older versions of mono
                    //
                    if ( Environment.OSVersion.Platform == PlatformID.MacOSX ) _os = OperatingSystem.Osx;
                    if ( Environment.OSVersion.Platform == PlatformID.Unix ) _os = OperatingSystem.Linux;

                    //
                    // Edging our bets
                    //
                    if ( Environment.OSVersion.VersionString.ToLower().Contains( "unix" ) ) _os = OperatingSystem.Linux;
                    if ( Environment.OSVersion.VersionString.ToLower().Contains( "osx" ) ) _os = OperatingSystem.Osx;
                }

                return _os;
            }

            set
            {
                _os = value;
            }
        }

        internal static Architecture Arch
        {
            get
            {
                //
                // Work out whether we're 64bit or 32bit
                //
                if ( _arch == Architecture.Unset )
                {
                    if ( IntPtr.Size == 8 )
                        _arch = Architecture.x64;
                    else if ( IntPtr.Size == 4 )
                        _arch = Architecture.x86;
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

        public static bool IsWindows64 { get { return Arch == Architecture.x64 && Os == OperatingSystem.Windows; } }
        public static bool IsWindows32 { get { return Arch == Architecture.x86 && Os == OperatingSystem.Windows; } }
        public static bool IsLinux64 { get { return Arch == Architecture.x64 && Os == OperatingSystem.Linux; } }
        public static bool IsLinux32 { get { return Arch == Architecture.x86 && Os == OperatingSystem.Linux; } }
        public static bool IsOsx { get { return Os == OperatingSystem.Osx; } }


        /// <summary>
        /// We're only Pack = 8 on Windows
        /// </summary>
        public static bool PackSmall { get { return Os != OperatingSystem.Windows; } }
    }
}
