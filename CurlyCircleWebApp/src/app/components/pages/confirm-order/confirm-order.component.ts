import { Component, Inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { OrderUpsertDto, PaymentMethod } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';
import { DOCUMENT } from '@angular/common';

@Component({
    selector: 'app-confirm-order',
    templateUrl: './confirm-order.component.html',
    styleUrls: ['./confirm-order.component.scss']
})
export class ConfirmOrderComponent implements OnInit {
    order: OrderUpsertDto | null = null;

    constructor(
        private readonly cartService: CartService,
        private readonly orderService: OrderService,
        private readonly authService: AuthService,
        private readonly router: Router,
        private readonly snackbar: MatSnackBar,
        @Inject(DOCUMENT) private document: Document
    ) { }

    ngOnInit(): void {
        this.order = this.orderService.currentOrderValue;
    }

    public placeOrder(): void {
        if (!this.order) {
            return;
        }

        if (this.order.paymentMethod.toString() === PaymentMethod.WebPayment.toString()) {
            this.orderService.placeWebPaymentOrder(this.order).subscribe({
                next: (webPaymentRequestViewModel) => {
                    this.document.location.href = webPaymentRequestViewModel.gatewayUrl;
                }
            });
        } else {
            this.orderService.placeOrder(this.order).subscribe({
                next: (entityCreatedViewModel) => {
                    this.cartService.refreshCurrentCart().subscribe();
                    this.router.navigate(['/order-complete']);
                },
                error: (err) => {
                    console.log(err);
                    this.snackbar.open('Nem sikerült leadni a rendelésed, kérlek próbálkozz később.', '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
                }
            });
        }
    }
}
