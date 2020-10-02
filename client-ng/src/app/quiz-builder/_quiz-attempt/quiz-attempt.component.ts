import { Component, ViewEncapsulation } from '@angular/core';
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

    attempt!: QuizAttemptInfo;
    currentPageIndex!: number;

    constructor(
        private route: ActivatedRoute
    ) {
        this.attempt = this.route.snapshot.data.attempt;
        this.currentPageIndex = 0;
    }

    
    gotoPreviousPage(): void {
        if (this.currentPageIndex === 0) {
            return;
        }
        this.currentPageIndex--;
    }

    gotoNextPage(): void {
        if (this.currentPageIndex === this.attempt.pages.length - 1) {
            return;
        }
        this.currentPageIndex++;
    }

    getQuestions(): QuestionAttemptInfo[] {
        const res = this.attempt.pages[this.currentPageIndex].questions;
        return res;
    }

    onAnswer(answer: QuestionAttemptResult): void {
    }

}