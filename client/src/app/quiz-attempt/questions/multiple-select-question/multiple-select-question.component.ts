import { Component } from '@angular/core';
import { QuestionComponent } from '../question.component';
import { MultipleSelectQuestionAttemptInfo, BinaryChoiceAttemptInfo } from 'src/app/_models/attemptInfo';
import { ChoicesDisplayType } from 'src/app/_models/_enums';

@Component({
  selector: 'app-multiple-select-question',
  templateUrl: './multiple-select-question.component.html'
})

export class MultipleSelectQuestionComponent extends QuestionComponent<MultipleSelectQuestionAttemptInfo> {

  choicesDisplayType = ChoicesDisplayType;

}
