import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';
import { AppConstants } from 'src/app/core/app-constants';
import { ProductCategoriesViewModel, ProductCategoryViewModel } from 'src/app/models/models';
import { ProductCategoryService } from 'src/app/services/product-category.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
    selector: 'app-product-categories',
    templateUrl: './product-categories.component.html',
    styleUrls: ['./product-categories.component.scss']
})
export class ProductCategoriesComponent implements OnInit {
    productCategories: ProductCategoryViewModel[] = [];
    imagesBaseUrl: string = AppConstants.IMAGES_URL;
    noImageUrl: string = AppConstants.NO_IMAGE_URL;

    constructor(
        private readonly productCategoryService: ProductCategoryService,
        private readonly router: Router,
        private readonly route: ActivatedRoute,
        private readonly snackBar: MatSnackBar,
        private readonly breadcrumbService: BreadcrumbService,
    ) { }

    ngOnInit(): void {
        this.breadcrumbService.set('@ProductCategories', 'Kategóriák');
        this.getData();
    }

    private getData(): void {
        this.productCategoryService.getProductCategories()
            .subscribe({
                next: (productCategoriesViewModel) => {
                    this.productCategories = productCategoriesViewModel.productCategories;
                },
                error: (error) => {
                    this.snackBar.open("A termék kategóriák betöltése sikertelen. Kérlek próbálkozz újra!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
                }
            });
    }

    onProductCategoryClicked(id: number): void {
        this.router.navigate([id], { relativeTo: this.route });
    }

}
