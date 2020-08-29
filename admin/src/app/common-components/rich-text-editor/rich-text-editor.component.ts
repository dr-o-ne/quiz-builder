import { Component, Input, ViewEncapsulation } from '@angular/core';
import { ControlContainer, FormGroupDirective } from '@angular/forms';
import { fadeInUp400ms } from 'src/@vex/animations/fade-in-up.animation';

@Component({
  selector: 'app-rich-text-editor',
  templateUrl: './rich-text-editor.component.html',
  styleUrls: ['./rich-text-editor.component.css',
    '../../../../node_modules/quill/dist/quill.snow.css',
    '../../../@vex/styles/partials/plugins/_quill.scss'],
  viewProviders: [
    {
      provide: ControlContainer,
      useExisting: FormGroupDirective
    }
  ],
  encapsulation: ViewEncapsulation.None,
  animations: [fadeInUp400ms]
})
export class RichTextEditorComponent {

  @Input() textFormControlName: string;
  @Input() displayName: string;

  onContentChanged(_): void { /*HACK*/ }

}
