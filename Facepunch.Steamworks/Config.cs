using System;

namespace Steamworks
{
    public static class Config
    {
		public static OsType Os { get; set; }
		public static bool PackSmall => Os != OsType.Windows;

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

				Os = OsType.Windows;
            }

            if ( platform == "OSXEditor" || platform == "OSXPlayer" || platform == "OSXDashboardPlayer" )
            {
				Os = OsType.macOS;
            }

            if ( platform == "LinuxPlayer" || platform == "LinuxEditor" )
            {
				Os = OsType.Linux;
            }

            Console.WriteLine( "Steamworks Unity: " + platform );
            Console.WriteLine( "Steamworks Os: " + Os );
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
    }

	public enum OsType
	{
		Windows,
		Linux,
		macOS,
	}
}
