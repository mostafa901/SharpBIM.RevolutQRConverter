using ZXing;
using System.Net.NetworkInformation;

namespace SharpBIM.RevolutQRConverter.Pages;

using System.Globalization;

using ZXing.Net.Maui;

public partial class Scanner : ContentPage
{
    public Scanner(Action<string> callBackAction)
    {

        InitializeComponent();
        cameraView.Options = new BarcodeReaderOptions
        {
            AutoRotate = true,
            TryHarder = true,
            Formats =
                    BarcodeFormat.QrCode |
                    BarcodeFormat.DataMatrix |
                    BarcodeFormat.Aztec
                ,
            Multiple = false,
            TryInverted = true,

        };

        cameraView.BarcodesDetected += CameraView_BarcodesDetected;
        CallBackAction = callBackAction;
    }

    public Action<string> CallBackAction { get; }

    private void CameraView_BarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
    {
        cameraView.IsDetecting = false;
        cameraView.BarcodesDetected -= CameraView_BarcodesDetected;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var barcode in e.Results)
            {
                CallBackAction(barcode.Value);
                break;
            }
        });
    }


    


    private void OnStartScanClicked(object sender, EventArgs e)
    {
        cameraView.IsDetecting = true;
    }

}