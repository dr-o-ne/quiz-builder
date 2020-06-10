import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AttemptService } from 'src/app/_services/attempt.service';

import { AttemptInfo, QuizAttemptInfo } from '../_models/attemptInfo';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html',
  styles: [
  ]
})

export class QuizAttemptComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private attemptService: AttemptService
    ) { 

  }

  ngOnInit(): void {
    const quizId = this.route.snapshot.paramMap.get('id') || '';
    this.startQuizAttempt( quizId );
  }

  startQuizAttempt( quizId: string ): void {
    this.attemptService.startAttempt( quizId ).subscribe( (attemptInfo: AttemptInfo) => {
      if( attemptInfo.id.trim() !== '' ) {
        console.log( attemptInfo );
      }
      else{
        console.log( 'ERROR' );
      }
    } );
  }

}
