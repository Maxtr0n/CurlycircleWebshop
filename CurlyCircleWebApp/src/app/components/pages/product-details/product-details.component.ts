import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, switchMap } from 'rxjs';
import { AppConstants } from 'src/app/core/app-constants';
import { ProductViewModel } from 'src/app/models/models';
import { ProductCategoryService } from 'src/app/services/product-category.service';
import { ProductService } from 'src/app/services/product.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
    selector: 'app-product-details',
    templateUrl: './product-details.component.html',
    styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit, OnDestroy {
    productCategory$: Subscription = new Subscription;
    product$: Subscription = new Subscription;
    product: ProductViewModel | null = null;
    productImages: string[] = [];

    constructor(
        private readonly productCategoryService: ProductCategoryService,
        private readonly productService: ProductService,
        private readonly router: Router,
        private readonly route: ActivatedRoute,
        private readonly snackBar: MatSnackBar,
        private readonly breadcrumbService: BreadcrumbService,
    ) { }

    ngOnInit(): void {
        this.breadcrumbService.set('@ProductCategories', 'Kategóriák');
        this.breadcrumbService.set('@Products', 'Kategória');
        this.breadcrumbService.set('@ProductDetails', 'Termék');
        this.getData();
    }

    ngOnDestroy(): void {
        this.product$.unsubscribe();
    }

    getData(): void {
        this.productCategory$ = this.route.params.pipe(
            switchMap(params => this.productCategoryService.getProductCategory(params['productCategoryId']))
        ).subscribe({
            next: (productCategoryViewModel) => {
                this.breadcrumbService.set('@Products', productCategoryViewModel.name);
            }
        });
        this.product$ = this.route.params.pipe(
            switchMap(params => this.productService.getProduct(params['productId']))
        ).subscribe({
            next: (productViewModel) => {
                console.log(productViewModel);
                if (productViewModel.imageUrls.length > 0) {
                    productViewModel.imageUrls = productViewModel.imageUrls.map((imageUrl) => {
                        return AppConstants.IMAGES_URL.concat(imageUrl);
                    });
                } else {
                    productViewModel.imageUrls = [AppConstants.NO_IMAGE_URL];
                }
                this.productImages = productViewModel.imageUrls;
                this.product = productViewModel;
                this.breadcrumbService.set('@ProductDetails', productViewModel.name);
            },
            error: () => {
                this.snackBar.open("A termék betöltése sikertelen. Kérlek próbálkozz újra!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
            }
        });
    }
}
