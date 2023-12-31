using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;

namespace Vk.Operation.Command;

public class ProductCommandHandler:
    IRequestHandler<CreateProductCommand, ApiResponse>,
    IRequestHandler<UpdateProductCommand, ApiResponse>,
    IRequestHandler<UpdateProductStockAfterCreateOrderCommand, ApiResponse>,
    IRequestHandler<UpdateProductStockAfterCancelledOrderCommand, ApiResponse>,
    IRequestHandler<DeleteProductCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public ProductCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product mapped = mapper.Map<Product>(request.Model);
        mapped.Id = mapped.MakeId(mapped.Id);

        unitOfWork.ProductRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.ProductRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }

        entity.Name = request.Model.Name;
        entity.Price = request.Model.Price;
        entity.Stock = request.Model.Stock;
        entity.Category = request.Model.Category;
        
        unitOfWork.ProductRepository.Update(entity);
        unitOfWork.Save();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.ProductRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.ProductRepository.Remove(request.Id);
        unitOfWork.Save();
        
        return new ApiResponse();
    }


    public async Task<ApiResponse> Handle(UpdateProductStockAfterCreateOrderCommand request, CancellationToken cancellationToken)
    {
        var basketItemList = await unitOfWork.BasketItemRepository.GetAsQueryable("Basket" , "Product")
            .Where(x => x.Basket.Id == request.basketId).ToListAsync(cancellationToken);

        if (!basketItemList.Any())
        {
            return new ApiResponse("Error");
        }

        for (int i = 0; i < basketItemList.Count; i++)
        {
            basketItemList[i].Product.Stock -= basketItemList[i].Quantity;
            unitOfWork.ProductRepository.Update(basketItemList[i].Product);
        }
        unitOfWork.Save();
        return new ApiResponse();
    }
    
    public async Task<ApiResponse> Handle(UpdateProductStockAfterCancelledOrderCommand request, CancellationToken cancellationToken)
    {
        var basketItemList = await unitOfWork.BasketItemRepository.GetAsQueryable("Basket" , "Product")
            .Where(x => x.Basket.Id == request.basketId).ToListAsync(cancellationToken);

        if (!basketItemList.Any())
        {
            return new ApiResponse("Error");
        }

        for (int i = 0; i < basketItemList.Count; i++)
        {
            basketItemList[i].Product.Stock += basketItemList[i].Quantity;
            unitOfWork.ProductRepository.Update(basketItemList[i].Product);
        }
        unitOfWork.Save();
        return new ApiResponse();
    }
}