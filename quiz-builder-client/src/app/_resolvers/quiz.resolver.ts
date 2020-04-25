import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Quiz } from '../_models/quiz';
import { QuizService } from '../_service/quiz.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class QuizResolver implements Resolve<Quiz> {
    constructor(private quizService: QuizService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Quiz> {
        const id = route.url[1].path;
        return this.quizService.getQuiz(id);
    //     .pipe(
    //         catchError(error => {
    //             console.log('Problem retrieving data');
    //             this.router.navigate(['/quizlist']);
    //             return of(null);
    //         })
    //     );
    }
}
