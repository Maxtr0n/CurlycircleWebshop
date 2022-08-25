import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PatternViewModel } from 'src/app/models/models';
import { PatternService } from 'src/app/services/pattern.service';

@Component({
    selector: 'app-delete-pattern-dialog',
    templateUrl: './delete-pattern-dialog.component.html',
    styleUrls: ['./delete-pattern-dialog.component.scss']
})
export class DeletePatternDialogComponent implements OnInit {
    patternName: string = '';

    constructor
        (@Inject(MAT_DIALOG_DATA) public data: { id: number; },
            private readonly patternService: PatternService,) { }

    ngOnInit(): void {
        this.patternService.getPattern(this.data.id)
            .subscribe({
                next: (patternViewModel: PatternViewModel) => {
                    this.patternName = patternViewModel.name;
                }
            });
    }
}
