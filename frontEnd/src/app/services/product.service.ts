import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../main/product/product';
import { Observable } from 'rxjs';

const AUTH_API = 'http://localhost:5087/vk/';

const httpOptions = {
  headers : new HttpHeaders({ 'Content-Type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  
  constructor(private http : HttpClient) { }

  // Ürünleri  kullanicinin profit değerine göre getirir
  getProducts():Observable<Product[]>{
   return this.http
          .get<Product[]>(AUTH_API + 'SessionCustomer/GetCustomerProductListInfo',httpOptions);
  }

  // Db'ye Ürün Eklemesi yapar
  addProduct(product : Product):Observable<Product>{
    return this.http.post<Product>(AUTH_API + 'Product',product,httpOptions);
  }

  // Seçilen Ürünün Bilgilerini Günceller
  updateProduct(product : Product):Observable<Product>{
    return this.http.put<Product>(AUTH_API + 'Product/' + product.id ,product,httpOptions);
  }

  // Seçilen Ürünün Bilgilerini Günceller
  deleteProduct(id : Product):Observable<Product>{
      return this.http.delete<Product>(AUTH_API + 'Product/' + id ,httpOptions);
  }



}