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
        quiz.introduction = value.introduction as string;
        quiz.isIntroductionEnabled = value.isIntroductionEnabled as boolean;
    }

}