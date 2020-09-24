import { Injectable } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { Choice } from 'app/quiz-builder/model/choice';

@Injectable({
    providedIn: 'root'
})
export class ChoiceUtilsService {

    constructor(protected fb: FormBuilder) {
    }

    createBinaryEmptyChoiceForm() {
        return this.fb.group({
            id: ['', Validators.required],
            text: ['', Validators.required],
            isCorrect: ['', Validators.required],
        });
    }

    createBinaryChoiceForm(choice: Choice) {
        return this.fb.group({
            id: [choice.id, Validators.required],
            text: [choice.text, Validators.required],
            isCorrect: [choice.isCorrect, Validators.required],
        });
    }

    createBinaryChoicesForm(choices: Choice[]) {

        const choicesForm = new FormArray([]);

        choices.forEach((x: Choice) => {
            const choiceForm = this.createBinaryChoiceForm(x);
            choicesForm.push(choiceForm);
        });

        return choicesForm;
    }

}
