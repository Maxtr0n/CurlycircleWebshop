import { Injectable } from '@angular/core';
import {
    ActivatedRouteSnapshot,
    CanActivate,
    Router,
    RouterStateSnapshot,
    UrlTree,
} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { catchError, map, Observable, of } from 'rxjs';
import { Role } from '../models/models';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root',
})
export class AdminGuard implements CanActivate {
    constructor(
        private readonly router: Router,
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

        if (currentUser !== null && currentUser.role === Role.Admin && !jwtHelper.isTokenExpired(currentUser.accessToken)) {
            console.log('nincs lejárva az access token');
            return true;
        }

        return this.authService.refreshToken().pipe(
            map((token) => {
                if (token) {
                    return true;
                }
                this.authService.logout();
                this.router.navigate(['login']);
                return false;
            }),
            catchError(() => {
                this.authService.logout();
                this.router.navigate(['login']);
                return of(false);
            })
        );
    }
}
