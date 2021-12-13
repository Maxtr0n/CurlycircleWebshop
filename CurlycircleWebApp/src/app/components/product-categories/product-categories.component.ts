import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map, tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { ProductCategoryViewModel } from 'src/app/models/models';
import { ProductCategoryService } from 'src/app/services/product-category.service';

@Component({
    selector: 'app-product-categories',
    templateUrl: './product-categories.component.html',
    styleUrls: ['./product-categories.component.css']
})
export class ProductCategoriesComponent extends UnsubscribeOnDestroy implements OnInit {

    productCategories: ProductCategoryViewModel[] = [];


    constructor(
        private readonly productCategoryService: ProductCategoryService,
        private readonly router: Router,
    ) {
        super();
    }

    ngOnInit(): void {
        this.getData();
    }

    private getData() {
        this.subscribe(this.productCategoryService.getProductCategories().pipe(
            map((response) => response.productCategories),
            tap((productCategories) => this.productCategories = productCategories)
        ));
    }

    onProductCategoryClicked(productCategoryId: number): void {
        this.router.navigate(['/productCategories', productCategoryId]);
    }

}
