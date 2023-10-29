namespace Vk.Schema;

public class CustomerCreateRequest
{
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}


public class CustomerUpdateRequest{
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}


public class CustomerResponse
{
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}