import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs';
import { RegisterDto } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { passwordsMatchValidator } from 'src/app/components/utilities/validators/password-match-validator';
import { FormErrorStateMatcher } from '../../utilities/state-matchers/form-error-state-matcher';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
    hidePassword = true;
    hidePasswordConfirmation = true;
    passwordErrorStateMatcher = new FormErrorStateMatcher();

    registrationFormGroup: FormGroup = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        passwordGroup: this.formBuilder.group({
            password: ['', [Validators.required, Validators.min(6), Validators.pattern('.*[0-9].*')]],
            passwordConfirmation: [''],
        }, { validators: passwordsMatchValidator }),
        city: ['', Validators.required],
        zipCode: ['', Validators.required],
        line1: ['', Validators.required],
        line2: ['',],
    });

    get email() { return this.registrationFormGroup.get('email'); }
    get firstName() { return this.registrationFormGroup.get('firstName'); }
    get lastName() { return this.registrationFormGroup.get('lastName'); }
    get phoneNumber() { return this.registrationFormGroup.get('phoneNumber'); }
    get password() { return this.registrationFormGroup.get('passwordGroup.password'); }
    get passwordConfirmation() { return this.registrationFormGroup.get('passwordGroup.passwordConfirmation'); }
    get city() { return this.registrationFormGroup.get('city'); }
    get zipCode() { return this.registrationFormGroup.get('zipCode'); }
    get line1() { return this.registrationFormGroup.get('line1'); }
    get line2() { return this.registrationFormGroup.get('line2'); }

    constructor(private readonly authService: AuthService,
        private readonly router: Router,
        private readonly formBuilder: FormBuilder,
        private readonly snackBar: MatSnackBar,) {
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
        this.authService.register(registerDto).subscribe({
            next: (r) => {
                this.snackBar.open("Sikeres regisztráció, kérlek jelentkezz be.", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'] });
                this.router.navigate(['/login']);
            },
            error: (err) => {
                this.snackBar.open("Ez az email már foglalt, kérlek válassz újat.", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
            }
        });
    }

    getPasswordRuleTooltipText() {
        return `A jelszónak legalább 6 karakterből kell állnia és tartalmaznia kell legalább egy számot.`;
    }

}
