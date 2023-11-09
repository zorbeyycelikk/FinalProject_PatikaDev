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
    IRequestHandler<GetCardTransactionById, ApiResponse<CardTransactionResponse>>
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
}