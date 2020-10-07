import { Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { QuizAttemptFeedback } from 'app/quiz-builder/model/attemptFeedback';

@Component({
    selector: 'qb-result-page-info',
    templateUrl: './result-page-info.component.html',
    styleUrls: ['./result-page-info.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ResultPageInfoComponent {

    data!: QuizAttemptFeedback;

    constructor(
        private route: ActivatedRoute
    ) {
        this.data = this.route.snapshot.data.data;
    }

}