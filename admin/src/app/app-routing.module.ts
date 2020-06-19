import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomLayoutComponent } from './custom-layout/custom-layout.component';

import { CommingSoonComponent } from './common/comming-soon/comming-soon.component';

import { AuthGuard } from './_guards/auth.guard';
import { QuizListComponent } from './quiz/quiz-list/quiz-list.component';
import { QuizPageComponent } from './quiz/quiz-page/quiz-page.component';
import { QuizResolver } from './_resolvers/quiz.resolver';
import { QuestionPageComponent } from './question/question-page/question-page.component';
import { QuestionResolver } from './_resolvers/question.resolver';
import { PreviewQuizComponent } from './quiz/quiz-preview/preview-quiz.component';
import { QuizPreviewResolver } from './_resolvers/quizpreview.resolver';

const routes: Routes = [
  {
    path: '',
    component: CustomLayoutComponent,
    children: [  {
      path: 'quizzes',
      runGuardsAndResolvers: 'always',
      canActivate: [ AuthGuard ],
      children:
        [
          { path: '', component: QuizListComponent },
          { path: 'new', component: QuizPageComponent }
        ]
    },
    {
      path: 'quizzes/:id',
      runGuardsAndResolvers: 'always',
      canActivate: [ AuthGuard ],
      children:
        [
          { path: 'edit', children:
            [
              {path: '', component: QuizPageComponent, resolve: { quizResolver: QuizResolver } },
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
          },
          {
            path: 'preview', children:
              [
                { path: '', component: PreviewQuizComponent, resolve: { quizResolver: QuizPreviewResolver } }
              ]
          }
        ]
    }]
  },
  {
    path: 'comming-soon',
    component: CommingSoonComponent,
    children: []
  },

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
