import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';

@Component({
  selector: 'app-single-choice-dropdown-answer',
  templateUrl: './single-choice-dropdown-answer.component.html',
  styleUrls: ['./single-choice-dropdown-answer.component.css']
})
export class SingleChoiceDropdownAnswerComponent implements OnInit {
  @Input() answerData: Answer[];

  constructor() { }

  ngOnInit() {
  }

   changeRadioButton(event) {
    this.answerData.forEach(item => {
      if (item.id === event.value) {
        item.isCorrect = true;
        return;
      }
      item.isCorrect = false;
    });
  }

   deleteAnswer(answer: Answer) {
    this.answerData.splice(this.answerData.findIndex(ans => ans.id === answer.id), 1);
    const storageAnswer = localStorage.getItem('answerlist');
    const currenAnswerList: Answer[] = JSON.parse(storageAnswer);
    currenAnswerList.splice(currenAnswerList.findIndex(ans => ans.id === answer.id), 1);
    localStorage.setItem('answerlist', JSON.stringify(currenAnswerList));
  }

}
