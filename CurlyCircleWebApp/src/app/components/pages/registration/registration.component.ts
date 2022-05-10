import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { RegisterDto } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { passwordsMatchValidator } from 'src/app/validators/password-match-validator';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent extends UnsubscribeOnDestroy implements OnInit {
    hidePassword = true;
    hidePasswordConfirmation = true;

    registrationFormGroup: FormGroup = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        password: ['', [Validators.required, Validators.min(6)]],
        passwordConfirmation: ['', Validators.required],
        city: ['', Validators.required],
        zipCode: ['', Validators.required],
        line1: ['', Validators.required],
        line2: ['',],
    }, { validators: passwordsMatchValidator });

    get email() { return this.registrationFormGroup.get('email'); }
    get firstName() { return this.registrationFormGroup.get('firstName'); }
    get lastName() { return this.registrationFormGroup.get('lastName'); }
    get phoneNumber() { return this.registrationFormGroup.get('phoneNumber'); }
    get password() { return this.registrationFormGroup.get('password'); }
    get city() { return this.registrationFormGroup.get('city'); }
    get zipCode() { return this.registrationFormGroup.get('zipCode'); }
    get line1() { return this.registrationFormGroup.get('line1'); }
    get line2() { return this.registrationFormGroup.get('line2'); }

    constructor(private readonly authService: AuthService,
        private readonly router: Router,
        private readonly formBuilder: FormBuilder,
        private readonly snackBar: MatSnackBar,) {
        super();
    }

    ngOnInit(): void {
    }

    register() {
        const registerDto: RegisterDto = {
            email: this.email?.value,
            firstName: this.firstName?.value,
            lastName: this.lastName?.value,
            city: this.city?.value,
            zipCode: this.zipCode?.value,
            line1: this.line1?.value,
            line2: this.line2?.value,
            phoneNumber: this.phoneNumber?.value,
            password: this.password?.value,
        };
        this.subscribe(this.authService.register(registerDto).pipe(
            tap(() => {
                this.snackBar.open("Sikeres regisztráció, kérlek jelentkezz be.", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'], verticalPosition: 'top' });
                this.router.navigate(['/login']);
            })
        ));
    }

}
