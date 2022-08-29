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
        const isAdmin = this.authService.isAdminValue;

        if (isAdmin) {
            return true;
        }

        this.router.navigate(['login']);
        return false;
    }
}
