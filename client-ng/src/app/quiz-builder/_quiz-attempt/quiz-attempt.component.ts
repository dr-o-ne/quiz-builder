import { Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { QuizAttemptInfo } from '../model/attemptInfo';

@Component({
    selector: 'qb-quiz-attempt',
    templateUrl: './quiz-attempt.component.html',
    styleUrls: ['./quiz-attempt.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizAttemptComponent {

    attempt!: QuizAttemptInfo;
    currentPageIndex!: number;

    constructor(
        private route: ActivatedRoute
    ) {
        this.attempt = this.route.snapshot.data.attempt;
        this.currentPageIndex = 0;
    }

}