import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ColorService } from 'src/app/services/color.service';
import { MaterialService } from 'src/app/services/material.service';
import { PatternService } from 'src/app/services/pattern.service';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { ColorsViewModel, ColorUpsertDto, ColorViewModel, MaterialsViewModel, MaterialUpsertDto, MaterialViewModel, PatternsViewModel, PatternUpsertDto, PatternViewModel } from 'src/app/models/models';
import { AddColorDialogComponent } from 'src/app/components/dialogs/add-color-dialog/add-color-dialog.component';
import { tap } from 'rxjs';
import { DeleteColorDialogComponent } from 'src/app/components/dialogs/delete-color-dialog/delete-color-dialog.component';
import { AddMaterialDialogComponent } from 'src/app/components/dialogs/add-material-dialog/add-material-dialog.component';
import { AddPatternDialogComponent } from 'src/app/components/dialogs/add-pattern-dialog/add-pattern-dialog.component';
import { DeleteMaterialDialogComponent } from 'src/app/components/dialogs/delete-material-dialog/delete-material-dialog.component';
import { DeletePatternDialogComponent } from 'src/app/components/dialogs/delete-pattern-dialog/delete-pattern-dialog.component';

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

    addMaterialClicked(): void {
        let dialogRef = this.dialog.open(AddMaterialDialogComponent, {
            width: '600px',
        });

        dialogRef.afterClosed().subscribe({
            next: (material: MaterialUpsertDto) => {
                if (material) {
                    this.materialService.createMaterial(material).pipe(
                        tap(() => {
                            this.getData();
                            this.snackBar.open(material.name + " hozzáadva", '', {
                                duration: 3000,
                                panelClass: ['mat-toolbar', 'mat-accent']
                            });
                        })
                    ).subscribe();
                }
            }
        });
    }

    deleteMaterialClicked(id: number): void {
        let dialogRef = this.dialog.open(DeleteMaterialDialogComponent, {
            width: '600px',
            data: { id: id }
        });

        dialogRef.afterClosed().subscribe({
            next: (result) => {
                if (result) {
                    this.materialService.deleteMaterial(id).pipe(
                        tap(() => {
                            this.snackBar.open("Az anyag törölve!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-accent'] });
                            this.getData();
                        })
                    ).subscribe();
                }
            }
        });
    }

    addPatternClicked(): void {
        let dialogRef = this.dialog.open(AddPatternDialogComponent, {
            width: '600px',
        });

        dialogRef.afterClosed().subscribe({
            next: (pattern: PatternUpsertDto) => {
                if (pattern) {
                    this.patternService.createPattern(pattern).pipe(
                        tap(() => {
                            this.getData();
                            this.snackBar.open(pattern.name + " hozzáadva", '', {
                                duration: 3000,
                                panelClass: ['mat-toolbar', 'mat-accent']
                            });
                        })
                    ).subscribe();
                }
            }
        });
    }

    deletePatternClicked(id: number): void {
        let dialogRef = this.dialog.open(DeletePatternDialogComponent, {
            width: '600px',
            data: { id: id }
        });

        dialogRef.afterClosed().subscribe({
            next: (result) => {
                if (result) {
                    this.patternService.deletePattern(id).pipe(
                        tap(() => {
                            this.snackBar.open("A minta törölve!", '', { duration: 3000, panelClass: ['mat-toolbar', 'mat-accent'] });
                            this.getData();
                        })
                    ).subscribe();
                }
            }
        });
    }
}
