import { Directive } from '@angular/core';
import { Question } from 'src/app/_models/question';
import { FormBuilder } from '@angular/forms';

@Directive()
export abstract class QuestionInfoSettingsBaseDirective {

    constructor(protected fb: FormBuilder) {
    }

    question!: Question;
}
