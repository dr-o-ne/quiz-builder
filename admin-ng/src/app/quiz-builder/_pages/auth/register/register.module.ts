import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

import { FuseSharedModule } from '@fuse/shared.module';
import { RegisterComponent } from './register.component';
import { CommonUtilsModule } from 'app/quiz-builder/common/common-utils.module';

const routes = [
    {
        path     : 'auth/register',
        component: RegisterComponent
    }
];

@NgModule({
    declarations: [
        RegisterComponent,
        
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,

        FuseSharedModule,

        CommonUtilsModule
    ]
})
export class RegisterModule
{
}
