import { Component, OnInit } from '@angular/core';
import { BaseQuestionPreviewComponent } from '../base-question-preview/base-question-preview.component';

@Component({
  selector: 'app-long-answer-preview',
  templateUrl: './long-answer-preview.component.html',
  styleUrls: ['./long-answer-preview.component.css']
})
export class LongAnswerPreviewComponent extends BaseQuestionPreviewComponent {

  changeLongAnswer(value: string): void {
    if ( !this.choices.length ) {
      return;
    }
    this.choices[0].text = value;
    this.choices[0].isCorrect = !!this.choices[0].text;
    this.initCheck();
  }

}
