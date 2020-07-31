import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AttemptService {

  clientUrl = environment.clientUrl;

  tryAttempt(quizId: string): void {
    window.open(this.clientUrl + 'quizzes/' + quizId, "_blank");
  }

}
