import { Component, Input, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { Quiz } from 'app/quiz-builder/model/quiz';

@Component({
    selector: 'app-quiz-info-start-page',
    templateUrl: './quiz-info-start-page.component.html',
    styleUrls: ['./quiz-info-start-page.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizInfoStartPageComponent {

    @Input() form!: FormGroup;

    saveFormData(quiz: Quiz): void {
        const value = this.form.value;

        quiz.description = value.description as string;
        quiz.isStartPageEnabled = value.isStartPageEnabled as boolean;
        quiz.isTotalAttemptsEnabled = value.isTotalAttemptsEnabled as boolean;
        quiz.isTimeLimitEnabled = value.isTimeLimitEnabled as boolean;
        quiz.isTotalQuestionsEnabled = value.isTotalQuestionsEnabled as boolean;
        quiz.isPassingScoreEnabled = value.isPassingScoreEnabled as boolean;
    }

}