import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import { Question } from 'src/app/_models/question';
import { SettingsTrueFalse, ChoicesDisplayType, ChoicesEnumerationType } from 'src/app/_models/settings/answer.settings';

@Component({
  template: ''
})
export class BaseChoiceComponent implements OnInit {
  @Input() answerData: Answer[];

  choicesDisplayTypes = ChoicesDisplayType;
  choicesDisplayTypesKeys: number[] = [];

  choicesEnumerationTypes = ChoicesEnumerationType;
  choicesEnumerationTypesKeys: number[] = [];

  isFeedback = false;

  constructor() { }

  ngOnInit() {
    this.initFeedback();
    this.initEnums('choicesDisplayTypes', 'choicesDisplayTypesKeys');
    this.initEnums('choicesEnumerationTypes', 'choicesEnumerationTypesKeys');
  }

  initEnums(enums, keys) {
    this[keys] = Object.keys(this[enums]).filter(Number).map(v => Number(v));
  }

  initFeedback() {
    this.isFeedback = this.answerData.some(ans => ans.feedback);
  }

  generateId() {
    return Math.floor(Math.random() * 10000) + 1;
  }

  deleteAnswer(answer: Answer) {
    this.answerData.splice(this.answerData.findIndex(ans => ans.id === answer.id), 1);
  }

  addAnswer(name?: string, isCorrect?: boolean) {
    const newAnswer = new Answer();
    newAnswer.text = name || '';
    newAnswer.id = this.generateId();
    newAnswer.isCorrect = isCorrect || false;
    this.answerData.push(newAnswer);
  }

  selectDisplayType(settings) {
    if (settings.choicesDisplayType === this.choicesDisplayTypes.Dropdown) {
      settings.choicesEnumerationType = 0;
    }
  }

}
