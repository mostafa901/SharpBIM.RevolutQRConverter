
using Microsoft.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SharpBIM.RevolutQRConverter.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication  
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
       
    }
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        var window = Microsoft.Maui.Controls.Application.Current.Windows[0].Handler.PlatformView as Microsoft.UI.Xaml.Window;

        IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = AppWindow.GetFromWindowId(windowId);

        // Auto-size to content: estimate size (WinUI 3 doesn’t fully support dynamic content size)
        appWindow.Resize(new SizeInt32(800, 600)); // Start with preferred size
        appWindow.ResizeClient(new SizeInt32(500, 916));
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

public class WinClipService : IClipBoardService
{
    public Task<string> GetFromClipboardAsync()
    {
        var text = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent().GetTextAsync();
        return text.AsTask();
    }
    public Task CopyToClipboardAsync(string text)
    {
        var dataPackage = new Windows.ApplicationModel.DataTransfer.DataPackage();
        dataPackage.SetText(text);
        Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
        return Task.CompletedTask;
    }
}
