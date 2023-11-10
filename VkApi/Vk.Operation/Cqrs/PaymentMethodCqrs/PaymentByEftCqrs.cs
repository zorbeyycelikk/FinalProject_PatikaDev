using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreatePaymentByEftTransferCommand(CreatePaymentByEftRequest Model) : IRequest<ApiResponse<PaymentByEftResponse>>;
