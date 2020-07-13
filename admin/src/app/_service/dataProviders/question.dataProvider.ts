import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class QuestionDataProvider {

    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
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
