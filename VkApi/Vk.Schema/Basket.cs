namespace Vk.Schema;

public class CreateBasketRequest
{
    public string CustomerId { get; set; }
}

public class BasketResponse
{
    public string CustomerId { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<BasketItemResponse> BasketItems { get; set; }
}