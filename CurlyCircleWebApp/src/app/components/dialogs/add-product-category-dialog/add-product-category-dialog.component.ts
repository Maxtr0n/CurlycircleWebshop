import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ProductCategoryUpsertDto, ProductCategoryWithThumbnail } from 'src/app/models/models';

@Component({
    selector: 'app-add-product-category-dialog',
    templateUrl: './add-product-category-dialog.component.html',
    styleUrls: ['./add-product-category-dialog.component.scss']
})
export class AddProductCategoryDialogComponent implements OnInit {
    productCategoryForm = new FormGroup({
        name: new FormControl<string | null>('', [Validators.required]),
        description: new FormControl<string | null>(''),
        imageFile: new FormControl<File | null>(null)
    });

    constructor(public dialogRef: MatDialogRef<AddProductCategoryDialogComponent>) { }

    ngOnInit(): void {
    }

    clickAdd(): void {
        let productCategory: ProductCategoryWithThumbnail = {
            name: this.productCategoryForm.value.name as string,
            description: this.productCategoryForm.value.description ?? null,
            thumbnailImage: this.productCategoryForm.value.imageFile ?? null
        };
        console.log(productCategory);
        this.dialogRef.close(productCategory);
    }

}
