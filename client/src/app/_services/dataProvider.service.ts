import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { QuizAttemptInfo } from '../_models/attemptInfo';
import { QuizAttemptResult } from '../_models/attemptResult';
import { QuizAttemptFeedback } from '../_models/attemptFeedback';
import { ApiResponse } from './apiResponse';

@Injectable({
  providedIn: 'root'
})

export class DataProviderService {

  readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  startAttempt(quizId: string): Observable<ApiResponse<QuizAttemptInfo>> {
    return this.http.post<ApiResponse<QuizAttemptInfo>>(this.apiUrl + 'attempts', { quizId });
  }

  endAttempt(quizAttempt: QuizAttemptResult): Observable<ApiResponse<QuizAttemptFeedback>> {
    return this.http.put<ApiResponse<QuizAttemptFeedback>>(this.apiUrl + 'attempts', quizAttempt);
  }

}
