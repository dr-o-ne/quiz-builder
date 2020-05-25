import { Routes, ExtraOptions, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { QuizListComponent } from './quiz/quiz-list/quiz-list.component';
import { QuizPageComponent } from './quiz/quiz-page/quiz-page.component';
import { QuizResolver } from './_resolvers/quiz.resolver';
import { QuestionPageComponent } from './question/question-page.component';
import { QuestionResolver } from './_resolvers/question.resolver';
import { PreviewQuizComponent } from './quiz/quiz-preview/preview-quiz.component';
import { QuizPreviewResolver } from './_resolvers/quizpreview.resolver';
import { QuizAttemptComponent } from './quiz/quiz-attempt/quiz-attempt.component';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
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
        { path: '', component: QuizAttemptComponent },
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
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
