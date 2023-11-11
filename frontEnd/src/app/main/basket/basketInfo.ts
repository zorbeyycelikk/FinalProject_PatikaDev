import { Product } from './../product/product';

export interface BasketInfo{
    basketId: string;
    quantity: number;
    product : Product;
}