using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateBasketItemCommand(CreateBasketItemRequest Model) : IRequest<ApiResponse>;
public record UpdateBasketItemCommand(CreateBasketItemRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteBasketItemCommand(int Id) : IRequest<ApiResponse>;

public record GetAllBasketItemQuery() : IRequest<ApiResponse<List<BasketItemResponse>>>;
public record GetBasketItemById(int Id) : IRequest<ApiResponse<BasketItemResponse>>;