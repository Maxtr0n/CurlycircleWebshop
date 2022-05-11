import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserViewModel } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
    @Output() public sidenavToggle = new EventEmitter();

    currentUser: UserViewModel | null = null;

    constructor(
        private readonly authService: AuthService,
        private readonly router: Router
    ) {
        authService.currentUser$.subscribe((user) => {
            this.currentUser = user;
        });
    }

    ngOnInit(): void { }

    public onToggleSidenav = () => {
        this.sidenavToggle.emit();
    };
}
