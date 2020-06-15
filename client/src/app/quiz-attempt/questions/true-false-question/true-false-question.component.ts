import { Component } from '@angular/core';
import { QuestionComponent } from '../question.component';
import { BinaryChoiceAttemptInfo, TrueFalseQuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { ChoicesDisplayType } from 'src/app/_models/_enums';

@Component({
  selector: 'app-true-false-question',
  templateUrl: './true-false-question.component.html'
})

export class TrueFalseQuestionComponent extends QuestionComponent<TrueFalseQuestionAttemptInfo> {

  choicesDisplayType = ChoicesDisplayType;

}


