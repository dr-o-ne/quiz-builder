import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of, NEVER } from 'rxjs';
import { map } from 'rxjs/operators';
import { Question } from '../_models/question';
import { QuestionDataProvider } from '../_service/dataProviders/question.dataProvider';

@Injectable()
export class QuestionResolver implements Resolve<Question> {

  constructor(
    private questionDataProvider: QuestionDataProvider,
    private router: Router) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Question> {
    const id = route.params.id;
    return this.questionDataProvider.getQuestion(id).pipe( map( x => x.payload ) );
  }

  onEmpty(): Observable<never> {
    this.router.navigate(['/404']);
    return NEVER;
  }

}
