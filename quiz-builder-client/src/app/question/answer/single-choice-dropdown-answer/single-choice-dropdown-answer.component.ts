import { Component} from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import {TrueFalseAnswerComponent} from '../true-false-answer/true-false-answer.component';

@Component({
  selector: 'app-single-choice-dropdown-answer',
  templateUrl: './single-choice-dropdown-answer.component.html',
  styleUrls: ['./single-choice-dropdown-answer.component.css']
})
export class SingleChoiceDropdownAnswerComponent extends TrueFalseAnswerComponent {

   deleteAnswer(answer: Answer) {
    this.answerData.splice(this.answerData.findIndex(ans => ans.id === answer.id), 1);
  }
}
