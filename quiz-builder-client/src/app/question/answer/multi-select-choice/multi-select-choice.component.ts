import {Component, OnInit} from '@angular/core';
import {BaseChoiceComponent} from '../base-choice/base-choice.component';
import {SettingsMultipleSelectQuestion, QuestionGradingType, BaseChoiceSettings} from 'src/app/_models/settings/answer.settings';

@Component({
  selector: 'app-multi-select-choice',
  templateUrl: './multi-select-choice.component.html',
  styleUrls: ['./multi-select-choice.component.css']
})

export class MultiSelectChoiceComponent extends BaseChoiceComponent implements OnInit {
  settings: SettingsMultipleSelectQuestion;
  questionGradingTypes = QuestionGradingType;
  questionGradingTypesKeys: number[] = [];

  constructor() {
    super();
    this.initEnums('questionGradingTypes', 'questionGradingTypesKeys');
  }

  ngOnInit() {
    super.ngOnInit();
    this.initSettings();
  }

  initSettings() {
    if (this.settings instanceof BaseChoiceSettings) {
      this.settings = Object.assign(this.settings, new SettingsMultipleSelectQuestion());
    }
  }
}
