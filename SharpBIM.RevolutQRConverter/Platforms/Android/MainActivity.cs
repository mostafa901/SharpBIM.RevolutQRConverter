using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;

using Java.Util.Streams;

using SharpBIM.RevolutQRConverter.Shared;

using ZXing;

using ZXing.Common;
using ZXing.Net.Maui;

namespace SharpBIM.RevolutQRConverter;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
[IntentFilter(
    new[] { Android.Content.Intent.ActionSend },
    Categories = new[] { Android.Content.Intent.CategoryDefault },
    DataMimeType = "image/*"
)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        HandleSendIntent(Intent);
    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);
        HandleSendIntent(intent);
    }

    private void HandleSendIntent(Intent intent)
    {
        if (intent.Action == Intent.ActionSend && intent.Type?.StartsWith("image/") == true)
        {
            Android.Net.Uri imageUri = (Android.Net.Uri)intent.GetParcelableExtra(Intent.ExtraStream);
            if (imageUri != null)
            {
                using var imageStream = ContentResolver.OpenInputStream(imageUri);
                Bitmap bitmap = BitmapFactory.DecodeStream(imageStream);
                bitmap = bitmap.Copy(Bitmap.Config.Argb8888, false);

                var qrService = Microsoft.Maui.Controls.Application.Current.Handler.MauiContext.Services.GetService<IQrService>();
                (byte[] luminances, int width, int height) lum = DecodeQrFromBitmap(bitmap);
                qrService?.DecodeQrFromStream(lum);
            }
        }
    }

    private (byte[] luminances, int width, int height) DecodeQrFromBitmap(Bitmap bitmap)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;
        int[] pixels = new int[width * height];

        bitmap.GetPixels(pixels, 0, width, 0, 0, width, height);

        byte[] luminances = new byte[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            int color = pixels[i];
            int r = (color >> 16) & 0xFF;
            int g = (color >> 8) & 0xFF;
            int b = color & 0xFF;
            luminances[i] = (byte)((r + g + b) / 3);
        }

        return (luminances, width, height);
    }
}