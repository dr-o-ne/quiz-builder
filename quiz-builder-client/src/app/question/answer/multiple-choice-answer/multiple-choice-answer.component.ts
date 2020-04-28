import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';

@Component({
  selector: 'app-multiple-choice-answer',
  templateUrl: './multiple-choice-answer.component.html',
  styleUrls: ['./multiple-choice-answer.component.css']
})
export class MultipleChoiceAnswerComponent implements OnInit {
  @Input() answerData: Answer[];

  constructor() { }

  ngOnInit() {
  }

  deleteAnswer(answer: Answer) {
    this.answerData.splice(this.answerData.findIndex(ans => ans.id === answer.id), 1);
    const storageAnswer = localStorage.getItem('answerlist');
    const currenAnswerList: Answer[] = JSON.parse(storageAnswer);
    currenAnswerList.splice(currenAnswerList.findIndex(ans => ans.id === answer.id), 1);
    localStorage.setItem('answerlist', JSON.stringify(currenAnswerList));
  }

}
