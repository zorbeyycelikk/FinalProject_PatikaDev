import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../main/customer/customer';
const AUTH_API = 'http://localhost:5087/vk/';

const httpOptions = {
  headers : new HttpHeaders({ 'Content-Type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http : HttpClient) { }


  // Ürünleri  kullanicinin profit değerine göre getirir
  getCustomer():Observable<Customer[]>{
    return this.http
           .get<Customer[]>(AUTH_API + 'Customer',httpOptions);
   }


   getCustomerById(id :string):Observable<Customer>{
    return this.http
            .get<Customer>(AUTH_API + 'Customer/' + id ,httpOptions);
   }

   // Db'ye Ürün Eklemesi yapar
   addCustomer(name:string ,email:string , phone:string , role:string , password:string ,profit:number):Observable<Customer>{
    const body = {name , email , phone , role , password , profit} as Customer
    console.log(body);
    return this.http.post<Customer>(AUTH_API + 'Customer',body,httpOptions);
   }
 
   // Seçilen Ürünün Bilgilerini Günceller
   updateCustomer(customer : Customer):Observable<Customer>{
 
    const body = {
      name: customer.name ,
      email : customer.email ,
      phone : customer.phone,
      password : customer.password,
      profit : customer.profit 
    } as Customer
 
     return this.http.put<Customer>(AUTH_API + 'Customer/' + customer.id ,body,httpOptions);
   }
 
   // Seçilen Ürünün Bilgilerini Günceller
   deleteCustomer(id : Customer):Observable<Customer>{
      return this.http.delete<Customer>(AUTH_API + 'Customer/' + id ,httpOptions);
   }
}
