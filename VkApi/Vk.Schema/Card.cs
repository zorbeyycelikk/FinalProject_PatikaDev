namespace Vk.Schema;

public class CreateCardRequest
{ 
    public string AccountId { get; set; }
    public string CardNumber { get; set; }
    public string Cvv { get; set; } // nnn
    public DateTime ExpiryDate { get; set; } // DDyy
}

public class UpdateCardRequest
{
    public DateTime ExpiryDate { get; set; } // DDyy
}

public class CardResponse
{
    public string AccountId { get; set; }
    public string AccountNumber { get; set; } // Account'tan gelir
    public string CardHolderId { get; set; } // Account'tan gelir
    public string CardNumber { get; set; }
    public string Cvv { get; set; } // nnn
    public DateTime ExpiryDate { get; set; } // DDyy
}

