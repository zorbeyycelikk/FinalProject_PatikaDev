namespace Vk.Schema;

public class CreatePaymentByOpenAccountRequest
{     
    public string CustomerId { get; set; }
    public string ReceiverCustomerId { get; set; }
    public decimal Amount { get; set; }
}

public class PaymentByOpenAccountResponse
{
    public string CustomerId { get; set; }
    public string ReceiverCustomerId { get; set; }
    public string refNumber { get; set; }
    public string PaymentMethod { get; set; }
    public string Who { get; set; }
    public decimal Amount { get; set; }
    public string Status  { get; set; } // islem sonucuna göre olusacak
    public DateTime TransactionDate { get; set; } // islem sonucuna göre olusacak
}