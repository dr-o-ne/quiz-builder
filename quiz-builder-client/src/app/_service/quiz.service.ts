import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Quiz } from '../_models/quiz';
import { Observable } from 'rxjs';
import { Group } from '../_models/group';
import { environment } from 'src/environments/environment';

@Injectable( {
  providedIn: 'root'
} )
export class QuizService {
  group: Observable<Group>;
  apiUrl = environment.apiUrl;

  constructor( private http: HttpClient ) {
  }

  getAllQuizzes(): Observable<object> {
    return this.http.get( this.apiUrl + 'quizzes' );
  }

  getQuiz( id: string ): Observable<object> {
    return this.http.get( this.apiUrl + 'quizzes/' + id );
  }

  createQuiz( quiz: Quiz ): Observable<object> {
    return this.http.post( this.apiUrl + 'quizzes/', quiz );
  }

  updateQuiz( quiz: Quiz ): Observable<object> {
    return this.http.put( this.apiUrl + 'quizzes/', quiz );
  }

  deleteQuiz( id: string ): Observable<object> {
    return this.http.delete( this.apiUrl + 'quizzes/' + id );
  }

  deleteQuizzes( ids: string[] ): Observable<object> {
    const body = JSON.stringify( { ids } );
    const options = {
      headers: new HttpHeaders( {
        'Content-Type': 'application/json',
      } ),
      body
    };
    return this.http.delete( this.apiUrl + 'quizzes', options );
  }

  createGroup( group: Group ): Observable<object> {
    return this.http.post( this.apiUrl + 'groups', group );
  }
}
