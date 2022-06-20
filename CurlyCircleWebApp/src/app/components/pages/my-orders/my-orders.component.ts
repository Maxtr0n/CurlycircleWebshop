import { Component, OnInit } from '@angular/core';
import { AppConstants } from 'src/app/core/app-constants';
import { OrderViewModel } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
    selector: 'app-my-orders',
    templateUrl: './my-orders.component.html',
    styleUrls: ['./my-orders.component.scss']
})
export class MyOrdersComponent implements OnInit {
    imagesBaseUrl: string = AppConstants.IMAGES_URL;
    noImageUrl: string = AppConstants.NO_IMAGE_URL;
    orders: OrderViewModel[] = [];

    constructor(
        private readonly userService: UserService,
        private readonly authService: AuthService
    ) { }

    ngOnInit(): void {
        if (this.authService.currentUserValue) {
            this.userService.getUserOrders(this.authService.currentUserValue.id).subscribe({
                next: (orders) => {
                    this.orders = orders.orders;
                }
            });
        }


    }

}
