namespace Vk.Schema;

public class CreateCardRequest
{ 
    public string AccountNumber { get; set; }
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
    public string AccountNumber { get; set; }
    public string CardHolderNumber { get; set; } // Account'tan gelir
    public string CardNumber { get; set; }
    public string Cvv { get; set; } // nnn
    public DateTime ExpiryDate { get; set; } // DDyy
}

