import { Component } from '@angular/core';
import { QuestionComponent } from '../question.component';
import { ChoicesDisplayType } from 'src/app/_models/_enums';
import { MultipleChoiceQuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { MultipleChoiceQuestionAttemptResult } from 'src/app/_models/attemptResult';
import { MatRadioChange } from '@angular/material/radio';
import { MatSelectChange } from '@angular/material/select';

@Component({
  selector: 'app-multiple-choice-question',
  templateUrl: './multiple-choice-question.component.html'
})

export class MultipleChoiceQuestionComponent extends QuestionComponent<MultipleChoiceQuestionAttemptInfo, MultipleChoiceQuestionAttemptResult> {

  choicesDisplayType = ChoicesDisplayType;

  onChange(event: MatRadioChange | MatSelectChange): void {
    const value = new MultipleChoiceQuestionAttemptResult(
      this.question.id,
      event.value
    );

    this.answer.emit(value);
  }

}
