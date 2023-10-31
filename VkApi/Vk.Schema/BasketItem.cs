namespace Vk.Schema;

public class CreateBasketItemRequest
{
    public string BasketNumber { get; set; }
    public string ProductNumber { get; set; }
}

public class BasketItemResponse
{
    public string CustomerNumber { get; set; }
    public string ProductNumber { get; set; }
    public string ProductName { get; set; }
}