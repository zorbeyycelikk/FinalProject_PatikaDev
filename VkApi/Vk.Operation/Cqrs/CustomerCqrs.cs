using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateCustomerCommand(CreateCustomerRequest Model) : IRequest<ApiResponse>;
public record UpdateCustomerCommand(UpdateCustomerRequest Model,string Id) : IRequest<ApiResponse>;
public record DeleteCustomerCommand(string Id) : IRequest<ApiResponse>;

public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomerById(string Id) : IRequest<ApiResponse<CustomerResponse>>;
public record GetCustomerByParametersQuery( 
    string? Id, string? Name, string? Email, string? Phone,
    string? Role, decimal? minProfit, decimal? maxProfit, 
    decimal? minopenAccountLimit,decimal? maxopenAccountLimit) :
    IRequest<ApiResponse<List<CustomerResponse>>>;
