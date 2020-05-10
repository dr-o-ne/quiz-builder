import { OnInit} from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import {
  ChoicesDisplayType,
  ChoicesEnumerationType,
  BaseChoiceSettings
} from 'src/app/_models/settings/answer.settings';
import { QuestionType } from 'src/app/_models/question';

export class BaseChoiceComponent implements OnInit {
  answerData: Answer[];
  settings: BaseChoiceSettings;
  choicesDisplayTypes = ChoicesDisplayType;
  choicesDisplayTypesKeys: number[] = [];
  choicesEnumerationTypes = ChoicesEnumerationType;
  choicesEnumerationTypesKeys: number[] = [];
  isFeedback = false;
  defaultCountAnswer = 4;
  questionType: QuestionType;
  isNewState = false;

  ngOnInit() {
    this.initFeedback();
    this.initEnums('choicesDisplayTypes', 'choicesDisplayTypesKeys');
    this.initEnums('choicesEnumerationTypes', 'choicesEnumerationTypesKeys');
    this.initDefaultChoice();
  }

  initDefaultChoice() {
    if (!this.isNewState) {
      return;
    }
    switch (this.questionType) {
      case QuestionType.TrueFalse:
        this.addAnswer('True', true);
        this.addAnswer('False');
        break;
      default:
        this.makeCustomListAnswer(this.defaultCountAnswer);
        break;
    }
  }

  makeCustomListAnswer(count: number) {
    for (let i = 0; i < count; i++) {
      this.addAnswer();
    }
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
      settings.choicesEnumerationType = 1;
    }
  }
}
