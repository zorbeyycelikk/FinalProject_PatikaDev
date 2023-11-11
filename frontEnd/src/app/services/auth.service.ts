import { Injectable } from '@angular/core';
import { HttpClient , HttpHeaders } from '@angular/common/http'
import { Observable} from 'rxjs'
import { StorageService } from './storage.service';

const AUTH_API = 'http://localhost:5087/vk/';

const httpOptions = {
  headers : new HttpHeaders({ 'Content-Type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(
    private http:HttpClient,
    private storage:StorageService
    ) { }

  logIn(email:any , password:any):Observable<any>{
    return this.http.post(AUTH_API+'Token',{
      email , 
      password
    },httpOptions);
  }

  logOut(){
    this.storage.clean();
    window.location.href = '/login'
  }

  isLoggin(){
    let user = this.storage.getUser();
    if(user){
      return true;
    }
    else{
      return false;
    }
  }
  fetchExample():Observable<any>{
    return this.http.get(AUTH_API+'Customer',httpOptions);
  }
}
