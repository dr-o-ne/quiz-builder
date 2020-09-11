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

import { QuillModule } from 'ngx-quill';

import {
  NgxMatDatetimePickerModule, 
  NgxMatNativeDateModule, 
  NgxMatTimepickerModule 
} from '@angular-material-components/datetime-picker';
import { NgxMatMomentModule } from '@angular-material-components/moment-adapter';

import { AppComponent } from 'app/app.component';
import { LayoutModule } from 'app/layout/layout.module';
import { SampleModule } from 'app/main/sample/sample.module';
import { AuthService } from './quiz-builder/services/auth/auth.service';
import { AuthDataProvider } from './quiz-builder/services/dataProviders/auth.dataProvider';
import { GroupDataProvider } from './quiz-builder/services/dataProviders/group.dataProvider';
import { QuestionDataProvider } from './quiz-builder/services/dataProviders/question.dataProvider';
import { QuizDataProvider } from './quiz-builder/services/dataProviders/quiz.dataProvider';
import { QuestionLangService } from './quiz-builder/services/lang/question.lang.service';
import { QuizLangService } from './quiz-builder/services/lang/quiz.lang.service';
import { AttemptService } from './quiz-builder/services/attempt.service';
import { DebounceClickDirective } from './quiz-builder/common/utils/directives/debounceClick.directive';
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
import { RichTextEditorComponent } from './quiz-builder/common/ui/rich-text-editor.component';
import { RegisterModule } from './quiz-builder/_pages/auth/register/register.module';
import { ForgotPasswordModule } from './quiz-builder/_pages/auth/forgot-password/forgot-password.module';

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
            ]
        }
      ]
    }
  ];

@NgModule({
    declarations: [
        AppComponent,

        // 3th party
        RichTextEditorComponent,

        // Utils
        //DebounceClickDirective,
        EnumToArrayPipe,

        // UI
        QuizListComponent,
        QuizInfoComponent,
        QuizInfoSettingsComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes),

        TranslateModule.forRoot(),

        QuillModule.forRoot(),

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

        // 3th party
        NgxMatDatetimePickerModule,
        NgxMatTimepickerModule,
        NgxMatNativeDateModule,      
        NgxMatMomentModule, 

        // App modules
        LayoutModule,
        SampleModule,

        // Pages
        LoginModule,
        RegisterModule,
        ForgotPasswordModule,

        // UI
        MaterialModule,        
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
