import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, switchMap, tap } from 'rxjs';
import { AppConstants } from 'src/app/core/app-constants';
import { ProductCategoryViewModel, ProductViewModel } from 'src/app/models/models';
import { ProductCategoryService } from 'src/app/services/product-category.service';
import { ProductService } from 'src/app/services/product.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit, OnDestroy {
    productCategory: ProductCategoryViewModel | null = null;
    products: ProductViewModel[] | null = [];
    productCategory$: Subscription = new Subscription;
    products$: Subscription = new Subscription;

    constructor(
        private readonly productCategoryService: ProductCategoryService,
        private readonly router: Router,
        private readonly route: ActivatedRoute,
        private readonly snackBar: MatSnackBar,
        private readonly breadcrumbService: BreadcrumbService,
    ) { }


    ngOnInit(): void {
        this.breadcrumbService.set('@ProductCategories', 'Kategóriák');
        this.breadcrumbService.set('@Products', 'Kategória');
        this.getData();
    }

    ngOnDestroy(): void {
        this.productCategory$.unsubscribe();
        this.products$.unsubscribe();
    }

    getData(): void {
        this.productCategory$ = this.route.params.pipe(
            switchMap(params => this.productCategoryService.getProductCategory(params['productCategoryId']))
        ).subscribe({
            next: (productCategoryViewModel) => {
                this.productCategory = productCategoryViewModel;
                this.breadcrumbService.set('@Products', productCategoryViewModel.name);
            }
        });
        this.products$ = this.route.params.pipe(
            switchMap(params => this.productCategoryService.getProductCategoryProducts(params['productCategoryId']))
        ).subscribe({
            next: (productsViewModel) => {
                productsViewModel.products.forEach((productViewModel) => {
                    if (productViewModel.imageUrls.length > 0) {
                        productViewModel.imageUrls = productViewModel.imageUrls.map((imageUrl) => {
                            return AppConstants.IMAGES_URL.concat(imageUrl);
                        });
                    } else {
                        productViewModel.imageUrls = [AppConstants.NO_IMAGE_URL];
                    }
                });
                this.products = productsViewModel.products;
            },
            error: (error) => {
                console.log(error);
                this.snackBar.open("A termékek betöltése sikertelen. Kérlek próbálkozz újra!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
            }
        });
    }

    onProductClicked(id: number): void {
        this.router.navigate([id], { relativeTo: this.route });
    }

}
