using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateProductCommand(CreateProductRequest Model) : IRequest<ApiResponse>;
public record UpdateProductCommand(UpdateProductRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteProductCommand(int Id) : IRequest<ApiResponse>;

public record GetAllProductQuery() : IRequest<ApiResponse<List<ProductResponse>>>;
public record GetProductById(int Id) : IRequest<ApiResponse<ProductResponse>>;