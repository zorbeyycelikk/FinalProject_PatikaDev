import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


const AUTH_API = 'http://localhost:5087/vk/';

const httpOptions = {
  headers : new HttpHeaders({ 'Content-Type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class BasketItemService {

  constructor(private http : HttpClient) { }

  // Add To Basket butonuna tıklayınca kullanıcının sahip olduğu basket'a ürün eklemesi yapar.
  addItemToBasket(basketId:string , productId : string , quantity? : number){

    console.log(basketId)
    console.log(productId)
    console.log(quantity)

    const body = {
      basketId : basketId,
      productId : productId,
      quantity : quantity,
    }
    return this.http.post<any>(AUTH_API + 'BasketItem', body, httpOptions)
  }

  getBasketItemInfoForActiveBasket(){
    return this.http.get<any>(AUTH_API + 'SessionCustomer/GetCustomerBasketItemInfoForActiveBasketByCustomerNumber', httpOptions)
  }

  deleteBasketItemFromBasket(id : string){
    return this.http.delete<any>(AUTH_API + 'BasketItem/HardDeleteByProductId/'+ id, httpOptions)
  }
}

