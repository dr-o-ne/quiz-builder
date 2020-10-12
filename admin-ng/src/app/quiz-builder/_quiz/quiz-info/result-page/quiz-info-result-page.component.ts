import { Component, Input, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { Quiz } from 'app/quiz-builder/model/quiz';

@Component({
    selector: 'app-quiz-info-result-page',
    templateUrl: './quiz-info-result-page.component.html',
    styleUrls: ['./quiz-info-result-page.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizInfoResultPageComponent {

    @Input() form!: FormGroup;

    saveFormData(quiz: Quiz): void {
        const value = this.form.value;
        
        quiz.resultPassText = value.resultPassText as string;
        quiz.resultFailText = value.resultFailText as string;
        quiz.isResultPageEnabled = value.isResultPageEnabled as boolean;
        quiz.isResultTotalScoreEnabled = value.isResultTotalScoreEnabled as boolean;
        quiz.isResultPassFailEnabled = value.isResultPassFailEnabled as boolean;
        //quiz.isResultFeedbackEnabled = value.isResultFeedbackEnabled as boolean;
        quiz.isResultDurationEnabled = value.isResultDurationEnabled as boolean;
    }
}