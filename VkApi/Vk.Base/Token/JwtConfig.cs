namespace Vk.Base.Token;

public class JwtConfig
{
    //appsetting.json'da yaptıgımız ayarlara karşılık geliyor.Otomatik serizilation yapacağız.
    //Hard code yazmak yerine bu daha makul.
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpiration { get; set; }
}