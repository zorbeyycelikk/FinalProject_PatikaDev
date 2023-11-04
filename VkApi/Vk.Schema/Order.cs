namespace Vk.Schema;

public class CreateOrderRequest
{
    public string   OrderNumber      { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   Status           { get; set; }
    public string   BasketId     { get; set; }
}

public class UpdateOrderRequest
{
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   Status           { get; set; }
}

public class OrderResponse
{
    public string   OrderNumber      { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   Status           { get; set; }
    public string   BasketId         { get; set; }
    public string   CustomerId       { get; set; } // MAPTEN GELECEK

    public List<BasketItemResponse>   BasketItems     { get; set; }
}