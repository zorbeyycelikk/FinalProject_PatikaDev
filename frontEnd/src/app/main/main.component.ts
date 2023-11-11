import { Component, OnDestroy, OnInit } from '@angular/core';
import { StorageService } from '../services/storage.service';
import { BasketService } from '../services/basket.service';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit , OnDestroy {
  constructor(
    private storage:StorageService,
    private basketService:BasketService,
  ){}
    userRole?:string
    customerId?:string

  ngOnInit(): void {
    this.customerId = this.storage.getCustomerId();
    this.userRole = this.storage.getRole();

    // admin'in basket degeri olamaz
    if(this.userRole != 'admin'){
      console.log('sepete basarili sekilde olsuturuldu')
      this.basketService.createBasket(this.customerId);
    }
  }

  ngOnDestroy(): void {
  }
}