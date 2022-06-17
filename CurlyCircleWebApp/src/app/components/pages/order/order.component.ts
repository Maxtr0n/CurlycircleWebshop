import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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

    personalDataFormGroup: UntypedFormGroup = this.formBuilder.group({
        email: [{ value: '', disabled: true }, [Validators.required, Validators.email]],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        city: ['', Validators.required],
        zipCode: ['', Validators.required],
        line1: ['', Validators.required],
        line2: ['',],
        note: [''],
    });

    shippingFormGroup = this.formBuilder.group({
        shippingMethod: ['', Validators.required]

    });

    paymentFormGroup = this.formBuilder.group({
        paymentMethod: ['', Validators.required]
    });

    constructor(
        private formBuilder: UntypedFormBuilder,
        private readonly cartService: CartService,
        private readonly productService: ProductService,
        private readonly orderService: OrderService,
        private readonly authService: AuthService,
        private readonly router: Router,
    ) { }

    ngOnInit(): void {
        if (this.authService.currentUserValue) {
            this.personalDataFormGroup.patchValue(this.authService.currentUserValue);
        }
    }

    public onSubmit(): void {
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
