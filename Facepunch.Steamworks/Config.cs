using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public static class Config
    {
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
        public static void ForcePlatform( SteamNative.OperatingSystem os, SteamNative.Architecture arch )
        {
            SteamNative.Platform.Os = os;
            SteamNative.Platform.Arch = arch;
        }

    }
}
