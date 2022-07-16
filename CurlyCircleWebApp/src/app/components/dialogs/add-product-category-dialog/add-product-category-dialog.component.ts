import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-add-product-category-dialog',
    templateUrl: './add-product-category-dialog.component.html',
    styleUrls: ['./add-product-category-dialog.component.scss']
})
export class AddProductCategoryDialogComponent implements OnInit {
    productCategoryForm = new FormGroup({
        name: new FormControl<string | null>('', [Validators.required]),
        description: new FormControl<string | null>(''),
        imageFile: new FormControl('')
    });

    constructor(public dialogRef: MatDialogRef<AddProductCategoryDialogComponent>) { }

    ngOnInit(): void {
        this.productCategoryForm.valueChanges.subscribe((result) => {
            if (result) {
                console.log(result);
            }
        });
    }

    clickAdd(): void {
        this.dialogRef.close(this.productCategoryForm.value);
    }

}
