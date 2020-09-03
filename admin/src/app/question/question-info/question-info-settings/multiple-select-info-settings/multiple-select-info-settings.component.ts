import { Component } from '@angular/core';
import { QuestionInfoSettingsBaseDirective } from '../question-info-settings-base.directive';
import { ChoicesDisplayType, ChoicesEnumerationType, QuestionGradingType } from 'src/app/_models/settings/answer.settings';

@Component({
  selector: 'app-multiple-select-info-settings',
  templateUrl: './multiple-select-info-settings.component.html'
})

export class MultipleSelectInfoSettingsComponent extends QuestionInfoSettingsBaseDirective {

  choicesDisplayTypes = ChoicesDisplayType;
  choicesEnumerationTypes = ChoicesEnumerationType;
  gradingTypes = QuestionGradingType;

}