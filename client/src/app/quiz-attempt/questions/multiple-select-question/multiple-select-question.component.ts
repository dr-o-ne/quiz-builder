import { Component } from '@angular/core';
import { QuestionComponent } from '../question.component';
import { ChoicesDisplayType } from 'src/app/_models/_enums';
import { MultipleSelectQuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { MultipleSelectQuestionAttemptResult } from 'src/app/_models/attemptResult';
import { MatCheckboxChange } from '@angular/material/checkbox';

@Component({
  selector: 'app-multiple-select-question',
  templateUrl: './multiple-select-question.component.html'
})

export class MultipleSelectQuestionComponent extends QuestionComponent<MultipleSelectQuestionAttemptInfo, MultipleSelectQuestionAttemptResult> {

  constructor() {
    super();
    this.choices = new Set<string>();
  }

  choicesDisplayType = ChoicesDisplayType;
  choices: Set<string>;

  onChange(event: MatCheckboxChange): void {

    if (event.checked)
      this.choices.add(event.source.value);
    else
      this.choices.delete(event.source.value);

    const value = new MultipleSelectQuestionAttemptResult();
    value.questionId = this.question.id;
    value.choices = [...this.choices].map(x => parseInt(x));

    this.answer.emit(value);
  }


}
