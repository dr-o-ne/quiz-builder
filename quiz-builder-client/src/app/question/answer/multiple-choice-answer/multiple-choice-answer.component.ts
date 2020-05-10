import { Component} from '@angular/core';
import {BaseChoiceComponent} from '../base-choice/base-choice.component';

@Component({
  selector: 'app-multiple-choice-answer',
  templateUrl: './multiple-choice-answer.component.html',
  styleUrls: ['./multiple-choice-answer.component.css']
})

export class MultipleChoiceAnswerComponent extends BaseChoiceComponent {
  private defaultChoicesCount = 4;

  initDefaultChoices() {
    for (let i = 0; i < this.defaultChoicesCount; i++) {
      this.addChoice(i.toString(), i === 0);
    }
  }
}
