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

@Injectable({
    providedIn: 'root',
})
export class TokenInterceptor implements HttpInterceptor {
    private isRefreshing = false;
    private refreshTokenSubject: BehaviorSubject<any> =
        new BehaviorSubject<any>(null);

    constructor(private readonly authService: AuthService) {}

    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        let authReq = request;
        const authToken = this.authService.getAccessToken();
        if (authToken != null) {
            authReq = this.addTokenHeader(request, authToken);
        }

        return next.handle(authReq).pipe(
            catchError((error) => {
                if (
                    error instanceof HttpErrorResponse &&
                    !authReq.url.includes('auth/login') &&
                    error.status === 401
                ) {
                    return this.handle401Error(authReq, next);
                } else if (
                    error instanceof HttpErrorResponse &&
                    error.status === 400 &&
                    error.message === 'Refresh attempt failed.'
                ) {
                    //please log in again
                    this.authService.logout();
                }

                return throwError(() => error);
            })
        );
    }

    private handle401Error(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        if (!this.isRefreshing) {
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);
            const refreshToken = this.authService.getRefreshToken();
            if (refreshToken)
                return this.authService.refreshToken().pipe(
                    switchMap((token: TokenViewModel) => {
                        this.isRefreshing = false;
                        this.authService.setUserTokens(token);
                        this.refreshTokenSubject.next(token.accessToken);

                        return next.handle(
                            this.addTokenHeader(request, token.accessToken)
                        );
                    }),
                    catchError((error) => {
                        this.isRefreshing = false;

                        this.authService.logout();
                        return throwError(() => error);
                    })
                );
        }
        return this.refreshTokenSubject.pipe(
            filter((token) => token !== null),
            take(1),
            switchMap((token) =>
                next.handle(this.addTokenHeader(request, token))
            )
        );
    }

    private addTokenHeader(request: HttpRequest<any>, token: string) {
        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`,
            },
        });
    }
}
