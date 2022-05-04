import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { LoginDto } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends UnsubscribeOnDestroy implements OnInit {
    hidePassword = true;
    isLoggedIn = false;

    formControls: Record<keyof LoginDto, FormControl> = {
        email: new FormControl(null, [Validators.required]),
        password: new FormControl(null, [Validators.required]),
        cartId: new FormControl(),
    };

    constructor(
        private readonly authService: AuthService,
        private readonly router: Router
    ) {
        super();
    }

    ngOnInit(): void {}
}
