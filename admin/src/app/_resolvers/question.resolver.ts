import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { QuestionDataProvider } from '../_service/dataProviders/question.dataProvider';
import { Question } from '../_models/question';

@Injectable()
export class QuestionResolver implements Resolve<Question> {

  constructor(
    private questionDataProvider: QuestionDataProvider) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Question> {
    const id = route.params.id;
    return this.questionDataProvider.getQuestion(id).pipe( map( x => x.payload ) );
  }

}
