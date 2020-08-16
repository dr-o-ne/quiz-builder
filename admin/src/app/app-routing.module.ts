import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomLayoutComponent } from './custom-layout/custom-layout.component';
import { AuthGuard } from './_guards/auth.guard';
import { QuizListComponent } from './quiz/quiz-list/quiz-list.component';
import { QuizResolver } from './_resolvers/quiz.resolver';
import { QuestionPageComponent } from './question/question-page/question-page.component';
import { QuestionResolver } from './_resolvers/question.resolver';
import { QuizInfoComponent } from './quiz/quiz-info/quiz-info.component';
import { Error404Component } from './common/404/error-404.component';
import { Error500Component } from './common/500/error-500.component';
import { LoginComponent } from './common/auth/login/login.component';
import { RegisterComponent } from './common/auth/register/register.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: '', component: CustomLayoutComponent,
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
      { path: '404', component: Error404Component },
      { path: '500', component: Error500Component },
      {
        path: 'quizzes/:id',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children:
          [
            {
              path: 'edit', children:
                [
                  { path: '', component: QuizInfoComponent, resolve: { quizResolver: QuizResolver } },
                  {
                    path: 'questions', children:
                      [
                        { path: '', redirectTo: 'new', pathMatch: 'full' },
                        { path: 'new', component: QuestionPageComponent }
                      ]
                  },
                  {
                    path: 'questions/:id',
                    component: QuestionPageComponent,
                    resolve: { questionResolver: QuestionResolver }
                  }
                ]
            }
          ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    // preloadingStrategy: PreloadAllModules,
    scrollPositionRestoration: 'enabled',
    relativeLinkResolution: 'corrected',
    anchorScrolling: 'enabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
