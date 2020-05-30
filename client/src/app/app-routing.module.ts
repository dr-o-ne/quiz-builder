import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomLayoutComponent } from './custom-layout/custom-layout.component';

import { CommingSoonComponent } from './common/comming-soon/comming-soon.component';

import { AuthGuard } from './_guards/auth.guard';
import { QuizListComponent } from './quiz/quiz-list/quiz-list.component';
import { QuizPageComponent } from './quiz/quiz-page/quiz-page.component';

const routes: Routes = [
  {
    path: '',
    component: CustomLayoutComponent,
    children: []
  },
  {
    path: 'comming-soon',
    component: CommingSoonComponent,
    children: []
  },
  {
    path: 'quizzes',
    runGuardsAndResolvers: 'always',
    canActivate: [ AuthGuard ],
    children:
      [
        { path: '', component: QuizListComponent },
        { path: 'new', component: QuizPageComponent }
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
