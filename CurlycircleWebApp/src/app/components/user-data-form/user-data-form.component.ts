import { Component, OnInit } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { emailsMatchValidator } from 'src/app/validators/emailsMatchValidator';

@Component({
    selector: 'app-user-data-form',
    templateUrl: './user-data-form.component.html',
    styleUrls: ['./user-data-form.component.css'],
    providers: [{
        provide: STEPPER_GLOBAL_OPTIONS, useValue: { showError: true }
    }]
})
export class UserDataFormComponent implements OnInit {

    userDataFormGroup = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        emailConfirm: ['', [Validators.required, Validators.email]],
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

    constructor(private fb: FormBuilder) { }

    ngOnInit(): void {

    }

    onSubmit() {
        // TODO: handle form submission (use EventEmitter)
    }

    get firstName() { return this.userDataFormGroup.get('firstName'); }
    get lastName() { return this.userDataFormGroup.get('lastName'); }
    get email() { return this.userDataFormGroup.get('email'); }
    get emailConfirm() { return this.userDataFormGroup.get('emailConfirm'); }


}
