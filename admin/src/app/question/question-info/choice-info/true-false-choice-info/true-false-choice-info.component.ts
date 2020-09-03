import { Component } from '@angular/core';
import { ChoiceBaseDirective } from '../choice-base.directive';
import { MatRadioChange } from '@angular/material/radio';
import { Choice } from 'src/app/_models/choice';

@Component({
  selector: 'app-true-false-choice-info',
  templateUrl: './true-false-choice-info.component.html',
  styleUrls: ['./true-false-choice-info.component.css']
})

export class TrueFalseChoiceInfoComponent extends ChoiceBaseDirective {

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