using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreatePaymentCardTransferCommand(CreatePaymentByCardRequest Model) : IRequest<ApiResponse<PaymentByCardResponse>>;
