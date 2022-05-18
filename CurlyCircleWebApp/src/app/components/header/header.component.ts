import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CartViewModel, UserViewModel } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
    @Output() public sidenavToggle = new EventEmitter();

    currentUser: UserViewModel | null = null;
    currentCart: CartViewModel | null = null;
    currentCartSize: string = "";

    constructor(
        private readonly authService: AuthService,
        private readonly router: Router,
        private readonly snackBar: MatSnackBar,
        private readonly cartService: CartService,
    ) {
    }

    logout(): void {
        this.authService.logout();
        this.snackBar.open("Sikeresen kijelentkeztél.", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'] });
    }

    ngOnInit(): void {
        this.authService.currentUser$.subscribe((user) => {
            this.currentUser = user;
        });

        this.cartService.currentCart$.subscribe((cart) => {
            this.currentCart = cart;
            const cartSize = cart?.cartItems?.length;
            if (cartSize && cartSize > 0) {
                this.currentCartSize = cartSize.toString();
            } else {
                this.currentCartSize = "";
            }
        });
    }

    public onToggleSidenav = () => {
        this.sidenavToggle.emit();
    };
}
