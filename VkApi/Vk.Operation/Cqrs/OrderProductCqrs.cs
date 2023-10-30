using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateOrderProductCommand(CreateOrderProductRequest Model) : IRequest<ApiResponse>;
public record DeleteOrderProductCommand(int Id) : IRequest<ApiResponse>;

public record GetAllOrderProductQuery() : IRequest<ApiResponse<List<OrderProductResponse>>>;
public record GetOrderProductById(int Id) : IRequest<ApiResponse<OrderProductResponse>>;