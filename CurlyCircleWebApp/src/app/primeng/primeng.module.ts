import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SliderModule } from 'primeng/slider';
import { PaginatorModule } from 'primeng/paginator';


@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        SliderModule,
        PaginatorModule
    ],
    exports: [
        SliderModule,
        PaginatorModule
    ]
})
export class PrimengModule { }
