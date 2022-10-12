import { Component, Inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CartViewModel, OrderUpsertDto, PaymentMethod } from 'src/app/models/models';
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
    cart: CartViewModel | null = null;
    total: number = 0;

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
        this.cart = this.cartService.currentCartValue;
        this.total = this.cart?.cartItems.reduce((acc, curr) => acc + curr.price * curr.quantity, 0) ?? 0;
    }

    public placeOrder(): void {
        if (!this.order) {
            this.snackbar.open('Nem sikerült leadni a rendelésed, kérlek próbálkozz később.', '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
            return;
        }

        if (this.order.paymentMethod.toString() === PaymentMethod.WebPayment.toString()) {
            this.orderService.placeWebPaymentOrder(this.order).subscribe({
                next: (webPaymentRequestViewModel) => {
                    this.orderService.clearOrder();
                    this.document.location.href = webPaymentRequestViewModel.gatewayUrl;
                }
            });
        } else {
            this.orderService.placeOrder(this.order).subscribe({
                next: (entityCreatedViewModel) => {
                    this.cartService.refreshCurrentCart().subscribe();
                    this.orderService.clearOrder();
                    this.router.navigate(['/order-complete'], { queryParams: { orderId: entityCreatedViewModel.id } });
                },
                error: (err) => {
                    console.log(err);
                    this.snackbar.open('Nem sikerült leadni a rendelésed, kérlek próbálkozz később.', '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
                }
            });
        }
    }

    public cancel(): void {
        this.router.navigate(['/order']);
    }
}
