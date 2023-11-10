namespace Vk.Schema;

public class CreatePaymentByEftRequest
{     
    public string SenderAccountNumber { get; set; }
    public string IBAN { get; set; }
    public string Name { get; set; }
    public string TransferDescription { get; set; }
    public decimal Amount { get; set; }
}

public class PaymentByEftResponse
{
    public string refNumber { get; set; } // otomatik oluşacak
    public string IBAN { get; set; }    // alıcı
    public string Name { get; set; }    // alıcı
    public string TransferDescription { get; set; } // alıcı
    public decimal Amount { get; set; }
    
    public string Status  { get; set; } // islem sonucuna göre olusacak
    public DateTime TransactionDate { get; set; } // islem sonucuna göre olusacak
}