namespace Vk.Schema;

public class CreateBasketItemRequest
{
    public int Quantity { get; set; }
    public string BasketId { get; set; }
    public string ProductId { get; set; }
}

public class BasketItemResponse
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public int    Quantity { get; set; }

}