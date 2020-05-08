import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import { Question } from 'src/app/_models/question';
import { FormControl } from '@angular/forms';
import { BaseChoiceComponent } from '../base-choice/base-choice.component';

@Component({
  selector: 'app-true-false-answer',
  templateUrl: './true-false-answer.component.html',
  styleUrls: ['./true-false-answer.component.css']
})
export class TrueFalseAnswerComponent extends BaseChoiceComponent {

  constructor() {
    super();
   }

  changeRadioButton(event) {
    this.answerData.forEach(item => {
      if (item.id === event.value) {
        item.isCorrect = true;
        return;
      }
      item.isCorrect = false;
    });
  }

}
