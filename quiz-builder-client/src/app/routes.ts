import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { QuizListComponent } from './quiz/quiz-list/quiz-list.component';
import { QuizPageComponent } from './quiz/quiz-page/quiz-page.component';
import { QuizResolver } from './_resolvers/quiz.resolver';
import { QuestionPageComponent } from './question/question-page.component';
import { QuestionResolver } from './_resolvers/question.resolver';
import { PreviewQuizComponent } from './quiz/quiz-preview/preview-quiz.component';
import { GroupResolver } from './_resolvers/group.resolver';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'quizlist', component: QuizListComponent},
            {path: 'createquiz', component: QuizPageComponent},
            {path: 'editquiz/:id', component: QuizPageComponent,
                    resolve: {quizResolver: QuizResolver}},
            {path: 'editquiz/:id/group/:id', component: QuizPageComponent,
                    resolve: {quizResolver: QuizResolver, group: GroupResolver}},
            {path: 'editquiz/:id/group/:id/addnewquestion', component: QuestionPageComponent,
                    resolve: {quizResolver: QuizResolver, group: GroupResolver}},
            {path: 'editquiz/:id/group/:id/editquestion/:id', component: QuestionPageComponent,
                     resolve: {quizResolver: QuizResolver, group: GroupResolver, question: QuestionResolver }},
            {path: 'preview/:id', component: PreviewQuizComponent,
                     resolve: {quizResolver: QuizResolver}}
        ]
    },
    {path: '**', redirectTo: 'home', pathMatch: 'full'}
];
