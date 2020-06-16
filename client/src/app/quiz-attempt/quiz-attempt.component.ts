import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { QuizAttemptInfo, QuestionAttemptInfo } from '../_models/attemptInfo';
import { QuestionAttemptResult } from '../_models/attemptResult';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html'
})

export class QuizAttemptComponent implements OnInit {

  attempt: QuizAttemptInfo;
  currentGroupIndex: number;
  answers: Map<string, QuestionAttemptResult>;

  constructor(
    private route: ActivatedRoute
  ) {
    this.attempt = this.route.snapshot.data.attempt;
    this.answers = new Map<string, QuestionAttemptResult>();
    this.currentGroupIndex = 0;
  }

  ngOnInit(): void {
  }

  getQuestions(): QuestionAttemptInfo[] {
    return this.attempt.groups[this.currentGroupIndex].questions;
  }

  onAnswer(answer: QuestionAttemptResult) {
    this.answers.set(answer.questionId, answer);

    console.log('got an answer');
  }

}
