import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Question } from 'src/app/_models/question';
import { ApiResponse } from './apiResponse';

@Injectable({
    providedIn: 'root'
})
export class QuestionDataProvider {

    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
    }

    getQuestion(id: string): Observable<ApiResponse<Question>> {
        return this.http.get<ApiResponse<Question>>(this.apiUrl + 'questions/' + id);
    }

    updateQuestion(question: Question): Observable<object> {
        return this.http.put(this.apiUrl + 'questions', question);
    }

    createQuestion(question: Question): Observable<object> {

        console.log(question);
        question.settings = JSON.stringify(question.settings);
        question.choices = JSON.stringify(question.choices);

        return this.http.post(this.apiUrl + 'questions', question);
    }

    reorderQuestions(groupId: string, questionIds: string[]): Observable<object> {
        const body = { groupId, questionIds };
        return this.http.put(this.apiUrl + 'questions/reorder', body);
    }

    moveQuestion(groupId: string, questionId: string, questionIds: string[]): Observable<object> {
        const body = { groupId, questionId, questionIds };
        return this.http.put(this.apiUrl + 'questions/move', body);
    }

    deleteQuestion(quizId: string, id: string): Observable<object> {
        return this.http.delete(this.apiUrl + 'quizzes/' + quizId + '/questions/' + id);
    }

}
