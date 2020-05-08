import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import {Question, QuestionType} from 'src/app/_models/question';
import { BaseChoiceComponent } from '../base-choice/base-choice.component';
import { SettingsMultipleChoiceQuestion } from 'src/app/_models/settings/answer.settings';

@Component({
  selector: 'app-multiple-choice-answer',
  templateUrl: './multiple-choice-answer.component.html',
  styleUrls: ['./multiple-choice-answer.component.css']
})
export class MultipleChoiceAnswerComponent extends BaseChoiceComponent {
  @Input() settings: SettingsMultipleChoiceQuestion;

  questionType = QuestionType;

  constructor() {
    super();
  }

  // tslint:disable-next-line: use-lifecycle-interface
  // ngOnInit() {
  //   super.ngOnInit();
  //   this.initSettings();
  // }

  // initSettings() {
  //   if (Object.keys(this.settings).length === 0) {
  //     this.settings.choicesDisplayType = 1;
  //     this.settings.choicesEnumerationType = 1;
  //     this.settings.randomize = 0;
  //   }
  // }

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
