using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateBasketCommand(CreateBasketRequest Model) : IRequest<ApiResponse>;
public record UpdateBasketCommand(CreateBasketRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteBasketCommand(int Id) : IRequest<ApiResponse>;

public record GetAllBasketQuery() : IRequest<ApiResponse<List<BasketResponse>>>;
public record GetBasketById(int Id) : IRequest<ApiResponse<BasketResponse>>;