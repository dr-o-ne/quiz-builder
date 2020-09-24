import { Directive } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Question } from 'app/quiz-builder/model/question';
import { OptionItem } from 'app/quiz-builder/model/UI/optionItem';

@Directive()
export abstract class ChoiceBaseDirective {

    constructor(protected fb: FormBuilder) {
    }

    question!: Question;
    questionForm!: FormGroup;
    options!: OptionItem[];

    abstract save(): void;
}
