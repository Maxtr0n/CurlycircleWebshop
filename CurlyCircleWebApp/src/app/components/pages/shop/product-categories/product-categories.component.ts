import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';
import { tap } from 'rxjs';
import { AddProductCategoryDialogComponent } from 'src/app/components/dialogs/add-product-category-dialog/add-product-category-dialog.component';
import { DeleteProductCategoryDialogComponent } from 'src/app/components/dialogs/delete-product-category-dialog/delete-product-category-dialog.component';
import { ModifyProductCategoryDialogComponent } from 'src/app/components/dialogs/modify-product-category-dialog/modify-product-category-dialog.component';
import { AppConstants } from 'src/app/core/app-constants';
import { ProductCategoriesViewModel, ProductCategoryUpsertDto, ProductCategoryViewModel, ProductCategoryWithThumbnail } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { ProductCategoryService } from 'src/app/services/product-category.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
    selector: 'app-product-categories',
    templateUrl: './product-categories.component.html',
    styleUrls: ['./product-categories.component.scss']
})
export class ProductCategoriesComponent implements OnInit {
    isAdmin: boolean = false;
    productCategories: ProductCategoryViewModel[] = [];

    constructor(
        private readonly productCategoryService: ProductCategoryService,
        private readonly router: Router,
        private readonly route: ActivatedRoute,
        private readonly snackBar: MatSnackBar,
        private readonly breadcrumbService: BreadcrumbService,
        private readonly authService: AuthService,
        private readonly dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.breadcrumbService.set('@ProductCategories', 'Kezdőoldal');
        this.authService.isAdmin$.subscribe((isAdmin) => {
            this.isAdmin = isAdmin;
        });
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

    onProductCategoryModifyClicked(id: number): void {
        let dialogRef = this.dialog.open(ModifyProductCategoryDialogComponent, {
            width: '600px',
            data: { id: id }
        });
        dialogRef.afterClosed().subscribe({
            next: (result: ProductCategoryWithThumbnail) => {
                if (result) {
                    this.productCategoryService.updateProductCategory(id, result)
                        .subscribe({
                            next: () => {
                                this.snackBar.open(result.name + "termék kategória sikeresen módosítva!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-accent'] });
                                this.getData();
                            }
                        });
                }
            }
        });
    }

    onProductCategoryDeleteClicked(id: number): void {
        let dialogRef = this.dialog.open(DeleteProductCategoryDialogComponent, {
            width: '600px',
            data: { id: id }
        });
        dialogRef.afterClosed().subscribe({
            next: (result) => {
                if (result) {
                    this.productCategoryService.deleteProductCategory(id).pipe(
                        tap(() => {
                            this.snackBar.open("A termék kategória törölve!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-accent'] });
                            this.getData();
                        })
                    ).subscribe();
                }
            }
        });
    }

    onProductCategoryAddClicked(): void {
        let dialogRef = this.dialog.open(AddProductCategoryDialogComponent, {
            width: '600px',
        });
        dialogRef.afterClosed().subscribe({
            next: (result: ProductCategoryWithThumbnail) => {
                if (result) {
                    this.productCategoryService.createProductCategory(result).pipe(
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
