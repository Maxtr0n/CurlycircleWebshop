import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AppConstants } from 'src/app/core/app-constants';
import { CartItemViewModel, CartViewModel } from 'src/app/models/models';
import { CartService } from 'src/app/services/cart.service';

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

    displayedColumns: string[] = ['product', 'price', 'quantity', 'total', 'delete'];
    dataSource = new MatTableDataSource<CartItemViewModel>([]);
    imagesBaseUrl: string = AppConstants.IMAGES_URL;
    noImageUrl: string = AppConstants.NO_IMAGE_URL;
    total: number = 0;

    constructor(
        private readonly cartService: CartService,
        private readonly router: Router,
        private readonly snackBar: MatSnackBar,
    ) { }

    ngOnInit(): void {
        this.cartService.currentCart$.pipe(
            map(cart => cart?.cartItems ?? [])
        ).subscribe({
            next: (cartItems: CartItemViewModel[]) => {
                this.dataSource.data = cartItems;
                this.total = cartItems.reduce((acc, curr) => acc + curr.price * curr.quantity, 0);
            }
        });
    }

    public removeItem(cartItem: CartItemViewModel): void {
        this.cartService.removeCartItem(cartItem.id).subscribe();
    }

    public checkout(): void {
        this.router.navigate(['/order']);
    }

    public clearCart(): void {
        this.cartService.clearCart().subscribe();
    }

}
