import { Directive } from '@angular/core';
import { ChoiceBaseDirective } from './choice-base.directive';

@Directive()
export class ChoiceEmptyDirective extends ChoiceBaseDirective {
    isValid(): boolean {
        return true;
    }
}
