import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';
import { ProductCategoriesViewModel, ProductCategoryViewModel } from 'src/app/models/models';
import { ProductCategoryService } from 'src/app/services/product-category.service';

@Component({
    selector: 'app-product-categories',
    templateUrl: './product-categories.component.html',
    styleUrls: ['./product-categories.component.scss']
})
export class ProductCategoriesComponent implements OnInit {
    productCategories: ProductCategoryViewModel[] = [];

    constructor(
        private readonly productCategoryService: ProductCategoryService,
        private readonly router: Router,
        private readonly route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.getData();
    }

    private getData(): void {
        this.productCategoryService.getProductCategories().subscribe({
            next: (productCategoriesViewModel) => {
                this.productCategories = productCategoriesViewModel.productCategories;
            }
        });
    }

    onProductCategoryClicked(id: number): void {
        this.router.navigate([id], { relativeTo: this.route });
        console.log('clicked: ' + id);
    }

}
