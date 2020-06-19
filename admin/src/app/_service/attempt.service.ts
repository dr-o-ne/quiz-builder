import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { InfoChoice } from '../_models/option';
import { QuizAttempt } from '../_models/attempt';

@Injectable({
  providedIn: 'root'
})
export class AttemptService {
  apiUrl = environment.apiUrl;
  clientUrl = environment.clientUrl;

  infoChoice = new BehaviorSubject<InfoChoice>(new InfoChoice());
  currentInfoChoice = this.infoChoice.asObservable();

  constructor(private http: HttpClient) {
  }

  runAttempt(quizId: string): void {
    window.open(this.clientUrl + 'quizzes/' + quizId, "_blank");
  }

  changeInfoChoice(infoChoice: InfoChoice): void {
    this.infoChoice.next(infoChoice);
  }

  resetInfoChoice(): void {
    this.infoChoice.next(null);
  }

  createAttempt(quizId: string): Observable<object> {
    return this.http.post(this.apiUrl + 'attempts', { quizId });
  }

  updateAttempt(quizAttempt: QuizAttempt): Observable<object> {
    return this.http.put(this.apiUrl + 'attempts', quizAttempt);
  }

}
