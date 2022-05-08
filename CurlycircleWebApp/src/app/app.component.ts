import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserViewModel } from './models/models';
import { AuthService } from './services/auth.service';
import { CartService } from './services/cart.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    title = 'CurlyCircleWebApp';

    currentUser: UserViewModel | null;

    constructor(private authService: AuthService, private cartService: CartService, private router: Router) {
        this.currentUser = null;
        this.authService.currentUser$.subscribe(x => this.currentUser = x);
    }

}
