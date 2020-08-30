import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[question-info-settings-host]',
})
export class QuestionInfoSettingsHostDirective {
    constructor(public viewContainerRef: ViewContainerRef) { }
}

