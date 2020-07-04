import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Question } from 'src/app/_models/question';

@Injectable({
    providedIn: 'root'
})
export class QuestionDataProvider {

    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
    }

    getAllQuestions(quizId: string): Observable<Question[]> {
        return this.http.get<Question[]>(this.apiUrl + 'quizzes/' + quizId + '/questions');
    }

}
