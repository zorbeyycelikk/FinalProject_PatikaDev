import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Product } from '../main/product/product';
import { Observable } from 'rxjs';

const AUTH_API = 'http://localhost:5087/vk/';

const httpOptions = {
  headers : new HttpHeaders({ 'Content-Type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(private http : HttpClient) { }

  // Sayfaya giriş yapılınca bayi için sepet oluşturulur.Aktif sepeti var ise oluşturulmaz.
  createBasket(customerId:string){
    const body = {customerId}
    this.http.post<any>(AUTH_API + 'Basket', body, httpOptions).subscribe();
  }

  // Kullanıcının sahip olduğu aktif basket'ın id'sini döndürür.
  activeBasketId(){
    return this.http.get<any>(AUTH_API + 'SessionCustomer/GetCustomerActiveBasketInfo',httpOptions);
   }

}
