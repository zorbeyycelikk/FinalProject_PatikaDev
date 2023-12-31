using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;
using AutoMapper;
using FluentValidation.Validators;
using LinqKit;
using MediatR;

namespace Vk.Operation.Query;

public class ProductQueryHandler :
    IRequestHandler<GetAllProductQuery, ApiResponse<List<ProductResponse>>>,
    IRequestHandler<GetProductById, ApiResponse<ProductResponse>>,
    IRequestHandler<GetAllUniqueProductCategoryNamesQuery, ApiResponse<List<string>>>,
    IRequestHandler<GetProductByParametersQuery, ApiResponse<List<ProductResponse>>>

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
            return new ApiResponse<ProductResponse>("Error" , false);
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

    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetProductByParametersQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Product>(true);
        if (!string.IsNullOrWhiteSpace(request.Id))
            predicate.And(x => x.Id == request.Id);
        if (!string.IsNullOrWhiteSpace(request.Name))
            predicate.And(x => x.Name.Contains(request.Name));
        if (!string.IsNullOrWhiteSpace(request.Category))
            predicate.And(x => x.Category == request.Category);
        if (request.minStock > 0)
            predicate.And(x => x.Stock >= request.minStock);
        if (request.maxStock > 0)
            predicate.And(x => x.Stock <= request.maxStock);
        if (request.minPrice > 0)
            predicate.And(x => x.Price >= request.minPrice);
        if (request.maxPrice > 0)
            predicate.And(x => x.Price <= request.maxPrice);
        
        List<Product> products = await unitOfWork.ProductRepository.GetAsQueryable()
            .Where(predicate).ToListAsync(cancellationToken);

        if (!products.Any())
        {
            return new ApiResponse<List<ProductResponse>>("Error");
        }
        
        var mapped = mapper.Map<List<ProductResponse>>(products);
        return new ApiResponse<List<ProductResponse>>(mapped);
    }
}