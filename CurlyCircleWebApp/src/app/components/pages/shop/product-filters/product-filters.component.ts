import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl } from '@angular/forms';
import { debounce, debounceTime, interval, Subscription, tap } from 'rxjs';
import { ColorsViewModel, ColorViewModel, MaterialsViewModel, MaterialViewModel, PatternsViewModel, PatternViewModel } from 'src/app/models/models';
import { ColorService } from 'src/app/services/color.service';
import { FilterService } from 'src/app/services/filter.service';
import { MaterialService } from 'src/app/services/material.service';
import { PatternService } from 'src/app/services/pattern.service';

@Component({
    selector: 'app-product-filters',
    templateUrl: './product-filters.component.html',
    styleUrls: ['./product-filters.component.scss']
})
export class ProductFiltersComponent implements OnInit, OnDestroy {
    colors: ColorViewModel[] = [];
    patterns: PatternViewModel[] = [];
    materials: MaterialViewModel[] = [];

    colorsValueChanges$: Subscription = new Subscription;
    materialsValueChanges$: Subscription = new Subscription;
    patternsValueChanges$: Subscription = new Subscription;
    pricesValueChanges$: Subscription = new Subscription;

    filterForm = this.fb.group({
        colorsFormArray: this.fb.array([]),
        patternsFormArray: this.fb.array([]),
        materialsFormArray: this.fb.array([]),
        priceValues: this.fb.control<number[]>([0, 20000])
    });

    constructor(
        private readonly colorService: ColorService,
        private readonly materialService: MaterialService,
        private readonly patternService: PatternService,
        private readonly filterService: FilterService,
        private readonly fb: FormBuilder,
    ) { }


    ngOnDestroy(): void {
        this.clearFilters();
        this.colorsValueChanges$.unsubscribe();
        this.materialsValueChanges$.unsubscribe();
        this.patternsValueChanges$.unsubscribe();
        this.pricesValueChanges$.unsubscribe();
    }

    ngOnInit(): void {
        this.getData();

        //subscribe to price range slider changes
        this.pricesValueChanges$ = this.filterForm.controls.priceValues.valueChanges.subscribe({
            next: (newValue) => {
                if (!newValue) {
                    return;
                }
                this.filterService.updateSelectedPrices(newValue);
            }
        });
    }

    get colorsFormArray() {
        return this.filterForm.controls.colorsFormArray as FormArray;
    }

    get patternsFormArray() {
        return this.filterForm.controls.patternsFormArray as FormArray;
    }

    get materialsFormArray() {
        return this.filterForm.controls.materialsFormArray as FormArray;
    }

    get priceValues() {
        return this.filterForm.controls.priceValues as FormControl;
    }

    private getData(): void {
        this.colorService.getColors().subscribe({
            next: (result: ColorsViewModel) => {
                this.colors = result.colors;

                //build checkbox form based on http result
                result.colors.forEach(() => this.colorsFormArray.push(new FormControl(false)));

                //subscribe to get checbox changes
                this.colorsValueChanges$ = this.filterForm.controls.colorsFormArray.valueChanges.subscribe({
                    next: (newValue) => {
                        const selectedColors: number[] = newValue.map((checked, i) => checked ? this.colors[i].id : 0).filter(v => v !== 0);
                        this.filterService.updateSelectedColors(selectedColors);
                    }
                });
            }
        });

        this.patternService.getPatterns().subscribe({
            next: (result: PatternsViewModel) => {
                this.patterns = result.patterns;

                //build checkbox form based on http result
                result.patterns.forEach(() => this.patternsFormArray.push(new FormControl(false)));

                //subscribe to get checbox changes
                this.patternsValueChanges$ = this.filterForm.controls.patternsFormArray.valueChanges.subscribe({
                    next: (newValue) => {
                        const selectedPatterns: number[] = newValue.map((checked, i) => checked ? this.patterns[i].id : 0).filter(v => v !== 0);
                        this.filterService.updateSelectedPatterns(selectedPatterns);
                    }
                });
            }
        });

        this.materialService.getMaterials().subscribe({
            next: (result: MaterialsViewModel) => {
                this.materials = result.materials;

                //build checkbox form based on http result
                result.materials.forEach(() => this.materialsFormArray.push(new FormControl(false)));

                //subscribe to get checbox changes
                this.materialsValueChanges$ = this.filterForm.controls.materialsFormArray.valueChanges.subscribe({
                    next: (newValue) => {
                        const selectedMaterials: number[] = newValue.map((checked, i) => checked ? this.materials[i].id : 0).filter(v => v !== 0);
                        this.filterService.updateSelectedMaterials(selectedMaterials);
                    }
                });
            }
        });
    }

    public clearFilters(): void {
        this.colorsFormArray.reset();
        this.materialsFormArray.reset();
        this.patternsFormArray.reset();
        this.priceValues.setValue([0, 20000]);
    }
}

