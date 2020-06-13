import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { FlexLayoutModule } from '@angular/flex-layout';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { QuizAttemptComponent } from './quiz-attempt/quiz-attempt.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { QuizAttemptResolver } from './_resolvers/quiz-attempt.resolver';

@NgModule({
  declarations: [
    AppComponent,
    QuizAttemptComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FlexLayoutModule
  ],
  providers: [QuizAttemptResolver],
  bootstrap: [AppComponent]
})
export class AppModule { }