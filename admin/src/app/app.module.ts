import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './_material/material.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { QuizListComponent } from './quiz/quiz-list/quiz-list.component';
import { QuizResolver } from './_resolvers/quiz.resolver';
import { QuestionResolver } from './_resolvers/question.resolver';

import { AttemptService } from './_service/attempt.service';

import { AppRoutingModule } from './app-routing.module';
import { VexModule } from '../@vex/vex.module';
import { CustomLayoutModule } from './custom-layout/custom-layout.module';

import { PageLayoutModule } from '../@vex/components/page-layout/page-layout.module';
import { SecondaryToolbarModule } from '../@vex/components/secondary-toolbar/secondary-toolbar.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BreadcrumbsModule } from '../@vex/components/breadcrumbs/breadcrumbs.module';
import { IconModule } from '@visurel/iconify-angular';
import { ContainerModule } from '../@vex/directives/container/container.module';
import { QuillModule } from 'ngx-quill';
import { QuizInfoComponent } from './quiz/quiz-info/quiz-info.component';
import { Error404Component } from './common/404/error-404.component';
import { Error500Component } from './common/500/error-500.component';
import { HttpErrorInterceptor } from './_common/interceptors/http-error.interceptor';
import { QuizInfoSettingsTabComponent } from './quiz/quiz-info/quiz-info-settings-tab/quiz-info-settings-tab.component';
import { QuizInfoQuestionsTabComponent } from './quiz/quiz-info/quiz-info-questions-tab/quiz-info-questions-tab.component';
import { QuestionLangService } from './_service/lang/question.lang.service';
import { GroupDataProvider } from './_service/dataProviders/group.dataProvider';
import { QuestionDataProvider } from './_service/dataProviders/question.dataProvider';
import { QuizDataProvider } from './_service/dataProviders/quiz.dataProvider';
import { QuizLangService } from './_service/lang/quiz.lang.service';
import { GroupInfoComponent } from './quiz/quiz-info/quiz-info-questions-tab/group-info/group-info';
import { NgxMatMomentModule } from '@angular-material-components/moment-adapter';

import {
  NgxMatDatetimePickerModule, 
  NgxMatNativeDateModule, 
  NgxMatTimepickerModule 
} from '@angular-material-components/datetime-picker';
import { LoginComponent } from './common/auth/login/login.component';
import { RegisterComponent } from './common/auth/register/register.component';
import { AuthDataProvider } from './_service/dataProviders/auth.dataProvider';
import { AuthService } from './_service/auth/auth.service';
import { AuthGuard } from './_common/guards/auth.guard';
import { JwtInterceptor } from './_common/interceptors/jwt.interceptor';
import { DebounceClickDirective } from './_common/directives/debounceClick.directive';
import { ForgotPasswordComponent } from './common/auth/forgotPassword/forgotPassword.component';
import { NewPasswordComponent } from './common/auth/newPassword/newPassword.component';
import { QuestionInfoComponent } from './question/question-info/question-info.component';
import { ChoiceHostDirective } from './question/question-info/choice-info/choice-host.directive';
import { ChoiceDynamicComponent } from './question/question-info/choice-info/choice-dynamic.component';
import { NewQuestionResolver } from './_resolvers/new-question.resolver';
import { OptionsMenuComponent } from './common-components/options-menu/options-menu.component';
import { TrueFalseChoiceInfoComponent } from './question/question-info/choice-info/true-false-choice-info/true-false-choice-info.component';
import { EnumToArrayPipe } from './_common/pipes/enumToArray.pipe';
import { RichTextEditorComponent } from './common-components/rich-text-editor/rich-text-editor.component';
import { QuestionInfoSettingsHostDirective } from './question/question-info/question-info-settings/question-info-settings-host.directive';
import { QuestionInfoSettingsDynamicComponent } from './question/question-info/question-info-settings/question-info-settings-dynamic.component';
import { TrueFalseInfoSettingsComponent } from './question/question-info/question-info-settings/true-false-info-settings/true-false-info-settings.component';
import { MultipleChoiceInfoSettingsComponent } from './question/question-info/question-info-settings/multiple-choice-info-settings/multiple-choice-info-settings.component';
import { MultipleSelectInfoSettingsComponent } from './question/question-info/question-info-settings/multiple-select-info-settings/multiple-select-info-settings.component';
import { MultipleChoiceChoiceInfoComponent } from './question/question-info/choice-info/multiple-choice-choice-info/multiple-choice-choice-info.component';
import { MultipleSelectChoiceInfoComponent } from './question/question-info/choice-info/multiple-select-choice-info/multiple-select-choice-info.component';

@NgModule({
  declarations: [
    AppComponent,

    Error404Component,
    Error500Component,

    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    NewPasswordComponent,

    DebounceClickDirective,

    EnumToArrayPipe,

    OptionsMenuComponent,
    RichTextEditorComponent,

    QuizListComponent,
    QuizInfoComponent,
    QuizInfoSettingsTabComponent,
    QuizInfoQuestionsTabComponent,
    GroupInfoComponent,
    QuestionInfoComponent,

    QuestionInfoSettingsHostDirective,
    QuestionInfoSettingsDynamicComponent,

    ChoiceHostDirective,
    ChoiceDynamicComponent,

    TrueFalseChoiceInfoComponent,
    TrueFalseInfoSettingsComponent,
    MultipleChoiceChoiceInfoComponent,
    MultipleChoiceInfoSettingsComponent,
    MultipleSelectChoiceInfoComponent,
    MultipleSelectInfoSettingsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,

    // Vex
    VexModule,
    CustomLayoutModule,


    //TOTO: check copy-pasted from VEX ui single page - not sure if all of them are required
    PageLayoutModule,
    SecondaryToolbarModule,
    FlexLayoutModule,
    BreadcrumbsModule,
    IconModule,
    ContainerModule,
    //TOTO: check

    //DateTime Picker component
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    NgxMatMomentModule,

    QuillModule.forRoot({
      modules: {
        toolbar: [
          ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
          ['blockquote', 'code-block'],

          [{ header: 1 }, { header: 2 }],               // custom button values
          [{ list: 'ordered' }, { list: 'bullet' }],
          [{ script: 'sub' }, { script: 'super' }],      // superscript/subscript
          [{ indent: '-1' }, { indent: '+1' }],          // outdent/indent
          [{ direction: 'rtl' }],                         // text direction

          [{ size: ['small', false, 'large', 'huge'] }],  // custom dropdown
          [{ header: [1, 2, 3, 4, 5, 6, false] }],

          [{ color: [] }, { background: [] }],          // dropdown with defaults from theme
          [{ align: [] }],

          ['clean'],                                         // remove formatting button

          ['link', 'image', 'video']                         // link and image, video
        ]
      }
    })
  ],
  entryComponents: [
    TrueFalseChoiceInfoComponent,
    TrueFalseInfoSettingsComponent,
    MultipleChoiceChoiceInfoComponent,
    MultipleChoiceInfoSettingsComponent,
    MultipleSelectChoiceInfoComponent,
    MultipleSelectInfoSettingsComponent,

    QuizInfoSettingsTabComponent,
    QuizInfoQuestionsTabComponent,
    GroupInfoComponent
  ],
  providers: [

    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },

    AuthGuard,
    AuthService,
    QuizLangService,
    QuestionLangService,
    QuizResolver,
    QuestionResolver,
    NewQuestionResolver,
    AttemptService,
    AuthDataProvider,
    QuizDataProvider,
    GroupDataProvider,
    QuestionDataProvider
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {}
