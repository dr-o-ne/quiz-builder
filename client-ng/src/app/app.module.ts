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
import { StartPageInfoComponent } from './quiz-builder/_quiz-attempt/start-page/start-page-info.component';

import { ResultPageInfoComponent } from './quiz-builder/_quiz-attempt/result-page/result-page-info.component';
import { InfoCardComponent } from './quiz-builder/_quiz-attempt/info-card/info-card.component';

import { QuizAttemptResolver } from './quiz-builder/resolvers/quiz-attempt.resolver';
import { StartPageInfoResolver } from './quiz-builder/resolvers/start-page-info.resolver';
import { ResultPageInfoResolver } from './quiz-builder/resolvers/result-page-info.resolver';

import { MaterialModule } from './quiz-builder/common/material.module';
import { QuestionHostDirective } from './quiz-builder/_quiz-attempt/questions/question-host.directive';
import { QuestionDynamicComponent } from './quiz-builder/_quiz-attempt/questions/question-dynamic.component';
import { TrueFalseQuestionComponent } from './quiz-builder/_quiz-attempt/questions/true-false-question/true-false-question.component';
import { MultipleChoiceQuestionComponent } from './quiz-builder/_quiz-attempt/questions/multiple-choice-question/multiple-choice-question.component';
import { MultipleSelectQuestionComponent } from './quiz-builder/_quiz-attempt/questions/multiple-select-question/multiple-select-question.component';

import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { RichTextEditorComponent } from './quiz-builder/common/ui/editor/rich-text-editor.component';

const appRoutes: Routes = [
    { path: 'quizzes/:id/start', component: StartPageInfoComponent, resolve: { startPageInfo: StartPageInfoResolver } },
    { path: 'quizzes/:id', component: QuizAttemptComponent, resolve: { attempt: QuizAttemptResolver } },
    { path: 'results/:id', component: ResultPageInfoComponent, resolve: { data: ResultPageInfoResolver } }
];

@NgModule({
    declarations: [

        // Utils
        RichTextEditorComponent,

        AppComponent,
        QuizAttemptComponent,
        StartPageInfoComponent,
        ResultPageInfoComponent,
        InfoCardComponent,

        QuestionHostDirective,
        QuestionDynamicComponent,
        TrueFalseQuestionComponent,
        MultipleChoiceQuestionComponent,
        MultipleSelectQuestionComponent
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
        QuizAttemptResolver,
        StartPageInfoResolver,
        ResultPageInfoResolver
    ],
    bootstrap   : [
        AppComponent
    ]
})
export class AppModule
{
}
