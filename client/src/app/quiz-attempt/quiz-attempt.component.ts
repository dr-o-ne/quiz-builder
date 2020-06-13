import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AttemptInfo, QuestionAttemptInfo } from '../_models/attemptInfo';
import { Appearance } from '../_models/appearance';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html',
  styles: [
  ]
})

export class QuizAttemptComponent implements OnInit {

  attempt: AttemptInfo;
  currentGroupIndex: number;

  @ViewChild("header") headerView: ElementRef;
  @ViewChild("content") contentView: ElementRef;
  @ViewChild("footer") footerView: ElementRef;

  constructor(
    private elementRef: ElementRef,
    private route: ActivatedRoute
    ) { 
      this.attempt = this.route.snapshot.data.attempt;
      this.currentGroupIndex = 0;
   }

  ngOnInit(): void {
  }

  setAppearance( appearance: Appearance ): void {
    this.headerView.nativeElement.style.backgroundColor = appearance.headerBackground;
    this.contentView.nativeElement.style.backgroundColor = appearance.mainBackground;
    this.footerView.nativeElement.style.backgroundColor = appearance.footerBackground;
  }

  ngAfterViewInit(): void {
    this.setAppearance(this.attempt.appearance);
  }

  getQuestions(): QuestionAttemptInfo[] {
    return this.attempt.quiz.groups[this.currentGroupIndex].questions;
  }

}
