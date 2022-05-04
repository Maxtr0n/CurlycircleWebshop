import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, switchMap, tap } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { UnsubscribeOnDestroy } from '../core/UnsubscribeOnDestroy';
import {
    CartItemUpsertDto,
    CartItemViewModel,
    CartViewModel,
    EntityCreatedViewModel,
    ProductViewModel,
    UserViewModel,
} from '../models/models';
import { AuthService } from './auth.service';
private const CART_ID_KEY = 'currentCartId';


@Injectable({
    providedIn: 'root',
})
export class CartService extends UnsubscribeOnDestroy {
    private readonly cartUrl = 'api/cart';

    private currentCartSubject: BehaviorSubject<CartViewModel | null>;
    public currentCart: Observable<CartViewModel | null>;

    constructor(
        private readonly httpClient: AppHttpClient,
        private readonly authService: AuthService
    ) {
        super();
        let currentCartId = this.getCurrentCartId();
        if(currentCartId === null) {
            this.currentCartSubject = new BehaviorSubject<CartViewModel | null>(
                null
            );
            this.currentCart = this.currentCartSubject.asObservable();

        } else {
            this.getCartById(currentCartId).pipe(
                tap((cart) => {
                    this.currentCartSubject = new BehaviorSubject<CartViewModel | null>(
                        cart
                    );
                })
            );
        }
        
        this.subscribe(
            this.authService.currentUser.pipe(
                tap((user) => {
                    this.handleUserChanged(user);
                })
            )
        );
    }

    public get currentCartValue(): CartViewModel | null {
        return this.currentCartSubject.value;
    }

    public addItemToCart(product: ProductViewModel): void {
        if (this.currentCartValue === null) {
            if (this.authService.currentUserValue === null) {
                this.subscribe(this.createCartAndGetById());
            } else {
                this.subscribe(
                    this.getCartById(this.authService.currentUserValue.cartId)
                );
            }
        }
        const cartItemDto: CartItemUpsertDto = {
            cartId: this.currentCartValue!.id,
            productId: product.id,
            price: product.price,
            quantity: 1,
        };
        //TODO: add
    }

    private getCurrentCartId(): number | null {
        const currentCartId = localStorage.getItem(CART_ID_KEY);
        if (currentCartId === null) {
            return null;
        }

        return JSON.parse(currentCartId);
    }

    private setCurrentCartId(cartId: number): void {
        localStorage.setItem(CART_ID_KEY, JSON.stringify(cartId));
    }

    private removeCurrentCartId(): void {
        localStorage.removeItem(CART_ID_KEY);
    }

    private createCart(): Observable<EntityCreatedViewModel> {
        return this.httpClient.get<EntityCreatedViewModel>(this.cartUrl);
    }

    private getCartById(cartId: number): Observable<CartViewModel> {
        return this.httpClient.get<CartViewModel>(`${this.cartUrl}/${cartId}`);
    }

    private createCartAndGetById(): Observable<CartViewModel> {
        return this.createCart().pipe(
            switchMap((entityCreated) => this.getCartById(entityCreated.id)),
            tap((cart) => {
                this.setCurrentCartId(cart.id);
                this.currentCartSubject.next(cart);
                console.log('cart with id retrieved:' + cart.id);
            })
        );
    }

    //ha a user nem null, akkor bejelentkezett -> a mostani cart ból vegyük ki az elemeket,
    //töröljük a cartot, és tegyük át az elemeket a user cartjába
    private handleUserChanged(user: UserViewModel | null): void {
        if (user === null) {
            this.removeCurrentCartId();
            this.currentCartSubject.next(null);
        } else {
            if (this.currentCartValue === null || this.currentCartValue.cartItems?.length === 0) {
                //üres kosár
                return;
            }
            
             const tempCart: CartViewModel = this.currentCartValue;
            if (this.currentCartValue.cartItems)
            const oldCartItems: CartItemViewModel[] = this.currentCartValue.cartItems?;
            this.subscribe(
                this.getCartById(user.cartId).pipe(tap((cart) => {
                    cart.cartItems?.concat()
                }))
            );
        }
    }
}
