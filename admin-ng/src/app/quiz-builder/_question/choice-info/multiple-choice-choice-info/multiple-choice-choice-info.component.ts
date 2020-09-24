import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ChoiceBaseDirective } from '../choice-base.directive';
import { MatRadioChange } from '@angular/material/radio';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ChoiceUtilsService } from '../choice-utls.service';
import { Choice } from 'app/quiz-builder/model/choice';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector: 'app-multiple-choice-choice-info',
    templateUrl: './multiple-choice-choice-info.component.html',
    styleUrls: ['./multiple-choice-choice-info.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})

export class MultipleChoiceChoiceInfoComponent extends ChoiceBaseDirective implements OnInit {

    constructor(
        protected fb: FormBuilder,
        private choiceUtilsService: ChoiceUtilsService) {

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
        // update model

        // remove
        const ids = this.choiceForm().controls.map(x => x.get('id').value);
        this.question.choices = this.question.choices.filter(x => ids.includes(x.id));
        this.choiceForm().controls.forEach(
            (choiceForm: FormGroup) => {
                const choiceIdForm = choiceForm.get('id').value;
                const choice = this.question.choices.find(x => x.id == choiceIdForm) as Choice;

                if (choice) { // update
                    Object.assign(choice, choiceForm.value);
                }
                else { // add
                    const choice = this.choiceUtilsService.createBinaryChoice(choiceForm);
                    this.question.choices.push(choice);
                }
            }
        );
    }

    onChangeChoice(event: MatRadioChange): void {
        // update form
        this.choiceForm().controls.forEach(
            (x: FormGroup) => {
                const isSelected = x.get('id').value === event.value
                x.patchValue({ isCorrect: isSelected });
            }
        );
    }

    onAddChoice(): void {
        const id = this.getNextChoiceId();
        const isCorrect = this.choiceForm().length === 0;
        const choiceForm = this.choiceUtilsService.createBinaryEmptyChoiceForm(id, isCorrect);
        this.choiceForm().push(choiceForm);
    }

    onDeleteChoice(id: number): void {
        const index = this.choiceForm().controls.findIndex(x => x.get('id').value === id);
        this.choiceForm().removeAt(index);
    }

    isOptionEnabled(name: string): boolean {
        const option = this.options.find(x => x.name === name);
        return option && option.enabled;
    }

    getNextChoiceId(): number {
        if (this.choiceForm().length === 0)
            return 0;
        const ids = this.choiceForm().controls.map(x => x.get('id').value);
        return Math.max(...ids) + 1;
    }

}