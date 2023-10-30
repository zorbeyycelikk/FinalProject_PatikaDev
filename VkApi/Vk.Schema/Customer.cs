namespace Vk.Schema;

public class CreateCustomerRequest
{ 
    public string CustomerNumber { get; set; }
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public float  Profit { get; set; }
}

public class UpdateCustomerRequest
{
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public float  Profit { get; set; }
}

public class CustomerResponse
{
    public string CustomerNumber { get; set; }
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public float  Profit { get; set; }

}