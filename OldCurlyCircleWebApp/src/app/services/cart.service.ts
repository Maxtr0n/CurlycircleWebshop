import { Injectable } from '@angular/core';
import { OrderItem, OrderItemUpsertDto, OrderItemViewModel, ProductViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class CartService {
    items: OrderItem[] = [];

    constructor() { }

    addToCart(newItem: OrderItem): void {
        let itemAlreadyExists = false;
        this.items.forEach(item => {
            if (item.productId === newItem.productId) {
                item.quantity++;
                itemAlreadyExists = true;
            }
        });

        if (!itemAlreadyExists) {
            this.items.push(newItem);
        }
    }

    getItems() {
        return this.items;
    }

    clearCart() {
        this.items = [];
        return this.items;
    }

    removeItem(item: OrderItem) {
        this.items = this.items.filter(obj => obj !== item);
    }
}
