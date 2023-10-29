using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateCustomerCommand(CustomerCreateRequest Model) : IRequest<ApiResponse>;
public record UpdateCustomerCommand(CustomerUpdateRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteCustomerCommand(string Id) : IRequest<ApiResponse>;

public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomerById(string Id) : IRequest<ApiResponse<CustomerResponse>>;


// IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method , bool tracking = true); //

// Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method , bool tracking = true); //

// Task<bool> AddRangeAsync(List<TEntity> entities);

// bool RemoveRange(List<TEntity> entities);
