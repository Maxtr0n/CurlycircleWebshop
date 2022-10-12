import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, Subscription, switchMap } from 'rxjs';
import { OrderService } from 'src/app/services/order.service';

@Component({
    selector: 'app-order-complete',
    templateUrl: './order-complete.component.html',
    styleUrls: ['./order-complete.component.scss']
})
export class OrderCompleteComponent implements OnInit, OnDestroy {
    hasWebPayment: boolean = false;
    isWebPaymentSuccessful: boolean = false;
    webPaymentQueryParams$: Subscription = new Subscription;
    orderIdQueryParams$: Subscription = new Subscription;
    orderId: number | null = null;

    constructor(
        private readonly orderService: OrderService,
        private readonly route: ActivatedRoute
    ) { }


    ngOnDestroy(): void {
        this.webPaymentQueryParams$.unsubscribe();
        this.orderIdQueryParams$.unsubscribe();
    }

    ngOnInit(): void {
        this.webPaymentQueryParams$ = this.route.queryParams.pipe(
            filter(queryParams => queryParams['paymentId']),
            switchMap((queryParams) => this.orderService.getWebPaymentResult(queryParams['paymentId']))
        ).subscribe({
            next: (webPayment) => {
                this.hasWebPayment = true;
                this.isWebPaymentSuccessful = webPayment.paymentStatus === "Succeeded";
                this.orderId = webPayment.orderId;
            }
        });

        this.orderIdQueryParams$ = this.route.queryParams.pipe(
            filter(queryParams => queryParams['orderId']),
        ).subscribe({
            next: (params) => {
                this.hasWebPayment = false;
                this.isWebPaymentSuccessful = false;
                this.orderId = params['orderId'];
            }
        });
    }

}
