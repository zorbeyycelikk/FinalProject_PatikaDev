using MediatR;
using Vk.Base.Response;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Operation.Cqrs.CompleteOrderWithPaymentMethodsCqrs;
using Vk.Schema;

namespace Vk.Operation.Command.CompleteOrderWithPaymentMethodsCommandHandlers;

public class CompleteOrderWithEftTransferCommandHandler:
     IRequestHandler<CompleteOrderWithEftTransfer, ApiResponse>
     
 {
     private readonly IUnitOfWork unitOfWork;
     private readonly IMediator mediator;
     
     private PaymentByEftTransferCommandHandler Eft;
     private OrderCommandHandler order;
     private checkStockCommand checkStock;
     
     public CompleteOrderWithEftTransferCommandHandler(
         IUnitOfWork unitOfWork,
         IMediator mediator
         )
     {
         this.unitOfWork = unitOfWork;
         this.mediator = mediator;
     }

     public async Task<ApiResponse> Handle(CompleteOrderWithEftTransfer request, CancellationToken cancellationToken)
     { 
         var checkCustomer = await CheckCustomer(request.Model.CustomerId, cancellationToken);

         if (!checkCustomer.Success)
         {
             return new ApiResponse("Error");
         }

         var checkStockResult = await new checkStockCommand(unitOfWork).CheckStock(request.Model.BasketId, cancellationToken);
         
         if (!checkStockResult.Success)
         {
             return new ApiResponse("Error");
         }
         
         // Ödeme İslemi
         CreatePaymentByEftRequest eftReq = new CreatePaymentByEftRequest
         {
             SenderAccountNumber = request.Model.SenderAccountNumber,
             IBAN = request.Model.IBAN,
             Name = request.Model.Name,
             TransferDescription = request.Model.TransferDescription,
             Amount = request.Model.Amount
         };
         
         var resultPayment = await mediator.Send(new CreatePaymentByEftTransferCommand(eftReq));

         var orderRequest = manuelMapping(request.Model.CustomerId, request.Model.Description,
             request.Model.Address, request.Model.PaymentMethod,
             request.Model.Amount, request.Model.BasketId);

         if (resultPayment.Success)
         {
             unitOfWork.BasketRepository.Remove(request.Model.BasketId);
             orderRequest.PaymentRefCode = resultPayment.Response.refNumber;
             await mediator.Send(new CreateOrderCommand(orderRequest));
             await mediator.Send(new UpdateProductStockAfterCreateOrderCommand(request.Model.BasketId));
             return new ApiResponse();
         }
         orderRequest.PaymentRefCode = " ";
         await mediator.Send(new CreateOrderCommand(orderRequest));

         return new ApiResponse("Error");
     }
     
     // Check Customer -> siparisi veren böyle bir kullanici mevcut mu ?
     private async Task<ApiResponse> CheckCustomer(string customerId , CancellationToken cancellationToken)
     {
         var customer = await unitOfWork.CustomerRepository.GetById(customerId, cancellationToken , "Accounts" , "Baskets");
         
         // Siparişi veren böyle bir customer var mı?
         if (customer is null)
         {
            return new ApiResponse("Error");
         }
         return new ApiResponse();
    }
     
     private CreateOrderRequest manuelMapping( string CustomerId , string Description , string Address ,
         string PaymentMethod , decimal Amount , string BasketId )
     {
         CreateOrderRequest request = new CreateOrderRequest();
         request.CustomerId = CustomerId;
         request.Description = Description;
         request.Amount = Amount;
         request.Address = Address;
         request.PaymentMethod = PaymentMethod;
         request.BasketId = BasketId;
         return request;
     }
 }