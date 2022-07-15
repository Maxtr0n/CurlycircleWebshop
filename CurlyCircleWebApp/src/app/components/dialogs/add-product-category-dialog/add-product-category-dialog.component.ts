import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-add-product-category-dialog',
    templateUrl: './add-product-category-dialog.component.html',
    styleUrls: ['./add-product-category-dialog.component.scss']
})
export class AddProductCategoryDialogComponent implements OnInit {
    productCategoryForm = new FormGroup({
        name: new FormControl<string | null>('', [Validators.required]),
        description: new FormControl<string | null>(''),
        imageUrls: new FormControl<string | null>('')
    });

    constructor() { }

    ngOnInit(): void {
    }

}
