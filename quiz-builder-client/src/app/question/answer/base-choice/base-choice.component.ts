import {OnInit} from '@angular/core';
import {Choice} from 'src/app/_models/choice';
import {
  ChoicesDisplayType,
  ChoicesEnumerationType,
  BaseChoiceSettings
} from 'src/app/_models/settings/answer.settings';
import {QuestionType} from 'src/app/_models/question';

export class BaseChoiceComponent implements OnInit {
  choices: Choice[];
  settings: BaseChoiceSettings;
  choicesDisplayTypes = ChoicesDisplayType;
  choicesDisplayTypesKeys: number[] = [];
  choicesEnumerationTypes = ChoicesEnumerationType;
  choicesEnumerationTypesKeys: number[] = [];
  isFeedback = false;
  questionType: QuestionType;

  ngOnInit() {
    this.initDefaults();
  }

  initDefaults(): void {
    this.initFeedback();
    this.initEnums('choicesDisplayTypes', 'choicesDisplayTypesKeys');
    this.initEnums('choicesEnumerationTypes', 'choicesEnumerationTypesKeys');
    if (!this.choices?.length) {
      this.initDefaultChoices();
    }
  }

  initDefaultChoices(): void {
  }

  initEnums(enums, keys): void {
    this[keys] = Object.keys(this[enums]).filter(Number).map(v => Number(v));
  }

  initFeedback(): void {
    this.isFeedback = this.choices.some(ans => ans.feedback);
  }

  generateId(): number {
    return this.choices?.length || 0;
  }

  changeCorrectChoice(choiceId: number): void {
    this.choices.forEach((elem, idx) => elem.isCorrect = idx === choiceId);
  }

  deleteChoice(choice: Choice): void {
    this.choices.splice(this.choices.findIndex(item => item.id === choice.id), 1);
  }

  addChoice(text?: string, isCorrect?: boolean): void {
    const choice = new Choice(this.generateId(), text, isCorrect);
    this.choices.push(choice);
  }

  clickOption(event: any, prop: string): void {
    this[prop] = !this[prop];
    event.stopPropagation();
  }
}
