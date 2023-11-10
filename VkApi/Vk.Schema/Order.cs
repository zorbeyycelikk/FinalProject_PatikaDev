namespace Vk.Schema;

public class CreateOrderRequest
{
    public string   CustomerId       { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   PaymentMethod    { get; set; }
    public string   PaymentRefCode   { get; set; }
    public decimal   Amount          { get; set; }
    public string BasketId { get; set; }
}

public class UpdateOrderRequest
{
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   Status           { get; set; }
}

public class OrderResponse
{
    public string   CustomerId      { get; set; }
    public string   OrderNumber      { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   PaymentMethod    { get; set; }
    public string   PaymentRefCode   { get; set; }
    public decimal   Amount          { get; set; }
    public string   Status           { get; set; }
    
    public string BasketId { get; set; }  
    public List<BasketItemResponse>   BasketItems     { get; set; }
}