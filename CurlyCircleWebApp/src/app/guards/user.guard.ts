import { Injectable } from '@angular/core';
import {
    ActivatedRouteSnapshot,
    CanActivate,
    Router,
    RouterStateSnapshot,
    UrlTree,
} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { catchError, map, Observable, of, switchMap } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root',
})
export class UserGuard implements CanActivate {
    constructor(
        private router: Router,
        private readonly authService: AuthService
    ) { }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ):
        | Observable<boolean | UrlTree>
        | Promise<boolean | UrlTree>
        | boolean
        | UrlTree {
        const jwtHelper = new JwtHelperService();
        const currentUser = this.authService.currentUserValue;

        if (currentUser !== null && !jwtHelper.isTokenExpired(currentUser.accessToken)) {
            console.log('nincs lejárva az access token');
            return true;
        }

        return this.authService.refreshToken().pipe(
            map((token) => {
                if (token) {
                    return true;
                }
                this.router.navigate(['login']);
                return false;
            }),
            catchError(() => {
                this.router.navigate(['login']);
                return of(false);
            })
        );

    }
}
