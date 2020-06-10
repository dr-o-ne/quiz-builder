import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { QuizAttemptComponent } from './quiz-attempt/quiz-attempt.component';

const routes: Routes = [
  { path: '', redirectTo: '/1234', pathMatch: 'full' }, 
  { path: 'quizzes/:id', component: QuizAttemptComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
