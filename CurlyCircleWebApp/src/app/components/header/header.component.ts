import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
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
        private readonly router: Router,
        private readonly snackBar: MatSnackBar,
    ) {
        authService.currentUser$.subscribe((user) => {
            this.currentUser = user;
        });
    }

    logout(): void {
        this.authService.logout();
        this.snackBar.open("Sikeresen kijelentkeztél.", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'] });
    }

    ngOnInit(): void { }

    public onToggleSidenav = () => {
        this.sidenavToggle.emit();
    };
}
