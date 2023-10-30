namespace Vk.Schema;

public class CreateProductRequest
{
    public string ProductNumber  { get; set; }
    public string Name           { get; set; }
    public string Category       { get; set; }
    public int    Stock          { get; set; }
    public float  Price          { get; set; }
}

public class UpdateProductRequest
{
    public string Name           { get; set; }
    public string Category       { get; set; }
    public int    Stock          { get; set; }
    public float  Price          { get; set; }
}

public class ProductResponse
{
    public string ProductNumber  { get; set; }
    public string Name           { get; set; }
    public string Category       { get; set; }
    public int    Stock          { get; set; }
    public float  Price          { get; set; }
}
