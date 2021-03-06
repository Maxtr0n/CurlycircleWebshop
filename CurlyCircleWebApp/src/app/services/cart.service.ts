import { Injectable } from '@angular/core';
import { BehaviorSubject, first, forkJoin, from, map, mergeMap, Observable, of, switchMap, tap } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
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
        const cartItemDto: CartItemUpsertDto = {
            productId: product.id,
            price: product.price,
            quantity: quantity,
        };

        if (cartId) {
            return this.addToCart(cartId, cartItemDto);
        }
        else {
            return this.createCartAndGetById().pipe(
                switchMap(cart => this.addToCart(cart.id, cartItemDto))
            );
        }
    }

    public clearCart(): Observable<CartViewModel> {
        const cartId = this.currentCartValue?.id;
        if (!cartId) {
            return of();
        }

        return this.httpClient.delete<void>(`${this.cartUrl}/${cartId}/clear`).pipe(
            switchMap(() => this.getCartByIdAndEmit(cartId))
        );
    }

    public removeCartItem(cartItemId: number): Observable<CartViewModel> {
        const cartId = this.currentCartValue?.id;
        if (!cartId) {
            return of();
        }

        return this.httpClient.delete<void>(`${this.cartUrl}/${cartId}/cartItems/${cartItemId}`).pipe(
            switchMap(() => this.getCartByIdAndEmit(cartId))
        );
    }

    public updateCartItem(cartItemId: number, quantity: number): Observable<CartViewModel> {
        const cartId = this.currentCartValue?.id;
        if (!cartId) {
            return of();
        }

        return this.httpClient.put<void>(`${this.cartUrl}/${cartId}/cartItems/${cartItemId}`, { quantity }).pipe(
            switchMap(() => this.getCartByIdAndEmit(cartId))
        );
    }

    public getCurrentLocalCart(): LocalCart | null {
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

    private getCartByIdAndEmit(cartId: number): Observable<CartViewModel> {
        return this.getCartById(cartId).pipe(
            tap(cart => this.currentCartSubject.next(cart))
        );
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
        return this.httpClient.post<EntityCreatedViewModel>(`${this.cartUrl}/${cartId}/cartItems`, cartItemDto).pipe(
            switchMap(() => this.getCartByIdAndEmit(cartId))
        );
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

    //ha a user nem null, akkor bejelentkezett -> a mostani cart b??l vegy??k ki az elemeket,
    //t??r??lj??k a cartot, ??s tegy??k ??t az elemeket a user cartj??ba
    private handleUserChanged(user: UserViewModel | null): Observable<CartViewModel | null> {
        const localCart = this.getCurrentLocalCart();

        if (user !== null) {
            //ha a user nem null, loginn??l a backend elint??zi a kosarak ??sszevon??s??t, ??gy el??g lek??rni a user kosar??t
            //ha nem login, hanem program indul??s miatt ker??l??nk ide, akkor is el??g lek??rni a user kosar??t
            console.log('User detected from CartService.');
            return this.getCartByIdAndSetAsUserCart(user.cartId);

        } else {
            if (localCart !== null) {
                //ha van elmentve cart a localstoragebe
                if (localCart.isAnonymous) {
                    //ha a localCart anonim userhez tartozik, akkor lek??rj??k a kosarat, ??s be??ll??tjuk kos??rk??nt
                    return this.getCartById(localCart.id).pipe(
                        tap(cart => {
                            this.currentCartSubject.next(cart);
                            console.log('No user but anonymous local cart detected.');
                        })
                    );
                } else {
                    //ha nem anonim, akkor egy userhez tartozik, de nincs bejelentkezett user, teh??t t??r??lj??k a localstorageb??l
                    return new Observable(() => {
                        this.removeCurrentLocalCart();
                        this.currentCartSubject.next(null);
                        console.log('No user but user local cart detected. Happens at logout.');
                    });
                }
            }
        }

        //ha nincs user ??s nincs local cart, akkor nincs teend??nk, majd egy term??k hozz??ad??sn??l l??trej??n a kos??r
        console.log('No user and no local cart detected.');
        return new Observable();
    }
}
