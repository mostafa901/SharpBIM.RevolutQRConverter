using System.Globalization;
using System.Reflection;
using System.Security;

namespace SharpBIM.RevolutQRConverter.Shared;

public static class UpnQrParser
{
    public static string ConvertToRevolutString(UpnQrData scannedClass)
    {
        // Define payment info - replace with your actual data
        string bic = "REVOGB21";
        string name = scannedClass.Beneficiary;
        string iban = scannedClass.IBAN;
        string currency = "EUR";
        string amount = scannedClass.Amount.ToString();
        string reference = scannedClass.ReferenceNumber;

        // Build EPC QR code text (SEPA Credit Transfer - EPC069-12)
        string epc = $@"BCD
001
1
SCT
{bic}
{name}
{iban.Replace(" ", "")}
{currency}{amount}
{scannedClass.Beneficiary}
{reference.Replace(" ", "")}";

        return epc;
    }

    public static List<PropertyItem> LoadProperties(UpnQrData scannedUPN)
    {
        var properties = new List<PropertyItem>();
        if (scannedUPN == null)
            return properties;

        foreach (var prop in scannedUPN.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            properties.Add(new PropertyItem
            {
                Name = prop.Name,
                Value = prop.GetValue(scannedUPN)?.ToString() ?? string.Empty
            });
        }

        return properties;
    }

    public static UpnQrData Parse(string rawText)
    {
        if (string.IsNullOrWhiteSpace(rawText))
            return null;

        var lines = rawText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        var data = new UpnQrData();

        try
        {
            int idx = 0;

            data.QrType = lines[idx++]; // "UPNQR"

            // Skip blank lines, they were removed by RemoveEmptyEntries

            data.Payee = lines[idx++];
            data.Address = lines[idx++];
            data.Address2 = lines[idx++];
            int amount = 0;
            if (int.TryParse(lines[idx++], out amount))
            {
                data.Amount = amount / 100.0m; // Convert to decimal, assuming amount is in cents
            }

            // Next might be blank lines skipped

            data.CodeName = lines[idx++];
            data.PaymentName = lines[idx++];
            data.Date = lines[idx++];
            data.IBAN = separate(lines[idx++], 4);
            data.ReferenceNumber = separate(lines[idx++], 3);
            data.Beneficiary = lines[idx++];
            data.beneficiaryAddress = lines[idx++];
            data.beneficiaryAddress2 = lines[idx++];
            //  data.SomeCode = lines[idx++];
        }
        catch (Exception ex)
        {

        }

        return data;
    }

    public static string FormatPropertyValue(string propName, string args)
    {
        if (propName == nameof(UpnQrData.ReferenceNumber) || propName == nameof(UpnQrData.IBAN))
        {
            // Copy to clipboard
            return args.Replace(" ", "");
        }
        return args;
    }

    private static string separate(string text, int by)
    {
        var result = new System.Text.StringBuilder();
        bool placed = false;
        for (int i = 0; i < text.Length; i++)
        {
            var sep = !placed ? 4 : by; // First character is always separated by 4, others by 'by'
            if (i > 0 && i % sep == 0)
            {
                result.Append(" ");
                placed = true;
            }
            result.Append(text[i]);
        }

        return result.ToString();
    }
}
