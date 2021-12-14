import { Injectable } from '@angular/core';
import { OrderItemUpsertDto, OrderItemViewModel, ProductViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class CartService {
    items: OrderItemUpsertDto[] = [];

    constructor() { }

    addToCart(item: OrderItemUpsertDto) {
        this.items.push(item);
    }

    getItems() {
        return this.items;
    }

    clearCart() {
        this.items = [];
        return this.items;
    }

    removeItem(item: OrderItemUpsertDto) {
        this.items = this.items.filter(obj => obj !== item);
    }
}
