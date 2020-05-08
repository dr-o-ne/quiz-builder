import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import { Question } from 'src/app/_models/question';
import { FormControl } from '@angular/forms';
import { BaseChoiceComponent } from '../base-choice/base-choice.component';
import { ChoicesDisplayType, SettingsTrueFalse } from 'src/app/_models/settings/answer.settings';

@Component({
  selector: 'app-true-false-answer',
  templateUrl: './true-false-answer.component.html',
  styleUrls: ['./true-false-answer.component.css']
})
export class TrueFalseAnswerComponent extends BaseChoiceComponent {
  @Input() settings: SettingsTrueFalse;
  @Input() isSelectType: boolean;

  constructor() {
    super();
  }

  // tslint:disable-next-line: use-lifecycle-interface
  // ngOnInit() {
  //   super.ngOnInit();
  //   this.initTrueFalse();
  // }

  // initTrueFalse() {
  //   if (this.isSelectType) {
  //     this.addAnswer('True', true);
  //     this.addAnswer('False');
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
