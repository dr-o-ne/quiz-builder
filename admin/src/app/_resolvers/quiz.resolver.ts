import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, NEVER } from 'rxjs';
import { single, tap } from 'rxjs/operators';
import { QuizDataProvider } from '../_service/dataProviders/quiz.dataProvider';
import { Quiz } from '../_models/quiz';

@Injectable()
export class QuizResolver implements Resolve<Quiz> {

  constructor(
    private quizDataProvider: QuizDataProvider,
    private router: Router) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Quiz> {
    const id = route.params.id;

    return this.quizDataProvider.getQuiz(id)
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