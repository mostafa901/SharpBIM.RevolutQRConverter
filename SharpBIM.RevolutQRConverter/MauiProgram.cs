using System.Text;

using Microsoft.Extensions.Logging;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Graphics.Platform;

using SharpBIM.RevolutQRConverter.Pages;
using SharpBIM.RevolutQRConverter.Shared;

using ZXing;
using ZXing.Common;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace SharpBIM.RevolutQRConverter;

public static class MauiProgram
{
    //public static IClipBoardService Meth { get; set; }
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        builder
            .UseMauiApp<App>()
            .UseBarcodeReader()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // builder.Services.AddMauiBlazorWebView();
        //  builder.Services.AddSingleton<IScannerService, ScannerService>();
        builder.Services.AddSingleton<IQrService, QrService>();

#if WINDOWS

            builder.Services.AddSingleton<IClipBoardService, SharpBIM.RevolutQRConverter.WinUI.WinClipService>();

#elif ANDROID
        builder.Services.AddSingleton<IClipBoardService, AndroidClipService>();
#endif

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

public class QrService : IQrService
{
    public event EventHandler<string?> QrCodeDecoded;

    public void DecodeQrFromStream((byte[] luminances, int width, int height) imageStream)
    {
        // ZXing expects luminance source
        
        var lu = new PlanarYUVLuminanceSource(imageStream.luminances, imageStream.width, imageStream.height,0,0,imageStream.width,imageStream.height,false);
        var reader = new BarcodeReader<Stream>((stream) => lu);
        reader.Options.TryHarder = true;
        reader.Options.PureBarcode= false;
        reader.Options.TryInverted= true;
        

        var result = reader.Decode(lu);



        QrCodeDecoded?.Invoke(null, result?.Text);
    }
}