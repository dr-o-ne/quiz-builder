import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Quiz } from 'src/app/_models/quiz';

@Component({
    selector: 'app-quiz-info-settings-tab',
    templateUrl: './quiz-info-settings-tab.component.html'
})
export class QuizInfoSettingsTabComponent {

    @Input() form: FormGroup;
    
    SaveFormData(quiz: Quiz): void {
        quiz.name = this.form.value.name as string;
    }

}