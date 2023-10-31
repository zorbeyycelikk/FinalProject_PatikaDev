using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;

namespace Vk.Operation.Command;

public class BasketItemCommandHandler:
    IRequestHandler<CreateBasketItemCommand, ApiResponse>,
    IRequestHandler<DeleteBasketItemCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public BasketItemCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
    {
        BasketItem mapped = mapper.Map<BasketItem>(request.Model);
        
        unitOfWork.BasketItemRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        
        return new ApiResponse();
    }


    public async Task<ApiResponse> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.BasketItemRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.BasketItemRepository.Remove(request.Id);
        unitOfWork.Save();
        return new ApiResponse();
    }
}