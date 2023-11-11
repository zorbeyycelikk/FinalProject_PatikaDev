using LinqKit;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

using AutoMapper;
using MediatR;

public class CardTransactionQueryHandler :
    IRequestHandler<GetAllCardTransactionQuery, ApiResponse<List<CardTransactionResponse>>>,
    IRequestHandler<GetCardTransactionById, ApiResponse<CardTransactionResponse>>,
IRequestHandler<GetCardTransactionByParametersQuery, ApiResponse<List<CardTransactionResponse>>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public CardTransactionQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<CardTransactionResponse>>> Handle(GetAllCardTransactionQuery request, CancellationToken cancellationToken)
    {
        List<CardTransaction> x =  await unitOfWork.CardTransactionRepository.GetAll(cancellationToken, "Card");
        List<CardTransactionResponse> response = mapper.Map<List<CardTransactionResponse>>(x);
        return new ApiResponse<List<CardTransactionResponse>>(response);
    }

    public async Task<ApiResponse<CardTransactionResponse>> Handle(GetCardTransactionById request, CancellationToken cancellationToken)
    {
        CardTransaction x = await unitOfWork.CardTransactionRepository.GetById(request.Id, cancellationToken , "Card");
        
        if (x is null)
        {
            return new ApiResponse<CardTransactionResponse>("Error" , false);
        }
        
        CardTransactionResponse response = mapper.Map<CardTransactionResponse>(x);
        return new ApiResponse<CardTransactionResponse>(response);
    }

    public async Task<ApiResponse<List<CardTransactionResponse>>> Handle(GetCardTransactionByParametersQuery request, CancellationToken cancellationToken)
    {            
        var predicate = PredicateBuilder.New<CardTransaction>(true);
        
        if (!string.IsNullOrWhiteSpace(request.transactionRefNumber))
            predicate.And(x => x.transactionRefNumber == request.transactionRefNumber);
        if (!string.IsNullOrWhiteSpace(request.CardId))
            predicate.And(x => x.CardId == request.CardId);
        if (!string.IsNullOrWhiteSpace(request.receiverAccountNumber))
            predicate.And(x => x.receiverAccountNumber == request.receiverAccountNumber);
        if (!string.IsNullOrWhiteSpace(request.CardNumber))
            predicate.And(x => x.CardNumber == request.CardNumber);
        if (!string.IsNullOrWhiteSpace(request.Status))
            predicate.And(x => x.Status == request.Status);
        if (request.minAmount > 0)
            predicate.And(x => x.Amount >= request.minAmount);
        if (request.maxAmount > 0)
            predicate.And(x => x.Amount <= request.maxAmount);
       
        List<CardTransaction> cardtransactions = await unitOfWork.CardTransactionRepository.GetAsQueryable("Card")
            .Where(predicate).ToListAsync(cancellationToken);
        
        if (!cardtransactions.Any())
        {
            return new ApiResponse<List<CardTransactionResponse>>("Error");
        }
        var mapped = mapper.Map<List<CardTransactionResponse>>(cardtransactions);
        return new ApiResponse<List<CardTransactionResponse>>(mapped);
    }
}