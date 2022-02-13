import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { faFacebook, faInstagram, faYoutube } from '@fortawesome/free-brands-svg-icons';
import { tap } from 'rxjs/operators';
import { AuthService } from 'src/app/core/auth.service';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { UserViewModel } from 'src/app/models/models';

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html',
    styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent extends UnsubscribeOnDestroy implements OnInit {
    isExpanded = false;

    currentUser: UserViewModel | null = null;

    faFacebook = faFacebook;
    faInstagram = faInstagram;
    faYoutube = faYoutube;

    constructor(
        private readonly authService: AuthService,
        private readonly router: Router
    ) {
        super();
        this.subscribe(this.authService.currentUser.pipe(
            tap(user => this.currentUser = user)
        ));
    }

    ngOnInit(): void { }

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }

    logout() {
        this.authService.logout();
        this.router.navigate(['']);
    }

}
