import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { OrderUpsertDto, ShippingMethod, PaymentMethod } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
    selector: 'app-order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
    currentCart = this.cartService.currentCartValue;

    personalDataFormGroup: UntypedFormGroup = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        city: ['', Validators.required],
        zipCode: ['', Validators.required],
        line1: ['', Validators.required],
        line2: ['',],
        note: [''],
    });

    shippingFormGroup = new FormGroup({
        shippingMethod: new FormControl<ShippingMethod | null>(null, [Validators.required])
    });

    paymentFormGroup = new FormGroup({
        paymentMethod: new FormControl<PaymentMethod | null>(null, [Validators.required])
    });

    constructor(
        private formBuilder: UntypedFormBuilder,
        private readonly cartService: CartService,
        private readonly productService: ProductService,
        private readonly orderService: OrderService,
        private readonly authService: AuthService,
        private readonly router: Router,
        private readonly snackbar: MatSnackBar
    ) { }

    ngOnInit(): void {
        if (this.orderService.currentOrderValue) {
            this.personalDataFormGroup.patchValue(this.orderService.currentOrderValue);
            this.shippingFormGroup.patchValue(this.orderService.currentOrderValue);
            this.paymentFormGroup.patchValue(this.orderService.currentOrderValue);
        }
        else if (this.authService.currentUserValue) {
            this.personalDataFormGroup.patchValue(this.authService.currentUserValue);
        }
    }

    public onSubmit(): void {
        if (!this.currentCart || this.currentCart.cartItems.length <= 0) {
            this.snackbar.open('Nincs termék a kosaradban.', '', { duration: 3000 });
            return;
        }

        let order: OrderUpsertDto = {
            cartId: this.currentCart.id,
            applicationUserId: this.authService.currentUserValue?.id ?? null,
            shippingMethod: this.shippingMethod?.value,
            paymentMethod: this.paymentMethod?.value,
            ...this.personalDataFormGroup.value,

        };

        this.orderService.setCurrentOrder(order);
        this.router.navigate(['/confirm-order']);
    }

    get email() { return this.personalDataFormGroup.get('email'); }
    get firstName() { return this.personalDataFormGroup.get('firstName'); }
    get lastName() { return this.personalDataFormGroup.get('lastName'); }
    get phoneNumber() { return this.personalDataFormGroup.get('phoneNumber'); }
    get city() { return this.personalDataFormGroup.get('city'); }
    get zipCode() { return this.personalDataFormGroup.get('zipCode'); }
    get line1() { return this.personalDataFormGroup.get('line1'); }
    get line2() { return this.personalDataFormGroup.get('line2'); }
    get note() { return this.personalDataFormGroup.get('note'); }
    get shippingMethod() { return this.shippingFormGroup.get('shippingMethod'); }
    get paymentMethod() { return this.paymentFormGroup.get('paymentMethod'); }
}
