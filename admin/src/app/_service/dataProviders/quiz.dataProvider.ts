import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Quiz } from 'src/app/_models/quiz';
import { ApiResponse } from './apiResponse';

@Injectable({
    providedIn: 'root'
})
export class QuizDataProvider {

    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
    }

    getAllQuizzes(): Observable<Quiz[]> {
        return this.http.get<Quiz[]>(this.apiUrl + 'quizzes');
    }

    deleteQuiz(id: string): Observable<object> {
        return this.http.delete(this.apiUrl + 'quizzes/' + id);
    }

    deleteQuizzes(ids: string[]): Observable<object> {
        const body = JSON.stringify({ ids });
        const options = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            }),
            body
        };
        return this.http.delete(this.apiUrl + 'quizzes', options);
    }

    updateQuiz(quiz: Quiz): Observable<object> {
        return this.http.put(this.apiUrl + 'quizzes/', quiz);
    }

    getQuiz(id: string): Observable<ApiResponse<Quiz>> {
        return this.http.get<ApiResponse<Quiz>>(this.apiUrl + 'quizzes/' + id);
    }

    createQuiz(quiz: Quiz): Observable<ApiResponse<Quiz>> {
        return this.http.post<ApiResponse<Quiz>>(this.apiUrl + 'quizzes/', quiz);
    }


}
