using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateCardTransactionCommand(CreateCardTransactionRequest Model) : IRequest<ApiResponse>;
public record UpdateCardTransactionCommand(UpdateCardTransactionRequest Model,string Id) : IRequest<ApiResponse>;
public record DeleteCardTransactionCommand(string Id) : IRequest<ApiResponse>;

public record GetAllCardTransactionQuery() : IRequest<ApiResponse<List<CardTransactionResponse>>>;
public record GetCardTransactionById(string Id) : IRequest<ApiResponse<CardTransactionResponse>>;

public record GetCardTransactionByParametersQuery(
    string? transactionRefNumber, string? CardId, string? receiverAccountNumber,
    string? CardNumber, string? Status , decimal? minAmount, decimal? maxAmount) :
    IRequest<ApiResponse<List<CardTransactionResponse>>>;