namespace Vk.Schema;

public class CreateBasketItemRequest
{
    public string BasketNumber { get; set; }
    public string ProductNumber { get; set; }
}

public class BasketItemResponse
{
    public string BasketNumber { get; set; }
    public string CustomerNumber { get; set; }
}