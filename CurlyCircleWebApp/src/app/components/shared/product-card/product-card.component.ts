import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AppConstants } from 'src/app/core/app-constants';
import { ProductCategoryViewModel, ProductViewModel, Role, UserViewModel } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-product-card',
    templateUrl: './product-card.component.html',
    styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {
    currentUser: UserViewModel | null = null;
    isAdmin: boolean = false;
    imagesBaseUrl: string = AppConstants.IMAGES_URL;
    noImageUrl: string = AppConstants.NO_IMAGE_URL;

    @Input()
    item!: ProductViewModel | ProductCategoryViewModel;

    @Output()
    onCardClickedEvent = new EventEmitter<number>();

    constructor(private readonly authService: AuthService,) { }

    ngOnInit(): void {
        this.authService.currentUser$.subscribe((user) => {
            this.currentUser = user;
            if (user) {
                this.isAdmin = Role[user.role].toString() === Role.Admin.toString();
            } else {
                this.isAdmin = false;
            }
        });

    }

    onCardClicked(id: number): void {
        this.onCardClickedEvent.emit(id);
    }

}
