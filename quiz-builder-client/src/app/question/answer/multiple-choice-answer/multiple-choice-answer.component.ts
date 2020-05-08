import { Component} from '@angular/core';
import {TrueFalseAnswerComponent} from '../true-false-answer/true-false-answer.component';

@Component({
  selector: 'app-multiple-choice-answer',
  templateUrl: './multiple-choice-answer.component.html',
  styleUrls: ['./multiple-choice-answer.component.css']
})

export class MultipleChoiceAnswerComponent extends TrueFalseAnswerComponent {
}
