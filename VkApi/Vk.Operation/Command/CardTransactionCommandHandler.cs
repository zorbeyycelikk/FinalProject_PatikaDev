using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Encryption;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class CardTransactionCommandHandler:
    IRequestHandler<CreateCardTransactionCommand, ApiResponse>,
    IRequestHandler<UpdateCardTransactionCommand, ApiResponse>,
    IRequestHandler<DeleteCardTransactionCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public CardTransactionCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateCardTransactionCommand request, CancellationToken cancellationToken)
    {
        
        CardTransaction mapped = mapper.Map<CardTransaction>(request.Model);
        
        mapped.Id = mapped.MakeId(mapped.Id);
        mapped.transactionRefNumber = mapped.MakeId(mapped.transactionRefNumber);
        mapped.Status = "Bekleniyor";
        
        unitOfWork.CardTransactionRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateCardTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CardTransactionRepository.GetById(request.Id, cancellationToken);
        
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        
        entity.Status = request.Model.Status;
        
        unitOfWork.CardTransactionRepository.Update(entity);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCardTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CardTransactionRepository.GetById(request.Id, cancellationToken);
        
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.CardTransactionRepository.Remove(request.Id);
        unitOfWork.Save();
        
        return new ApiResponse();
    }
}