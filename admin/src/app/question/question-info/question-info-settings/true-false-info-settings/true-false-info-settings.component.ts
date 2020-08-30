import { Component } from '@angular/core';
import { QuestionInfoSettingsBaseDirective } from '../question-info-settings-base.directive';
import { ChoicesDisplayType, ChoicesEnumerationType } from 'src/app/_models/settings/answer.settings';

@Component({
  selector: 'app-true-false-info-settings',
  templateUrl: './true-false-info-settings.component.html',
  styleUrls: ['./true-false-info-settings.component.css']
})

export class TrueFalseInfoSettingsComponent extends QuestionInfoSettingsBaseDirective {

  choicesDisplayTypes = ChoicesDisplayType;
  choicesEnumerationTypes = ChoicesEnumerationType;

}