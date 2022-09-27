import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AppConstants } from 'src/app/core/app-constants';
import { ProductCategoryViewModel } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-product-category-card',
    templateUrl: './product-category-card.component.html',
    styleUrls: ['./product-category-card.component.scss']
})
export class ProductCategoryCardComponent implements OnInit {

    isAdmin: boolean = false;
    imagesBaseUrl: string = environment.baseUrl + AppConstants.PRODUCT_CATEGORY_THUMBNAILS_URL;
    noImageUrl: string = environment.baseUrl + AppConstants.NO_IMAGE_URL;

    @Input()
    item!: ProductCategoryViewModel;

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
