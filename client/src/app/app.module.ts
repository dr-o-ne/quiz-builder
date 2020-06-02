import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './_material/material.module';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AuthGuard } from './_guards/auth.guard';
import { QuizListComponent } from './quiz/quiz-list/quiz-list.component';
import { QuizService } from './_service/quiz.service';
import { QuizResolver } from './_resolvers/quiz.resolver';
import { QuestionService } from './_service/question.service';
import { QuizPageComponent } from './quiz/quiz-page/quiz-page.component';
import { QuestionPageComponent } from './question/question-page.component';
import { QuestionResolver } from './_resolvers/question.resolver';
import { PreviewQuizComponent } from './quiz/quiz-preview/preview-quiz.component';
import { QuestionListComponent } from './question/question-list/question-list.component';
import { TrueFalseAnswerComponent } from './question/answer/true-false-answer/true-false-answer.component';
import { MultipleChoiceAnswerComponent } from './question/answer/multiple-choice-answer/multiple-choice-answer.component';
import { MultiSelectChoiceComponent } from './question/answer/multi-select-choice/multi-select-choice.component';
import { SingleChoiceDropdownAnswerComponent } from './question/answer/single-choice-dropdown-answer/single-choice-dropdown-answer.component';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ChoiceDirective } from './_directive/choice.directive';
import { DynamicChoiceComponent } from './question/answer/base-choice/dynamic-choice.component';
import { ModalWindowPreviewQuestionComponent } from './question/modal-window/modal-window-preview-question.component';
import { QuestionPreviewDirective } from './_directive/question.preview.directive';
import { DynamicQuestionPreviewComponent } from './question/question-preview/base-question-preview/dynamic-question-preview.component';
import { TrueFalsePreviewComponent } from './question/question-preview/true-false-preview/true-false-preview.component';
import { MultiSelectPreviewComponent } from './question/question-preview/multi-select-preview/multi-select-preview.component';
import { LongAnswerComponent } from './question/answer/long-answer/long-answer.component';
import { LongAnswerPreviewComponent } from './question/question-preview/long-answer-preview/long-answer-preview.component';
import { QuizPreviewResolver } from './_resolvers/quizpreview.resolver';
import { QuizAttemptComponent } from './quiz/quiz-attempt/quiz-attempt.component';
import { QuestionNavComponent } from './quiz/quiz-attempt/question-nav/question-nav.component';
import { AttemptService } from './_service/attempt.service';
import { ModalWindowAttemptComponent } from './quiz/quiz-attempt/modal-window-attempt/modal-window-attempt.component';

import { AppRoutingModule } from './app-routing.module';
import { VexModule } from '../@vex/vex.module';
import { CustomLayoutModule } from './custom-layout/custom-layout.module';
import { CommingSoonComponent } from './common/comming-soon/comming-soon.component';

import { PageLayoutModule } from '../@vex/components/page-layout/page-layout.module';
import { SecondaryToolbarModule } from '../@vex/components/secondary-toolbar/secondary-toolbar.module';
import { MatButtonModule } from '@angular/material/button';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BreadcrumbsModule } from '../@vex/components/breadcrumbs/breadcrumbs.module';
import { IconModule } from '@visurel/iconify-angular';
import { MatIconModule } from '@angular/material/icon';
import { ContainerModule } from '../@vex/directives/container/container.module';

@NgModule( {
  declarations: [
    AppComponent,
    QuizListComponent,
    QuizPageComponent,
    QuestionPageComponent,
    PreviewQuizComponent,
    QuestionListComponent,
    TrueFalseAnswerComponent,
    MultipleChoiceAnswerComponent,
    MultiSelectChoiceComponent,
    SingleChoiceDropdownAnswerComponent,
    ChoiceDirective,
    QuestionPreviewDirective,
    DynamicChoiceComponent,
    DynamicQuestionPreviewComponent,
    ModalWindowPreviewQuestionComponent,
    ModalWindowAttemptComponent,
    TrueFalsePreviewComponent,
    MultiSelectPreviewComponent,
    LongAnswerComponent,
    LongAnswerPreviewComponent,
    QuizAttemptComponent,
    QuestionNavComponent,
    CommingSoonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    MatNativeDateModule,
    MatTooltipModule,
	
    // Vex
    VexModule,
    CustomLayoutModule,


    //TOTO: check copy-pasted from VEX ui single page - not sure if all of them are required
    PageLayoutModule,
    SecondaryToolbarModule,
    MatButtonModule,
    FlexLayoutModule,
    BreadcrumbsModule,
    IconModule,
    MatIconModule,
    ContainerModule
    //TOTO: check

  ],
  entryComponents: [
    QuestionListComponent,
    QuestionPageComponent,
    ModalWindowPreviewQuestionComponent,
    ModalWindowAttemptComponent,
    MultipleChoiceAnswerComponent,
    TrueFalseAnswerComponent,
    MultiSelectChoiceComponent,
    LongAnswerComponent,
    TrueFalsePreviewComponent,
    MultiSelectPreviewComponent,
    LongAnswerPreviewComponent
  ],
  providers: [
    AuthGuard,
    QuizService,
    QuestionService,
    QuizResolver,
    QuestionResolver,
    QuizPreviewResolver,
    AttemptService
  ],
  bootstrap: [
    AppComponent
  ]
} )
export class AppModule {
}
