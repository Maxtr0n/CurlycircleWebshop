import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map, tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { OrderViewModel } from 'src/app/models/models';
import { OrderService } from 'src/app/services/order.service';
import { TableColumn } from '../table/table.models';

@Component({
    selector: 'app-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.css']
})
export class OrdersComponent extends UnsubscribeOnDestroy implements OnInit {
    orders: OrderViewModel[] = [];
    readonly columns: TableColumn[] = [{
        name: 'Azonosító',
        dataField: 'id',
        dataType: 'number'
    },
    {
        name: 'Dátum',
        dataField: 'orderDateTime',
        dataType: 'date',
        format: 'yyyy.MM.dd.'
    },
    {
        name: 'Név',
        dataField: 'name',
    }];

    constructor(
        private readonly orderService: OrderService,
        private readonly router: Router,
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

    onRowClicked(order: OrderViewModel): void {
        this.router.navigate(['/order-details', order.id]);
    }
}
