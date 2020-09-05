import { Directive, AfterViewChecked } from '@angular/core';
import { Question } from 'src/app/_models/question';
import { FormBuilder } from '@angular/forms';

@Directive()
export abstract class ChoiceBaseDirective {

    constructor(
        protected fb: FormBuilder){
    }

    question!: Question;

    abstract isValid(): boolean;
}
