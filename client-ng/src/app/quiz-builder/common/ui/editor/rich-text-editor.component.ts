import { Component, Input, ViewEncapsulation } from '@angular/core';
import { ControlContainer, FormGroupDirective } from '@angular/forms';
import * as InlineEditor from '@ckeditor/ckeditor5-build-inline';

@Component({
  selector: 'qb-rich-text-editor',
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

  @Input() data: string;
  @Input() height: number;

  public Editor = InlineEditor;

}
