import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, switchMap, tap } from 'rxjs';
import { AddProductDialogComponent } from 'src/app/components/dialogs/add-product-dialog/add-product-dialog.component';
import { DeleteProductDialogComponent } from 'src/app/components/dialogs/delete-product-dialog/delete-product-dialog.component';
import { ModifyProductDialogComponent } from 'src/app/components/dialogs/modify-product-dialog/modify-product-dialog.component';
import { AppConstants } from 'src/app/core/app-constants';
import { ProductCategoryViewModel, ProductViewModel, ProductWithImages } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { ProductCategoryService } from 'src/app/services/product-category.service';
import { ProductService } from 'src/app/services/product.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit, OnDestroy {
    isAdmin: boolean = false;
    productCategory: ProductCategoryViewModel | null = null;
    products: ProductViewModel[] | null = [];
    productCategory$: Subscription = new Subscription;
    products$: Subscription = new Subscription;

    constructor(
        private readonly productCategoryService: ProductCategoryService,
        private readonly productService: ProductService,
        private readonly router: Router,
        private readonly route: ActivatedRoute,
        private readonly snackBar: MatSnackBar,
        private readonly breadcrumbService: BreadcrumbService,
        private readonly authService: AuthService,
        private readonly dialog: MatDialog
    ) { }


    ngOnInit(): void {
        this.breadcrumbService.set('@ProductCategories', 'Kategóriák');
        this.breadcrumbService.set('@Products', 'Kategória');
        this.authService.isAdmin$.subscribe((isAdmin) => {
            this.isAdmin = isAdmin;
        });
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
            switchMap(params => this.productService.getProductPage(params['productCategoryId']))
        ).subscribe({
            next: (productsViewModel) => {
                console.log(productsViewModel.products);
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

    onProductModifyClicked(id: number): void {
        let dialogRef = this.dialog.open(ModifyProductDialogComponent, {
            width: '600px',
            data: { id: id }
        });
        dialogRef.afterClosed().subscribe({
            next: (result: ProductWithImages) => {
                if (result) {
                    this.productService.updateProduct(id, result)
                        .subscribe({
                            next: () => {
                                this.snackBar.open(result.name + "termék sikeresen módosítva!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-accent'] });
                                this.getData();
                            }
                        });
                }
            }
        });
    }

    onProductDeleteClicked(id: number): void {
        let dialogRef = this.dialog.open(DeleteProductDialogComponent, {
            width: '600px',
            data: { id: id }
        });
        dialogRef.afterClosed().subscribe({
            next: (result) => {
                if (result) {
                    this.productService.deleteProduct(id).pipe(
                        tap(() => {
                            this.snackBar.open("A termék törölve!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-accent'] });
                            this.getData();
                        })
                    ).subscribe();
                }
            }
        });
    }

    onProductAddClicked(): void {
        let dialogRef = this.dialog.open(AddProductDialogComponent, {
            width: '600px',
            data: { productCategoryId: this.productCategory?.id }
        });
        dialogRef.afterClosed().subscribe({
            next: (result: ProductWithImages) => {
                if (result) {
                    this.productService.createProduct(result).pipe(
                        tap(() => {
                            this.snackBar.open(result.name + " hozzáadva!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-accent'] });
                            this.getData();
                        })
                    ).subscribe();
                }
            }
        });
    }
}
