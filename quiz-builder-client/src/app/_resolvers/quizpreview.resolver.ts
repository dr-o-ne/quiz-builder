import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Quiz } from '../_models/quiz';
import { QuizService } from '../_service/quiz.service';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class QuizPreviewResolver implements Resolve<Quiz> {
  constructor( private quizService: QuizService, private router: Router ) {
  }

  resolve( route: ActivatedRouteSnapshot ) {
    const id = route.params.id;
    return this.quizService.getAllQuestions( id )
      .pipe(
        catchError( error => {
          console.log( 'Problem retrieving data' );
          this.router.navigate( [ '/quizzes' ] );
          return of( null );
        } )
      );
  }
}
