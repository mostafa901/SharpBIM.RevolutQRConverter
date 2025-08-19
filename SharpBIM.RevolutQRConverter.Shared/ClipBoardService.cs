namespace SharpBIM.RevolutQRConverter.Shared
{
    public interface IClipBoardService
    {
        public   Task<string> GetFromClipboardAsync();
        public Task CopyToClipboardAsync(string text);


    }
    public interface IQrService
    {
        event EventHandler<string> QrCodeDecoded;

        void DecodeQrFromStream((byte[] luminances, int width, int height) imagebitmap);
    }
   

}
