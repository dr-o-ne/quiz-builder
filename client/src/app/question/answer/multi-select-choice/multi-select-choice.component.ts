import {Component} from '@angular/core';
import {SettingsMultipleSelectQuestion, QuestionGradingType, BaseChoiceSettings} from 'src/app/_models/settings/answer.settings';
import {MultipleChoiceAnswerComponent} from '../multiple-choice-answer/multiple-choice-answer.component';

@Component({
  selector: 'app-multi-select-choice',
  templateUrl: './multi-select-choice.component.html',
  styleUrls: ['./multi-select-choice.component.css']
})

export class MultiSelectChoiceComponent extends MultipleChoiceAnswerComponent {
  settings: SettingsMultipleSelectQuestion;
  questionGradingTypes = QuestionGradingType;
  questionGradingTypesKeys: number[] = [];

  initDefaults(): void {
    this.initEnums('questionGradingTypes', 'questionGradingTypesKeys');
    this.initSettings();
    super.initDefaults();
  }

  initSettings() {
    if (this.settings instanceof BaseChoiceSettings) {
      this.settings = Object.assign(this.settings, new SettingsMultipleSelectQuestion());
    }
  }
}
