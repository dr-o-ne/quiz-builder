import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import { Question } from 'src/app/_models/question';
import { FormControl, Validators, FormArray } from '@angular/forms';

@Component({
  selector: 'app-multiple-choice-answer',
  templateUrl: './multiple-choice-answer.component.html',
  styleUrls: ['./multiple-choice-answer.component.css']
})
export class MultipleChoiceAnswerComponent implements OnInit {
  @Input() answerData: Answer[];
  @Input() question: Question;

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

  addNewAnswer() {
    switch (this.question.type) {
      default:
        this.addAnswer();
        break;
    }
  }

  addAnswer(name?: string, isCorrect?: boolean) {
    const newAnswer = new Answer();
    newAnswer.name = name || '';
    newAnswer.id = this.generateId();
    newAnswer.isCorrect = isCorrect || false;
    newAnswer.questionId = this.question.id;
    this.answerData.push(newAnswer);
  }

  generateId() {
    return Math.floor(Math.random() * 10000) + 1;
  }

}
