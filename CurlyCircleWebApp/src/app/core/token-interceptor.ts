import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpErrorResponse,
} from '@angular/common/http';
import {
    BehaviorSubject,
    catchError,
    filter,
    Observable,
    switchMap,
    take,
    throwError,
} from 'rxjs';
import { AuthService } from '../services/auth.service';
import { RefreshDto, TokenViewModel } from '../models/models';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class TokenInterceptor implements HttpInterceptor {
    private isRefreshing = false;
    private accessTokenSubject: BehaviorSubject<any> =
        new BehaviorSubject<any>(null);

    constructor(
        private readonly authService: AuthService,
        private readonly router: Router) { }

    intercept(
        req: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        let authReq = req;
        const token = this.authService.getAccessToken();
        if (token != null) {
            authReq = this.addTokenHeader(req, token);
        }

        return next.handle(authReq).pipe(catchError(error => {
            if (error instanceof HttpErrorResponse && !authReq.url.includes('auth/login') && error.status === 401) {
                return this.handle401Error(authReq, next);
            }
            return throwError(() => new Error(error));
        }));
    }

    private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!this.isRefreshing) {
            this.isRefreshing = true;
            this.accessTokenSubject.next(null);
            const token = this.authService.getRefreshToken();
            if (token)
                return this.authService.refreshToken().pipe(
                    switchMap((token: TokenViewModel) => {
                        this.isRefreshing = false;
                        this.accessTokenSubject.next(token.accessToken);
                        console.log("Token interceptor: Refresh - Új access token megszerezve");

                        return next.handle(this.addTokenHeader(request, token.accessToken));
                    }),
                    catchError((err) => {
                        console.log("Token interceptor: catchError " + err);

                        this.isRefreshing = false;
                        this.authService.logout();
                        this.router.navigate(['login']);
                        return throwError(() => new Error(err));
                    })
                );
        }
        return this.accessTokenSubject.pipe(
            filter(token => token !== null),
            take(1),
            switchMap((token) => next.handle(this.addTokenHeader(request, token)))
        );
    }

    private addTokenHeader(request: HttpRequest<any>, token: string) {
        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
    }
}
