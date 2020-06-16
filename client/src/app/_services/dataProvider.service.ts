import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { QuizAttemptInfo } from '../_models/attemptInfo';

@Injectable({
  providedIn: 'root'
})

export class DataProviderService {

  readonly apiUrl = environment.apiUrl;

  constructor( private http: HttpClient ) {}

  startAttempt(quizId: string): Observable<QuizAttemptInfo> {
    return this.http.post<QuizAttemptInfo>( this.apiUrl + 'attempts', { quizId } );
  }

}
