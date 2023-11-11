import { Component, OnInit } from '@angular/core';
import { BasketItemService } from 'src/app/services/basket-item.service';
import { BasketService } from 'src/app/services/basket.service';
import { BasketInfo } from './basketInfo';
import { Product } from '../product/product';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {

  filterText = "";
  userActiveBasketId? : string
  basketItems?: BasketInfo[]
  totalBasketPrice : number = 0;

  constructor(
    private basketService : BasketService,
    private basketItemService : BasketItemService,
    private formBuilder: FormBuilder
    ){}

  // Added Query //
  model?: Product;
  basketPaymentForm!: FormGroup;

  createbasketPaymentForm() {
    this.basketPaymentForm = this.formBuilder.group({
      // name: ["", Validators.required],
      // category: ["", Validators.required],
      // stock: ["", Validators.required],
      // price: ["", Validators.required],
      // imgUrl: ["", Validators.required],
    });
  }

    ngOnInit(){

      this.basketService.activeBasketId()
      .subscribe(
        response => this.userActiveBasketId = response.id
        )

        this.basketItemService.getBasketItemInfoForActiveBasket()
            .subscribe(response => 
                this.basketItems = response
              )

        this.createbasketPaymentForm();

    }// End-ngOnInit


    calculateTotalPrice(): void {
      this.totalBasketPrice = 0;
    
      if (this.basketItems) {
        this.basketItems.forEach(item => {
          if (item.product && item.product.price && item.quantity) {
            this.totalBasketPrice += item.product.price * item.quantity;
          }
        });
      }
    }

    delete(product : Product){
      this.basketItemService.deleteBasketItemFromBasket(product.id!).subscribe();
    }

    confirmOrder(){
      this.calculateTotalPrice();
    }
} 