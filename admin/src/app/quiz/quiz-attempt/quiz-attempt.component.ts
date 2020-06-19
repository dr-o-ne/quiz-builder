import { Component, OnInit } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { Question } from 'src/app/_models/question';
import { Choice } from 'src/app/_models/choice';
import { Router, ActivatedRoute } from '@angular/router';
import { AttemptService } from 'src/app/_service/attempt.service';
import { QuizAttempt } from 'src/app/_models/attempt';
import { ModalWindowAttemptComponent } from './modal-window-attempt/modal-window-attempt.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html',
  styleUrls: ['./quiz-attempt.component.css']
})
export class QuizAttemptComponent implements OnInit {
  quiz: Quiz;
  questions: Question[];
  quizAttemptId: string;

  anchor = 'q';

  constructor( private router: Router,
               private activeRoute: ActivatedRoute,
               private attemptService: AttemptService,
               public dialog: MatDialog
  ) {
  }

  ngOnInit(): void {
    if ( !history.state.quiz ) {
      this.router.navigate(['../../'], { relativeTo: this.activeRoute.parent });
      return;
    }
    this.quiz = history.state.quiz;
    this.createQuizAttempt();
  }

  createQuizAttempt(): void {

    this.attemptService.runAttempt(this.quiz.id);

  }

  initPreview(): void {
    this.questions.forEach(q => {
      q.choices = q.choices ? JSON.parse( q.choices ) : [new Choice(1, '', false)];
    });
  }

  updateQuizAttempt(quizAttempt: QuizAttempt): void {
    this.attemptService.updateAttempt(quizAttempt).subscribe((response: any) => {
      if (response.hasOwnProperty( 'score' )) {
        this.openResult(response.score);
      }
    }, error => console.log(error));
  }

  openResult(socore: number): void {
    const dialogRef = this.dialog.open( ModalWindowAttemptComponent, {
      width: '50em',
      data: { socore }
    } );

    dialogRef.afterClosed().subscribe( result => {
      this.router.navigate([''], { relativeTo: this.activeRoute.parent });
    } );
  }

}
