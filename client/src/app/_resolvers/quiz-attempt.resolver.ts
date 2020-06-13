import { Injectable } from '@angular/core';
import { Router, Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, NEVER } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AttemptInfo } from '../_models/attemptInfo';
import { AttemptService } from 'src/app/_services/attempt.service';

@Injectable()
export class QuizAttemptResolver implements Resolve<AttemptInfo> {

    constructor(
        private attemptService: AttemptService,
        private router: Router) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<AttemptInfo> | Observable<never> {

        const quizId = route.params['id'];

        return this.attemptService.startAttempt(quizId).pipe(
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