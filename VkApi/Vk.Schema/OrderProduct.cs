namespace Vk.Schema;

public class CreateOrderProductRequest
{
    public string OrderNumber { get; set; }
    public string ProductNumber { get; set; }
}

public class OrderProductResponse
{
    public string OrderNumber { get; set; }
    public string ProductNumber { get; set; }
    
    public string CustomerNumberWhoCreateOrder { get; set; }
    public string ProductNameWhichCreatedInOrder { get; set; }

}