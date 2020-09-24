import { Component, OnInit } from '@angular/core';
import { ChoiceBaseDirective } from '../choice-base.directive';
import { MatRadioChange } from '@angular/material/radio';
import { Choice } from 'app/quiz-builder/model/choice';
import { FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { ChoiceUtilsService } from '../choice-utls.service';

@Component({
    selector: 'app-true-false-choice-info',
    templateUrl: './true-false-choice-info.component.html',
    styleUrls: ['./true-false-choice-info.component.css']
})

export class TrueFalseChoiceInfoComponent extends ChoiceBaseDirective implements OnInit {

    constructor(
        protected fb: FormBuilder,
        private choiceUtilsService: ChoiceUtilsService) {

        super(fb);
    }

    ngOnInit(): void {
        const choicesForm = this.choiceUtilsService.createBinaryChoicesForm(this.question.choices);
        this.questionForm.addControl("choices", choicesForm);
    }

    save(): void {
        // update data
        this.choiceForm().controls.forEach(
            (choiceForm: FormGroup) => {
                const choiceIdForm = choiceForm.get('id').value;
                const choice = this.question.choices.find(x => x.id == choiceIdForm) as Choice;

                Object.assign(choice, choiceForm.value);
            }
        );
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

    choiceForm(): FormArray {
        return this.questionForm.get("choices") as FormArray;
    }

}