import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { QuizDataProvider } from '../_service/dataProviders/quiz.dataProvider';
import { Quiz } from '../_models/quiz';

@Injectable()
export class QuizResolver implements Resolve<Quiz> {

  constructor(
    private quizDataProvider: QuizDataProvider) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Quiz> {
    const id = route.params.id;
    return this.quizDataProvider.getQuiz(id).pipe( map( x => x.payload ) );
  }

}