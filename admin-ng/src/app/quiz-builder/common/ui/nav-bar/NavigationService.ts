import { Injectable } from '@angular/core';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';

@Injectable({
    providedIn: 'root'
})
export class NavigationService {

    public static QUIZ_NAV_ROOT = 'QuizRoot';
    public static QUESTION_NAV_ROOT = 'QuestionRoot';

    constructor(
        private fuseNavigationService: FuseNavigationService
    ) {
    }

    public clean(): void {
        this.fuseNavigationService.removeNavigationItem(NavigationService.QUIZ_NAV_ROOT);
        this.fuseNavigationService.removeNavigationItem(NavigationService.QUESTION_NAV_ROOT);
    }

}