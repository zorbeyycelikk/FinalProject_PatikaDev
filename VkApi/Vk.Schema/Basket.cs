namespace Vk.Schema;

public class CreateBasketRequest
{
    public string BasketNumber { get; set; }
    public string CustomerNumber { get; set; }
}

public class BasketResponse
{
    public string BasketNumber { get; set; }
    public string CustomerNumber { get; set; }
    public virtual ICollection<BasketItemResponse> BasketItems { get; set; }
}