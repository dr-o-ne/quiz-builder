import { Component, OnInit, Input } from '@angular/core';
import { Answer } from 'src/app/_models/answer';
import {Question, QuestionType} from 'src/app/_models/question';

@Component({
  selector: 'app-multiple-choice-answer',
  templateUrl: './multiple-choice-answer.component.html',
  styleUrls: ['./multiple-choice-answer.component.css']
})
export class MultipleChoiceAnswerComponent implements OnInit {
  @Input() answerData: Answer[];
  @Input() question: Question;
  questionType = QuestionType;
  constructor() { }

  ngOnInit() {
  }

  changeRadioButton(event) {
    if (!event.value) {
      return;
    }
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
    newAnswer.text = name || '';
    newAnswer.id = this.generateId();
    newAnswer.isCorrect = isCorrect || false;
    this.answerData.push(newAnswer);
  }

  generateId() {
    return Math.floor(Math.random() * 10000) + 1;
  }

}
