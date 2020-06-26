import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Quiz } from 'src/app/_models/quiz';

@Component({
    selector: 'app-quiz-info-settings',
    templateUrl: './quiz-info-settings.component.html'
})
export class QuizInfoSettingsComponent {

    @Input() form: FormGroup;
    
    SaveFormData(quiz: Quiz): void {
        quiz.name = this.form.value.name as string;
    }

}