import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ColorUpsertDto } from 'src/app/models/models';

@Component({
    selector: 'app-add-color-dialog',
    templateUrl: './add-color-dialog.component.html',
    styleUrls: ['./add-color-dialog.component.scss']
})
export class AddColorDialogComponent implements OnInit {
    colorForm = new FormGroup({
        name: new FormControl<string | null>('', [Validators.required])
    });

    constructor(public dialogRef: MatDialogRef<AddColorDialogComponent>) { }

    ngOnInit(): void {
    }

    clickAdd(): void {
        let color: ColorUpsertDto = {
            name: this.colorForm.value.name as string,
        };
        this.dialogRef.close(color);
    }
}
