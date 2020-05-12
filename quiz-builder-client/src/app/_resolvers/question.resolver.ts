import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import {Observable, of, pipe} from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Question } from '../_models/question';
import { QuestionService } from '../_service/question.service';

@Injectable()
export class QuestionResolver implements Resolve<Question> {
    constructor(private questionService: QuestionService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Question> {
        const id = route.url[3].path;
        return this.questionService.getQuestion(id)
        .pipe(
            catchError(error => {
                console.log('Problem retrieving data');
                this.router.navigate(['/quizlist']);
                return of(null);
            })
        );
    }
}
