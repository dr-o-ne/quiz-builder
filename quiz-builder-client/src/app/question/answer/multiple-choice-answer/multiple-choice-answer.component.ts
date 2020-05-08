import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import {Question, QuestionType} from 'src/app/_models/question';
import { BaseChoiceComponent } from '../base-choice/base-choice.component';

@Component({
  selector: 'app-multiple-choice-answer',
  templateUrl: './multiple-choice-answer.component.html',
  styleUrls: ['./multiple-choice-answer.component.css']
})
export class MultipleChoiceAnswerComponent extends BaseChoiceComponent {
  questionType = QuestionType;

  constructor() {
    super();
  }

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
