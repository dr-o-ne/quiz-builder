import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { InfoChoice } from '../_models/option';
import { QuizAttempt } from '../_models/attempt';

@Injectable( {
  providedIn: 'root'
} )
export class AttemptService {
  apiUrl = environment.apiUrl;

  infoChoice = new BehaviorSubject<InfoChoice>(new InfoChoice());
  currentInfoChoice = this.infoChoice.asObservable();

  constructor( private http: HttpClient ) {
  }

  changeInfoChoice(infoChoice: InfoChoice): void {
    this.infoChoice.next(infoChoice);
  }

  createAttempt(quizId: string): Observable<object> {
    return this.http.post( this.apiUrl + 'attempts', { quizId } );
  }

  updateAttempt(quizAttempt: QuizAttempt): Observable<object> {
    return this.http.put( this.apiUrl + 'attempts', quizAttempt );
  }

}
