import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[choice-host]',
})
export class ChoiceHostDirective {
    constructor(public viewContainerRef: ViewContainerRef) { }
}

