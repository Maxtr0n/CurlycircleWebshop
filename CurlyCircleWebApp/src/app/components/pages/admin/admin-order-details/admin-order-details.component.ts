import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription, switchMap } from 'rxjs';
import { OrderViewModel } from 'src/app/models/models';
import { OrderService } from 'src/app/services/order.service';

@Component({
    selector: 'app-admin-order-details',
    templateUrl: './admin-order-details.component.html',
    styleUrls: ['./admin-order-details.component.scss']
})
export class AdminOrderDetailsComponent implements OnInit {
    order$: Subscription = new Subscription;
    order: OrderViewModel | null = null;

    constructor(
        private readonly orderService: OrderService,
        private readonly router: Router,
        private readonly snackBar: MatSnackBar,
        private readonly route: ActivatedRoute,
    ) { }

    ngOnInit(): void {
        this.order$ = this.route.params.pipe(
            switchMap(params => this.orderService.getOrder(params['orderId']))
        ).subscribe({
            next: (order) => {
                this.order = order;
                console.log(order);
            }
        });

    }
}

