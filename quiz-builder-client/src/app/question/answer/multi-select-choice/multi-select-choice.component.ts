import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import { BaseChoiceComponent } from '../base-choice/base-choice.component';
import { SettingsMultipleSelectQuestion, QuestionGradingType, BaseChoiceSettings } from 'src/app/_models/settings/answer.settings';

@Component({
  selector: 'app-multi-select-choice',
  templateUrl: './multi-select-choice.component.html',
  styleUrls: ['./multi-select-choice.component.css']
})
export class MultiSelectChoiceComponent extends BaseChoiceComponent {
  @Input() settings: SettingsMultipleSelectQuestion;

  questionGradingTypes = QuestionGradingType;
  questionGradingTypesKeys: number[] = [];

  constructor() {
    super();
    this.initEnums('questionGradingTypes', 'questionGradingTypesKeys');
  }

  // tslint:disable-next-line: use-lifecycle-interface
  ngOnInit() {
    super.ngOnInit();
    this.initSettings();
  }

  initSettings() {
    if (!this.settings.hasOwnProperty('gradingType')) {
      this.settings = Object.assign( this.settings, new SettingsMultipleSelectQuestion());
    }
  }

}
