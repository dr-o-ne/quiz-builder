import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appDynamicChoiceHost]',
})
export class ChoiceDirective {
  constructor(public viewContainerRef: ViewContainerRef) { }
}
