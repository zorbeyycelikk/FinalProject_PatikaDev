using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs.CompleteOrderWithPaymentMethodsCqrs;

public record CompleteOrderWithCardTransfer(CreateCompleteOrderWithCardRequest Model) : IRequest<ApiResponse>;