import { Component } from '@angular/core';
import { ChoiceBaseDirective } from '../choice-base.directive';
import { OptionItem } from 'src/app/_models/UI/optionItem';
import { MatRadioChange } from '@angular/material/radio';
import { Choice } from 'src/app/_models/choice';
import { ChoicesDisplayType, ChoicesEnumerationType } from 'src/app/_models/settings/answer.settings';

@Component({
  selector: 'app-true-false-choice',
  templateUrl: './true-false-choice.component.html',
  styleUrls: ['./true-false-choice.component.css']
})

export class TrueFalseChoiceComponent extends ChoiceBaseDirective {

  options: OptionItem[] = [];

  choicesDisplayTypes = ChoicesDisplayType;
  choicesEnumerationTypes = ChoicesEnumerationType;

  isValid(): boolean {

    const choices = this.question.choices;

    if (choices.length !== 2) return false;
    if (choices[0].text === '') return false;
    if (choices[1].text === '') return false;
    if (choices[0].isCorrect === choices[1].isCorrect) return false;

    return true;

  }

  onChoiceChange(event: MatRadioChange): void {
    this.question.choices.forEach((elem: Choice) => elem.isCorrect = elem.id === event.value);
  }

}