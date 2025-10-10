using System.Threading.Tasks;

namespace Steamworks.Data
{
    public struct Browser
    {

        public readonly uint Handle;

        public Browser(uint handle)
        {
            Handle = handle;
        }

        public static async Task<Browser> Create(string userAgent = null, string userCss = null)
        {
            var result = await SteamHTMLSurface.Internal.CreateBrowser(userAgent, userCss);
            return new Browser(result.Value.UnBrowserHandle);
        }

        public void FileLoadDialogResponse(string selectedFiles)
        {
            SteamHTMLSurface.Internal.FileLoadDialogResponse(Handle, selectedFiles);
        }

        public void JSDialogResponse(bool response)
        {
            SteamHTMLSurface.Internal.JSDialogResponse(Handle, response);
        }

        public void AllowStartRequest(bool allowed)
        {
            SteamHTMLSurface.Internal.AllowStartRequest(Handle, allowed);
        }

        public void SetSize(int width, int height)
        {
            SteamHTMLSurface.Internal.SetSize(Handle, (uint)width, (uint)height);
        }

        public void LoadURL(string url, string postData = null)
        {
            SteamHTMLSurface.Internal.LoadURL(Handle, url, postData);
        }

        public void StopLoad()
        {
	        SteamHTMLSurface.Internal.StopLoad(Handle);
        }

        public void Reload()
        {
	        SteamHTMLSurface.Internal.Reload(Handle);
        }

        public void ExecuteJavascript( string code )
        {
	        SteamHTMLSurface.Internal.ExecuteJavascript(Handle, code);
        }
        
        public void Remove()
        {
            SteamHTMLSurface.Internal.RemoveBrowser(Handle);
        }

        public void MouseMove(int x, int y)
        {
            SteamHTMLSurface.Internal.MouseMove(Handle, x, y);
        }

        public void GoBack()
        {
	        SteamHTMLSurface.Internal.GoBack(Handle);
        }
        
        public void KeyDown(int nativeKeyCode, bool alt, bool ctrl, bool shift, bool isSystemKey = false)
        {
            int keyModifiers = 0;
            if (alt) keyModifiers |= 1 << 0;
            if (ctrl) keyModifiers |= 1 << 1;
            if (shift) keyModifiers |= 1 << 2;
            SteamHTMLSurface.Internal.KeyDown(Handle, (uint)nativeKeyCode, (System.IntPtr)keyModifiers, isSystemKey);
        }

        public void KeyChar(char unicode, bool alt, bool ctrl, bool shift)
        {
            int keyModifiers = 0;
            if (alt) keyModifiers |= 1 << 0;
            if (ctrl) keyModifiers |= 1 << 1;
            if (shift) keyModifiers |= 1 << 2;
            SteamHTMLSurface.Internal.KeyChar(Handle, unicode, (System.IntPtr)keyModifiers);
        }

        public void KeyUp(int nativeKeyCode, bool alt, bool ctrl, bool shift)
        {
            int keyModifiers = 0;
            if (alt) keyModifiers |= 1 << 0;
            if (ctrl) keyModifiers |= 1 << 1;
            if (shift) keyModifiers |= 1 << 2;
            SteamHTMLSurface.Internal.KeyUp(Handle, (uint)nativeKeyCode, (System.IntPtr)keyModifiers);
        }

        public void SetKeyFocus(bool hasFocus)
        {
            SteamHTMLSurface.Internal.SetKeyFocus(Handle, hasFocus);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button">0 = left, 1 = right, 2 = middle</param>
        public void MouseDown(int button)
        {
            SteamHTMLSurface.Internal.MouseDown(Handle, (System.IntPtr)button);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button">0 = left, 1 = right, 2 = middle</param>
        public void MouseUp(int button)
        {
            SteamHTMLSurface.Internal.MouseUp(Handle, (System.IntPtr)button);
        }

        public void MouseWheel(int delta)
        {
            SteamHTMLSurface.Internal.MouseWheel(Handle, delta);
        }

    }
}
