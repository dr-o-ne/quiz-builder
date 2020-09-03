import { Component } from '@angular/core';
import { ChoiceBaseDirective } from '../choice-base.directive';
import { MatRadioChange } from '@angular/material/radio';
import { Choice } from 'src/app/_models/choice';

@Component({
  selector: 'app-multiple-choice-choice-info',
  templateUrl: './multiple-choice-choice-info.component.html'
})

export class MultipleChoiceChoiceInfoComponent extends ChoiceBaseDirective {

  isValid(): boolean {

    return true;

  }

  onChoiceChange(event: MatRadioChange): void {
    this.question.choices.forEach((elem: Choice) => elem.isCorrect = elem.id === event.value);
  }

}