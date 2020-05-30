import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appQuestionPreviewHost]',
})
export class QuestionPreviewDirective {
  constructor(public viewContainerRef: ViewContainerRef) { }
}
