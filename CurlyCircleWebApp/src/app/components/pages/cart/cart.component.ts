import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CartViewModel } from 'src/app/models/models';
import { CartService } from 'src/app/services/cart.service';

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

    currentCart: CartViewModel | null = null;

    constructor(
        private readonly cartService: CartService,
        private readonly router: Router,
        private readonly snackBar: MatSnackBar,
    ) { }

    ngOnInit(): void {
        this.cartService.currentCart$.subscribe((cart) => {
            this.currentCart = cart;
        });
    }

}
