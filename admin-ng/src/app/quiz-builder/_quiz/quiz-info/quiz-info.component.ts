import { Component, ViewChild, OnInit, ViewEncapsulation } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Quiz } from 'app/quiz-builder/model/quiz';
import { ApiResponse } from 'app/quiz-builder/services/dataProviders/apiResponse';
import { QuizDataProvider } from 'app/quiz-builder/services/dataProviders/quiz.dataProvider';
import * as moment from 'moment';
import { fuseAnimations } from '@fuse/animations';
import { QuizInfoSettingsComponent } from './settings/quiz-info-settings.component';
import { QuizInfoStructureComponent } from './structure/quiz-info-structure.component';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { NavigationService } from 'app/quiz-builder/common/ui/nav-bar/NavigationService';
import { QuizInfoStartPageComponent } from './start-page/quiz-info-start-page.component';
import { QuizInfoEppearancePageComponent } from './appearance/quiz-info-appearance.component';

@Component({
    selector: 'app-quiz-info',
    templateUrl: './quiz-info.component.html',
    styleUrls: ['./quiz-info.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizInfoComponent implements OnInit {

    @ViewChild(QuizInfoStructureComponent) structureControl: QuizInfoStructureComponent;
    @ViewChild(QuizInfoStartPageComponent) startPageControl: QuizInfoStartPageComponent;
    @ViewChild(QuizInfoSettingsComponent) settingsControl: QuizInfoSettingsComponent;
    @ViewChild(QuizInfoEppearancePageComponent) appearanceControl: QuizInfoEppearancePageComponent;

    quiz: Quiz;
    quizForm: FormGroup;
    selectedIndex: number;

    constructor(
        private fuseNavigationService: FuseNavigationService,
        private navService: NavigationService,
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

        this.createMenuItems();

        if (this.isEditMode())
            this.selectTab(0);
        else
            this.selectTab(2);

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
                description: [this.quiz.description],
                headerColor: [this.quiz.headerColor],
                backgroundColor: [this.quiz.backgroundColor],
                sideColor: [this.quiz.sideColor],
                footerColor: [this.quiz.footerColor],
                isStartPageEnabled: [this.quiz.isStartPageEnabled],
                isTotalAttemptsEnabled: [this.quiz.isTotalAttemptsEnabled],
                isTimeLimitEnabled: [this.quiz.isTimeLimitEnabled],
                isTotalQuestionsEnabled: [this.quiz.isTotalQuestionsEnabled],
                isPassingScoreEnabled: [this.quiz.isPassingScoreEnabled]
            })
        })
    }

    isEditMode = () => this.quiz.id ? true : false;

    onReturn = () => this.location.back();

    onSave(): void {
        this.structureControl.saveFormData(this.quiz);
        this.startPageControl.saveFormData(this.quiz);
        this.settingsControl.saveFormData(this.quiz);
        this.appearanceControl.saveFormData(this.quiz);

        if (this.isEditMode())
            this.updateQuiz();
        else
            this.createQuiz();

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

    addGroup(): void {
        this.structureControl.addGroup();
    }

    selectTab(index: number) {

        this.selectedIndex = index;

        const tabs = [
            { index: 0, id: "questions" },
            { index: 1, id: "startPage" },
            { index: 2, id: "settings" },
            { index: 3, id: "appearance" }
        ];

        tabs.forEach(x => {
            const classes = x.index === index ? 'active accent' : '';
            this.fuseNavigationService.updateNavigationItem(x.id, { classes: classes });
        });

    }

    createMenuItems(): void {

        this.navService.clean();

        const createNavItem = {
            id: NavigationService.QUIZ_NAV_ROOT,
            title: 'CREATE QUIZ',
            type: 'group',
            children: [
                {
                    id: 'questions',
                    title: 'Questions',
                    type: 'item',
                    icon: 'playlist_add',
                    function: () => {
                        this.selectTab(0)
                    }
                },
                {
                    id: 'startPage',
                    title: 'Start Page',
                    type: 'item',
                    icon: 'exit_to_app',
                    function: () => {
                        this.selectTab(1)
                    }
                },
                {
                    id: 'settings',
                    title: 'Settings',
                    type: 'item',
                    icon: 'settings',
                    function: () => {
                        this.selectTab(2)
                    }
                },
                {
                    id: 'appearance',
                    title: 'Appearance',
                    type: 'item',
                    icon: 'color_lens',
                    function: () => {
                        this.selectTab(3)
                    }
                },
            ]
        };

        this.fuseNavigationService.addNavigationItem(createNavItem, 'end');
    }

}
