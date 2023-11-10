namespace Vk.Schema;

public class CreateProductRequest
{
    public string Name           { get; set; }
    public string Category       { get; set; }
    public int    Stock          { get; set; }
    public decimal  Price          { get; set; }
    public string imgUrl       { get; set; }

}

public class UpdateProductRequest
{
    public string Name           { get; set; }
    public string Category       { get; set; }
    public int    Stock          { get; set; }
    public decimal  Price          { get; set; }
    public string imgUrl       { get; set; }
}

public class ProductResponse
{
    public string Id             { get; set; }
    public string Name           { get; set; }
    public string Category       { get; set; }
    public int    Stock          { get; set; }
    public decimal  Price          { get; set; }
    public string imgUrl         { get; set; }
    public bool IsActive         { get; set; }
}
