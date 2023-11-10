using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;

namespace Vk.Operation.Command;

public class checkStockCommand

{
    
    private readonly IUnitOfWork unitOfWork;
    
    public checkStockCommand(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    // Check Stock -> Sepette olan ürünlerin yeterli stokları var mı?
    public async Task<ApiResponse> CheckStock(string basketId , CancellationToken cancellationToken)
    {
        var x = await CheckBasket(basketId, cancellationToken);

        if (!x.Success)
        {
            return new ApiResponse("Error");
        }
        
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

    // Check Basket -> Bu basketId ' ye sahip bir basket var mı ve bu basket aktif mi ? ( stock içinden çağırılır)
    private async Task<ApiResponse<List<BasketItem>>> CheckBasket(string basketId , CancellationToken cancellationToken)
    {
        var basketItems = await unitOfWork.BasketItemRepository.GetAsQueryable("Product")
            .Where(x => x.Basket.Id == basketId && x.Basket.IsActive == true).ToListAsync(cancellationToken);

        // Böyle bir basket var mı ve bu basket aktif mi ?
        if (!basketItems.Any())
        {
            return new ApiResponse<List<BasketItem>>("Error");
        }
        return new ApiResponse<List<BasketItem>>(basketItems);
    }
     
 }