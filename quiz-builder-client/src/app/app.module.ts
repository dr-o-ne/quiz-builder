import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { MatirealModule } from './_material/material.module';
import { RouterModule } from '@angular/router';
import {HttpClientModule} from '@angular/common/http';

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
import { AnswerResolver } from './_resolvers/answer.resolver';
import { AnswerService } from './_service/answer.service';
import { PreviewQuizComponent } from './quiz/quiz-preview/preview-quiz.component';
import { QuestionListComponent } from './question/question-list/question-list.component';
import { GroupResolver } from './_resolvers/group.resolver';
import { TrueFalseAnswerComponent } from './question/answer/true-false-answer/true-false-answer.component';
import { MultipleChoiceAnswerComponent } from './question/answer/multiple-choice-answer/multiple-choice-answer.component';
import { SingleChoiceRadioAnswerComponent } from './question/answer/single-choice-radio-answer/single-choice-radio-answer.component';
import { SingleChoiceDropdownAnswerComponent } from './question/answer/single-choice-dropdown-answer/single-choice-dropdown-answer.component';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTooltipModule } from '@angular/material/tooltip';


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
      SingleChoiceRadioAnswerComponent,
      SingleChoiceDropdownAnswerComponent
   ],
   imports: [
      BrowserModule,
      FormsModule,
      ReactiveFormsModule,
      BrowserAnimationsModule,
      HttpClientModule,
      MatirealModule,
      RouterModule.forRoot(appRoutes),
      MatNativeDateModule,
      MatTooltipModule
   ],
   entryComponents: [QuestionListComponent],
   providers: [
      AuthGuard,
      QuizService,
      QuestionService,
      QuizResolver,
      QuestionResolver,
      AnswerResolver,
      AnswerService,
      GroupResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
