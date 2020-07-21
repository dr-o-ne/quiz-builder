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

    saveFormData(quiz: Quiz): void {

        console.log(this.form.value);

        quiz.name = this.form.value.name as string;
        quiz.pageSettings = this.form.value.pageSettings as number;
        quiz.questionsPerPage = this.form.value.questionsPerPage as number;
        quiz.isPrevButtonEnabled = this.form.value.isPrevButtonEnabled as boolean;
        quiz.randomizeQuestions = this.form.value.randomizeQuestions as boolean;
    }

    isQuestionsPerPageVisisble(): boolean {
        return this.form.value.pageSettings === PageSettings.Custom;
    }

    isRandomizeAllVisible(): boolean {
        const pageSettings = this.form.value.pageSettings as PageSettings;
        return pageSettings === PageSettings.Custom || pageSettings === PageSettings.PagePerQuiz || pageSettings === PageSettings.PagePerQuestion;
    }

}