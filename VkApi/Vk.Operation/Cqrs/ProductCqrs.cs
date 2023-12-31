using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateProductCommand(CreateProductRequest Model) : IRequest<ApiResponse>;
public record UpdateProductCommand(UpdateProductRequest Model,string Id) : IRequest<ApiResponse>;
public record DeleteProductCommand(string Id) : IRequest<ApiResponse>;

public record UpdateProductStockAfterCreateOrderCommand(string basketId) : IRequest<ApiResponse>;
public record UpdateProductStockAfterCancelledOrderCommand(string basketId) : IRequest<ApiResponse>;

public record GetAllProductQuery() : IRequest<ApiResponse<List<ProductResponse>>>;
public record GetProductById(string Id) : IRequest<ApiResponse<ProductResponse>>;
public record GetProductByParametersQuery(
    string? Id, string? Name, string? Category, int? minStock,int? maxStock,
    decimal? minPrice, decimal? maxPrice) :
    IRequest<ApiResponse<List<ProductResponse>>>;

public record GetAllUniqueProductCategoryNamesQuery() : IRequest<ApiResponse<List<string>>>;


