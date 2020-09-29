import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[qb-question-host]',
})

export class QuestionHostDirective {
    constructor(public viewContainerRef: ViewContainerRef) { }
}
