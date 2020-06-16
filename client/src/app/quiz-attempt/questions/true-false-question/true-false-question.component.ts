import { Component } from '@angular/core';
import { MatRadioChange } from '@angular/material/radio';
import { QuestionComponent } from '../question.component';
import { ChoicesDisplayType } from 'src/app/_models/_enums';
import { TrueFalseQuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { TrueFalseQuestionAttemptResult } from 'src/app/_models/attemptResult';

@Component({
  selector: 'app-true-false-question',
  templateUrl: './true-false-question.component.html'
})

export class TrueFalseQuestionComponent extends QuestionComponent<TrueFalseQuestionAttemptInfo, TrueFalseQuestionAttemptResult> {

  choicesDisplayType = ChoicesDisplayType;

  onChange(event: MatRadioChange): void {
    const value = new TrueFalseQuestionAttemptResult();
    value.questionId = this.question.id;
    value.choice = event.value;

    this.answer.emit(value);
  }

}

