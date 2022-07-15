import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProductCategoryViewModel } from 'src/app/models/models';
import { ProductCategoryService } from 'src/app/services/product-category.service';

@Component({
    selector: 'app-delete-product-category-dialog',
    templateUrl: './delete-product-category-dialog.component.html',
    styleUrls: ['./delete-product-category-dialog.component.scss']
})
export class DeleteProductCategoryDialogComponent implements OnInit {
    productCategoryName: string = '';

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: { id: number; },
        private readonly productCategoryService: ProductCategoryService,
    ) { }

    ngOnInit(): void {
        this.productCategoryService.getProductCategory(this.data.id)
            .subscribe({
                next: (productCategoryViewModel: ProductCategoryViewModel) => {
                    this.productCategoryName = productCategoryViewModel.name;
                }
            });
    }

}
