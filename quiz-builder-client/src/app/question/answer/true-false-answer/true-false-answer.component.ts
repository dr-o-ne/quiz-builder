import { Component} from '@angular/core';
import { BaseChoiceComponent } from '../base-choice/base-choice.component';

@Component({
  selector: 'app-true-false-answer',
  templateUrl: './true-false-answer.component.html',
  styleUrls: ['./true-false-answer.component.css']
})

export class TrueFalseAnswerComponent extends BaseChoiceComponent {

  changeRadioButton(event) {
    if (!event.value) {
      return;
    }
    this.answerData.forEach(item => {
      if (item.id === event.value) {
        item.isCorrect = true;
        return;
      }
      item.isCorrect = false;
    });
  }
}
