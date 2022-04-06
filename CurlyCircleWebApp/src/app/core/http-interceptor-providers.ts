import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { ErrorInterceptor } from './error-interceptor';
import { TokenInterceptor } from './token-interceptor';

/** Http interceptor providers in outside-in order */
export const httpInterceptorProviders = [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
];