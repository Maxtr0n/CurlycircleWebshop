import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, switchMap, tap } from 'rxjs/operators';
import { UnsubscribeOnDestroy } from 'src/app/core/UnsubscribeOnDestroy';
import { OrderItemUpsertDto, OrderItemViewModel, ProductViewModel } from 'src/app/models/models';
import { CartService } from 'src/app/services/cart.service';
import { ProductCategoryService } from 'src/app/services/product-category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
    selector: 'app-product-details',
    templateUrl: './product-details.component.html',
    styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent extends UnsubscribeOnDestroy implements OnInit {
    product: ProductViewModel | undefined;

    constructor(
        private readonly productCategoryService: ProductCategoryService,
        private readonly productService: ProductService,
        private readonly router: Router,
        private readonly route: ActivatedRoute,
        private readonly cartService: CartService,
        private readonly toast: ToastrService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.getData();
    }

    private getData() {
        const product$ = this.route.params.pipe(
            switchMap((params => this.productService.getProduct(params['id'])))
        );
        this.subscribe(product$.pipe(
            switchMap((product) => this.productService.getProduct(product.id)),
            tap((product) => this.product = product),
        ));
    }

    addProductToCart(product: ProductViewModel) {
        const orderItem: OrderItemUpsertDto = {
            orderId: 0,
            productId: product.id,
            price: product.price,
            quantity: 1
        }
        this.cartService.addToCart(orderItem);
        this.toast.success('Termék hozzáadva a kosárhoz!');
    }

}
