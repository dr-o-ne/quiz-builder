import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from './apiResponse';
import { environment } from 'environments/environment';
import { Question } from 'app/quiz-builder/model/question';

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

    getQuestionTemplate(type: number): Observable<ApiResponse<Question>> {
        return this.http.get<ApiResponse<Question>>(this.apiUrl + 'questions/template/' + type);
    }

    updateQuestion(question: Question): Observable<object> {
        return this.http.put(this.apiUrl + 'questions', question);
    }

    createQuestion(question: Question): Observable<object> {
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
