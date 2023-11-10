namespace Vk.Schema;

public class CreatePaymentByCardRequest
{     
    public string receiverAccountNumber  { get; set; } // Alıcının hesap numarasi
    public string CardNumber { get; set; }
    public string Cvv { get; set; } // nnn
    public DateTime ExpiryDate { get; set; } // DDyy
    public decimal Amount { get; set; } // İşlemin Tutar
}

public class PaymentByCardResponse
{
    public string CardId { get; set; } // card'dan gelecek
    
    public string transactionRefNumber { get; set; } // otomatik yaratilirken olusturulacak
    public string receiverAccountNumber  { get; set; } // Alıcının hesap numarasi

    public string CardNumber { get; set; }
    public string Cvv { get; set; } // nnn
    public DateTime ExpiryDate { get; set; } // DDyy
    
    public decimal Amount { get; set; } // İşlemin Tutarı
    public string Status { get; set; } // Bekleniyor | Basarili | Basarisiz
}

