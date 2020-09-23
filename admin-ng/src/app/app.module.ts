import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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

import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

import {
  NgxMatDatetimePickerModule, 
  NgxMatNativeDateModule, 
  NgxMatTimepickerModule 
} from '@angular-material-components/datetime-picker';
import { NgxMatMomentModule } from '@angular-material-components/moment-adapter';

import { AppComponent } from 'app/app.component';
import { LayoutModule } from 'app/layout/layout.module';
import { AuthService } from './quiz-builder/services/auth/auth.service';
import { AuthDataProvider } from './quiz-builder/services/dataProviders/auth.dataProvider';
import { GroupDataProvider } from './quiz-builder/services/dataProviders/group.dataProvider';
import { QuestionDataProvider } from './quiz-builder/services/dataProviders/question.dataProvider';
import { QuizDataProvider } from './quiz-builder/services/dataProviders/quiz.dataProvider';
import { QuestionLangService } from './quiz-builder/services/lang/question.lang.service';
import { QuizLangService } from './quiz-builder/services/lang/quiz.lang.service';
import { AttemptService } from './quiz-builder/services/attempt.service';
import { AuthGuard } from './quiz-builder/common/utils/guards/auth.guard';
import { JwtInterceptor } from './quiz-builder/common/utils/interceptors/jwt.interceptor';
import { HttpErrorInterceptor } from './quiz-builder/common/utils/interceptors/http-error.interceptor';
import { EnumToArrayPipe } from './quiz-builder/common/utils/pipes/enumToArray.pipe';
import { MaterialModule } from './quiz-builder/common/material.module';
import { QuizListComponent } from './quiz-builder/_quiz/quiz-list/quiz-list.component';
import { LoginModule } from './quiz-builder/_pages/auth/login/login.module';
import { QuizInfoComponent } from './quiz-builder/_quiz/quiz-info/quiz-info.component';
import { QuizResolver } from './quiz-builder/resolvers/quiz.resolver';
import { QuestionResolver } from './quiz-builder/resolvers/question.resolver';
import { NewQuestionResolver } from './quiz-builder/resolvers/new-question.resolver';
import { QuizInfoSettingsComponent } from './quiz-builder/_quiz/quiz-info/settings/quiz-info-settings.component';
import { RichTextEditorComponent } from './quiz-builder/common/ui/editor/rich-text-editor.component';
import { RegisterModule } from './quiz-builder/_pages/auth/register/register.module';
import { ForgotPasswordModule } from './quiz-builder/_pages/auth/forgot-password/forgot-password.module';
import { ResetPasswordModule } from './quiz-builder/_pages/auth/reset-password/reset-password.module';
import { GroupInfoComponent } from './quiz-builder/_quiz/quiz-info/structure/group-info/group-info.component';
import { QuizInfoStructureTabComponent } from './quiz-builder/_quiz/quiz-info/structure/quiz-info-structure-tab.component';
import { ChoiceHostDirective } from './quiz-builder/_question/choice-info/choice-host.directive';
import { ChoiceDynamicComponent } from './quiz-builder/_question/choice-info/choice-dynamic.component';
import { TrueFalseChoiceInfoComponent } from './quiz-builder/_question/choice-info/true-false-choice-info/true-false-choice-info.component';
import { MultipleChoiceChoiceInfoComponent } from './quiz-builder/_question/choice-info/multiple-choice-choice-info/multiple-choice-choice-info.component';
import { MultipleSelectChoiceInfoComponent } from './quiz-builder/_question/choice-info/multiple-select-choice-info/multiple-select-choice-info.component';
import { QuestionDisplayTypeComponent } from './quiz-builder/_question/question-info/display-type/question-display-type.component';
import { QuestionInfoComponent } from './quiz-builder/_question/question-info/question-info.component';
import { CommonUtilsModule } from './quiz-builder/common/common-utils.module';
import { Error500Module } from './quiz-builder/_pages/error/500/error-500.module';
import { Error404Module } from './quiz-builder/_pages/error/404/error-404.module';

const appRoutes: Routes = [
    {
      path: '',
      children: [
        {
          path: 'quizzes',
          runGuardsAndResolvers: 'always',
          canActivate: [AuthGuard],
          children:
            [
              { path: '', component: QuizListComponent },
              { path: 'new', component: QuizInfoComponent }
            ]
        },
        {
          path: 'quizzes/:id',
          runGuardsAndResolvers: 'always',
          canActivate: [AuthGuard],
          children:
            [
              { path: '', component: QuizInfoComponent, resolve: { quizResolver: QuizResolver } },
              { path: 'questions/new', component: QuestionInfoComponent, resolve: { questionResolver: NewQuestionResolver } },
              { path: 'questions/:id', component: QuestionInfoComponent, resolve: { questionResolver: QuestionResolver } }
            ]
        }
      ],
      
    },
    { path: '**', redirectTo: 'errors/404' }
  ];

@NgModule({
    declarations: [
        AppComponent,

        // Utils
        RichTextEditorComponent,

        //DebounceClickDirective,
        EnumToArrayPipe,

        // UI
        QuizListComponent,
        QuizInfoComponent,
        QuizInfoSettingsComponent,
        GroupInfoComponent,
        QuizInfoStructureTabComponent,
        ChoiceHostDirective,
        ChoiceDynamicComponent,

        TrueFalseChoiceInfoComponent,
        MultipleChoiceChoiceInfoComponent,
        MultipleSelectChoiceInfoComponent,

        QuestionInfoComponent,
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes),

        TranslateModule.forRoot(),

        CKEditorModule,

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
        Error404Module,
        Error500Module,

        // 3th party
        NgxMatDatetimePickerModule,
        NgxMatTimepickerModule,
        NgxMatNativeDateModule,      
        NgxMatMomentModule, 

        // App modules
        LayoutModule,

        // Pages Auth
        LoginModule,
        RegisterModule,
        ForgotPasswordModule,
        ResetPasswordModule,
        CommonUtilsModule,

        // UI
        MaterialModule,
        
        CommonUtilsModule
    ],
    entryComponents: [
      TrueFalseChoiceInfoComponent,
      MultipleChoiceChoiceInfoComponent,
      MultipleSelectChoiceInfoComponent
    ],
    providers: [

        // Services
        AuthService,
        AuthDataProvider,
        GroupDataProvider,
        QuestionDataProvider,
        QuizDataProvider,
        QuestionLangService,
        QuizLangService,
        AttemptService,

        // Utils
        AuthGuard,
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },

        // Resolvers
        QuizResolver,
        QuestionResolver,
        NewQuestionResolver,
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule {
}
