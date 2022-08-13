import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ColorService } from 'src/app/services/color.service';
import { MaterialService } from 'src/app/services/material.service';
import { PatternService } from 'src/app/services/pattern.service';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { ColorsViewModel, ColorUpsertDto, ColorViewModel, MaterialsViewModel, MaterialViewModel, PatternsViewModel, PatternViewModel } from 'src/app/models/models';
import { AddColorDialogComponent } from 'src/app/components/dialogs/add-color-dialog/add-color-dialog.component';
import { tap } from 'rxjs';
import { DeleteColorDialogComponent } from 'src/app/components/dialogs/delete-color-dialog/delete-color-dialog.component';

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

    addColorClicked(): void {
        let dialogRef = this.dialog.open(AddColorDialogComponent, {
            width: '600px',
        });

        dialogRef.afterClosed().subscribe({
            next: (color: ColorUpsertDto) => {
                if (color) {
                    this.colorService.createColor(color).pipe(
                        tap(() => {
                            this.getData();
                            this.snackBar.open(color.name + " hozzáadva", '', {
                                duration: 3000,
                                panelClass: ['mat-toolbar', 'mat-accent']
                            });
                        })
                    ).subscribe();
                }
            }
        });
    }

    deleteColorClicked(id: number): void {
        let dialogRef = this.dialog.open(DeleteColorDialogComponent, {
            width: '600px',
            data: { id: id }
        });

        dialogRef.afterClosed().subscribe({
            next: (result) => {
                if (result) {
                    this.colorService.deleteColor(id).pipe(
                        tap(() => {
                            this.snackBar.open("A szín törölve!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-accent'] });
                            this.getData();
                        })
                    ).subscribe();
                }
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
