import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductCategoryViewModel, ProductCategoryWithThumbnail } from 'src/app/models/models';
import { ProductCategoryService } from 'src/app/services/product-category.service';
import { AddProductCategoryDialogComponent } from '../add-product-category-dialog/add-product-category-dialog.component';

@Component({
    selector: 'app-modify-product-category-dialog',
    templateUrl: './modify-product-category-dialog.component.html',
    styleUrls: ['./modify-product-category-dialog.component.scss']
})
export class ModifyProductCategoryDialogComponent implements OnInit {
    productCategory: ProductCategoryViewModel | null = null;

    productCategoryForm = new FormGroup({
        name: new FormControl<string | null>('', [Validators.required]),
        description: new FormControl<string | null>(''),
        imageFile: new FormControl<File | null>(null)
    });

    constructor(
        public dialogRef: MatDialogRef<AddProductCategoryDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { id: number; },
        private readonly productCategoryService: ProductCategoryService,
    ) { }

    ngOnInit(): void {
        this.productCategoryService.getProductCategory(this.data.id)
            .subscribe({
                next: (productCategoryViewModel: ProductCategoryViewModel) => {
                    this.productCategory = productCategoryViewModel;
                    this.setFormDefaults(productCategoryViewModel);
                }
            });
    }

    clickModify(): void {
        let productCategory: ProductCategoryWithThumbnail = {
            name: this.productCategoryForm.value.name as string,
            description: this.productCategoryForm.value.description ?? null,
            thumbnailImage: this.productCategoryForm.value.imageFile ?? null
        };

        this.dialogRef.close(productCategory);
    }

    setFormDefaults(productCategory: ProductCategoryViewModel | null) {
        this.productCategoryForm.setValue({
            name: productCategory?.name ?? '',
            description: productCategory?.description ?? '',
            imageFile: null
        });

        this.productCategoryForm.markAsPristine();
    }

}
