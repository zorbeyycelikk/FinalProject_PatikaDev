using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateCardCommand(CreateCardRequest Model) : IRequest<ApiResponse>;
public record UpdateCardCommand(UpdateCardRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteCardCommand(int Id) : IRequest<ApiResponse>;

public record GetAllCardQuery() : IRequest<ApiResponse<List<CardResponse>>>;
public record GetCardById(int Id) : IRequest<ApiResponse<CardResponse>>;