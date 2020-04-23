import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Quiz } from '../_models/quiz';
import { Observable } from 'rxjs';
import { Question } from '../_models/question';

@Injectable({
    providedIn: 'root'
})
export class QuestionService{
    question: Observable<Question>;

    constructor(private http: HttpClient){}

    getQuestionData(){
        return this.http.get('assets/question.json');
    }

    getQuestion(id): Observable<Question> {
        const tempListQuestion = localStorage.getItem('questionlist');
        const questionList = JSON.parse(tempListQuestion);
        // tslint:disable-next-line: radix
        const index = questionList.findIndex((obj => obj.id === Number.parseInt(id)));
        this.question = questionList[index];
        return this.question;
    }
}
