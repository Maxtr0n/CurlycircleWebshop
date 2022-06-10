import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GalleriaModule } from 'primeng/galleria';
import { InputNumberModule } from 'primeng/inputnumber';



@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        GalleriaModule,
        InputNumberModule,
    ],
    exports: [
        GalleriaModule,
        InputNumberModule,
    ],
})
export class PrimengModule { }
