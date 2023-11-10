using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs.CompleteOrderWithPaymentMethodsCqrs;

public record CompleteOrderWithEftTransfer(CreateCompleteOrderWithEftRequest Model) : IRequest<ApiResponse>;