import { Directive } from '@angular/core';
import { Question } from 'src/app/_models/question';

@Directive()
export class ChoiceBaseDirective {

    question: Question;

}
