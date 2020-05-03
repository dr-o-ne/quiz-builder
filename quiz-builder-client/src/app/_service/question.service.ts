import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Question } from '../_models/question';
import {environment} from '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class QuestionService{
    question: Observable<Question>;
    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient){}

    getQuestionsByGroupId(groupId: number): Observable<object>{
      return this.http.get(this.apiUrl + 'questions/group/' + groupId);
    }

    getQuestion(id: number): Observable<object> {
      return this.http.get(this.apiUrl + 'questions/' + id);
    }

    updateQuestion(question: Question): Observable<object> {
      return this.http.put(this.apiUrl + 'questions', question);
    }

    createQuestion(question: Question): Observable<object> {
      return this.http.post(this.apiUrl + 'questions', question);
    }
}
