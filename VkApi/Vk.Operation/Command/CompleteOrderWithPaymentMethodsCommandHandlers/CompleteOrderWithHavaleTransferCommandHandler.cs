using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Operation.Cqrs.CompleteOrderWithPaymentMethodsCqrs;
using Vk.Schema;

namespace Vk.Operation.Command.CompleteOrderWithPaymentMethodsCommandHandlers;

public class CompleteOrderWithHavaleTransferCommandHandler:
     IRequestHandler<CompleteOrderWithHavaleTransfer, ApiResponse>
     
 {
     private readonly IMapper mapper;
     private readonly IUnitOfWork unitOfWork;
     private readonly IMediator mediator;
     
     private PaymentByHavaleTransferCommandHandler havale;
     private OrderCommandHandler order;
     
     public CompleteOrderWithHavaleTransferCommandHandler(
         IMapper mapper,IUnitOfWork unitOfWork,
         IMediator mediator
         )
     {
         
         this.unitOfWork = unitOfWork;
         this.mapper = mapper;
         this.mediator = mediator;
     }

     public async Task<ApiResponse> Handle(CompleteOrderWithHavaleTransfer request, CancellationToken cancellationToken)
     { 
         var checkCustomer = await CheckCustomer(request.Model.CustomerId, cancellationToken);

         if (!checkCustomer.Success)
         {
             return new ApiResponse("Error");
         }

         var checkStock = await CheckStock(request.Model.BasketId, cancellationToken);

         if (!checkStock.Success)
         {
             return new ApiResponse("Error");
         }
           
         // Ödeme İslemi
         CreatePaymentByHavaleRequest havReq = new CreatePaymentByHavaleRequest
         {
             SenderAccountNumber = request.Model.SenderAccountNumber,
             AccountNumber = request.Model.AccountNumber,
             Name = request.Model.Name,
             Description = request.Model.Description,
             Amount = request.Model.Amount
         };
         
         
         var operationPayment = new CreatePaymentByHavaleTransferCommand(havReq);
         var resultPayment = await mediator.Send(operationPayment);
         
         CreateOrderRequest orderRequest = manuelMapping(request.Model.CustomerId, request.Model.Description,
             request.Model.Address, request.Model.PaymentMethod, request.Model.Amount, request.Model.BasketId
         );
         
         orderRequest.PaymentRefCode = " ";
         var operationOrder = new CreateOrderCommand(orderRequest);
         // Ödeme basarisiz ise ;
         if (!resultPayment.Success)
         {
             var resultOrderUnSuccess = await mediator.Send(operationOrder);
             return new ApiResponse("Error");
         }

         orderRequest.PaymentRefCode = resultPayment.Response.refNumber;
         operationOrder = new CreateOrderCommand(orderRequest);
         var resultOrder = await mediator.Send(operationOrder);
         return new ApiResponse();
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
     
     // Check Basket -> Bu basketId ' ye sahip bir basket var mı ve bu basket aktif mi ? ( stock içinden çağırılır)
     private async Task<ApiResponse<List<BasketItem>>> CheckBasket(string basketId , CancellationToken cancellationToken)
     {
         var basketItems = await unitOfWork.BasketItemRepository.GetAsQueryable("Product")
             .Where(x => x.Basket.Id == basketId && x.Basket.IsActive == true).ToListAsync(cancellationToken);

         // Böyle bir basket var mı ve bu basket aktif mi ?
         if (basketItems is null)
         {
             return new ApiResponse<List<BasketItem>>("Error" , false);
         }
         return new ApiResponse<List<BasketItem>>(basketItems);
     }
     
     // Check Stock -> Sepette olan ürünlerin yeterli stokları var mı?
     private async Task<ApiResponse> CheckStock(string basketId , CancellationToken cancellationToken)
     {
         var x = await CheckBasket(basketId, cancellationToken);

         var basketItems = x.Response;
         
         //Basket'de bulunan ürünlerin yeterli stokları var mı ?
         
         for (int i = 0; i < basketItems.Count; i++)
         {
             if (basketItems[i].Product.Stock < basketItems[i].Quantity)
             {
                 return new ApiResponse("Yetersiz Stock Miktari");
             }
         }
         return new ApiResponse();
     }
     
     private async Task<ApiResponse> CreateOrder(CreateOrderRequest request , CancellationToken cancellationToken)
     {
         // Artık sipariş oluşturabiliriz
         var refNumber = Guid.NewGuid().ToString().Replace("-", "").ToLower();
         Order order = new Order();
         order.Id = order.MakeId("");
         order.CustomerId = request.CustomerId;
         order.BasketId = request.BasketId;
         order.OrderNumber = refNumber;
         order.Description = request.Description;
         order.Address = request.Address;
         order.PaymentMethod = request.PaymentMethod;
         order.Amount = request.Amount;
         order.Status = "Odeme Basarili";
         order.InsertDate = DateTime.UtcNow;
         await unitOfWork.OrderRepository.AddAsync(order,cancellationToken);
         unitOfWork.Save();
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