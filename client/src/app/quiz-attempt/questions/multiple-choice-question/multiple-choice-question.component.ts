import { Component } from '@angular/core';
import { QuestionComponent } from '../question.component';
import { MultipleChoiceQuestionAttemptInfo } from 'src/app/_models/attemptInfo';

@Component({
  selector: 'app-multiple-choice-question',
  templateUrl: './multiple-choice-question.component.html'
})

export class MultipleChoiceQuestionComponent extends QuestionComponent {

  question: MultipleChoiceQuestionAttemptInfo;

}
