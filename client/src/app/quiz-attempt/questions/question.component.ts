import { EventEmitter } from '@angular/core';

export class QuestionComponent<QuestionAttemptInfo, QuestionAttemptResult> {

  question!: QuestionAttemptInfo;
  answer = new EventEmitter<QuestionAttemptResult>();

}