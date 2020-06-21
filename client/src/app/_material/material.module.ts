import { NgModule } from '@angular/core';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
    exports: [
        MatRadioModule,
        MatCheckboxModule,
        MatButtonModule,
        MatDialogModule,
        MatFormFieldModule,
        TextFieldModule,
        MatInputModule,
        MatSelectModule
    ]
})

export class MaterialModule { }
