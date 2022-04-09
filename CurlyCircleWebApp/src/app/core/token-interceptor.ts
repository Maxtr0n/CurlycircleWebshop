import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root',
})
export class TokenInterceptor implements HttpInterceptor {
    constructor(private readonly authService: AuthService) {}

    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        const authToken = this.authService.getToken();
        if (authToken != null) {
            const authReq = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${authToken.accessToken}`,
                },
            });

            return next.handle(authReq);
        }

        return next.handle(request);
    }
}
