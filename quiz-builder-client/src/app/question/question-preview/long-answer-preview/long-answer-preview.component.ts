import { Component, OnInit } from '@angular/core';
import { BaseQuestionPreviewComponent } from '../base-question-preview/base-question-preview.component';

@Component({
  selector: 'app-long-answer-preview',
  templateUrl: './long-answer-preview.component.html',
  styleUrls: ['./long-answer-preview.component.css']
})
export class LongAnswerPreviewComponent extends BaseQuestionPreviewComponent {

  changeLongAnswer(): void {
    this.choices[0].isCorrect = !!this.choices[0].text;
    this.initCheck();
  }

}
