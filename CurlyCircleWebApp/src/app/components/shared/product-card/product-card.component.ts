import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AppConstants } from 'src/app/core/app-constants';
import { ProductCategoryViewModel, ProductViewModel } from 'src/app/models/models';

@Component({
    selector: 'app-product-card',
    templateUrl: './product-card.component.html',
    styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

    imagesBaseUrl: string = AppConstants.IMAGES_URL;
    noImageUrl: string = AppConstants.NO_IMAGE_URL;

    @Input()
    item!: ProductViewModel | ProductCategoryViewModel;

    @Output()
    onCardClickedEvent = new EventEmitter<number>();

    constructor() { }

    ngOnInit(): void {
    }

    onCardClicked(id: number): void {
        this.onCardClickedEvent.emit(id);
    }

}
