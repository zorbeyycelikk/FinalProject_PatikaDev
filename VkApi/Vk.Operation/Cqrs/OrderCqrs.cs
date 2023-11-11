using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateOrderCommand(CreateOrderRequest Model) : IRequest<ApiResponse>;
public record UpdateOrderCommand(UpdateOrderRequest Model,string Id) : IRequest<ApiResponse>;
public record DeleteOrderCommand(string Id) : IRequest<ApiResponse>;

public record ConfirmWithOrderNumberCommand(string orderNumber) : IRequest<ApiResponse>;
public record ConfirmWithIdCommand(string id) : IRequest<ApiResponse>;
public record CancelledWithOrderNumberCommand(string orderNumber) : IRequest<ApiResponse>;

public record GetAllOrderQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
public record GetOrderById(string Id) : IRequest<ApiResponse<OrderResponse>>;
public record GetOrderByParametersQuery( string? Id,
    string? CustomerId, string? OrderNumber, string? Description,
    string? Address ,string? PaymentMethod, string? PaymentRefCode ,
    decimal? minAmount, decimal? maxAmount ,string? Status ) :
    IRequest<ApiResponse<List<OrderResponse>>>;