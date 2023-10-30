using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateAccountCommand(CreateAccountRequest Model) : IRequest<ApiResponse>;
public record UpdateAccountCommand(UpdateAccountRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteAccountCommand(int Id) : IRequest<ApiResponse>;

public record GetAllAccountQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
public record GetAccountById(int Id) : IRequest<ApiResponse<AccountResponse>>;