import { Component, OnInit } from '@angular/core';
import { BaseQuestionPreviewComponent } from '../base-question-preview/base-question-preview.component';

@Component({
  selector: 'app-multi-select-preview',
  templateUrl: './multi-select-preview.component.html',
  styleUrls: ['./multi-select-preview.component.css']
})
export class MultiSelectPreviewComponent extends BaseQuestionPreviewComponent {

  selectMultiCorrectChoice(arrSelect: number[]): void {
    arrSelect.forEach(select => this.choices[select].isCorrect = true);
  }

}
