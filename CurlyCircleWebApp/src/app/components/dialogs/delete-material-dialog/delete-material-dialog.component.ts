import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MaterialViewModel } from 'src/app/models/models';
import { MaterialService } from 'src/app/services/material.service';

@Component({
    selector: 'app-delete-material-dialog',
    templateUrl: './delete-material-dialog.component.html',
    styleUrls: ['./delete-material-dialog.component.scss']
})
export class DeleteMaterialDialogComponent implements OnInit {
    materialName: string = '';

    constructor
        (@Inject(MAT_DIALOG_DATA) public data: { id: number; },
            private readonly materialService: MaterialService,) { }

    ngOnInit(): void {
        this.materialService.getMaterial(this.data.id)
            .subscribe({
                next: (materialViewModel: MaterialViewModel) => {
                    this.materialName = materialViewModel.name;
                }
            });
    }
}
