import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ColorService } from 'src/app/services/color.service';
import { MaterialService } from 'src/app/services/material.service';
import { PatternService } from 'src/app/services/pattern.service';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { ColorsViewModel, ColorUpsertDto, ColorViewModel, MaterialsViewModel, MaterialViewModel, PatternsViewModel, PatternViewModel } from 'src/app/models/models';

@Component({
    selector: 'app-admin-colors-patterns-materials',
    templateUrl: './admin-colors-patterns-materials.component.html',
    styleUrls: ['./admin-colors-patterns-materials.component.scss']
})
export class AdminColorsPatternsMaterialsComponent implements OnInit {
    colors: ColorViewModel[] = [];
    materials: MaterialViewModel[] = [];
    patterns: PatternViewModel[] = [];

    addOnBlur = true;
    readonly separatorKeysCodes = [ENTER, COMMA] as const;

    constructor(
        private readonly dialog: MatDialog,
        private readonly snackBar: MatSnackBar,
        private readonly colorService: ColorService,
        private readonly patternService: PatternService,
        private readonly materialService: MaterialService
    ) { }

    ngOnInit(): void {
        this.getData();
    }

    private getData(): void {
        this.colorService.getColors()
            .subscribe({
                next: (colors: ColorsViewModel) => {
                    this.colors = colors.colors;
                }
            });
        this.patternService.getPatterns()
            .subscribe({
                next: (patterns: PatternsViewModel) => {
                    this.patterns = patterns.patterns;
                }
            });
        this.materialService.getMaterials()
            .subscribe({
                next: (materials: MaterialsViewModel) => {
                    this.materials = materials.materials;
                }
            });
    }

    addColor(event: MatChipInputEvent): void {
        const value = (event.value || '').trim();
        const color: ColorUpsertDto = {
            name: value,
        };
        console.log(color);

        if (value) {
            this.colorService.createColor(color)
                .subscribe({
                    next: (color: ColorViewModel) => {
                        this.snackBar.open(`${color.name} sikeresen hozzáadva!`, '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'] });
                        this.getData();
                    }
                });
        }

        event.chipInput!.clear();
    }

    removeColor(color: ColorViewModel): void {
        this.colorService.deleteColor(color.id)
            .subscribe({
                next: () => {
                    this.snackBar.open(`${color.name} sikeresen törölve!`, '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-primary'] });
                    this.getData();
                }
            });
    }

    addMaterial(event: MatChipInputEvent): void {
    }

    removeMaterial(material: MaterialViewModel): void {
    }

    addPattern(event: MatChipInputEvent): void {
    }

    removePattern(pattern: PatternViewModel): void {
    }
}
