import { Component, ViewChild, ViewEncapsulation, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OptionItem } from 'app/quiz-builder/model/UI/optionItem';
import { ChoicesDisplayType, ChoicesEnumerationType, QuestionGradingType } from 'app/quiz-builder/model/settings/answer.settings';
import { OptionItemsService } from 'app/quiz-builder/model/UI/optionItemService';
import { ChoiceDynamicComponent } from '../choice-info/choice-dynamic.component';
import { QuestionLangService } from 'app/quiz-builder/services/lang/question.lang.service';
import { QuestionDataProvider } from 'app/quiz-builder/services/dataProviders/question.dataProvider';
import { QuestionType, Question } from 'app/quiz-builder/model/question';
import { fuseAnimations } from '@fuse/animations';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { NavigationService } from 'app/quiz-builder/common/ui/nav-bar/NavigationService';
import { empty } from 'rxjs';
import { ChoiceLangService } from 'app/quiz-builder/services/lang/choice.lang.service';

@Component({
    selector: 'app-question-info',
    templateUrl: './question-info.component.html',
    styleUrls: ['./question-info.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuestionInfoComponent implements OnInit {

    @ViewChild(ChoiceDynamicComponent)
    private choicesForm!: ChoiceDynamicComponent;

    optionItemsService = OptionItemsService;
    
    question: Question;
    questionForm: FormGroup;

    emptyQuestionType: QuestionType;
    gradingTypes = QuestionGradingType;
    choicesDisplayType = ChoicesDisplayType;
    choicesEnumerationType = ChoicesEnumerationType;

    isEditMode = () => this.question.id;

    options: OptionItem[];

    constructor(
        private fuseNavigationService: FuseNavigationService,
        private navService: NavigationService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private fb: FormBuilder,
        private questionDataProvider: QuestionDataProvider,
        private optionItemService: OptionItemsService,
        public questionLangService: QuestionLangService,
        public choiceLangService: ChoiceLangService) {

        this.emptyQuestionType = QuestionType.Empty;
    }

    ngOnInit(): void {

        this.question = this.activatedRoute.snapshot.data.questionResolver;

        if (this.question.id == null) {
            this.question.quizId = history.state.quizId;
            this.question.groupId = history.state.groupId;
        }
        this.question.settings = JSON.parse(this.question.settings);
        //TODO:
        if (!this.question.choices || this.question.choices.length === 0) {
            this.question.choices = '{}';
        }
        this.question.choices = JSON.parse(this.question.choices);


        this.questionForm = this.fb.group({
            name: [this.question.name],
            type: [this.question.type, Validators.required],
            text: [this.question.text, Validators.required],
            points: [this.question.points],
            correctFeedback: [this.question.correctFeedback],
            incorrectFeedback: [this.question.incorrectFeedback],
            gradingType: [this.question.settings.gradingType],
            choicesDisplayType: [this.question.settings.choicesDisplayType],
            choicesEnumerationType: [this.question.settings.choicesEnumerationType],
            isRequired: [this.question.isRequired]
        })

        this.options = this.optionItemService.getQuestionTypeOptionItems(this.question.type);

        this.createMenu();
    }

    getQuestionOptions = () => this.options.filter(x => x.enabled);

    isOptionEnabled(name: string): boolean {
        const option = this.options.find(x => x.name === name);
        return option && option.enabled;
    }

    navigateToParent(): void {
        this.router.navigate(
            ['../../'],
            {
                relativeTo: this.activatedRoute,
                state: {
                    groupId: this.question.groupId
                }
            }
        );
    }

    onReturn = () => this.navigateToParent();

    onSave(): void {

        if (!this.questionForm.valid) {
            return;
        }

        this.choicesForm.save();

        this.question = Object.assign(this.question, this.questionForm.value);
        this.question.settings.choicesDisplayType = this.questionForm.get('choicesDisplayType').value;
        this.question.settings.choicesEnumerationTypes = this.questionForm.get('choicesEnumerationTypes').value;

        this.question.settings = JSON.stringify(this.question.settings);
        this.question.choices = JSON.stringify(this.question.choices);

        if (this.isEditMode()) {
            this.questionDataProvider.updateQuestion(this.question).subscribe(
                () => { this.navigateToParent(); });
        }
        else {
            this.questionDataProvider.createQuestion(this.question).subscribe(
                () => { this.navigateToParent(); });
        }
    }

    isEnabled(): boolean {

        if (!this.questionForm.valid) return false;
        if (!this.choicesForm) return true;
        if (!this.choicesForm.isValid()) return false;

        return true;
    }

    onOptionItemClick(event: MouseEvent, option: OptionItem): void {
        option.enabled = !option.enabled;
        event.stopPropagation();
    }

    checkOption(menuItemId: string, optionId: string){
        const option = this.options.find(x => x.name === optionId);
        option.enabled = !option.enabled;
        const icon = option.enabled ? 'done' : 'empty';
        this.fuseNavigationService.updateNavigationItem(menuItemId, { icon: icon });
    }

    createMenu(): void {

        this.navService.clean();

        const createNavItem = {
            id: NavigationService.QUESTION_NAV_ROOT,
            title: 'Show More Options',
            type: 'group',
            children: [
                {
                    id: 'name',
                    title: 'Name',
                    type: 'item',
                    icon: 'blank',
                    function: () => {
                        this.checkOption('name', OptionItemsService.OPTION_NAME);
                    }
                },
                {
                    id: 'correctFeedback',
                    title: 'Correct Feedback',
                    type: 'item',
                    icon: 'blank',
                    function: () => {
                        this.checkOption('correctFeedback', OptionItemsService.OPTION_QUESTION_CORRECT_FEEDBACK);
                    }
                },
                {
                    id: 'incorrectFeedback',
                    title: 'Incorrect Feedback',
                    type: 'item',
                    icon: 'blank',
                    function: () => {
                        this.checkOption('incorrectFeedback', OptionItemsService.OPTION_QUESTION_INCORRECT_FEEDBACK);
                    }
                },
                {
                    id: 'displayType',
                    title: 'Display Type',
                    type: 'item',
                    icon: 'blank',
                    function: () => {
                        this.checkOption('displayType', OptionItemsService.OPTION_QUESTION_DISPLAY_TYPE);
                    }
                },
            ]
        };

        this.fuseNavigationService.addNavigationItem(createNavItem, 'end');
    }

}