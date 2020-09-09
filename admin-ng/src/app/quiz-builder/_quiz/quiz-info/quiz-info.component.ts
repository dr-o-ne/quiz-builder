import { Component, ViewChild, AfterViewInit, OnInit, ViewEncapsulation } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTabGroup } from '@angular/material/tabs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Quiz } from 'app/quiz-builder/model/quiz';
import { ApiResponse } from 'app/quiz-builder/services/dataProviders/apiResponse';
import { QuizDataProvider } from 'app/quiz-builder/services/dataProviders/quiz.dataProvider';
import * as moment from 'moment';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector: 'app-quiz-info',
    templateUrl: './quiz-info.component.html',
    styleUrls: ['./quiz-info.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})

export class QuizInfoComponent implements OnInit, AfterViewInit {

    @ViewChild(MatTabGroup) tabGroup: MatTabGroup;

    //@ViewChild(QuizInfoQuestionsTabComponent) questionsControl: QuizInfoQuestionsTabComponent;
    //@ViewChild(QuizInfoSettingsTabComponent) settingsControl: QuizInfoSettingsTabComponent;

    quiz: Quiz;
    quizForm: FormGroup;

    constructor(
        private route: ActivatedRoute,
        private fb: FormBuilder,
        private router: Router,
        private location: Location,
        private quizDataProvider: QuizDataProvider
    ) {
        if (this.route.snapshot.data.quizResolver)
            this.quiz = this.route.snapshot.data.quizResolver;
        else
            this.quiz = new Quiz();
    }

    ngOnInit(): void {
        this.quizForm = this.fb.group({
            settings: this.fb.group({
                name: [this.quiz.name, Validators.required],
                pageSettings: [this.quiz.pageSettings],
                questionsPerPage: [this.quiz.questionsPerPage, Validators.min(1)],
                isPrevButtonEnabled: [this.quiz.isPrevButtonEnabled],
                randomizeGroups: [this.quiz.randomizeGroups],
                randomizeQuestions: [this.quiz.randomizeQuestions],
                isScheduleEnabled: [this.quiz.isScheduleEnabled],
                startDate: [moment(this.quiz.startDate).utc()],
                endDate: [moment(this.quiz.endDate).utc()],
                introduction: [this.quiz.introduction]
            })
        })
    }

    ngAfterViewInit(): void {
        if (!this.isEditMode())
            this.tabGroup.selectedIndex = 1;
    }

    isEditMode = () => this.quiz.id ? true : false;

    onReturn = () => this.location.back();

    onSave(): void {
        //this.questionsControl.saveFormData(this.quiz);
        //this.settingsControl.saveFormData(this.quiz);

        if (!this.isEditMode())
            this.createQuiz();
        else
            this.updateQuiz();
    }

    createQuiz(): void {
        this.quizDataProvider.createQuiz(this.quiz)
            .subscribe(
                (response: ApiResponse<Quiz>) => { this.router.navigateByUrl('quizzes/' + response.payload.id); }
            );
    }

    updateQuiz(): void {
        this.quizDataProvider.updateQuiz(this.quiz).subscribe();
    }

}
