using System;

namespace Facepunch.Steamworks
{
    public static class Config
    {
        /// <summary>
        /// Should be called before creating any interfaces, to configure Steam for Unity.
        /// </summary>
        /// <param name="platform">Please pass in Application.platform.ToString()</param>
        public static void ForUnity( string platform )
        {
            //
            // Windows Config
            //
            if ( platform == "WindowsEditor" || platform == "WindowsPlayer" )
            {
                //
                // 32bit windows unity uses a stdcall
                //
                if (IntPtr.Size == 4) UseThisCall = false;

                ForcePlatform( OperatingSystem.Windows, IntPtr.Size == 4 ? Architecture.x86 : Architecture.x64 );                
            }

            if ( platform == "OSXEditor" || platform == "OSXPlayer" || platform == "OSXDashboardPlayer" )
            {
                ForcePlatform( OperatingSystem.macOS, IntPtr.Size == 4 ? Architecture.x86 : Architecture.x64 );
            }

            if ( platform == "LinuxPlayer" || platform == "LinuxEditor" )
            {
                ForcePlatform( OperatingSystem.Linux, IntPtr.Size == 4 ? Architecture.x86 : Architecture.x64 );
            }

            Console.WriteLine( "Facepunch.Steamworks Unity: " + platform );
            Console.WriteLine( "Facepunch.Steamworks Os: " + SteamNative.Platform.Os );
            Console.WriteLine( "Facepunch.Steamworks Arch: " + SteamNative.Platform.Arch );
        }

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


        /// <summary>
        /// You can force the platform to a particular one here.
        /// This is useful if you're on OSX because some versions of mono don't have a way
        /// to tell which platform we're running
        /// </summary>
        public static void ForcePlatform( Facepunch.Steamworks.OperatingSystem os, Facepunch.Steamworks.Architecture arch )
        {
            SteamNative.Platform.Os = os;
            SteamNative.Platform.Arch = arch;
        }

    }
}
