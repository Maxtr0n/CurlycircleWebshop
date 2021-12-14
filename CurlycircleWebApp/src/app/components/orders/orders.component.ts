import { Component, OnInit } from '@angular/core';
import { map, tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { OrderViewModel } from 'src/app/models/models';
import { OrderService } from 'src/app/services/order.service';

@Component({
    selector: 'app-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.css']
})
export class OrdersComponent extends UnsubscribeOnDestroy implements OnInit {
    orders: OrderViewModel[] = [];

    constructor(
        private readonly orderService: OrderService
    ) {
        super();
    }

    ngOnInit(): void {
        this.getData();
    }

    getData() {
        this.subscribe(this.orderService.getOrders().pipe(
            map((response) => response.orders),
            tap((orders) => this.orders = orders)
        ));
    }
}
