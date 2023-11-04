using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

using AutoMapper;
using MediatR;

public class ProductQueryHandler :
    IRequestHandler<GetAllProductQuery, ApiResponse<List<ProductResponse>>>,
    IRequestHandler<GetProductById, ApiResponse<ProductResponse>>,
    IRequestHandler<GetAllUniqueProductCategoryNamesQuery, ApiResponse<List<string>>>

{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public ProductQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        List<Product> x =  await unitOfWork.ProductRepository.GetAll(cancellationToken);
        List<ProductResponse> response = mapper.Map<List<ProductResponse>>(x);
        return new ApiResponse<List<ProductResponse>>(response);
    }

    public async Task<ApiResponse<ProductResponse>> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        Product x = await unitOfWork.ProductRepository.GetById(request.Id, cancellationToken );
        
        if (x is null)
        {
            return new ApiResponse<ProductResponse>("Error");
        }
        
        ProductResponse response = mapper.Map<ProductResponse>(x);
        return new ApiResponse<ProductResponse>(response);
    }

    public async Task<ApiResponse<List<string>>> Handle(GetAllUniqueProductCategoryNamesQuery request, CancellationToken cancellationToken)
    {
        var uniqueCategoryNames = await unitOfWork.ProductRepository.GetAsQueryable()
            .Select(p => p.Category).Distinct().ToListAsync(cancellationToken);

        return new ApiResponse<List<string>>(uniqueCategoryNames);
    }
}