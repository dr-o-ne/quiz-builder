import { Component } from '@angular/core';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatSelectChange } from '@angular/material/select';
import { QuestionComponent } from '../question.component';
import { ChoicesDisplayType } from 'src/app/_models/_enums';
import { MultipleSelectQuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { MultipleSelectQuestionAttemptResult } from 'src/app/_models/attemptResult';

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

  onChange(event: MatCheckboxChange | MatSelectChange): void {

    if (event instanceof MatCheckboxChange) {
      if (event.checked)
        this.choices.add(event.source.value);
      else
        this.choices.delete(event.source.value);
    }

    if (event instanceof MatSelectChange) {
      this.choices = new Set<string>(event.value);
    }

    const value = new MultipleSelectQuestionAttemptResult(
      this.question.id,
      [...this.choices].map(x => parseInt(x))
    );

    this.answer.emit(value);
  }

}
