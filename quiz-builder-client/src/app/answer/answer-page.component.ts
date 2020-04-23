import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AnswerService } from '../_service/answer.service';
import { Quiz } from '../_models/quiz';
import { Question } from '../_models/question';
import { Answer } from '../_models/answer';

@Component({
  selector: 'app-answer-page',
  templateUrl: './answer-page.component.html',
  styleUrls: ['./answer-page.component.css']
})
export class AnswerPageComponent implements OnInit {
  quiz: Quiz;
  question: Question;
  answer: Answer;
  answerForm: FormGroup;

  constructor(private fb: FormBuilder, private router: Router, private activeRout: ActivatedRoute,
              private answerService: AnswerService) {}

  ngOnInit() {
    this.initValidate();

    this.activeRout.data.subscribe(data => {
      if (data.hasOwnProperty('quiz') && data.hasOwnProperty('question')) {
        this.quiz = data.quiz;
        this.question = data.question;
        if (data.hasOwnProperty('answer')) {
          this.answer = data.answer;
          return;
        }
        this.answer = new Answer();
      } else {
        console.log('Not found correct quiz');
      }
    });
  }

  initValidate() {
    this.answerForm = this.fb.group({
      name: ['', Validators.required],
      isCorrect: ['']
    });
  }

  saveOrUpdate(operation: string) {
    if (this.answerForm.valid) {
      this.answer = Object.assign(this.answer, this.answerForm.value);
      this.answer.questionId = this.question.id;
      localStorage.setItem('answer-' + operation, JSON.stringify(this.answer));
      this.router.navigate(['/editquiz/', this.quiz.id, 'editquestion', this.question.id]);
    }
  }

  saveAnswer() {
    this.saveOrUpdate('save');
  }

  updateAnswer() {
    this.saveOrUpdate('update');
  }

}
