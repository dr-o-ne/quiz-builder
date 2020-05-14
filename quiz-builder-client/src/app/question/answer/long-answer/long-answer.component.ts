import { Component } from '@angular/core';
import { BaseChoiceComponent } from '../base-choice/base-choice.component';

@Component({
  selector: 'app-long-answer',
  templateUrl: './long-answer.component.html',
  styleUrls: ['./long-answer.component.css']
})
export class LongAnswerComponent extends BaseChoiceComponent {
  initDefaults() {}
}
