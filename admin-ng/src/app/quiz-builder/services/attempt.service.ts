import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AttemptService {

  clientUrl = environment.clientUrl;

  getLink(quizId: string): string {
    return this.clientUrl + 'quizzes/' + quizId + '/start';
  }

  tryAttempt(quizId: string): void {
    window.open(this.getLink(quizId), "_blank");
  }

}
