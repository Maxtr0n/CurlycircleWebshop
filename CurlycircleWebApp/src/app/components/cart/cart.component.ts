import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { OrderItem, OrderItemUpsertDto, ProductViewModel } from 'src/app/models/models';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.css']
})
export class CartComponent extends UnsubscribeOnDestroy implements OnInit {
    items: OrderItem[] = [];

    constructor(
        private readonly cartService: CartService,
        private readonly productService: ProductService
    ) {
        super();
    }

    ngOnInit(): void {
        this.getData();
    }

    private getData() {
        this.items = this.cartService.getItems();
    }

    removeItem(orderItem: OrderItem) {
        this.cartService.removeItem(orderItem);
        this.getData();
    }
}
