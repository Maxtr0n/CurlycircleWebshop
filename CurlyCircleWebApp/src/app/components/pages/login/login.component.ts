import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, FormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs';
import { LoginDto } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
    hidePassword = true;

    loginFormGroup: UntypedFormGroup = this.formBuilder.group({
        email: ['', Validators.required],
        password: ['', Validators.required],
    });

    constructor(
        private readonly authService: AuthService,
        private readonly cartService: CartService,
        private readonly router: Router,
        private readonly formBuilder: UntypedFormBuilder,
        private readonly snackBar: MatSnackBar,
    ) {
    }

    get email() { return this.loginFormGroup.get('email'); }
    get password() { return this.loginFormGroup.get('password'); }

    ngOnInit(): void { }

    login(): void {
        //lekérni az cartservicetől a cartId-t és ha nem null akk küldjük el azt is
        const currentCart = this.cartService.currentCartValue;
        const currentLocalCart = this.cartService.getCurrentLocalCart();
        let currentCartId: number | null = null;

        if (currentCart && currentLocalCart && currentLocalCart.isAnonymous) {
            currentCartId = currentCart.id;
        }

        const loginDto: LoginDto = {
            email: this.loginFormGroup.controls['email'].value,
            password: this.loginFormGroup.controls['password'].value,
            cartId: currentCartId
        };

        this.authService.login(loginDto).subscribe({
            next: (token) => {
                console.log(token);
                this.snackBar.open("Sikeres bejelentkezés.", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'] });
                this.router.navigate(['']);
            },
            error: (err) => {
                this.snackBar.open("Helytelen email vagy jelszó.", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
                this.loginFormGroup?.setErrors({ wrongCredentials: true });
            }
        });
    }

}
