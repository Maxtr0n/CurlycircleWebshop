import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { map, switchMap, tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { OrderItemViewModel, OrderViewModel } from 'src/app/models/models';
import { OrderItemProduct } from 'src/app/models/OrderItemProduct';
import { OrderItemService } from 'src/app/services/order-item.service';
import { OrderService } from 'src/app/services/order.service';
import { TableColumn } from '../../table/table.models';

@Component({
    selector: 'app-order-details',
    templateUrl: './order-details.component.html',
    styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent extends UnsubscribeOnDestroy implements OnInit {
    order: OrderViewModel;
    orderItems: OrderItemProduct[] = [];

    readonly orderItemColumns: TableColumn[] = [
        {
            name: 'Termék azonosító',
            dataField: 'productId',
        },
        {
            name: 'Termék név',
            dataField: 'name',
        },
        {
            name: 'Ár',
            dataField: 'price',
        },
        {
            name: 'Mennyiség',
            dataField: 'quantity',
        },

    ];

    constructor(
        private readonly route: ActivatedRoute,
        private readonly orderService: OrderService,
        private readonly orderItemService: OrderItemService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.getData();
    }

    private getData(): void {
        const order$ = this.route.params.pipe(
            switchMap((params => this.orderService.getOrder(params['id']))),
            tap((order) => {
                this.order = order;
            }),
        );
        this.subscribe(order$.pipe(
            switchMap((order) => this.orderService.getOrderOrderItems(order.id)),
            map((response) => {
                const orderItemProducts: OrderItemProduct[] = [];

                response.orderItems.forEach(orderItem => {
                    const orderItemProduct: OrderItemProduct = {
                        id: orderItem.id,
                        orderId: orderItem.orderId,
                        quantity: orderItem.quantity,
                        productId: orderItem.productId,
                        price: orderItem.product.price,
                        name: orderItem.product.name,
                        productCategoryId: orderItem.product.productCategoryId,
                        description: orderItem.product.description,
                        imageUrl: orderItem.product.imageUrl,
                        color: orderItem.product.color,
                        pattern: orderItem.product.pattern,
                        material: orderItem.product.material
                    }

                    orderItemProducts.push(orderItemProduct);
                });

                return orderItemProducts;
            }),
            tap((orderItems) => this.orderItems = orderItems),
        ));
    }

}
