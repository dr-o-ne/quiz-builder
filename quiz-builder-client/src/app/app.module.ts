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
import { GroupResolver } from './_resolvers/group.resolver';
import { TrueFalseAnswerComponent } from './question/answer/true-false-answer/true-false-answer.component';
import { MultipleChoiceAnswerComponent } from './question/answer/multiple-choice-answer/multiple-choice-answer.component';
import { MultiSelectChoiceComponent } from './question/answer/multi-select-choice/multi-select-choice.component';
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
      MultiSelectChoiceComponent,
      SingleChoiceDropdownAnswerComponent
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
   entryComponents: [QuestionListComponent],
   providers: [
      AuthGuard,
      QuizService,
      QuestionService,
      QuizResolver,
      QuestionResolver,
      GroupResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
