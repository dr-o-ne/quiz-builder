import { Injectable } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Choice } from 'app/quiz-builder/model/choice';

@Injectable({
    providedIn: 'root'
})
export class ChoiceUtilsService {

    constructor(protected fb: FormBuilder) {
    }

    createBinaryEmptyChoiceForm(id: number, isCorrect: boolean): FormGroup {
        return this.fb.group({
            id: [id, Validators.required],
            text: ['', Validators.required],
            isCorrect: [isCorrect, Validators.required],
            feedback: ['']
        });
    }

    createBinaryChoiceForm(choice: Choice): FormGroup {
        return this.fb.group({
            id: [choice.id, Validators.required],
            text: [choice.text, Validators.required],
            isCorrect: [choice.isCorrect, Validators.required],
            feedback: [choice.feedback]
        });
    }

    createBinaryChoice(choiceForm: FormGroup): Choice {
        return new Choice(
            choiceForm.get('id').value,
            choiceForm.get('text').value,
            choiceForm.get('isCorrect').value,
            choiceForm.get('feedback').value);
    }

    createBinaryChoicesForm(choices: Choice[]) {

        const choicesForm = new FormArray([]);

        choices.forEach((x: Choice) => {
            const choiceForm = this.createBinaryChoiceForm(x);
            choicesForm.push(choiceForm);
        });

        return choicesForm;
    }

    getNextBinaryChoiceId(choiceForm: FormArray): number {
        if (choiceForm.length === 0)
            return 0;
        const ids = choiceForm.controls.map(x => x.get('id').value);
        return Math.max(...ids) + 1;
    }


}
