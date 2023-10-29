using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateOrderCommand(CreateOrderRequest Model) : IRequest<ApiResponse>;
public record UpdateOrderCommand(UpdateOrderRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteOrderCommand(int Id) : IRequest<ApiResponse>;

public record GetAllOrderQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
public record GetOrderById(int Id) : IRequest<ApiResponse<OrderResponse>>;