import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LoginDto, TokenViewModel } from '../models/models';
import { AppHttpClient } from './app-http-client';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly authUrl = 'api/auth';

    private currentUserSubject: BehaviorSubject<boolean | null>;
    public currentUser: Observable<boolean | null>;

    constructor(private readonly httpClient: AppHttpClient) {
        if (this.getAccessToken() !== null) {
            this.currentUserSubject = new BehaviorSubject<boolean | null>(true);
        }
        else {
            this.currentUserSubject = new BehaviorSubject<boolean | null>(null);
        }

        this.currentUser = this.currentUserSubject.asObservable();
    }

    login(loginDto: LoginDto): Observable<TokenViewModel> {
        return this.httpClient.post<TokenViewModel>(`${this.authUrl}/login`, loginDto).pipe(
            tap((response) => {
                this.setSession(response);
                this.currentUserSubject.next(true);
            }));
    }

    public get currentUserValue(): boolean | null {
        return this.currentUserSubject.value;
    }

    logout(): void {
        this.removeSession();
        this.currentUserSubject.next(null);
    }

    getAccessToken(): string | null {
        return localStorage.getItem("access_token");
    }

    private setSession(tokenViewModel: TokenViewModel): void {
        localStorage.setItem('access_token', tokenViewModel.accessToken);
    }

    private removeSession(): void {
        localStorage.removeItem("access_token");
    }
}

