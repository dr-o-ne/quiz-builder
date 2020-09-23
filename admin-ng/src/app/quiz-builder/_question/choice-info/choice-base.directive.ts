import { Directive } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Question } from 'app/quiz-builder/model/question';

@Directive()
export abstract class ChoiceBaseDirective {

    constructor(protected fb: FormBuilder) {
    }

    question!: Question;
    questionForm!: FormGroup;

    abstract save(): void;
}
