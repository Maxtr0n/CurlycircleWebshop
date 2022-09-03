import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { merge, Subscription, switchMap, tap } from 'rxjs';
import { AddProductDialogComponent } from 'src/app/components/dialogs/add-product-dialog/add-product-dialog.component';
import { DeleteProductDialogComponent } from 'src/app/components/dialogs/delete-product-dialog/delete-product-dialog.component';
import { ModifyProductDialogComponent } from 'src/app/components/dialogs/modify-product-dialog/modify-product-dialog.component';
import { AppConstants } from 'src/app/core/app-constants';
import { ProductCategoryViewModel, ProductQueryParameters, ProductViewModel, ProductWithImages } from 'src/app/models/models';
import { ProductsDataSource } from 'src/app/models/ProductsDataSource';
import { AuthService } from 'src/app/services/auth.service';
import { FilterService } from 'src/app/services/filter.service';
import { ProductCategoryService } from 'src/app/services/product-category.service';
import { ProductService } from 'src/app/services/product.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit, OnDestroy, AfterViewInit {
    isAdmin: boolean = false;
    productCategory: ProductCategoryViewModel | null = null;
    products: ProductViewModel[] | null = [];
    routeParams$: Subscription = new Subscription;
    dataSource: ProductsDataSource;
    resultsLength: number = 0;

    @ViewChild(MatPaginator) paginator!: MatPaginator;

    constructor(
        private readonly productCategoryService: ProductCategoryService,
        private readonly productService: ProductService,
        private readonly router: Router,
        private readonly route: ActivatedRoute,
        private readonly snackBar: MatSnackBar,
        private readonly breadcrumbService: BreadcrumbService,
        private readonly authService: AuthService,
        private readonly filterService: FilterService,
        private readonly dialog: MatDialog
    ) {
        this.dataSource = new ProductsDataSource(this.productService);
        this.dataSource.resultsLength$.subscribe(resultsLength => this.resultsLength = resultsLength);
        this.dataSource.products$.subscribe(products => this.products = products);
    }

    ngOnInit(): void {
        this.breadcrumbService.set('@ProductCategories', 'Kategóriák');
        this.breadcrumbService.set('@Products', 'Kategória');

        this.authService.isAdmin$.subscribe((isAdmin) => {
            this.isAdmin = isAdmin;
        });

        merge(this.filterService.selectedColors$, this.filterService.selectedMaterials$, this.filterService.selectedPatterns$).pipe(
            tap(() => this.loadProductsPage())
        ).subscribe();

        this.getData();
    }

    ngAfterViewInit(): void {
        this.paginator.page.pipe(
            tap(() => this.loadProductsPage())
        ).subscribe();
    }

    ngOnDestroy(): void {
        this.routeParams$.unsubscribe();
    }

    getData(): void {
        this.routeParams$ = this.route.params.pipe(
            switchMap(params => this.productCategoryService.getProductCategory(params['productCategoryId']))
        ).subscribe({
            next: (productCategoryViewModel) => {
                this.productCategory = productCategoryViewModel;
                this.breadcrumbService.set('@Products', productCategoryViewModel.name);
                this.dataSource.loadProducts(productCategoryViewModel.id);
            },
            error: (error) => {
                console.log(error);
                this.snackBar.open("A termékek betöltése sikertelen. Kérlek próbálkozz újra!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-warn'] });
            }
        });
    }

    loadProductsPage(): void {
        if (!this.productCategory) {
            return;
        }

        this.dataSource.loadProducts(
            this.productCategory?.id,
            this.paginator.pageIndex,
            this.filterService.selectedPricesValue[0],
            this.filterService.selectedPricesValue[1],
            this.filterService.selectedColorsValue,
            this.filterService.selectedMaterialsValue,
            this.filterService.selectedPatternsValue
        );
    }

    filter(): void {
        this.paginator.pageIndex = 0;
        this.loadProductsPage();
    }

    clearFilters() {
        // TODO
        this.loadProductsPage();
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
