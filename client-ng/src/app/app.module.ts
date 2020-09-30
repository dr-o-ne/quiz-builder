import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';

import { FuseModule } from '@fuse/fuse.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseProgressBarModule, FuseSidebarModule, FuseThemeOptionsModule } from '@fuse/components';

import { fuseConfig } from 'app/fuse-config';

import { AppComponent } from 'app/app.component';
import { LayoutModule } from 'app/layout/layout.module';
import { SampleModule } from 'app/main/sample/sample.module';

import { QuizAttemptComponent } from './quiz-builder/_quiz-attempt/quiz-attempt.component';
import { QuizAttemptResolver } from './quiz-builder/resolvers/quiz-attempt.resolver';

import { MaterialModule } from './quiz-builder/common/material.module';
import { QuestionHostDirective } from './quiz-builder/_quiz-attempt/questions/question-host.directive';
import { QuestionDynamicComponent } from './quiz-builder/_quiz-attempt/questions/question-dynamic.component';
import { TrueFalseQuestionComponent } from './quiz-builder/_quiz-attempt/questions/true-false-question/true-false-question.component';

import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

const appRoutes: Routes = [
    { path: 'quizzes/:id', component: QuizAttemptComponent, resolve: { attempt: QuizAttemptResolver } }
];

@NgModule({
    declarations: [
        AppComponent,
        QuizAttemptComponent,

        QuestionHostDirective,
        QuestionDynamicComponent,
        TrueFalseQuestionComponent
    ],
    imports     : [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes),

        TranslateModule.forRoot(),

        // Material moment date module
        MatMomentDateModule,

        // Material
        MatButtonModule,
        MatIconModule,

        // Fuse modules
        FuseModule.forRoot(fuseConfig),
        FuseProgressBarModule,
        FuseSharedModule,
        FuseSidebarModule,
        FuseThemeOptionsModule,

        // App modules
        LayoutModule,
        SampleModule,

        CKEditorModule,

        // UI
        MaterialModule,
    ],
    providers: [
        QuizAttemptResolver
    ],
    bootstrap   : [
        AppComponent
    ]
})
export class AppModule
{
}
