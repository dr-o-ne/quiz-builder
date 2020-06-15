import { Component } from '@angular/core';
import { QuestionComponent } from '../question.component';
import { ChoicesDisplayType } from 'src/app/_models/_enums';
import { MultipleSelectQuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { MultipleSelectQuestionAttemptResult } from 'src/app/_models/attemptResult';

@Component({
  selector: 'app-multiple-select-question',
  templateUrl: './multiple-select-question.component.html'
})

export class MultipleSelectQuestionComponent extends QuestionComponent<MultipleSelectQuestionAttemptInfo, MultipleSelectQuestionAttemptResult> {

  choicesDisplayType = ChoicesDisplayType;

}
