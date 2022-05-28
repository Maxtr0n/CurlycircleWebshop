import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, switchMap, tap } from 'rxjs';
import { ProductCategoryViewModel, ProductViewModel } from 'src/app/models/models';
import { ProductCategoryService } from 'src/app/services/product-category.service';
import { ProductService } from 'src/app/services/product.service';

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
        private readonly route: ActivatedRoute
    ) { }


    ngOnInit(): void {
        this.getData();
    }

    ngOnDestroy(): void {
        this.productCategory$.unsubscribe();
        this.products$.unsubscribe();
    }

    getData(): void {
        this.productCategory$ = this.route.params.pipe(
            switchMap(params => this.productCategoryService.getProductCategory(params['id']))
        ).subscribe({
            next: (productCategoryViewModel) => {
                this.productCategory = productCategoryViewModel;
            },
            error: (error) => {
                console.log(error);
                //TODO throw snackbar with error
            }
        });
        this.products$ = this.route.params.pipe(
            switchMap(params => this.productCategoryService.getProductCategoryProducts(params['id']))
        ).subscribe({
            next: (productsViewModel) => {
                this.products = productsViewModel.products;
                console.log(this.products);
            },
            error: (error) => {
                console.log(error);
                //TODO throw snackbar with error
            }
        });
    }

    onProductClicked(id: number): void {
        this.router.navigate(['/product', id]);
    }

}
