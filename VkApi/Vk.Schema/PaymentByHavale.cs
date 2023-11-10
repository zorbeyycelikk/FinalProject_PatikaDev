namespace Vk.Schema;

public class CreatePaymentByHavaleRequest
{     
    public string SenderAccountNumber { get; set; }
    public string AccountNumber { get; set; }
    public string Name { get; set; }
    public string TransferDescription { get; set; }
    public decimal Amount { get; set; }
}

public class PaymentByHavaleResponse
{
    public string refNumber { get; set; } // otomatik oluşacak
    public string AccountNumber { get; set; } // alıcı
    public string Name { get; set; }    // alıcı
    public string TransferDescription { get; set; } 
    public decimal Amount { get; set; }
    
    public string Status  { get; set; } // islem sonucuna göre olusacak
    public DateTime TransactionDate { get; set; } // islem sonucuna göre olusacak
}