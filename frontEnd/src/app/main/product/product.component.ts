import { StorageService } from './../../services/storage.service';
import { Component, OnInit } from '@angular/core';
import { Product } from './product';
import { ProductService } from 'src/app/services/product.service';
import { BasketService } from 'src/app/services/basket.service';
import { BasketItemService } from 'src/app/services/basket-item.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})

export class ProductComponent implements OnInit {

  constructor(
    private storage: StorageService,
    private productService: ProductService,
    private basketService: BasketService,
    private basketItemService: BasketItemService,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit() {
    this.createProductAddForm();
    this.createProductUpdateForm();

    this.userRole = this.storage.getRole();
    this.getActiveProducts();
    this.getActiveBasketId();

  }// End-ngOnInit

  product?: Product[];
  userRole?: string;
  userId?: string;
  userActiveBasketId?: any;
  quantity?: number = 1;
  filterText = "";
  selectedProduct: Product = {};

  // Added Query //
  model?: Product;
  productAddForm!: FormGroup;
  productUpdateForm!: FormGroup;


  createProductAddForm() {
    this.productAddForm = this.formBuilder.group({
      name: ["", Validators.required],
      category: ["", Validators.required],
      stock: ["", Validators.required],
      price: ["", Validators.required],
      imgUrl: ["", Validators.required],
    });
  }

  createProductUpdateForm() {
    this.productUpdateForm = this.formBuilder.group({
      name: ["", Validators.required],
      category: ["", Validators.required],
      stock: ["", Validators.required],
      price: ["", Validators.required],
      imgUrl: ["", Validators.required],
    });
  }

  update() {
    if (this.productUpdateForm?.valid) {
      this.model = Object.assign({}, this.productUpdateForm.value)
    }
    const body = {
      id : this.selectedProduct.id,
      name : this.model!.name,
      category : this.model!.category,
      stock : this.model!.stock,
      price : this.model!.price,
      imgUrl : this.model!.imgUrl,
    } as Product


    this.productService.updateProduct(body).subscribe(() => {
      window.location.reload();
    });
  }

  add() {
    if (this.productAddForm?.valid) {
      this.model = Object.assign({}, this.productAddForm.value)
    }
    console.log(this.model?.id);

    this.productService.addProduct(this.model!).subscribe(() => {
      window.location.reload();
    });
  }

  getActiveProducts() {
    return this.productService.getProducts()
      .subscribe((response: Product[]) => {
        this.product = response.filter(product => product.isActive === true);
      })
  }// End-getProducts


  updateProduct(product: Product) {

    if (confirm("Are you sure to update ?")) {
      this.productService.updateProduct(product).subscribe();
    }
  } // End-updateProduct


  deleteProduct(id: Product) {
    if (confirm("Are you sure to delete ?")) {
      this.productService.deleteProduct(id).subscribe();
    }

  }// End-deleteProduct

  getProductForModal(product: Product) {
    this.selectedProduct = product;
  } // End-getProductForModal

  addItemToBasket(productId: string, event: Event) {
    event.preventDefault()
    this.basketItemService.addItemToBasket(this.userActiveBasketId, productId, this.quantity).subscribe();
    console.log(this.userActiveBasketId)
    this.quantity = 1;
  }

  getActiveBasketId() {
    this.basketService.activeBasketId().subscribe
      (response =>
        this.userActiveBasketId = response.id
      );
  }

  updateQuantity(newQuantity?: number) {
    // quantity değerini güncelle
    this.quantity = newQuantity;

  }
}
