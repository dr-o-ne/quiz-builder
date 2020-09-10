import { Component, Input, ViewEncapsulation } from '@angular/core';
import { ControlContainer, FormGroupDirective } from '@angular/forms';

@Component({
  selector: 'app-rich-text-editor',
  templateUrl: './rich-text-editor.component.html',
  styleUrls: ['./rich-text-editor.component.css'],
  viewProviders: [
    {
      provide: ControlContainer,
      useExisting: FormGroupDirective
    }
  ],
  encapsulation: ViewEncapsulation.None
})
export class RichTextEditorComponent {

  @Input() textFormControlName: string;
  @Input() displayName: string;

  onContentChanged(_): void { /*HACK*/ }

}
