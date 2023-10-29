using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateCustomerCommand(CreateCustomerRequest Model) : IRequest<ApiResponse>;
public record UpdateCustomerCommand(UpdateCustomerRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteCustomerCommand(int Id) : IRequest<ApiResponse>;

public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomerById(int Id) : IRequest<ApiResponse<CustomerResponse>>;