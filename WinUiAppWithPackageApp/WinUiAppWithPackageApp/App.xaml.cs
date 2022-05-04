using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUiAppWithPackageApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        AppWindow appWindow;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();

            if (AppWindowTitleBar.IsCustomizationSupported()) //Run only on Windows 11
            {
                m_window.SizeChanged += SizeChanged; //Register handler for setting draggable rects

                appWindow = GetAppWindow(m_window); //Set ExtendsContentIntoTitleBar for the AppWindow not the window
                appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
            m_window.Activate();
        }

        private void SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            //Update the title bar draggable region. We need to indent from the left both for the nav back button and to avoid the system menu
            Windows.Graphics.RectInt32[] rects = new Windows.Graphics.RectInt32[] { new Windows.Graphics.RectInt32(48, 0, (int)args.Size.Width - 48, 48) };
            appWindow.TitleBar.SetDragRectangles(rects);
        }

        private AppWindow GetAppWindow(Window window)
        {
            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            return AppWindow.GetFromWindowId(windowId);
        }

        private Window m_window;
    }
}
