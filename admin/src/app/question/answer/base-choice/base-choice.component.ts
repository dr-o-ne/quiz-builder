import {OnInit, ViewChild, Directive} from '@angular/core';
import {Choice} from 'src/app/_models/choice';
import {
  ChoicesDisplayType,
  ChoicesEnumerationType,
  BaseChoiceSettings
} from 'src/app/_models/settings/answer.settings';
import {MatMenuTrigger} from '@angular/material/menu';
import {Option} from '../../../_models/option';
import { ChoiceBaseDirective } from '../../question-info/choice-info/choice-base.directive';

@Directive()
export class BaseChoiceComponent extends ChoiceBaseDirective implements OnInit {
  isValid(): boolean {
    return true;
  }

  @ViewChild(MatMenuTrigger) optionsMenu: MatMenuTrigger;
  choices: Choice[];
  settings: BaseChoiceSettings;
  choicesDisplayTypes = ChoicesDisplayType;
  choicesDisplayTypesKeys: number[] = [];
  choicesEnumerationTypes = ChoicesEnumerationType;
  choicesEnumerationTypesKeys: number[] = [];

  options: Option[] = [
    new Option('feedback', 'Feedback', 'text')
  ];

  ngOnInit() {
    this.initDefaults();
  }

  initDefaults(): void {
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

  clickOption(event: MouseEvent, option: Option): void {
    option.enabled = !option.enabled;
    event.stopPropagation();
    if (this.options.every(i => !i.enabled)) {
      this.optionsMenu.closeMenu();
    }
  }
}
