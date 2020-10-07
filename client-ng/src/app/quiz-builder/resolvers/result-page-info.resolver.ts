import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, NEVER } from 'rxjs';
import { map } from 'rxjs/operators';
import { QuizAttemptFeedback } from '../model/attemptFeedback';
import { DataProviderService } from '../services/dataProviders/dataProvider.service';

@Injectable()
export class ResultPageInfoResolver implements Resolve<QuizAttemptFeedback> {

    constructor(
        private dataProviderService: DataProviderService,
        private router: Router) {
    }

    resolve(route: ActivatedRouteSnapshot): Observable<QuizAttemptFeedback> {
        const attemptId = route.params['id'];
        return this.dataProviderService.getResultPageInfo(attemptId).pipe(map(x => x.payload));
    }

    onError(): Observable<never> {
        this.router.navigate(['/404']); // TODO: check and add page. Quiz not found | not visible | error
        return NEVER;
    }
    

}