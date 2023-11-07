namespace Vk.Schema;

public class CreatePaymentByEftRequest
{     
    public string SenderAccountId { get; set; }
    public string IBAN { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
}

public class PaymentByEftResponse
{
    public string refNumber { get; set; } // otomatik oluşacak
    public string IBAN { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    
    public string Status  { get; set; } // islem sonucuna göre olusacak
    public DateTime TransactionDate { get; set; } // islem sonucuna göre olusacak
}