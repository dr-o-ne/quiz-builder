import { Component } from '@angular/core';
import { ChoiceBaseDirective } from './choice-base.directive';

@Component({
    selector: 'app-empty-choice',
    template: ``,
})
export class ChoiceEmptyComponent extends ChoiceBaseDirective {

    isValid(): boolean {
        return true;
    }

    save(): void {
    }
    
}