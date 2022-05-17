import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ChangePasswordDto, UserUpdateDto, UserViewModel } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { FormErrorStateMatcher } from '../../utilities/state-matchers/form-error-state-matcher';
import { passwordsMatchValidator } from '../../utilities/validators/password-match-validator';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
    user: UserViewModel | null = null;
    hidePassword = true;
    hidePasswordConfirmation = true;
    hideOldPassword = true;
    passwordErrorStateMatcher = new FormErrorStateMatcher();

    personalDataFormGroup: FormGroup = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        city: ['', Validators.required],
        zipCode: ['', Validators.required],
        line1: ['', Validators.required],
        line2: ['',],
    });

    passwordFormGroup: FormGroup = this.formBuilder.group({
        oldPassword: ['', [Validators.required, Validators.min(6), Validators.pattern('.*[0-9].*')]],
        password: ['', [Validators.required, Validators.min(6), Validators.pattern('.*[0-9].*')]],
        passwordConfirmation: ['', Validators.required],
    }, { validators: passwordsMatchValidator });

    get email() { return this.personalDataFormGroup.get('email'); }
    get firstName() { return this.personalDataFormGroup.get('firstName'); }
    get lastName() { return this.personalDataFormGroup.get('lastName'); }
    get phoneNumber() { return this.personalDataFormGroup.get('phoneNumber'); }
    get city() { return this.personalDataFormGroup.get('city'); }
    get zipCode() { return this.personalDataFormGroup.get('zipCode'); }
    get line1() { return this.personalDataFormGroup.get('line1'); }
    get line2() { return this.personalDataFormGroup.get('line2'); }
    get password() { return this.personalDataFormGroup.get('password'); }
    get passwordConfirmation() { return this.personalDataFormGroup.get('passwordConfirmation'); }
    get oldPassword() { return this.personalDataFormGroup.get('oldPassword'); }

    constructor(
        private readonly authService: AuthService,
        private readonly router: Router,
        private readonly formBuilder: FormBuilder,
        private readonly snackBar: MatSnackBar,
    ) { }

    ngOnInit(): void {
        this.authService.currentUser$.subscribe({
            next: (user) => this.user = user
        });
    }

    updateUser(): void {
        if (this.user === null) {
            return;
        }

        const userUpdateDto: UserUpdateDto = {
            userId: this.user.id,
            email: this.email?.value,
            firstName: this.firstName?.value,
            lastName: this.lastName?.value,
            city: this.city?.value,
            zipCode: this.zipCode?.value,
            line1: this.line1?.value,
            line2: this.line2?.value,
            phoneNumber: this.phoneNumber?.value,
        };

        //TODO call UpdateUser
    }

    changePassword(): void {
        if (this.user === null) {
            return;
        }

        const changePasswordDto: ChangePasswordDto = {
            email: this.user.email,
            oldPassword: '',
            newPassword: ''
        };

        //TODO call ChangePassword
    }

    getPasswordRuleTooltipText(): string {
        return `A jelszónak legalább 6 karakterből kell állnia és tartalmaznia kell legalább egy számot.`;
    }

}
