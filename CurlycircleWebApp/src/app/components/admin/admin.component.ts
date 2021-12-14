import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap } from 'rxjs/operators';
import { AuthService } from 'src/app/core/auth.service';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { LoginDto } from 'src/app/models/models';

@Component({
    selector: 'app-admin',
    templateUrl: './admin.component.html',
    styleUrls: ['./admin.component.css']
})
export class AdminComponent extends UnsubscribeOnDestroy implements OnInit {

    hidePassword = true;
    isLoggedIn = false;

    formControls: Record<keyof LoginDto, FormControl> = {
        username: new FormControl(null, [Validators.required]),
        password: new FormControl(null, [Validators.required])
    };
    form = new FormGroup(this.formControls);

    constructor(private readonly authService: AuthService,
        private readonly router: Router,
        private readonly toast: ToastrService) {
        super();
    }

    ngOnInit(): void {

    }

    login(): void {
        this.subscribe(this.authService.login(this.form.value).pipe(
            tap((token) => {
                console.log(token);
                this.toast.success('Sikeres bejelentkezés.');
                this.router.navigate(['']);
            })
        ));
    }

}
