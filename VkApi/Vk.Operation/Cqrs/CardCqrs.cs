using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateCardCommand(CreateCardRequest Model) : IRequest<ApiResponse>;
public record UpdateCardCommand(UpdateCardRequest Model,string Id) : IRequest<ApiResponse>;
public record DeleteCardCommand(string Id) : IRequest<ApiResponse>;

public record GetAllCardQuery() : IRequest<ApiResponse<List<CardResponse>>>;
public record GetCardById(string Id) : IRequest<ApiResponse<CardResponse>>;
public record GetCardByAccountNumber(string Id) : IRequest<ApiResponse<List<CardResponse>>>;