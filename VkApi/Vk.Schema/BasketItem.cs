namespace Vk.Schema;

public class CreateBasketItemRequest
{
    public string BasketId { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }

}

public class BasketItemResponse
{
    public string BasketId { get; set; }
    public int    Quantity { get; set; }
    public ProductResponse Product { get; set; }

}