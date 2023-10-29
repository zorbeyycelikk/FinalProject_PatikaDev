namespace Vk.Schema;

public class CreateOrderRequest
{
    public string   OrderNumber      { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public int      CustomerId       { get; set; }
}

public class UpdateOrderRequest
{
    public string   Description      { get; set; }
    public string   Address          { get; set; }
}

public class OrderResponse
{
    public string   OrderNumber      { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public int      CustomerId       { get; set; }
    public string   CustomerName     { get; set; }
}