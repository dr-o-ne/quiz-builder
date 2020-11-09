import { Component, ViewChild, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Quiz } from 'app/quiz-builder/model/quiz';
import { QuizDataProvider } from 'app/quiz-builder/services/dataProviders/quiz.dataProvider';
import * as moment from 'moment';
import { fuseAnimations } from '@fuse/animations';
import { QuizInfoSettingsComponent } from './settings/quiz-info-settings.component';
import { QuizInfoStructureComponent } from './structure/quiz-info-structure.component';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { NavigationService } from 'app/quiz-builder/common/ui/nav-bar/NavigationService';
import { QuizInfoStartPageComponent } from './start-page/quiz-info-start-page.component';
import { QuizInfoEppearancePageComponent } from './appearance/quiz-info-appearance.component';
import { QuizInfoResultPageComponent } from './result-page/quiz-info-result-page.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { QuizDialogFormComponent } from '../quiz-dialog-form/quiz-dialog-form.component';

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
    @ViewChild(QuizInfoResultPageComponent) resultPageControl: QuizInfoResultPageComponent;
    @ViewChild(QuizInfoEppearancePageComponent) appearanceControl: QuizInfoEppearancePageComponent;

    quiz: Quiz;
    quizForm: FormGroup;
    selectedIndex: number;

    addDialogRef: MatDialogRef<QuizDialogFormComponent>;

    constructor(
        private matDialog: MatDialog,
        private fuseNavigationService: FuseNavigationService,
        private navService: NavigationService,
        private route: ActivatedRoute,
        private fb: FormBuilder,
        private quizDataProvider: QuizDataProvider
    ) {
        if (this.route.snapshot.data.quizResolver)
            this.quiz = this.route.snapshot.data.quizResolver;
    }

    ngOnInit(): void {

        this.createMenuItems();
        this.selectTab(0);

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
                isPassingScoreWidgetEnabled: [this.quiz.isPassingScoreWidgetEnabled],

                resultPassText: [this.quiz.resultPassText],
                resultFailText: [this.quiz.resultFailText],
                isResultPageEnabled: [this.quiz.isResultPageEnabled],
                isResultTotalScoreEnabled: [this.quiz.isResultTotalScoreEnabled],
                isResultPassFailEnabled: [this.quiz.isResultPassFailEnabled],
                isResultFeedbackEnabled: [this.quiz.isResultFeedbackEnabled],
                isResultDurationEnabled: [this.quiz.isResultDurationEnabled],
                passingScore: [this.quiz.passingScore]
            })
        })
    }

    updateTitle():void {
        this.addDialogRef = this.matDialog.open(QuizDialogFormComponent,
            {
                panelClass: 'quiz-dialog-form',
                data: {
                    name: this.quizForm.get('settings.name').value
                }
            },
        );

        this.addDialogRef.afterClosed()
            .subscribe(response => {
                if (!response)
                    return;

                const form: FormGroup = response;
                this.quizForm.patchValue({settings:{name: form.value.name}});
                this.onSave();
            });
    }

    onSave(): void {
        this.structureControl.saveFormData(this.quiz);
        this.startPageControl.saveFormData(this.quiz);
        this.settingsControl.saveFormData(this.quiz);
        this.resultPageControl.saveFormData(this.quiz);
        this.appearanceControl.saveFormData(this.quiz);

        this.updateQuiz();
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
            { index: 3, id: "resultPage" },
            { index: 4, id: "appearance" }
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
                    id: 'resultPage',
                    title: 'Result Page',
                    type: 'item',
                    icon: 'forward',
                    function: () => {
                        this.selectTab(3)
                    }
                },
                {
                    id: 'appearance',
                    title: 'Appearance',
                    type: 'item',
                    icon: 'color_lens',
                    function: () => {
                        this.selectTab(4)
                    }
                },
            ]
        };

        this.fuseNavigationService.addNavigationItem(createNavItem, 'end');
    }

}
