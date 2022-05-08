import { Injectable } from '@angular/core';
import { BehaviorSubject, first, forkJoin, from, map, mergeMap, Observable, of, switchMap, tap } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { UnsubscribeOnDestroy } from '../core/UnsubscribeOnDestroy';
import {
    CartItemUpsertDto,
    CartItemViewModel,
    CartViewModel,
    EntityCreatedViewModel,
    LocalCart,
    ProductViewModel,
    UserViewModel,
} from '../models/models';
import { AuthService } from './auth.service';
const LOCAL_CART_KEY = 'localCart';

@Injectable({
    providedIn: 'root',
})
export class CartService {
    private readonly cartUrl = 'api/cart';

    private currentCartSubject: BehaviorSubject<CartViewModel | null>;
    public currentCart$: Observable<CartViewModel | null>;

    constructor(
        private readonly httpClient: AppHttpClient,
        private readonly authService: AuthService
    ) {
        this.currentCartSubject = new BehaviorSubject<CartViewModel | null>(null);
        this.currentCart$ = this.currentCartSubject.asObservable();

        this.authService.currentUser$.pipe(
            switchMap(user => this.handleUserChanged(user))
        ).subscribe();
    }

    public get currentCartValue(): CartViewModel | null {
        return this.currentCartSubject.value;
    }

    public addItemToCart(product: ProductViewModel, quantity: number): Observable<EntityCreatedViewModel> {
        const cartId = this.currentCartValue?.id;

        if (cartId) {
            const cartItemDto: CartItemUpsertDto = {
                productId: product.id,
                price: product.price,
                quantity: quantity,
            };

            return this.addToCart(cartId, cartItemDto);
        }
        else {
            return this.createCartAndGetById().pipe(
                switchMap(cart => this.addToCart(cart.id, {
                    productId: product.id,
                    price: product.price,
                    quantity: quantity,
                }))
            );
        }
    }

    private getCurrentLocalCart(): LocalCart | null {
        const currentLocalCart = localStorage.getItem(LOCAL_CART_KEY);
        if (currentLocalCart === null) {
            return null;
        }

        return JSON.parse(currentLocalCart);
    }

    private setCurrentLocalCart(localCart: LocalCart): void {
        localStorage.setItem(LOCAL_CART_KEY, JSON.stringify(localCart));
    }

    private removeCurrentLocalCart(): void {
        localStorage.removeItem(LOCAL_CART_KEY);
    }

    private createCart(): Observable<EntityCreatedViewModel> {
        return this.httpClient.get<EntityCreatedViewModel>(this.cartUrl);
    }

    private getCartById(cartId: number): Observable<CartViewModel> {
        return this.httpClient.get<CartViewModel>(`${this.cartUrl}/${cartId}`);
    }

    private deleteCart(cartId: number): Observable<void> {
        return this.httpClient.delete(`${this.cartUrl}/${cartId}`);
    }

    private getCartByIdAndSetAsUserCart(cartId: number): Observable<CartViewModel> {
        return this.getCartById(cartId).pipe(
            tap(cart => {
                this.setCurrentLocalCart({ id: cart.id, isAnonymous: false });
                this.currentCartSubject.next(cart);
            })
        );
    }

    private addToCart(cartId: number, cartItemDto: CartItemUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(`${this.cartUrl}/${cartId}/cartItems`, cartItemDto);
    }

    private createCartAndGetById(): Observable<CartViewModel> {
        return this.createCart().pipe(
            switchMap((entityCreated) => this.getCartById(entityCreated.id)),
            tap((cart) => {
                const localCart: LocalCart = {
                    id: cart.id,
                    isAnonymous: true
                };
                this.setCurrentLocalCart(localCart);
                this.currentCartSubject.next(cart);
            })
        );
    }

    //ha a user nem null, akkor bejelentkezett -> a mostani cart ból vegyük ki az elemeket,
    //töröljük a cartot, és tegyük át az elemeket a user cartjába
    private handleUserChanged(user: UserViewModel | null): Observable<CartViewModel | null> {
        const localCart = this.getCurrentLocalCart();

        if (user !== null) {
            return this.getCartByIdAndSetAsUserCart(user.cartId);
        } else if (localCart !== null) {
            if (localCart.isAnonymous) {
                return this.getCartById(localCart.id).pipe(
                    tap(cart => {
                        this.currentCartSubject.next(cart);
                        console.log('No user but anonymous local cart detected.');
                    })
                );
            } else {
                return new Observable(() => {
                    this.removeCurrentLocalCart();
                    this.currentCartSubject.next(null);
                    console.log('No user but user local cart detected. Happens at logout.');
                });
            }
        }

        console.log('No user and no local cart detected.');
        return new Observable();
    }

    private addOldItemsAndGetCart(oldCartItems: CartItemViewModel[], user: UserViewModel, cartToDelete: number): Observable<CartViewModel> {
        const observableItems = oldCartItems.map(cartItem => this.addToCart(user.cartId, {
            productId: cartItem.productId,
            price: cartItem.price,
            quantity: cartItem.quantity
        }));

        return forkJoin(observableItems, (...results) => {
            return this.getCartByIdAndSetAsUserCart(user.cartId);
        }).pipe(
            switchMap(result => result),
            tap(this.deleteCart(cartToDelete))
        );
    }
}
