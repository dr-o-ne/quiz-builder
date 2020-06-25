import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Quiz } from '../_models/quiz';
import { QuizService } from '../_service/quiz.service';
import { Observable, NEVER } from 'rxjs';
import { single, tap } from 'rxjs/operators';

@Injectable()
export class QuizResolver implements Resolve<Quiz> {

  constructor(
    private quizService: QuizService,
    private router: Router) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Quiz> {
    const id = route.params.id;

    return this.quizService.getQuiz(id)
      .pipe(
        single(),
        tap(quiz => { return quiz ? quiz : this.onEmpty(); })
      );
  }

  onEmpty(): Observable<never> {
    this.router.navigate(['/404']);
    return NEVER;
  }

}