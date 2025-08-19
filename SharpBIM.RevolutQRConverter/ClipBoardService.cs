using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBIM.RevolutQRConverter
{
    public interface IClipBoardService
    {
        public   Task<string> GetFromClipboardAsync();
        public Task CopyToClipboardAsync(string text);
     
    }
    // In Shared project
    public interface IScannerService
    {
        Task OpenScannerAsync(IClipBoardService clipBoardService);
    }

}
