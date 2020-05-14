import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './_material/material.module';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { appRoutes } from './routes';
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


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
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
      TrueFalsePreviewComponent,
      MultiSelectPreviewComponent,
      LongAnswerComponent,
      LongAnswerPreviewComponent
   ],
   imports: [
      BrowserModule,
      FormsModule,
      ReactiveFormsModule,
      BrowserAnimationsModule,
      HttpClientModule,
      MaterialModule,
      RouterModule.forRoot(appRoutes),
      MatNativeDateModule,
      MatTooltipModule
   ],
   entryComponents: [QuestionListComponent, QuestionPageComponent, ModalWindowPreviewQuestionComponent],
   providers: [
      AuthGuard,
      QuizService,
      QuestionService,
      QuizResolver,
      QuestionResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
