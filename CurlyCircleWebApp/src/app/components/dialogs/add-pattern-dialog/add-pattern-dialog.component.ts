import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MaterialUpsertDto, PatternUpsertDto } from 'src/app/models/models';
import { AddColorDialogComponent } from '../add-color-dialog/add-color-dialog.component';

@Component({
    selector: 'app-add-pattern-dialog',
    templateUrl: './add-pattern-dialog.component.html',
    styleUrls: ['./add-pattern-dialog.component.scss']
})
export class AddPatternDialogComponent implements OnInit {
    patternForm = new FormGroup({
        name: new FormControl<string | null>('', [Validators.required])
    });

    constructor(public dialogRef: MatDialogRef<AddColorDialogComponent>) { }

    ngOnInit(): void {
    }

    clickAdd(): void {
        let pattern: PatternUpsertDto = {
            name: this.patternForm.value.name as string,
        };
        this.dialogRef.close(pattern);
    }
}
