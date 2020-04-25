import { NgModule } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatTableModule} from '@angular/material/table';
import {MatDialogModule} from '@angular/material/dialog';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatSortModule} from '@angular/material/sort';
import {MatNativeDateModule, MatRippleModule} from '@angular/material/core';
import {MatMenuModule} from '@angular/material/menu';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {DragDropModule} from '@angular/cdk/drag-drop';
import {MatTabsModule} from '@angular/material/tabs';
import {MatRadioModule} from '@angular/material/radio';

@NgModule({
  exports: [
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        MatGridListModule,
        MatMenuModule,
        MatToolbarModule,
        MatSidenavModule,
        MatTabsModule,
        MatIconModule,
        MatListModule,
        MatCheckboxModule,
        MatPaginatorModule,
        MatTableModule,
        MatDialogModule,
        MatSelectModule,
        MatDatepickerModule,
        MatSortModule,
        MatNativeDateModule,
        MatRippleModule,
        DragDropModule,
        MatRadioModule
    ]
})
export class MatirealModule { }
