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
    public string Name { get; set; }
}

public class CreateCompleteOrderWithEftRequest
{
}

public class CreateCompleteOrderWithCardRequest
{
    
}

public class CreateCompleteOrderWithOpenAccountRequest
{

}


public class CompleteOrderResponse2
{

}