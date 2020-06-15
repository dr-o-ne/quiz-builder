import { NgModule } from '@angular/core';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
    exports: [
        MatRadioModule,
        MatCheckboxModule,
        MatButtonModule
    ]
})

export class MaterialModule { }
