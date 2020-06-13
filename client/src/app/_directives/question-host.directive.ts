import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[question-host]',
})

export class QuestionHostDirective {
    constructor(public viewContainerRef: ViewContainerRef) { }
}
