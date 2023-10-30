using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class CardCommandHandler:
    IRequestHandler<CreateCardCommand, ApiResponse>,
    IRequestHandler<UpdateCardCommand, ApiResponse>,
    IRequestHandler<DeleteCardCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public CardCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CardRepository.GetAsQueryable()
           .SingleOrDefaultAsync(x => x.CardNumber == request.Model.CardNumber ,cancellationToken);
        if (entity is not null)
        {
            return new ApiResponse("Error");
        }
        Card mapped = mapper.Map<Card>(request.Model);
        
        unitOfWork.CardRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CardRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }

        entity.ExpiryDate = request.Model.ExpiryDate;
        
        unitOfWork.CardRepository.Update(entity);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CardRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.CardRepository.Remove(request.Id);
        unitOfWork.Save();
        
        return new ApiResponse();
    }
}