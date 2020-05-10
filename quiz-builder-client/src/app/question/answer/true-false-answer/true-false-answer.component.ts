import {Component} from '@angular/core';
import {BaseChoiceComponent} from '../base-choice/base-choice.component';

@Component({
  selector: 'app-true-false-answer',
  templateUrl: './true-false-answer.component.html',
  styleUrls: ['./true-false-answer.component.css']
})

export class TrueFalseAnswerComponent extends BaseChoiceComponent {
  initDefaultChoices(): void {
    this.addChoice('No', true);
    this.addChoice('Yes', false);
  }
}
