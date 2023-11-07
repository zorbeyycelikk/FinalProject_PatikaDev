namespace Vk.Schema;

public class CreateBasketRequest
{
    public string CustomerId { get; set; }
}

public class BasketResponse
{
    public string Id { get; set; }
    public string CustomerId { get; set; }
    public bool IsActive { get; set; }
}