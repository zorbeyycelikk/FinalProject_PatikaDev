namespace Vk.Schema;

public class CreateCompleteOrderWithHavaleRequest 
{
    public string   CustomerId       { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   PaymentMethod    { get; set; }
    public decimal   Amount          { get; set; }
    public string BasketId { get; set; }
    
    public string SenderAccountNumber { get; set; }
    public string AccountNumber { get; set; }
    public string TransferDescription { get; set; }
    public string Name { get; set; }
}

public class CreateCompleteOrderWithEftRequest
{
    
    public string   CustomerId       { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   PaymentMethod    { get; set; }
    public string   BasketId { get; set; }
    
    public string SenderAccountNumber { get; set; }
    public string IBAN { get; set; }
    public string Name { get; set; }
    public string TransferDescription { get; set; }
    public decimal Amount { get; set; }
}

public class CreateCompleteOrderWithCardRequest
{
    public string   CustomerId       { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   PaymentMethod    { get; set; }
    public string   BasketId         { get; set; }
 
    public string receiverAccountNumber  { get; set; } // Alıcının hesap numarasi
    public string CardNumber             { get; set; }
    public string Cvv                    { get; set; } // nnn
    public DateTime ExpiryDate           { get; set; } // DDyy
    public decimal Amount                { get; set; } // İşlemin Tutar
}

public class CreateCompleteOrderWithOpenAccountRequest
{
    
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   PaymentMethod    { get; set; }
    public string BasketId { get; set; }
    
    public string CustomerId { get; set; }
    public string ReceiverCustomerId { get; set; }
    public decimal Amount { get; set; }

}


public class CompleteOrderResponse2
{

}