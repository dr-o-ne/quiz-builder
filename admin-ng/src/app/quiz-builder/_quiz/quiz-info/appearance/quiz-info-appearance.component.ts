import { Component, Input, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { Quiz } from 'app/quiz-builder/model/quiz';

@Component({
    selector: 'app-quiz-info-appearance',
    templateUrl: './quiz-info-appearance.component.html',
    styleUrls: ['./quiz-info-appearance.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizInfoEppearancePageComponent {

    @Input() form!: FormGroup;

    saveFormData(quiz: Quiz): void {
        const value = this.form.value;
    }

}