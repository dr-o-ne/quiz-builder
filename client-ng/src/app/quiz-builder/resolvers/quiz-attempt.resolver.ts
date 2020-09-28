import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, NEVER } from 'rxjs';
import { map } from 'rxjs/operators';
import { QuizAttemptInfo } from '../model/attemptInfo';
import { DataProviderService } from '../services/dataProviders/dataProvider.service';

@Injectable()
export class QuizAttemptResolver implements Resolve<QuizAttemptInfo> {

    constructor(
        private dataProviderService: DataProviderService,
        private router: Router) {
    }

    resolve(route: ActivatedRouteSnapshot): Observable<QuizAttemptInfo> {
        const quizId = route.params['id'];
        return  this.dataProviderService.startAttempt(quizId).pipe(map(x => x.payload));
    }

    onError(): Observable<never> {
        this.router.navigate(['/404']); // TODO: check and add page. Quiz not found | not visible | error
        return NEVER;
    }

}