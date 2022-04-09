import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import {
    EntityCreatedViewModel,
    LoginDto,
    RefreshDto,
    RegisterDto,
    TokenViewModel,
    UserViewModel,
} from '../models/models';
import { AppHttpClient } from '../core/app-http-client';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private readonly authUrl = 'api/auth';

    private currentUserSubject: BehaviorSubject<UserViewModel | null>;
    public currentUser: Observable<UserViewModel | null>;

    constructor(private readonly httpClient: AppHttpClient) {
        this.currentUserSubject = new BehaviorSubject<UserViewModel | null>(
            this.getCurrentUser()
        );
        this.currentUser = this.currentUserSubject.asObservable();
    }

    login(loginDto: LoginDto): Observable<UserViewModel> {
        return this.httpClient
            .post<UserViewModel>(`${this.authUrl}/login`, loginDto)
            .pipe(
                tap((user) => {
                    this.setCurrentUser(user);
                    this.currentUserSubject.next(user);
                    console.log('login success:' + user.token.accessToken);
                })
            );
    }

    register(registerDto: RegisterDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<UserViewModel>(
            `${this.authUrl}/register`,
            registerDto
        );
    }

    refreshToken(refreshDto: RefreshDto): Observable<TokenViewModel> {
        return this.httpClient.post<TokenViewModel>(
            `${this.authUrl}/refresh`,
            refreshDto
        );
    }

    public get currentUserValue(): UserViewModel | null {
        return this.currentUserSubject.value;
    }

    logout(): void {
        this.removeCurrentUser();
        this.currentUserSubject.next(null);
    }

    getCurrentUser(): UserViewModel | null {
        const currentUser = localStorage.getItem('currentUser');
        if (currentUser === null) {
            return null;
        }

        return JSON.parse(currentUser);
    }

    private setCurrentUser(userViewModel: UserViewModel): void {
        localStorage.setItem('currentUser', JSON.stringify(userViewModel));
    }

    private removeCurrentUser(): void {
        localStorage.removeItem('currentUser');
    }

    getToken(): TokenViewModel | null {
        const user = this.getCurrentUser();
        if (user !== null) {
            return user.token;
        }
        return null;
    }
}
