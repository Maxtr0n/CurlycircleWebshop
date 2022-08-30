import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs';
import { ColorsViewModel, ColorViewModel, MaterialsViewModel, MaterialViewModel, PatternsViewModel, PatternViewModel } from 'src/app/models/models';
import { ColorService } from 'src/app/services/color.service';
import { MaterialService } from 'src/app/services/material.service';
import { PatternService } from 'src/app/services/pattern.service';

@Component({
    selector: 'app-product-filters',
    templateUrl: './product-filters.component.html',
    styleUrls: ['./product-filters.component.scss']
})
export class ProductFiltersComponent implements OnInit {
    colors: ColorViewModel[] = [];
    patterns: PatternViewModel[] = [];
    materials: MaterialViewModel[] = [];

    constructor(
        private readonly colorService: ColorService,
        private readonly materialService: MaterialService,
        private readonly patternService: PatternService,
    ) { }

    ngOnInit(): void {
        this.getData();
    }

    private getData(): void {
        this.colorService.getColors().subscribe({
            next: (result: ColorsViewModel) => {
                this.colors = result.colors;
            }
        });

        this.patternService.getPatterns().subscribe({
            next: (result: PatternsViewModel) => {
                this.patterns = result.patterns;
            }
        });

        this.materialService.getMaterials().subscribe({
            next: (result: MaterialsViewModel) => {
                this.materials = result.materials;
            }
        });
    }
}

