import { Component, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { ChoiceBaseDirective } from '../choice-base.directive';
import { MatRadioChange } from '@angular/material/radio';
import { Choice } from 'src/app/_models/choice';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-multiple-choice-choice-info',
  templateUrl: './multiple-choice-choice-info.component.html',
  styleUrls: ['./multiple-choice-choice-info.component.css']
})

export class MultipleChoiceChoiceInfoComponent extends ChoiceBaseDirective implements AfterViewChecked {

  constructor(
    protected fb: FormBuilder,
    private cdRef: ChangeDetectorRef
  ) {
    super(fb);
  }

  ngAfterViewChecked(): void {
    this.cdRef.detectChanges();
  }

  isValid(): boolean {
    const choices = this.question.choices;

    if (choices.length < 2) return false;

    return true;
  }

  onChoiceChange(event: MatRadioChange): void {
    this.question.choices.forEach((elem: Choice) => elem.isCorrect = elem.id === event.value);
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
    if (!Array.isArray(this.question.choices) || !this.question.choices.length)
      return 0;

    const ids = this.question.choices.map(x => x.id);
    return Math.max(...ids) + 1;
  }

}