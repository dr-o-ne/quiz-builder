import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AttemptInfo } from '../_models/attemptInfo';
import { QuizAttemptResult } from '../_models/attemptResult';

@Injectable({
  providedIn: 'root'
})

export class DataProviderService {

  readonly apiUrl = environment.apiUrl;

  constructor( private http: HttpClient ) {}

  startAttempt(quizId: string): Observable<AttemptInfo> {
    return this.http.post<AttemptInfo>( this.apiUrl + 'attempts', { quizId } );
  }

}
