using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

using SharpBIM.RevolutQRConverter.Pages;

using SharpBIM.RevolutQRConverter.Shared;
using System.Reflection;

namespace SharpBIM.RevolutQRConverter;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        ClipBoardService = App.Services.GetService<IClipBoardService>();
        QrService = App.Services.GetService<IQrService>();
        QrService.QrCodeDecoded += QrService_QrCodeDecoded;

        Version = $"ver. {Assembly.GetExecutingAssembly().GetName().Version}";

    }

    async private void QrService_QrCodeDecoded(object? sender, string e)
    {
        await ParseandLoad(e);

    }

    private string _version;

    public string Version
    {
        get { return _version; }
        set
        {
            _version = value;
            OnPropertyChanged(nameof(Version));
        }
    }

    public IClipBoardService ClipBoardService { get; }
    public IQrService? QrService { get; }

    public ObservableCollection<PropertyItem> Properties { get; set; } = new();



    private string _revolutValue;

    public string RevolutValue
    {
        get { return _revolutValue; }
        set
        {
            _revolutValue = value;
            OnPropertyChanged(nameof(RevolutValue));
        }
    }

    private async void OnStartScanClicked(object sender, EventArgs e)
    {
        var scanner = new Scanner(async (x) =>
        {
            await Navigation.PopModalAsync();

            await ParseandLoad(x);
        });

        await Navigation.PushModalAsync(scanner);
    }

    async private Task ParseandLoad(string x)
    {
        if (!string.IsNullOrEmpty(x))
        {
            var scannedUPN = UpnQrParser.Parse(x);
            RevolutValue = UpnQrParser.ConvertToRevolutString(scannedUPN);
            await ClipBoardService.CopyToClipboardAsync(scannedUPN.ReferenceNumber);
            await ClipBoardService.CopyToClipboardAsync(scannedUPN.Amount.ToString());
            Properties.Clear();
            var data = UpnQrParser.LoadProperties(scannedUPN);
            foreach (var item in data)
            {
                Properties.Add(item);
            }

        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private async void Frame_Tapped(object sender, TappedEventArgs e)
    {
        var ed = sender as Editor;
        var tag = ed?.AutomationId as string;
        var formatted = UpnQrParser.FormatPropertyValue(tag, ed.Text);

        await ClipBoardService.CopyToClipboardAsync(formatted);
    }

    async private void OnPrivacyPolicyTapped(object sender, TappedEventArgs e)
    {
        try
        {
            // await  Navigation.PushModalAsync(new PrivacyPolicyPage());
            Microsoft.Maui.ApplicationModel.Browser.OpenAsync("https://slov-qr-revolout.tryasp.net/privacypolicy", Microsoft.Maui.ApplicationModel.BrowserLaunchMode.SystemPreferred);

        }
        catch (Exception ex)
        {
            // Handle exception
            Console.WriteLine(ex.Message);
        }
    }
}
