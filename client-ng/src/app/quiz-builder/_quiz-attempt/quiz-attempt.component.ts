import { ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { QuestionAttemptInfo, QuizAttemptInfo } from '../model/attemptInfo';
import { QuestionAttemptResult } from '../model/attemptResult';

@Component({
    selector: 'qb-quiz-attempt',
    templateUrl: './quiz-attempt.component.html',
    styleUrls: ['./quiz-attempt.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizAttemptComponent {

    animationDirection: 'left' | 'right' | 'none';

    attempt!: QuizAttemptInfo;
    currentPageIndex!: number;

    constructor(
        private changeDetectorRef: ChangeDetectorRef,
        private route: ActivatedRoute
    ) {
        this.attempt = this.route.snapshot.data.attempt;
        this.currentPageIndex = 0;
    }

    gotoPreviousPage(): void {
        if (this.currentPageIndex === 0) {
            return;
        }

        // Set the animation direction
        this.animationDirection = 'right';

        // Run change detection so the change
        // in the animation direction registered
        //this.changeDetectorRef.detectChanges();

        this.currentPageIndex--;
    }

    gotoNextPage(): void {
        if (this.currentPageIndex === this.attempt.pages.length - 1) {
            return;
        }

        // Set the animation direction
        this.animationDirection = 'left';

        // Run change detection so the change
        // in the animation direction registered
        //this.changeDetectorRef.detectChanges();

        this.currentPageIndex++;
    }

    getQuestions(): QuestionAttemptInfo[] {

        console.log(this.currentPageIndex);
        const res = this.attempt.pages[this.currentPageIndex].questions;

        console.log(res);

        return res;
    }

    onAnswer(answer: QuestionAttemptResult): void {
    }

}