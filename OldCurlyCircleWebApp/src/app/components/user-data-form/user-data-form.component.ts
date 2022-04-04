import { Component, OnInit } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { emailsMatchValidator } from 'src/app/validators/emailsMatchValidator';
import { OrderItem, OrderItemUpsertDto, OrderUpsertDto, PaymentMethod, ProductViewModel, ShippingMethod } from 'src/app/models/models';
import { OrderItemService } from 'src/app/services/order-item.service';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { tap } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
    selector: 'app-user-data-form',
    templateUrl: './user-data-form.component.html',
    styleUrls: ['./user-data-form.component.css'],
    providers: [{
        provide: STEPPER_GLOBAL_OPTIONS, useValue: { showError: true }
    }]
})
export class UserDataFormComponent extends UnsubscribeOnDestroy implements OnInit {
    items: OrderItem[] = [];
    total: number = 0;

    userDataFormGroup = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        emailConfirm: ['', [Validators.required, Validators.email]],
        phoneNumber: ['', [Validators.required]],
        zipcode: ['', Validators.required],
        city: ['', [Validators.required, Validators.max(4), Validators.min(4)]],
        address: ['', Validators.required],
        addressOptions: [''],
        note: [''],
    }, { validators: emailsMatchValidator });

    shippingFormGroup = this.fb.group({
        shippingMethod: ['', Validators.required]

    });

    paymentFormGroup = this.fb.group({
        paymentMethod: ['', Validators.required]
    })

    constructor(
        private fb: FormBuilder,
        private readonly cartService: CartService,
        private readonly orderService: OrderService,
        private readonly productService: ProductService,
        private readonly toast: ToastrService,
        private readonly router: Router,
    ) {
        super();
    }

    ngOnInit(): void {
        this.items = this.cartService.getItems();
        this.items.forEach(item => {
            this.total += item.price * item.quantity;
        });
    }

    onSubmit() {
        const dateOfOrder = new Date();
        const orderItemUpsertDtos: OrderItemUpsertDto[] = [];
        let shippingMethod: ShippingMethod;
        let paymentMethod: PaymentMethod;

        switch (this.shippingFormGroup.controls.shippingMethod.value) {
            case "Foxpost": {
                shippingMethod = ShippingMethod.Foxpost;
                break;
            }
            case "MagyarPostaPont": {
                shippingMethod = ShippingMethod.MagyarPostaPont;
                break;
            }
            case "MagyarPostaCsomagPont": {
                shippingMethod = ShippingMethod.MagyarPostaCsomagPont;
                break;
            }
            case "HomeDelivery": {
                shippingMethod = ShippingMethod.HomeDelivery;
                break;
            }
            case "PersonalDelivery": {
                shippingMethod = ShippingMethod.PersonalDelivery;
                break;
            }
            default: {
                shippingMethod = ShippingMethod.Foxpost;
                break;
            }
        }

        switch (this.paymentFormGroup.controls.paymentMethod.value) {
            case "MoneyTransfer": {
                paymentMethod = PaymentMethod.MoneyTransfer;
                break;
            }
            case "CashOnDelivery": {
                paymentMethod = PaymentMethod.CashOnDelivery;
                break;
            }
            default: {
                paymentMethod = PaymentMethod.MoneyTransfer;
                break;
            }
        }

        this.items.forEach(item => {
            const orderItemUpsertDto: OrderItemUpsertDto = {
                orderId: item.orderId,
                productId: item.productId,
                price: item.price,
                quantity: item.quantity
            };
            orderItemUpsertDtos.push(orderItemUpsertDto);
        });

        const orderDto: OrderUpsertDto = {
            orderDateTime: dateOfOrder,
            orderItems: orderItemUpsertDtos,
            name: this.userDataFormGroup.controls.lastName.value + ' ' + this.userDataFormGroup.controls.firstName.value,
            email: this.userDataFormGroup.controls.email.value,
            city: this.userDataFormGroup.controls.city.value,
            zipCode: this.userDataFormGroup.controls.zipcode.value,
            address: this.userDataFormGroup.controls.address.value,
            total: this.total,
            shippingMethod: shippingMethod,
            paymentMethod: paymentMethod,
            phoneNumber: this.userDataFormGroup.controls.phoneNumber.value,
            note: this.userDataFormGroup.controls.note.value
        }
        console.log(paymentMethod);
        this.subscribe(this.orderService.addOrder(orderDto).pipe(
            tap((response) => {
                this.toast.success("Sikeres rendelés.");
                this.cartService.clearCart();
                this.router.navigate(['/fooldal']);
                console.log('rendeles leadva');
            })
        ));
    }

    get firstName() { return this.userDataFormGroup.get('firstName'); }
    get lastName() { return this.userDataFormGroup.get('lastName'); }
    get email() { return this.userDataFormGroup.get('email'); }
    get emailConfirm() { return this.userDataFormGroup.get('emailConfirm'); }
    get phoneNumber() { return this.userDataFormGroup.get('phoneNumber'); }


}
