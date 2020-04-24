import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { QuizListComponent } from './quiz/quiz-list/quiz-list.component';
import { QuizPageComponent } from './quiz/quiz-page/quiz-page.component';
import { QuizResolver } from './_resolvers/quiz.resolver';
import { QuestionPageComponent } from './question/question-page.component';
import { QuestionResolver } from './_resolvers/question.resolver';
import { AnswerPageComponent } from './answer/answer-page.component';
import { AnswerResolver } from './_resolvers/answer.resolver';

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
                    resolve: {quiz: QuizResolver}},
            {path: 'editquiz/:id/addnewquestion', component: QuestionPageComponent,
                    resolve: {quiz: QuizResolver}},
            {path: 'editquiz/:id/editquestion/:id', component: QuestionPageComponent,
                     resolve: {quiz: QuizResolver, question: QuestionResolver }},
            {path: 'editquiz/:id/editquestion/:id/addnewanswer', component: AnswerPageComponent,
                     resolve: {quiz: QuizResolver, question: QuestionResolver }},
            {path: 'editquiz/:id/editquestion/:id/editanswer/:id', component: AnswerPageComponent,
                     resolve: {quiz: QuizResolver, question: QuestionResolver, answer: AnswerResolver }}
        ]
    },
    {path: '**', redirectTo: 'home', pathMatch: 'full'}
];
