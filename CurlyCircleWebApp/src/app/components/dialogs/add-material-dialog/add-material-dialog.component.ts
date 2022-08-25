import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ColorUpsertDto, MaterialUpsertDto } from 'src/app/models/models';
import { AddColorDialogComponent } from '../add-color-dialog/add-color-dialog.component';

@Component({
    selector: 'app-add-material-dialog',
    templateUrl: './add-material-dialog.component.html',
    styleUrls: ['./add-material-dialog.component.scss']
})
export class AddMaterialDialogComponent implements OnInit {
    materialForm = new FormGroup({
        name: new FormControl<string | null>('', [Validators.required])
    });

    constructor(public dialogRef: MatDialogRef<AddColorDialogComponent>) { }

    ngOnInit(): void {
    }

    clickAdd(): void {
        let material: MaterialUpsertDto = {
            name: this.materialForm.value.name as string,
        };
        this.dialogRef.close(material);
    }
}
