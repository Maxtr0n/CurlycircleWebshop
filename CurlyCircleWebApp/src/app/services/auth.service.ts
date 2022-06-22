import { Injectable } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable, of, tap, throwError } from 'rxjs';
import {
    ChangePasswordDto,
    EntityCreatedViewModel,
    LoginDto,
    RefreshDto,
    RegisterDto,
    TokenViewModel,
    UserDataViewModel,
    UserUpdateDto,
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
    }


    public get currentUserValue(): UserViewModel | null {
        return this.currentUserSubject.value;
    }

    public login(loginDto: LoginDto): Observable<UserViewModel> {
        return this.httpClient
            .post<UserViewModel>(`${this.authUrl}/login`, loginDto)
            .pipe(
                tap((user) => {
                    this.setCurrentUser(user);
                    this.currentUserSubject.next(user);
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
        let user = this.currentUserValue;
        if (user === null) {
            return throwError(() => {
                const error = new Error(`No user is logged in`);
                return error;
            });
        }
        let refreshDto: RefreshDto = {
            email: user.email,
            id: user.id,
            accessToken: user.accessToken,
            refreshToken: user.refreshToken,
        };
        return this.httpClient
            .post<TokenViewModel>(`${this.authUrl}/refresh`, refreshDto)
            .pipe(
                tap((tokenViewModel) => {
                    this.setUserTokens(tokenViewModel);
                })
            );
    }

    public updateUser(userUpdateDto: UserUpdateDto): Observable<UserDataViewModel> {
        return this.httpClient.put<UserDataViewModel>(`${this.authUrl}/update`, userUpdateDto).pipe(
            tap((userData) => {
                this.updateCurrentUser(userData);
            })
        );
    }

    public changePassword(changePasswordDto: ChangePasswordDto): Observable<void> {
        return this.httpClient.put(`${this.authUrl}/change-password`, changePasswordDto);
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

    public setUserTokens(tokenViewModel: TokenViewModel): void {
        const user = this.currentUserValue;

        if (user === null) {
            return;
        }

        console.log(`Régi token: ${user.refreshToken}`);
        user.accessToken = tokenViewModel.accessToken;
        user.refreshToken = tokenViewModel.refreshToken;
        console.log(`Új token: ${user.refreshToken}`);

        this.setCurrentUser(user);
        this.currentUserSubject.next(user);
    }

    private updateCurrentUser(userDataViewModel: UserDataViewModel): void {
        if (!this.currentUserValue)
            return;

        let currentUser = this.currentUserValue;
        currentUser.email = userDataViewModel.email;
        currentUser.firstName = userDataViewModel.firstName;
        currentUser.lastName = userDataViewModel.lastName;
        currentUser.phoneNumber = userDataViewModel.phoneNumber;
        currentUser.city = userDataViewModel.city;
        currentUser.zipCode = userDataViewModel.zipCode;
        currentUser.line1 = userDataViewModel.line1;
        currentUser.line2 = userDataViewModel.line2;

        this.setCurrentUser(currentUser);
        this.currentUserSubject.next(currentUser);
    }

    private setCurrentUser(userViewModel: UserViewModel): void {
        localStorage.setItem('currentUser', JSON.stringify(userViewModel));
    }

    private removeCurrentUser(): void {
        localStorage.removeItem('currentUser');
    }
}
