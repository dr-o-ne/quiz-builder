import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { QuizService } from '../_service/quiz.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Group } from '../_models/group';

@Injectable()
export class GroupResolver implements Resolve<Group> {
    constructor(private quizService: QuizService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Group> {
        const id = route.url[3].path;
        return this.quizService.getGroup(id);
    //     .pipe(
    //         catchError(error => {
    //             console.log('Problem retrieving data');
    //             this.router.navigate(['/quizlist']);
    //             return of(null);
    //         })
    //     );
    }
}
