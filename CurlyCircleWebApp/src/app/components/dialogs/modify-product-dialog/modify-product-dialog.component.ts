import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ColorsViewModel, ColorViewModel, MaterialsViewModel, MaterialViewModel, PatternsViewModel, PatternViewModel, ProductCategoryViewModel, ProductViewModel, ProductWithImages } from 'src/app/models/models';
import { ColorService } from 'src/app/services/color.service';
import { MaterialService } from 'src/app/services/material.service';
import { PatternService } from 'src/app/services/pattern.service';
import { ProductCategoryService } from 'src/app/services/product-category.service';
import { ProductService } from 'src/app/services/product.service';
import { AddProductCategoryDialogComponent } from '../add-product-category-dialog/add-product-category-dialog.component';

@Component({
    selector: 'app-modify-product-dialog',
    templateUrl: './modify-product-dialog.component.html',
    styleUrls: ['./modify-product-dialog.component.scss']
})
export class ModifyProductDialogComponent implements OnInit {
    product: ProductViewModel | null = null;

    materials: MaterialViewModel[] = [];
    colors: ColorViewModel[] = [];
    patterns: PatternViewModel[] = [];

    productForm = new FormGroup({
        price: new FormControl<number | null>(null, [Validators.required]),
        name: new FormControl<string | null>('', [Validators.required]),
        description: new FormControl<string | null>(''),
        thumbnail: new FormControl<File | null>(null),
        productImages: new FormControl<File[]>([]),
        colorIds: new FormControl<number[]>([]),
        patternId: new FormControl<number | null>(null),
        materialId: new FormControl<number | null>(null),
        isAvailable: new FormControl<boolean>(true),
    });

    constructor(
        public dialogRef: MatDialogRef<AddProductCategoryDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { id: number; },
        private readonly productService: ProductService,
        private readonly colorService: ColorService,
        private readonly materialService: MaterialService,
        private readonly patternService: PatternService
    ) { }

    ngOnInit(): void {
        this.materialService.getMaterials()
            .subscribe({
                next: (materialsViewModel: MaterialsViewModel) => {
                    this.materials = materialsViewModel.materials;
                }
            });
        this.colorService.getColors()
            .subscribe({
                next: (colorsViewModel: ColorsViewModel) => {
                    this.colors = colorsViewModel.colors;
                }
            });
        this.patternService.getPatterns()
            .subscribe({
                next: (patternsViewModel: PatternsViewModel) => {
                    this.patterns = patternsViewModel.patterns;
                }
            });

        this.productService.getProduct(this.data.id)
            .subscribe({
                next: (productViewModel: ProductViewModel) => {
                    this.product = productViewModel;
                    this.setFormDefaults(productViewModel);
                }
            });
    }

    clickModify(): void {
        let product: ProductWithImages = {
            name: this.productForm.value.name as string,
            price: this.productForm.value.price ?? 0,
            productCategoryId: this.product?.productCategoryId ?? 0,
            description: this.productForm.value.description ?? null,
            thumbnailImage: this.productForm.value.thumbnail ?? null,
            productImages: this.productForm.value.productImages ?? [],
            colorIds: this.productForm.value.colorIds ?? [],
            patternId: this.productForm.value.patternId ?? null,
            materialId: this.productForm.value.materialId ?? null,
            isAvailable: this.productForm.value.isAvailable ?? true,
        };

        this.dialogRef.close(product);
    }

    setFormDefaults(product: ProductViewModel) {
        let selectedColorIds: number[] = [];

        for (let color of product.colors) {
            selectedColorIds.push(color.id);
        }

        this.productForm.setValue({
            price: product.price,
            name: product.name,
            description: product.description,
            thumbnail: null,
            productImages: null,
            colorIds: selectedColorIds,
            patternId: product.pattern?.id ?? null,
            materialId: product.material?.id ?? null,
            isAvailable: product.isAvailable
        });

        this.productForm.markAsPristine();
    }

}
