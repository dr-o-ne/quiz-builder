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
import { QuizInfoStructureTabComponent } from './structure/quiz-info-structure-tab.component';
import { FuseConfigService } from '@fuse/services/config.service';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { NavigationService } from 'app/quiz-builder/common/ui/nav-bar/NavigationService';

@Component({
    selector: 'app-quiz-info',
    templateUrl: './quiz-info.component.html',
    styleUrls: ['./quiz-info.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizInfoComponent implements OnInit {

    @ViewChild(QuizInfoStructureTabComponent) structureControl: QuizInfoStructureTabComponent;
    @ViewChild(QuizInfoSettingsComponent) settingsControl: QuizInfoSettingsComponent;

    quiz: Quiz;
    quizForm: FormGroup;

    selectedIndex: number;

    constructor(
        private fuseConfigService: FuseConfigService,
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

        this.selectedIndex = 0;
    }

    ngOnInit(): void {

        this.createMenu();

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

    isEditMode = () => this.quiz.id ? true : false;

    onReturn = () => this.location.back();

    onSave(): void {
        this.structureControl.saveFormData(this.quiz);
        this.settingsControl.saveFormData(this.quiz);

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

    addGroup(): void {
        this.structureControl.addGroup();
    }

    createMenu(): void {

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

                        this.fuseNavigationService.updateNavigationItem('questions', { classes: 'active accent' });
                        this.fuseNavigationService.updateNavigationItem('settings', { classes: '' });
                        this.fuseNavigationService.updateNavigationItem('appearance', { classes: '' });

                        this.selectedIndex = 0;
                    }
                },
                {
                    id: 'settings',
                    title: 'Settings',
                    type: 'item',
                    icon: 'settings',
                    function: () => {

                        this.fuseNavigationService.updateNavigationItem('questions', { classes: '' });
                        this.fuseNavigationService.updateNavigationItem('settings', { classes: 'active accent' });
                        this.fuseNavigationService.updateNavigationItem('appearance', { classes: '' });

                        this.selectedIndex = 1;
                    }
                },
                {
                    id: 'appearance',
                    title: 'Appearance',
                    type: 'item',
                    icon: 'color_lens',
                    function: () => {

                        this.fuseNavigationService.updateNavigationItem('questions', { classes: '' });
                        this.fuseNavigationService.updateNavigationItem('settings', { classes: '' });
                        this.fuseNavigationService.updateNavigationItem('appearance', { classes: 'active accent' });
                        
                        this.selectedIndex = 2;
                    }
                },
            ]
        };

        this.fuseNavigationService.addNavigationItem(createNavItem, 'end');
    }

}
