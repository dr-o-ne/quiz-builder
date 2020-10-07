import { Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { QuizAttemptFeedback } from '../model/attemptFeedback';
import { QuestionAttemptInfo, QuizAttemptInfo } from '../model/attemptInfo';
import { QuestionAttemptResult, QuizAttemptResult } from '../model/attemptResult';
import { ApiResponse } from '../services/dataProviders/apiResponse';
import { DataProviderService } from '../services/dataProviders/dataProvider.service';

@Component({
    selector: 'qb-quiz-attempt',
    templateUrl: './quiz-attempt.component.html',
    styleUrls: ['./quiz-attempt.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizAttemptComponent {

    attempt!: QuizAttemptInfo;
    answers!: Map<string, QuestionAttemptResult>;
    currentPageIndex!: number;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private dataProviderService: DataProviderService,
    ) {
        this.attempt = this.route.snapshot.data.attempt;
        this.currentPageIndex = 0;
        this.answers = new Map<string, QuestionAttemptResult>();
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

    submit(): void {
        const result = new QuizAttemptResult(this.attempt.id, [...this.answers.values()]);
        this.dataProviderService.endAttempt(result).subscribe(
            (quizAttemptFeedback: ApiResponse<QuizAttemptFeedback>) => {
                this.router.navigate(['results/' + this.attempt.id]);
            }
        );
    }

}