namespace Vk.Schema;

public class CreateCustomerRequest
{ 
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Role  { get; set; } // admin or bayi  | default value bayi
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
    public string Id  { get; set; }
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Role  { get; set; } // admin or bayi  | default value bayi
    public float  Profit { get; set; }
    public bool IsActive { get; set; }


}