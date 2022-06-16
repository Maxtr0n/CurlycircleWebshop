import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-order-options',
    templateUrl: './order-options.component.html',
    styleUrls: ['./order-options.component.scss']
})
export class OrderOptionsComponent implements OnInit {

    constructor(
        private readonly router: Router,
    ) { }

    ngOnInit(): void {
    }

    public checkout(): void {
        this.router.navigate(['/order']);
    }

    public registration(): void {
        this.router.navigate(['/registration']);
    }

    public login(): void {
        this.router.navigate(['/login']);
    }

}
