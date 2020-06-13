import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AttemptInfo } from '../_models/attemptInfo';
import { Appearance } from '../_models/appearance';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html',
  styles: [
  ]
})

export class QuizAttemptComponent implements OnInit {

  attempt: AttemptInfo;

  @ViewChild("header") headerView: ElementRef;
  @ViewChild("content") contentView: ElementRef;
  @ViewChild("footer") footerView: ElementRef;

  constructor(
    private elementRef: ElementRef,
    private route: ActivatedRoute
    ) { 
      this.attempt = this.route.snapshot.data.attempt;
   }

  ngOnInit(): void {
  }

  setAppearance( appearance: Appearance ) {
    this.headerView.nativeElement.style.backgroundColor = appearance.headerBackground;
    this.contentView.nativeElement.style.backgroundColor = appearance.mainBackground;
    this.footerView.nativeElement.style.backgroundColor = appearance.footerBackground;
  }

  ngAfterViewInit() {
    this.setAppearance(this.attempt.appearance);
  }

}
