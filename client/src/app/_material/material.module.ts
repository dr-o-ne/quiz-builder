import { NgModule } from '@angular/core';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';

@NgModule({
    exports: [
        MatRadioModule,
        MatCheckboxModule
    ]
})

export class MaterialModule { }
