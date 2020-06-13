import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { QuizAttemptComponent } from './quiz-attempt/quiz-attempt.component';
import { QuizAttemptResolver } from './_resolvers/quiz-attempt.resolver';

const routes: Routes = [
  { path: '', redirectTo: '/1234', pathMatch: 'full' }, 
  { path: 'quizzes/:id', component: QuizAttemptComponent, resolve: { attempt: QuizAttemptResolver } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
