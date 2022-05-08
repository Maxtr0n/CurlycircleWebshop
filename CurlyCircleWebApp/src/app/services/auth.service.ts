import { Injectable } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable, of, tap } from 'rxjs';
import {
    EntityCreatedViewModel,
    LoginDto,
    RefreshDto,
    RegisterDto,
    TokenViewModel,
    UserViewModel,
} from '../models/models';
import { AppHttpClient } from '../core/app-http-client';
import { CartService } from './cart.service';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private readonly authUrl = 'api/auth';

    private currentUserSubject: BehaviorSubject<UserViewModel | null>;
    public currentUser$: Observable<UserViewModel | null>;

    constructor(private readonly httpClient: AppHttpClient) {
        this.currentUserSubject = new BehaviorSubject<UserViewModel | null>(
            this.getCurrentUser()
        );
        this.currentUser$ = this.currentUserSubject.asObservable();
        console.log('AuthService constructor');
    }

    public login(loginDto: LoginDto): Observable<UserViewModel> {
        return this.httpClient
            .post<UserViewModel>(`${this.authUrl}/login`, loginDto)
            .pipe(
                tap((user) => {
                    this.setCurrentUser(user);
                    this.currentUserSubject.next(user);
                    console.log('login success:' + user.accessToken);
                })
            );
    }

    public register(
        registerDto: RegisterDto
    ): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<UserViewModel>(
            `${this.authUrl}/register`,
            registerDto
        );
    }

    public refreshToken(): Observable<TokenViewModel> {
        let user = this.getCurrentUser();
        if (user === null) {
            return EMPTY; //not sure if its correct to return empty observable here
        }
        let refreshDto: RefreshDto = {
            email: user.email!,
            id: user.id,
            accessToken: user.accessToken!,
            refreshToken: user.refreshToken!,
        };
        return this.httpClient
            .post<TokenViewModel>(`${this.authUrl}/refresh`, refreshDto)
            .pipe(
                tap((tokenViewModel) => {
                    this.setUserTokens(tokenViewModel);
                })
            );
    }

    public get currentUserValue(): UserViewModel | null {
        return this.currentUserSubject.value;
    }

    public logout(): void {
        this.removeCurrentUser();
        this.currentUserSubject.next(null);
    }

    public getCurrentUser(): UserViewModel | null {
        const currentUser = localStorage.getItem('currentUser');
        if (currentUser === null) {
            return null;
        }

        return JSON.parse(currentUser);
    }

    public getAccessToken(): string | null {
        const user = this.getCurrentUser();
        if (user !== null) {
            return user.accessToken;
        }
        return null;
    }

    public getRefreshToken(): string | null {
        const user = this.getCurrentUser();
        if (user !== null) {
            return user.refreshToken;
        }
        return null;
    }

    public setUserTokens(tokenViewModel: TokenViewModel): boolean {
        const user = this.getCurrentUser();

        if (user !== null) {
            user.accessToken = tokenViewModel.accessToken;
            user.refreshToken = tokenViewModel.refreshToken;
            this.setCurrentUser(user);
            return true;
        }
        return false;
    }

    private setCurrentUser(userViewModel: UserViewModel): void {
        localStorage.setItem('currentUser', JSON.stringify(userViewModel));
    }

    private removeCurrentUser(): void {
        localStorage.removeItem('currentUser');
    }
}
