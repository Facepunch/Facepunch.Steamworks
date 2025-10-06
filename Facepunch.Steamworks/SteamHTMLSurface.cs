using Steamworks.Data;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Steamworks
{

    public struct BrowserNewWindow
    {
        public string Url;
        public int PosX;
        public int PosY;
        public int Width;
        public int Height;
        public Browser NewBrowser;

        internal BrowserNewWindow(HTML_NewWindow_t t)
        {
            Url = t.PchURL;
            PosX = (int)t.UnX;
            PosY = (int)t.UnY;
            Width = (int)t.UnWide;
            Height = (int)t.UnTall;
            NewBrowser = new Browser(t.UnNewWindow_BrowserHandle_IGNORE);
        }
    }

    public struct BrowserPaint
    {
        /// <summary>
        ///  B8G8R8A8 
        /// </summary>
        public IntPtr ImageData;
        public uint Width;
        public uint Height;
        public uint DirtyX;
        public uint DirtyY;
        public uint DirtyWidth;
        public uint DirtyHeight;
        public uint ScrollX;
        public uint ScrollY;
        public float PageScale;
        public uint PageSerial;
        public int DataSize;

        internal BrowserPaint(HTML_NeedsPaint_t t)
        {
            ImageData = t.PBGRA;
            Width = t.UnWide;
            Height = t.UnTall;
            DirtyX = t.UnUpdateX;
            DirtyY = t.UnUpdateY;
            DirtyWidth = t.UnUpdateWide;
            DirtyHeight = t.UnUpdateTall;
            ScrollX = t.UnScrollX;
            ScrollY = t.UnScrollY;
            PageScale = t.FlPageScale;
            PageSerial = t.UnPageSerial;
            DataSize = t.DataSize;
        }
    }

    public class SteamHTMLSurface : SteamClientClass<SteamHTMLSurface>
    {

        internal static ISteamHTMLSurface Internal => Interface as ISteamHTMLSurface;

        internal override bool InitializeInterface(bool server)
        {
            SetInterface(server, new ISteamHTMLSurface(server));
            InstallEvents();

            return true;
        }

        internal void InstallEvents()
        {
            Dispatch.Install<HTML_BrowserReady_t>(x => OnBrowserReady?.Invoke(new Browser(x.UnBrowserHandle)));
            Dispatch.Install<HTML_StartRequest_t>(x => OnStartRequest?.Invoke(new Browser(x.UnBrowserHandle), x.PchURL, x.PchTarget, x.PchPostData, x.BIsRedirect));
            Dispatch.Install<HTML_JSAlert_t>(x => OnJSAlert?.Invoke(new Browser(x.UnBrowserHandle), x.PchMessage));
            Dispatch.Install<HTML_JSConfirm_t>(x => OnJSConfirm?.Invoke(new Browser(x.UnBrowserHandle), x.PchMessage));
            Dispatch.Install<HTML_FileOpenDialog_t>(x => OnFileOpenDialog?.Invoke(new Browser(x.UnBrowserHandle), x.PchTitle, x.PchInitialFile));
            Dispatch.Install<HTML_FinishedRequest_t>(x => OnFinishedLoading?.Invoke(new Browser(x.UnBrowserHandle), x.PchURL, x.PchPageTitle));
            Dispatch.Install<HTML_NeedsPaint_t>(x => OnNeedsPaint?.Invoke(new Browser(x.UnBrowserHandle), new BrowserPaint(x)));
            Dispatch.Install<HTML_NewWindow_t>(x => OnNewWindow?.Invoke(new Browser(x.UnBrowserHandle), new BrowserNewWindow(x)));
            Dispatch.Install<HTML_OpenLinkInNewTab_t>(x => OnNewTab?.Invoke(new Browser(x.UnBrowserHandle), x.PchURL));
            Dispatch.Install<HTML_StatusText_t>(x => OnStatusText?.Invoke(new Browser(x.UnBrowserHandle), x.PchMsg));
        }

        internal override void DestroyInterface(bool server)
        {
            Internal.Shutdown();

            base.DestroyInterface(server);
        }

        /// <summary>
        /// Called when a browser wants you to display an informational message. This is most commonly used when you hover over links.
        /// </summary>
        public static event Action<Browser, string> OnStatusText;

        /// <summary>
        /// The browser has requested to load a url in a new tab.
        /// </summary>
        public static event Action<Browser, string> OnNewTab;

        /// <summary>
        /// A browser has created a new HTML window.
        /// </summary>
        public static event Action<Browser, BrowserNewWindow> OnNewWindow;

        /// <summary>
        /// A new browser was created and is ready for use.
        /// </summary>
        public static event Action<Browser> OnBrowserReady;

        /// <summary>
        /// Called when a browser surface has a pending paint. This is where you get the actual image data to render to the screen.
        /// </summary>
        public static event Action<Browser, BrowserPaint> OnNeedsPaint;

        /// <summary>
        /// Called when a browser has finished loading a page.
        /// Parameters: Browser, URL, Page Title
        /// </summary>
        public static event Action<Browser, string, string> OnFinishedLoading;

        /// <summary>
        /// Called when a browser surface has received a file open dialog from a `input type="file"` click or similar, you must call FileLoadDialogResponse with the file(s) the user selected.
        /// Parameters: Browser, Title, Initial File
        /// </summary>
        public static event Action<Browser, string, string> OnFileOpenDialog;

        /// <summary>
        /// Called when the browser wants to display a Javascript confirmation dialog, call JSDialogResponse when the user dismisses this dialog; or right away to ignore it.
        /// Parameters: Browser, Message
        /// </summary>
        public static event Action<Browser, string> OnJSConfirm;

        /// <summary>
        /// Called when the browser wants to display a Javascript alert dialog, call JSDialogResponse when the user dismisses this dialog; or right away to ignore it.
        /// Parameters: Browser, Message
        /// </summary>
        public static event Action<Browser, string> OnJSAlert;

        /// <summary>
        /// Called when a browser wants to navigate to a new page.
        /// Parameters: Browser, URL, Target, Post Data, Is Redirect
        /// </summary>
        public static event Action<Browser, string, string, string, bool> OnStartRequest;

        /// <summary>
        /// Initializes the HTML Surface API.
        /// This must be called prior to using any other functions in this interface.
        /// You MUST call Shutdown when you are done using the interface to free up the resources associated with it. Failing to do so will result in a memory leak!
        /// </summary>
        /// <returns></returns>
        public static bool Init()
        {
            return Internal.Init();
        }

        /// <summary>
        /// Shutdown the ISteamHTMLSurface interface, releasing the memory and handles.
        /// </summary>
        /// <returns></returns>
        public static bool Shutdown()
        {
            return Internal.Shutdown();
        }

        /// <summary>
        /// Create a browser object for displaying of an HTML page.
        /// NOTE: You MUST call RemoveBrowser when you are done using this browser to free up the resources associated with it. Failing to do so will result in a memory leak.
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="userCss"></param>
        /// <returns></returns>
        public static async Task<Browser> CreateBrowser(string userAgent, string userCss)
        {
            var result = await Internal.CreateBrowser(userAgent, userCss);
            return new Browser(result.Value.UnBrowserHandle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser"></param>
        public static void RemoveBrowser(Browser browser)
        {
            Internal.RemoveBrowser(browser.Handle);
        }

    }
}
