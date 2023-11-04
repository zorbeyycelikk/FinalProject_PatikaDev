namespace Vk.Schema;

public class CreateAccountRequest
{ 
    public string CustomerId { get; set; }
    public string AccountNumber { get; set; }
    public string Name { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
}

public class UpdateAccountRequest
{
    public string Name { get; set; }
}

public class AccountResponse
{
    public string CustomerId { get; set; }
    public string Id { get; set; }
    public string AccountNumber { get; set; }
    public string Name { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
}