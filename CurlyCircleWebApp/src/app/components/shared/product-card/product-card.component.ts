import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AppConstants } from 'src/app/core/app-constants';
import { ProductCategoryViewModel, ProductViewModel, Role, UserViewModel } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-product-card',
    templateUrl: './product-card.component.html',
    styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {
    isAdmin: boolean = false;
    imagesBaseUrl: string = environment.baseUrl + AppConstants.PRODUCT_THUMBNAILS_URL;
    noImageUrl: string = environment.baseUrl + AppConstants.NO_IMAGE_URL;

    @Input()
    item!: ProductViewModel;

    @Output()
    onCardClickedEvent = new EventEmitter<number>();

    @Output()
    onAdminModifyClickedEvent = new EventEmitter<number>();

    @Output()
    onAdminDeleteClickedEvent = new EventEmitter<number>();

    constructor(private readonly authService: AuthService,) { }

    ngOnInit(): void {
        this.authService.isAdmin$.subscribe((isAdmin) => {
            this.isAdmin = isAdmin;
        });
    }

    onCardClicked(id: number): void {
        this.onCardClickedEvent.emit(id);
    }

    onAdminModifyClicked(id: number): void {
        this.onAdminModifyClickedEvent.emit(id);
    }

    onAdminDeleteClicked(id: number): void {
        this.onAdminDeleteClickedEvent.emit(id);
    }
}
