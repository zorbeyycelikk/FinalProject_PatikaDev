using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateBasketItemCommand(CreateBasketItemRequest Model) : IRequest<ApiResponse>;
public record UpdateBasketItemCommand(CreateBasketItemRequest Model,string Id) : IRequest<ApiResponse>;
public record DeleteBasketItemCommand(string Id) : IRequest<ApiResponse>;
public record HardDeleteBasketItemCommand(string Id) : IRequest<ApiResponse>;
public record HardDeleteBasketItemByProductNumberCommand(string Id) : IRequest<ApiResponse>;


public record GetAllBasketItemQuery() : IRequest<ApiResponse<List<BasketItemResponse>>>;
public record GetBasketItemById(string Id) : IRequest<ApiResponse<BasketItemResponse>>;
public record GetBasketItemByParametersQuery(int? minQuantity,int? maxQuantity  ): IRequest<ApiResponse<List<BasketItemResponse>>>;

