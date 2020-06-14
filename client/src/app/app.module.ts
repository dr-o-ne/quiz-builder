import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { FlexLayoutModule } from '@angular/flex-layout';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { QuizAttemptComponent } from './quiz-attempt/quiz-attempt.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { QuizAttemptResolver } from './_resolvers/quiz-attempt.resolver';
import { QuestionDynamicComponent } from './quiz-attempt/questions/question-dynamic.component';
import { TrueFalseQuestionComponent } from './quiz-attempt/questions/true-false-question/true-false-question.component';
import { MultipleChoiceQuestionComponent } from './quiz-attempt/questions/multiple-choice-question/multiple-choice-question.component';
import { QuestionHostDirective } from './_directives/question-host.directive';



import { QuillModule } from 'ngx-quill';


@NgModule({
  declarations: [
    AppComponent,
    QuizAttemptComponent,
    QuestionHostDirective,
    QuestionDynamicComponent,
    TrueFalseQuestionComponent,
    MultipleChoiceQuestionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    QuillModule.forRoot()
  ],
  providers: [QuizAttemptResolver],
  bootstrap: [AppComponent]
})
export class AppModule { }
