import { Component } from '@angular/core';
import { QuestionComponent } from '../question.component';
import { BinaryChoiceAttemptInfo, TrueFalseQuestionAttemptInfo } from 'src/app/_models/attemptInfo';

@Component({
  selector: 'app-true-false-question',
  templateUrl: './true-false-question.component.html'
})

export class TrueFalseQuestionComponent extends QuestionComponent {

  question: TrueFalseQuestionAttemptInfo;

  getChoices(): BinaryChoiceAttemptInfo[] {

    return this.question.choices;

  }


}


