
using Android.App;
using Android.Runtime;

namespace SharpBIM.RevolutQRConverter;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

public class AndroidClipService : IClipBoardService
{
    public async Task CopyToClipboardAsync(string text)
    {
        await Clipboard.Default.SetTextAsync(text);
    }

    public async Task<string> GetFromClipboardAsync()
    {
        if (Clipboard.Default.HasText)
            return await Clipboard.Default.GetTextAsync();
        return string.Empty;
    }

    
}