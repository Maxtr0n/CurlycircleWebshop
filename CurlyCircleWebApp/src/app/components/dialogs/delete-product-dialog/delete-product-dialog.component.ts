import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductViewModel } from 'src/app/models/models';
import { ProductService } from 'src/app/services/product.service';

@Component({
    selector: 'app-delete-product-dialog',
    templateUrl: './delete-product-dialog.component.html',
    styleUrls: ['./delete-product-dialog.component.scss']
})
export class DeleteProductDialogComponent implements OnInit {
    productName: string = '';

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: { id: number; },
        private readonly productService: ProductService,
    ) { }

    ngOnInit(): void {
        this.productService.getProduct(this.data.id)
            .subscribe({
                next: (productViewModel: ProductViewModel) => {
                    this.productName = productViewModel.name;
                }
            });
    }

}
