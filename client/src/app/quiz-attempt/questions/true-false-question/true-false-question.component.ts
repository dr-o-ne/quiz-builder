import { Component } from '@angular/core';
import { QuestionComponent } from '../question.component';
import { QuestionAttemptInfo } from 'src/app/_models/attemptInfo';

@Component({
  selector: 'app-true-false-question',
  templateUrl: './true-false-question.component.html'
})

export class TrueFalseQuestionComponent extends QuestionComponent {
}
