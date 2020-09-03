import { Component } from '@angular/core';
import { QuestionInfoSettingsBaseDirective } from '../question-info-settings-base.directive';
import { ChoicesDisplayType, ChoicesEnumerationType } from 'src/app/_models/settings/answer.settings';

@Component({
  selector: 'app-multiple-choice-info-settings',
  templateUrl: './multiple-choice-info-settings.component.html'
})

export class MultipleChoiceInfoSettingsComponent extends QuestionInfoSettingsBaseDirective {

  choicesDisplayTypes = ChoicesDisplayType;
  choicesEnumerationTypes = ChoicesEnumerationType;

}