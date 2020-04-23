import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Quiz } from '../_models/quiz';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class QuizService{
    quiz: Observable<Quiz>;

    constructor(private http: HttpClient){}

    getQuizData(){
        return this.http.get('assets/quiz.json');
    }

    getQuiz(id): Observable<Quiz> {
        const tempListQuiz = localStorage.getItem('quizlist');
        const quizList = JSON.parse(tempListQuiz);
        // tslint:disable-next-line: radix
        const index = quizList.findIndex((obj => obj.id === Number.parseInt(id)));
        this.quiz = quizList[index];
        return this.quiz;
    }
}
