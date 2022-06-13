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
                cartItems.forEach((cartItem) => {
                    if (cartItem.product.imageUrls.length > 0) {
                        cartItem.product.imageUrls = cartItem.product.imageUrls.map((imageUrl) => {
                            return AppConstants.IMAGES_URL.concat(imageUrl);
                        });
                    } else {
                        cartItem.product.imageUrls = [AppConstants.NO_IMAGE_URL];
                    }
                });

                this.dataSource.data = cartItems;
            }
        });
    }

    public removeItem(cartItem: CartItemViewModel): void {
        this.cartService.removeCartItem(cartItem.id).subscribe();
    }

    public checkout(): void {
        this.router.navigate(['/home']);
    }

    public clearCart(): void {
        this.cartService.clearCart().subscribe();
    }

}
