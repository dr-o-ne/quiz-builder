import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, NEVER } from 'rxjs';
import { map } from 'rxjs/operators';
import { StartPageInfo } from '../model/startPageInfo';
import { DataProviderService } from '../services/dataProviders/dataProvider.service';

@Injectable()
export class StartPageInfoResolver implements Resolve<StartPageInfo> {

    constructor(
        private dataProviderService: DataProviderService,
        private router: Router) {
    }

    resolve(route: ActivatedRouteSnapshot): Observable<StartPageInfo> {
        const quizId = route.params['id'];
        return this.dataProviderService.getStartPageInfo(quizId).pipe(map(x => x.payload));
    }

    onError(): Observable<never> {
        this.router.navigate(['/404']); // TODO: check and add page. Quiz not found | not visible | error
        return NEVER;
    }
    

}