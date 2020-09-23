import { Component, OnInit } from '@angular/core';
import { ChoiceBaseDirective } from '../choice-base.directive';
import { MatRadioChange } from '@angular/material/radio';
import { Choice } from 'app/quiz-builder/model/choice';
import { FormGroup, Validators, FormArray } from '@angular/forms';

@Component({
  selector: 'app-true-false-choice-info',
  templateUrl: './true-false-choice-info.component.html',
  styleUrls: ['./true-false-choice-info.component.css']
})

export class TrueFalseChoiceInfoComponent extends ChoiceBaseDirective implements OnInit {

  choiceForms = new FormArray([]);

  ngOnInit(): void {
    this.createForm();
  }

  isValid(): boolean {
    return this.choiceForms.valid;
  }

  save(): void {

    // update data
    this.choiceForms.controls.forEach(
      (choiceForm: FormGroup) => {
        const choiceIdForm = choiceForm.get('id').value;
        const choice = this.question.choices.find(x => x.id == choiceIdForm) as Choice;

        Object.assign(choice, choiceForm.value);
      }
    );
  }

  onChange(event: MatRadioChange): void {

    // update form data
    this.choiceForms.controls.forEach(
      (x: FormGroup) => {
        const isSelected = x.get('id').value === event.value
        x.patchValue({ isCorrect: isSelected });
      }
    );
  }

  private createForm(): void {

    let createChoiceForm = (choice: Choice) => {
      return this.fb.group({
        id: [choice.id, Validators.required],
        text: [choice.text, Validators.required],
        isCorrect: [choice.isCorrect, Validators.required],
      });
    }

    this.question.choices.forEach((x: Choice) => {
      const choiceForm = createChoiceForm(x);
      this.choiceForms.push(choiceForm);
    });
  }

}