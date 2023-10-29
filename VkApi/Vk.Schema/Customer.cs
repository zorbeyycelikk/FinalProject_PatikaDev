namespace Vk.Schema;

public class CreateCustomerRequest
{
    public int   CustomerNumber { get; set; }
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public class UpdateCustomerRequest
{
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public class CustomerResponse
{
    public int   CustomerNumber { get; set; }
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}