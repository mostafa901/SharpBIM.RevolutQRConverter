namespace SharpBIM.RevolutQRConverter.Shared;

public class UpnQrData
{
    public string QrType { get; set; }
    public string Payee { get; set; }
    public string Address { get; set; }
    public string Address2 { get; set; }
    public decimal Amount { get; set; }
    public string CodeName { get; set; }
    public string PaymentName { get; set; }
    public string Date { get; set; }
    public string IBAN { get; set; }
    public string ReferenceNumber { get; set; }
    public string Beneficiary { get; set; }
    public string beneficiaryAddress { get; set; }
    public string beneficiaryAddress2 { get; set; }
    //public string SomeCode { get; set; }
}
