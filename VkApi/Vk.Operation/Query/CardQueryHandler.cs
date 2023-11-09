using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

using AutoMapper;
using MediatR;

public class CardQueryHandler :
    IRequestHandler<GetAllCardQuery, ApiResponse<List<CardResponse>>>,
    IRequestHandler<GetCardById, ApiResponse<CardResponse>>,
    IRequestHandler<GetCardByAccountNumber, ApiResponse<List<CardResponse>>>

{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public CardQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<CardResponse>>> Handle(GetAllCardQuery request, CancellationToken cancellationToken)
    {
        List<Card> x =  await unitOfWork.CardRepository.GetAll(cancellationToken,"Account");
        List<CardResponse> response = mapper.Map<List<CardResponse>>(x);
        return new ApiResponse<List<CardResponse>>(response);
    }

    public async Task<ApiResponse<CardResponse>> Handle(GetCardById request, CancellationToken cancellationToken)
    {
        Card x = await unitOfWork.CardRepository.GetById(request.Id, cancellationToken,"Account");
        
        if (x is null)
        {
            return new ApiResponse<CardResponse>("Error" , false);
        }
        
        CardResponse response = mapper.Map<CardResponse>(x);
        return new ApiResponse<CardResponse>(response);
    }
    
    
    public async Task<ApiResponse<List<CardResponse>>> Handle(GetCardByAccountNumber request, CancellationToken cancellationToken)
    {
        List<Card> x = await unitOfWork.CardRepository.GetAsQueryable("Account")
            .Where(x => x.AccountId == request.Id).ToListAsync(cancellationToken);
        if (x is null)
        {
            return new ApiResponse<List<CardResponse>>("Error" , false);
        }
        List<CardResponse> response = mapper.Map<List<CardResponse>>(x);
        return new ApiResponse<List<CardResponse>>(response);
    }
}