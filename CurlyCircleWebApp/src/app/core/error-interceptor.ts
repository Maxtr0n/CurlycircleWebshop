import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthService) {}

    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError((error) => {
                if (error.status === 0) {
                    // A client-side or network error occurred. Handle it accordingly.
                    console.error('An error occurred:', error.error);
                } else if (error.status === 401) {
                    // try refresh if 401 response returned from api
                } else if (
                    error.status === 400 &&
                    error.message === 'Refresh attempt failed.'
                ) {
                    //please log in again
                    this.authenticationService.logout();
                }

                const err = error.error.message || error.statusText;
                return throwError(
                    () =>
                        new Error(
                            'Something bad happened; please try again later.'
                        )
                );
            })
        );
    }
}
