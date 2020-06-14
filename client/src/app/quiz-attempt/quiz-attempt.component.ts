import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AttemptInfo, QuestionAttemptInfo } from '../_models/attemptInfo';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html'
})

export class QuizAttemptComponent implements OnInit {

  attempt: AttemptInfo;
  currentGroupIndex: number;

  constructor(
    private route: ActivatedRoute
    ) { 
      this.attempt = this.route.snapshot.data.attempt;
      this.currentGroupIndex = 0;
   }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
  }

  getQuestions(): QuestionAttemptInfo[] {
    return this.attempt.quiz.groups[this.currentGroupIndex].questions;
  }

}
