import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ColorViewModel } from 'src/app/models/models';
import { ColorService } from 'src/app/services/color.service';

@Component({
    selector: 'app-delete-color-dialog',
    templateUrl: './delete-color-dialog.component.html',
    styleUrls: ['./delete-color-dialog.component.scss']
})
export class DeleteColorDialogComponent implements OnInit {
    colorName: string = '';

    constructor
        (@Inject(MAT_DIALOG_DATA) public data: { id: number; },
            private readonly colorService: ColorService,) { }

    ngOnInit(): void {
        this.colorService.getColor(this.data.id)
            .subscribe({
                next: (colorViewModel: ColorViewModel) => {
                    this.colorName = colorViewModel.name;
                }
            });
    }
}
