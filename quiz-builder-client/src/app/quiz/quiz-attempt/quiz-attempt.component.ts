import { Component, OnInit } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { Question } from 'src/app/_models/question';
import { Choice } from 'src/app/_models/choice';
import { Router, ActivatedRoute } from '@angular/router';
import { AttemptService } from 'src/app/_service/attempt.service';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html',
  styleUrls: ['./quiz-attempt.component.css']
})
export class QuizAttemptComponent implements OnInit {
  quiz: Quiz;
  questions: Question[];

  anchor = 'q';

  constructor( private router: Router,
               private activeRoute: ActivatedRoute,
               private attemptService: AttemptService
  ) {
  }

  ngOnInit(): void {
    if ( !history.state.quiz ) {
      this.router.navigate(['../../'], { relativeTo: this.activeRoute.parent });
      return;
    }
    this.quiz = history.state.quiz;

    this.attemptService.createAttempt(this.quiz.id).subscribe( (response: any) => {
      if ( response.hasOwnProperty( 'questions' ) ) {
        this.questions = response.questions;
        this.initPreview();
      }
    }, error => console.log(error));

  }

  initPreview(): void {
    this.questions.every(q => q.choices = JSON.parse( q.choices ));
  }

}
