using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateBasketCommand(CreateBasketRequest Model) : IRequest<ApiResponse>;
public record UpdateBasketCommand(CreateBasketRequest Model,string Id) : IRequest<ApiResponse>;
public record DeleteBasketCommand(string Id) : IRequest<ApiResponse>;

public record GetAllBasketQuery() : IRequest<ApiResponse<List<BasketResponse>>>;
public record GetBasketById(string Id) : IRequest<ApiResponse<BasketResponse>>;