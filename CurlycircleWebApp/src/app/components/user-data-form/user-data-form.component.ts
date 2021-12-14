import { Component, OnInit } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { emailsMatchValidator } from 'src/app/validators/emailsMatchValidator';
import { OrderItemUpsertDto, OrderUpsertDto, ProductViewModel } from 'src/app/models/models';
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
    items: OrderItemUpsertDto[] = [];
    products: ProductViewModel[] = [];

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
        comment: [''],
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
            this.subscribe(this.productService.getProduct(item.productId).pipe(
                tap((product) => this.products.push(product))
            ))
        });
    }

    onSubmit() {
        const dateOfOrder = new Date();
        const orderDto: OrderUpsertDto = {
            orderDateTime: dateOfOrder,
            orderItems: this.items,
            name: this.userDataFormGroup.controls.firstName.value + this.userDataFormGroup.controls.lastName.value,
            email: this.userDataFormGroup.controls.email.value,
            city: this.userDataFormGroup.controls.city.value,
            zipCode: this.userDataFormGroup.controls.zipcode.value,
            address: this.userDataFormGroup.controls.address.value,
            total: 0,
            shippingMethod: this.shippingFormGroup.controls.shippingMethod.value,
            paymentMethod: this.paymentFormGroup.controls.paymentMethod.value,
            phoneNumber: this.userDataFormGroup.controls.phoneNumber.value,
            note: this.userDataFormGroup.controls.note.value
        }
        this.subscribe(this.orderService.addOrder(orderDto).pipe(
            tap((response) => {
                this.toast.success("Sikeres rendelés.");
                this.router.navigate(['/fooldal']);
            })
        ));
    }

    get firstName() { return this.userDataFormGroup.get('firstName'); }
    get lastName() { return this.userDataFormGroup.get('lastName'); }
    get email() { return this.userDataFormGroup.get('email'); }
    get emailConfirm() { return this.userDataFormGroup.get('emailConfirm'); }
    get phoneNumber() { return this.userDataFormGroup.get('phoneNumber'); }


}
