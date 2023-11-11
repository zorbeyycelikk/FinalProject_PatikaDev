using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;


public record CreateProductReport(CreateProductRequest Model) : IRequest<ApiResponse<ProductResponse>>;
