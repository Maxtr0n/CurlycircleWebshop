import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, switchMap, tap } from 'rxjs/operators';
import { AuthService } from 'src/app/core/auth.service';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { ProductCategoryViewModel, ProductViewModel } from 'src/app/models/models';
import { ProductCategoryService } from 'src/app/services/product-category.service';

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent extends UnsubscribeOnDestroy implements OnInit {
    products: ProductViewModel[] = [];
    productCategory: ProductCategoryViewModel | undefined;


    constructor(
        private readonly productCategoryService: ProductCategoryService,
        private readonly router: Router,
        private readonly route: ActivatedRoute,
    ) {
        super();
    }

    ngOnInit(): void {
        this.getData();
    }

    private getData() {
        const product$ = this.route.params.pipe(
            switchMap((params => this.productCategoryService.getProductCategory(params['id']))),
            tap((productCategory) => {
                this.productCategory = productCategory;
            }),
        );
        this.subscribe(product$.pipe(
            switchMap((productCategory) => this.productCategoryService.getProductCategoryProducts(productCategory.id)),
            map((response) => response.products),
            tap((products) => this.products = products),
        ));
    }

    onProductClicked(productId: number): void {
        this.router.navigate(['/products', productId]);
    }

}
