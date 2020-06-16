import { Injectable } from '@angular/core';
import { Router, Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, NEVER } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { QuizAttemptInfo } from '../_models/attemptInfo';
import { DataProviderService } from 'src/app/_services/dataProvider.service';

@Injectable()
export class QuizAttemptResolver implements Resolve<QuizAttemptInfo> {

    constructor(
        private dataProviderService: DataProviderService,
        private router: Router) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<QuizAttemptInfo> | Observable<never> {

        const quizId = route.params['id'];

        return this.dataProviderService.startAttempt(quizId).pipe(
            attempt => {
                return attempt ? attempt : this.onError();
            }, catchError(() => this.onError())
        );
    }

    onError(): Observable<never> {
        this.router.navigate(['/404']); //TODO: check and add page. Quiz not found | not visible | error
        return NEVER;
    }

}