import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { QuizAttemptInfo, QuestionAttemptInfo } from '../_models/attemptInfo';
import { QuestionAttemptResult, QuizAttemptResult } from '../_models/attemptResult';
import { DataProviderService } from '../_services/dataProvider.service';
import { QuizAttemptFeedback } from '../_models/attemptFeedback';
import { MatDialog } from '@angular/material/dialog';
import { EndPageModalDialog } from './end-page/end-page-modal.component';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html'
})

export class QuizAttemptComponent implements OnInit {

  attempt: QuizAttemptInfo;
  currentGroupIndex: number;
  answers: Map<string, QuestionAttemptResult>;

  constructor(
    private route: ActivatedRoute,
    private dataProviderService: DataProviderService,
    public dialog: MatDialog
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

  onAnswer(answer: QuestionAttemptResult): void {
    this.answers.set(answer.questionId, answer);
  }

  onSubmit(): void {

    const result = new QuizAttemptResult();
    result.id = this.attempt.id;
    result.answers = [...this.answers.values()];

    this.dataProviderService.endAttempt(result).subscribe(
      (quizAttemptFeedback: QuizAttemptFeedback) => { console.log(quizAttemptFeedback.score); this.openDialog(quizAttemptFeedback); }
    );
  }

  openDialog(quizAttemptFeedback: QuizAttemptFeedback): void {
    const dialogRef = this.dialog.open(EndPageModalDialog, {
      width: '500px',
      data: quizAttemptFeedback
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

}
