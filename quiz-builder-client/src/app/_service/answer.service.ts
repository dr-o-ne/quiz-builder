import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Answer } from '../_models/answer';

@Injectable({
    providedIn: 'root'
})
export class AnswerService{
    answer: Observable<Answer>;

    constructor(private http: HttpClient){}

    getAnswerData(){
        return this.http.get('assets/answer.json');
    }

    getAnswer(id): Observable<Answer> {
        const tempListAnswer = localStorage.getItem('answerlist');
        const answerList = JSON.parse(tempListAnswer);
        // tslint:disable-next-line: radix
        const index = answerList.findIndex((obj => obj.id === Number.parseInt(id)));
        this.answer = answerList[index];
        return this.answer;
    }
}
