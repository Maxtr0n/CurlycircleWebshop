import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription, switchMap, tap, of } from 'rxjs';
import { AppConstants } from 'src/app/core/app-constants';
import { OrderViewModel } from 'src/app/models/models';
import { OrderService } from 'src/app/services/order.service';
import { UserService } from 'src/app/services/user.service';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-admin-order-details',
    templateUrl: './admin-order-details.component.html',
    styleUrls: ['./admin-order-details.component.scss']
})
export class AdminOrderDetailsComponent implements OnInit, OnDestroy {
    imagesBaseUrl: string = environment.baseUrl + AppConstants.PRODUCT_THUMBNAILS_URL;
    noImageUrl: string = environment.baseUrl + AppConstants.NO_IMAGE_URL;
    order$: Subscription = new Subscription;
    order: OrderViewModel | null = null;
    orderUserEmail: string | null = null;

    constructor(
        private readonly orderService: OrderService,
        private readonly userService: UserService,
        private readonly router: Router,
        private readonly snackBar: MatSnackBar,
        private readonly route: ActivatedRoute,
    ) { }



    ngOnInit(): void {
        const order$ = this.route.params.pipe(
            switchMap(params => this.orderService.getOrder(params['orderId'])),
        );

        order$.pipe(
            switchMap(order => {
                this.order = order;
                if (order.userId) {
                    return this.userService.getUserData(order.userId);
                }
                return of(null);
            }),
        ).subscribe({
            next: userData => {
                this.orderUserEmail = userData?.email;
            }
        });
    }

    ngOnDestroy(): void {
        this.order$.unsubscribe();
    }
}

