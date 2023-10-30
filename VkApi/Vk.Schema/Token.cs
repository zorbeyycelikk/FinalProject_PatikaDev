namespace Vk.Schema;

public class TokenRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class TokenResponse
{
    public DateTime ExpireDate { get; set; }
    public string Token { get; set; }
    public string CustomerNumber { get; set; }
    public string Email { get; set; }
}