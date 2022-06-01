import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GalleriaModule } from 'primeng/galleria';



@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        GalleriaModule,
    ],
    exports: [
        GalleriaModule,
    ],
})
export class PrimengModule { }
