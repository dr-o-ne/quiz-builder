import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';

@Component({
  selector: 'app-single-choice-radio-answer',
  templateUrl: './single-choice-radio-answer.component.html',
  styleUrls: ['./single-choice-radio-answer.component.css']
})
export class SingleChoiceRadioAnswerComponent implements OnInit {
  @Input() answerData: Answer[];

  constructor() { }

  ngOnInit() {
  }

   deleteAnswer(answer: Answer) {
    this.answerData.splice(this.answerData.findIndex(ans => ans.id === answer.id), 1);
  }

}
