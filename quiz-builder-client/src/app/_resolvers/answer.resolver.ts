import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Answer } from '../_models/answer';
import { AnswerService } from '../_service/answer.service';

@Injectable()
export class AnswerResolver implements Resolve<Answer> {
    constructor(private answerService: AnswerService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Answer> {
        return this.answerService.getAnswer(route.params.id);
    //     .pipe(
    //         catchError(error => {
    //             console.log('Problem retrieving data');
    //             this.router.navigate(['/quizlist']);
    //             return of(null);
    //         })
    //     );
    }
}
