import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { LoginDto } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends UnsubscribeOnDestroy implements OnInit {
    hidePassword = true;
    isLoggedIn = false;

    loginFormGroup: FormGroup = this.formBuilder.group({
        email: ['', Validators.required],
        password: ['', Validators.required],
    });

    constructor(
        private readonly authService: AuthService,
        private readonly cartService: CartService,
        private readonly router: Router,
        private readonly formBuilder: FormBuilder,
        private readonly snackBar: MatSnackBar,
    ) {
        super();
    }

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

        this.subscribe(this.authService.login(loginDto).pipe(
            tap((token) => {
                console.log(token);
                this.snackBar.open("Sikeres bejelentkezés", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'], verticalPosition: 'top' });
                this.router.navigate(['']);
            })
        ));
    }

}
