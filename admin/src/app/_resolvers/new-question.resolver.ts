import { Injectable } from '@angular/core';
import { Resolve, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { QuestionDataProvider } from '../_service/dataProviders/question.dataProvider';
import { Question } from '../_models/question';

@Injectable()
export class NewQuestionResolver implements Resolve<Question> {

  constructor(
    private router: Router,
    private questionDataProvider: QuestionDataProvider) {
  }

  resolve(): Observable<Question> {
    const navigation = this.router.getCurrentNavigation();
    const questionType = navigation.extras.state.questionType;

    return this.questionDataProvider.getQuestionTemplate(questionType).pipe( map( x => x.payload ) );
  }

}
