import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpErrorResponse,
} from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { RefreshDto } from '../models/models';

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
        const authToken = this.authService.getToken()?.accessToken;
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
            const token = this.authService.getToken()?.refreshToken;
            if (token) refreshDto = new RefreshDto();
            return this.authService.refreshToken(token).pipe(
                switchMap((token: any) => {
                    this.isRefreshing = false;
                    this.tokenService.saveToken(token.accessToken);
                    this.refreshTokenSubject.next(token.accessToken);

                    return next.handle(
                        this.addTokenHeader(request, token.accessToken)
                    );
                }),
                catchError((err) => {
                    this.isRefreshing = false;

                    this.tokenService.signOut();
                    return throwError(err);
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
