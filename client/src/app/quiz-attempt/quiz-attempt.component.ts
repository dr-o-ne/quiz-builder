import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuizAttemptInfo, QuestionAttemptInfo } from '../_models/attemptInfo';
import { QuestionAttemptResult, QuizAttemptResult } from '../_models/attemptResult';
import { DataProviderService } from '../_services/dataProvider.service';
import { QuizAttemptFeedback } from '../_models/attemptFeedback';
import { MatDialog } from '@angular/material/dialog';
import { EndPageModalDialog } from './end-page/end-page-modal.component';
import { QuizNavPanelComponent } from './quiz-nav-panel/quiz-nav-panel.component';
import { MatButton } from '@angular/material/button';
import { ApiResponse } from '../_services/apiResponse';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html'
})

export class QuizAttemptComponent implements AfterViewInit {

  attempt: QuizAttemptInfo;
  currentPageIndex: number;
  answers: Map<string, QuestionAttemptResult>;

  @ViewChild(QuizNavPanelComponent)
  private navPanelComponent!: QuizNavPanelComponent;

  @ViewChild("prev", { static: false })
  private prevButton!: MatButton;

  @ViewChild("next", { static: false })
  private nextButton!: MatButton;

  constructor(
    private route: ActivatedRoute,
    private dataProviderService: DataProviderService,
    public dialog: MatDialog
  ) {
    this.attempt = this.route.snapshot.data.attempt;

    console.log(123);

    console.log(this.route.snapshot.data);

    this.answers = new Map<string, QuestionAttemptResult>();
    this.currentPageIndex = 0;
  }

  ngAfterViewInit(): void {
    this.setPrevButtonVisibility();
    this.setNextButtonVisibility();
  }

  getAnchor(i: number): string {
    return 'q' + (i + 1);
  }

  getQuestions(): QuestionAttemptInfo[] {
    return this.attempt.pages[this.currentPageIndex].questions;
  }

  onAnswer(answer: QuestionAttemptResult): void {
    const i = this.getQuestions().map(x => x.id).indexOf(answer.questionId);
    this.navPanelComponent.setCheckedValue(i, answer.isCompleted());

    this.answers.set(answer.questionId, answer);
  }

  onSubmit(): void {

    const result = new QuizAttemptResult(this.attempt.id, [...this.answers.values()]);

    this.dataProviderService.endAttempt(result).subscribe(
      (quizAttemptFeedback: ApiResponse<QuizAttemptFeedback>) => { this.openDialog(quizAttemptFeedback.payload); }
    );
  }

  onPrev(): void {

    this.currentPageIndex -= 1;

    this.setPrevButtonVisibility();
    this.setNextButtonVisibility();
  }

  onNext(): void {

    this.currentPageIndex += 1;

    this.setPrevButtonVisibility();
    this.setNextButtonVisibility();
  }

  setNextButtonVisibility(): void {

    let isVisible = true;

    if (this.attempt.pages.length === 1) isVisible = false;
    if (this.currentPageIndex === this.attempt.pages.length - 1) isVisible = false;

    this.setButtonVisibility(this.nextButton, isVisible);
  }

  setPrevButtonVisibility(): void {

    let isVisible = true;

    if (this.attempt.pages.length === 1) isVisible = false;
    if (this.currentPageIndex === 0) isVisible = false;
    if (this.attempt.settings.isPrevButtonEnabled === false ) isVisible = false;

    this.setButtonVisibility(this.prevButton, isVisible);
  }

  setButtonVisibility(button: MatButton, isVisible: boolean): void {
    if (isVisible)
      button._elementRef.nativeElement.classList.remove("hidden");
    else
      button._elementRef.nativeElement.classList.add("hidden");
  }

  openDialog(quizAttemptFeedback: QuizAttemptFeedback): void {
    const dialogRef = this.dialog.open(EndPageModalDialog, {
      width: '500px',
      data: quizAttemptFeedback
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log('The dialog was closed');
    });
  }

}
