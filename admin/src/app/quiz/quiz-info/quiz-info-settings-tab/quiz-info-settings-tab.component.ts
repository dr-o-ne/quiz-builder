import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Quiz, PageSettings } from 'src/app/_models/quiz';
import { QuizLangService } from 'src/app/_service/lang/quiz.lang.service';

@Component({
    selector: 'app-quiz-info-settings-tab',
    templateUrl: './quiz-info-settings-tab.component.html'
})
export class QuizInfoSettingsTabComponent {

    @Input() form: FormGroup;
    @Input() isEditMode: boolean;

    pageSettings = PageSettings;
    pageSettingsKeys: number[];

    constructor(public quizLangService: QuizLangService) {
        this.pageSettingsKeys = Object.keys(this.pageSettings).filter(Number).map(x => Number(x));
    }

    SaveFormData(quiz: Quiz): void {
        quiz.name = this.form.value.name as string;
        quiz.pageSettings = this.form.value.pageSettings as number;
        quiz.isPrevButtonEnabled = this.form.value.isPrevButtonEnabled as boolean;
    }

}