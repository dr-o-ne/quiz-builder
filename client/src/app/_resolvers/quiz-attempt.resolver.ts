import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, NEVER } from 'rxjs';
import { QuizAttemptInfo } from '../_models/attemptInfo';
import { DataProviderService } from 'src/app/_services/dataProvider.service';
import { map } from 'rxjs/operators';

@Injectable()
export class QuizAttemptResolver implements Resolve<QuizAttemptInfo> {

    constructor(
        private dataProviderService: DataProviderService,
        private router: Router) {
    }

    resolve(route: ActivatedRouteSnapshot): Observable<QuizAttemptInfo> {

        const quizId = route.params['id'];

        const res =  this.dataProviderService.startAttempt(quizId).pipe(map(x => x.payload));


        return res;
    }

    onError(): Observable<never> {
        this.router.navigate(['/404']); // TODO: check and add page. Quiz not found | not visible | error
        return NEVER;
    }

}