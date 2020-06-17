import { NgModule } from '@angular/core';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';

@NgModule({
    exports: [
        MatRadioModule,
        MatCheckboxModule,
        MatButtonModule,
        MatDialogModule,
        MatFormFieldModule
    ]
})

export class MaterialModule { }
