 using System.IdentityModel.Tokens.Jwt;
 using System.Security.Claims;
 using System.Text;
 using MediatR;
 using Microsoft.EntityFrameworkCore;
 using Microsoft.Extensions.Options;
 using Microsoft.IdentityModel.Tokens;
 using Vk.Base.Encryption;
 using Vk.Base.Response;
 using Vk.Base.Token;
 using Vk.Data.Context;
using Vk.Data.Domain;
 using Vk.Data.Uow;
 using Vk.Schema;

namespace Vk.Operation.Command;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly JwtConfig jwtConfig;
    public TokenCommandHandler(IOptionsMonitor<JwtConfig> jwtConfig , IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.jwtConfig = jwtConfig.CurrentValue;
    }
    
    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerRepository.GetAsQueryable()
            .FirstOrDefaultAsync(x => x.Email == request.Model.Email, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<TokenResponse>("Error" , false);
        }
        
        var md5 = Md5.Create(request.Model.Password.ToUpper());
        if (entity.Password != md5)
        {
            return new ApiResponse<TokenResponse>( "Error" , false);
        }
        
        if (entity.IsActive == false)
        {
            return new ApiResponse<TokenResponse>("Error" , false);
        }
        
        string token = Token(entity);
        TokenResponse tokenResponse = new()
        {
            Token = token,
            ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            CustomerId= entity.Id,
            Role = entity.Role,
            Email = entity.Email
        };
        
        return new ApiResponse<TokenResponse>(tokenResponse); 
    }
    // Token olusturan hazır kod
    private string Token(Customer user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }
    
    private Claim[] GetClaims(Customer customer)
    {
        var claims = new[]
        {
            new Claim("Id", customer.Id),
            new Claim("Role", customer.Role),
            new Claim("Email", customer.Email),
            new Claim("Name", customer.Name),
            new Claim("Profit", customer.Profit.ToString()),
            new Claim(ClaimTypes.Role, customer.Role)
        };
        return claims;
    }
}