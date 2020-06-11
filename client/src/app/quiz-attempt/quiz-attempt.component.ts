import { Component, ElementRef, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AttemptService } from 'src/app/_services/attempt.service';

import { AttemptInfo } from '../_models/attemptInfo';
import { Appearance } from '../_models/appearance';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html',
  styles: [
  ]
})

export class QuizAttemptComponent implements OnInit {

  private attempt: AttemptInfo;

  constructor(
    private elementRef: ElementRef,
    private route: ActivatedRoute,
    private attemptService: AttemptService
    ) { 
  }

  ngOnInit(): void {
    const quizId = this.route.snapshot.paramMap.get('id') || '';
    
    this.startQuizAttempt( quizId );
    
  }

  startQuizAttempt( quizId: string ): void {
    this.attemptService.startAttempt( quizId ).subscribe( (attempt: AttemptInfo) => {
      this.attempt = attempt;
      this.setAppearance( attempt.appearance );
    } );

  }
  
  setAppearance( appearance: Appearance ) {
    this.elementRef.nativeElement.ownerDocument.body.style.backgroundColor = appearance.mainBackground;
  }

}
