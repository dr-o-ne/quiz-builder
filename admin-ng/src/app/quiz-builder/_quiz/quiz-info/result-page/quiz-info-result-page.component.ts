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
        /*quiz.IsResultPageEnabled = value.IsResultPageEnabled as boolean;
        quiz.IsResultTotalScoreEnabled = value.IsResultTotalScoreEnabled as boolean;
        quiz.IsResultPassFailEnabled = value.IsResultPassFailEnabled as boolean;
        quiz.IsResultFeedbackEnabled = value.IsResultFeedbackEnabled as boolean;
        quiz.IsResultDurationEnabled = value.IsResultDurationEnabled as boolean;*/
    }
}