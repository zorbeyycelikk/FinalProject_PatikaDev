using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateAccountCommand(CreateAccountRequest Model) : IRequest<ApiResponse>;
public record UpdateAccountCommand(UpdateAccountRequest Model,string Id) : IRequest<ApiResponse>;
public record DeleteAccountCommand(string Id) : IRequest<ApiResponse>;

public record GetAllAccountQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
public record GetAccountById(string Id) : IRequest<ApiResponse<AccountResponse>>;

public record GetAccountByParametersQuery(
    string? Name,string? AccountNumber,string? IBAN,
    int? minBalance,int? maxBalance  )
    : IRequest<ApiResponse<List<AccountResponse>>>;