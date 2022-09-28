import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SliderModule } from 'primeng/slider';
import { PaginatorModule } from 'primeng/paginator';
import { GalleriaModule } from 'primeng/galleria';


@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        SliderModule,
        PaginatorModule,
        GalleriaModule
    ],
    exports: [
        SliderModule,
        PaginatorModule,
        GalleriaModule
    ]
})
export class PrimengModule { }
