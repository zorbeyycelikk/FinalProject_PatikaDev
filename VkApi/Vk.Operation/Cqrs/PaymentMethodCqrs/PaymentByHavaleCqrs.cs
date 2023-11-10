using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreatePaymentByHavaleTransferCommand(CreatePaymentByHavaleRequest Model) : IRequest<ApiResponse<PaymentByHavaleResponse>>;
