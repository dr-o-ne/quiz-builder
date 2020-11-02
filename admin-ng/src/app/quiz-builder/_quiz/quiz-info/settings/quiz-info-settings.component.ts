import { Component, Input, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { PageSettings, Quiz } from 'app/quiz-builder/model/quiz';
import { QuizLangService } from 'app/quiz-builder/services/lang/quiz.lang.service';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector: 'app-quiz-info-settings',
    templateUrl: './quiz-info-settings.component.html',
    styleUrls: ['./quiz-info-settings.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizInfoSettingsComponent {

    @Input() form: FormGroup;

    pageSettings = PageSettings;
    pageSettingsKeys: number[];

    constructor(public quizLangService: QuizLangService) {
        this.pageSettingsKeys = Object.keys(this.pageSettings).filter(Number).map(x => Number(x));
    }

    saveFormData(quiz: Quiz): void {
        const value = this.form.value;

        quiz.name = value.name as string;
        quiz.pageSettings = value.pageSettings as number;
        quiz.questionsPerPage = value.questionsPerPage as number;
        quiz.isPrevButtonEnabled = value.isPrevButtonEnabled as boolean;
        quiz.randomizeGroups = value.randomizeGroups as boolean;
        quiz.randomizeQuestions = value.randomizeQuestions as boolean;
        quiz.isScheduleEnabled = value.isScheduleEnabled as boolean;
        quiz.startDate = (value.startDate as moment.Moment).utc().unix();
        quiz.endDate = (value.endDate as moment.Moment).utc().unix();
        quiz.isPassingScoreEnabled = value.isPassingScoreEnabled as boolean;
        quiz.passingScore = value.passingScore as number;
    }

    isQuestionsPerPageVisisble(): boolean {
        return this.form.value.pageSettings === PageSettings.Custom;
    }

    isRandomizeGroupsVisible(): boolean {
        const pageSettings = this.form.value.pageSettings as PageSettings;
        return pageSettings === PageSettings.PagePerGroup;
    }

    isRandomizeAllVisible(): boolean {
        const pageSettings = this.form.value.pageSettings as PageSettings;
        return pageSettings === PageSettings.Custom || pageSettings === PageSettings.PagePerQuiz || pageSettings === PageSettings.PagePerQuestion;
    }

    isScheduleVisible(): boolean {
        return this.form.value.isScheduleEnabled as boolean;
    }

}