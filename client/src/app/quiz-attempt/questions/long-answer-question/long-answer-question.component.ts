import { Component } from '@angular/core';
import { QuestionComponent } from '../question.component';
import { LongAnswerQuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { LongAnswerQuestionAttemptResult } from 'src/app/_models/attemptResult';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-long-answer-question',
  templateUrl: './long-answer-question.component.html'
})

export class LongAnswerQuestionComponent extends QuestionComponent<LongAnswerQuestionAttemptInfo, LongAnswerQuestionAttemptResult> {

  answerControl = new FormControl();

  onChange(i: any): void {
    const value = new LongAnswerQuestionAttemptResult(
      this.question.id,
      this.answerControl.value
    );

    this.answer.emit(value);
  }

}
