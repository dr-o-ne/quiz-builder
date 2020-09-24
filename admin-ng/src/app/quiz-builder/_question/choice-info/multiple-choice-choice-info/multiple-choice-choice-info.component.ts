import { Component, OnInit } from '@angular/core';
import { ChoiceBaseDirective } from '../choice-base.directive';
import { MatRadioChange } from '@angular/material/radio';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Choice } from 'app/quiz-builder/model/choice';
import { ChoiceUtilsService } from '../choice-utls.service';

@Component({
  selector: 'app-multiple-choice-choice-info',
  templateUrl: './multiple-choice-choice-info.component.html',
  styleUrls: ['./multiple-choice-choice-info.component.css']
})

export class MultipleChoiceChoiceInfoComponent extends ChoiceBaseDirective implements OnInit {

  constructor(    
    protected fb: FormBuilder,
    private choiceUtilsService: ChoiceUtilsService ) {

    super(fb);
  }

  ngOnInit(): void {
    const choicesForm = this.choiceUtilsService.createBinaryChoicesForm(this.question.choices);
    this.questionForm.addControl("choices", choicesForm);
  }

  choiceForm(): FormArray {
    return this.questionForm.get("choices") as FormArray;
  }
  
  save(): void {
    throw new Error("Method not implemented.");
  }

  onChangeChoice(event: MatRadioChange): void {
    // update form data
    this.choiceForm().controls.forEach(
      (x: FormGroup) => {
        const isSelected = x.get('id').value === event.value
        x.patchValue({ isCorrect: isSelected });
      }
    );
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