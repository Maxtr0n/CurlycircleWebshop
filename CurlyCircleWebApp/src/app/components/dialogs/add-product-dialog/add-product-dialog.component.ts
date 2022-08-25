import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ColorsViewModel, ColorViewModel, MaterialsViewModel, MaterialViewModel, PatternsViewModel, PatternViewModel, ProductWithImages } from 'src/app/models/models';
import { ColorService } from 'src/app/services/color.service';
import { MaterialService } from 'src/app/services/material.service';
import { PatternService } from 'src/app/services/pattern.service';

@Component({
    selector: 'app-add-product-dialog',
    templateUrl: './add-product-dialog.component.html',
    styleUrls: ['./add-product-dialog.component.scss']
})
export class AddProductDialogComponent implements OnInit {
    materials: MaterialViewModel[] = [];
    colors: ColorViewModel[] = [];
    patterns: PatternViewModel[] = [];

    productForm = new FormGroup({
        price: new FormControl<number | null>(0, [Validators.required]),
        name: new FormControl<string | null>('', [Validators.required]),
        description: new FormControl<string | null>(''),
        thumbnail: new FormControl<File | null>(null),
        productImages: new FormControl<File[]>([]),
        colors: new FormControl<number[]>([]),
        pattern: new FormControl<number | null>(null),
        material: new FormControl<number | null>(null),
        isAvailable: new FormControl<boolean | null>(null),
    });

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: { productCategoryId: number; },
        public dialogRef: MatDialogRef<AddProductDialogComponent>,
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
    }

    clickAdd(): void {
        let product: ProductWithImages = {
            name: this.productForm.value.name as string,
            price: this.productForm.value.price ?? 0,
            productCategoryId: this.data.productCategoryId,
            description: this.productForm.value.description ?? null,
            thumbnailImage: this.productForm.value.thumbnail ?? null,
            productImages: this.productForm.value.productImages ?? [],
            colorIds: this.productForm.value.colors ?? [],
            patternId: this.productForm.value.pattern ?? null,
            materialId: this.productForm.value.material ?? null,
            isAvailable: this.productForm.value.isAvailable ?? true,
        };
        this.dialogRef.close(product);
    }

}
