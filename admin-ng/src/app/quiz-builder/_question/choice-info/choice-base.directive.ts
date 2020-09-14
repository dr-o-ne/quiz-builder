import { Directive } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Question } from 'app/quiz-builder/model/question';

@Directive()
export abstract class ChoiceBaseDirective {

    constructor(protected fb: FormBuilder) {
    }

    question!: Question;

    abstract isValid(): boolean;

    abstract save(): void;
}
