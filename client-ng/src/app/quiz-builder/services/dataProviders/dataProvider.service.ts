import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { QuizAttemptInfo } from '../../model/attemptInfo';
import { QuizAttemptResult } from '../../model/attemptResult';
import { QuizAttemptFeedback } from '../../model/attemptFeedback';
import { StartPageInfo } from '../../model/startPageInfo';

import { ApiResponse } from './apiResponse';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})

export class DataProviderService {

  readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getStartPageInfo(quizId: string): Observable<ApiResponse<StartPageInfo>> {
    return this.http.get<ApiResponse<StartPageInfo>>(this.apiUrl + 'attempts/' + quizId);
  }

  startAttempt(quizId: string): Observable<ApiResponse<QuizAttemptInfo>> {
    return this.http.post<ApiResponse<QuizAttemptInfo>>(this.apiUrl + 'attempts', { quizId });
  }

  endAttempt(quizAttempt: QuizAttemptResult): Observable<ApiResponse<QuizAttemptFeedback>> {
    return this.http.put<ApiResponse<QuizAttemptFeedback>>(this.apiUrl + 'attempts', quizAttempt);
  }

}
