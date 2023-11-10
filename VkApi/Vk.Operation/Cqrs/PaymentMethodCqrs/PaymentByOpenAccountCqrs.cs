using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreatePaymentOpenAccountTransferCommand(CreatePaymentByOpenAccountRequest Model) : IRequest<ApiResponse<PaymentByOpenAccountResponse>>;
