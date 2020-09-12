import { Component } from '@angular/core';
import { ChoiceBaseDirective } from '../choice-base.directive';
import { Choice } from 'app/quiz-builder/model/choice';

@Component({
  selector: 'app-multiple-select-choice-info',
  templateUrl: './multiple-select-choice-info.component.html',
  styleUrls: ['./multiple-select-choice-info.component.css']
})

export class MultipleSelectChoiceInfoComponent extends ChoiceBaseDirective {

  isValid(): boolean {
    const choices = this.question.choices;

    if (choices.length < 2) return false;

    return true;
  }

  onAddChoice(): void {
    const id = this.getNextChoiceId();
    const choice = new Choice(id, "", false);

    this.question.choices.push(choice);
  }

  onDeleteChoice(choice: Choice): void {
    const index = this.question.choices.findIndex(x => x.id === choice.id);
    this.question.choices.splice(index, 1);
  }

  getNextChoiceId(): number {

    if (!this.question.choices)
      return 0;

    const ids = this.question.choices.map(x => x.id);

    return Math.max(...ids) + 1;
  }

}