import { Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Question } from 'src/app/_models/question';
import { ActivatedRoute, Router } from '@angular/router';
import { OptionItem } from 'src/app/_models/UI/optionItem';
import { fadeInUp400ms } from 'src/@vex/animations/fade-in-up.animation';
import { QuestionDataProvider } from 'src/app/_service/dataProviders/question.dataProvider';
import { QuestionLangService } from 'src/app/_service/lang/question.lang.service';
import { BaseChoiceSettings } from 'src/app/_models/settings/answer.settings';

@Component({
    selector: 'app-question-info',
    templateUrl: './question-info.component.html',
    styleUrls: [ './question-info.component.css',
    '../../../../node_modules/quill/dist/quill.snow.css',
    '../../../@vex/styles/partials/plugins/_quill.scss' ],
    encapsulation: ViewEncapsulation.None,
    animations: [fadeInUp400ms]
})
export class QuestionInfoComponent {

    question: Question;
    questionForm: FormGroup;

    isEditMode = () => this.question.id;

    options: OptionItem[] = [
        new OptionItem('feedback', 'Feedback', 'wysiwyg', false),
        new OptionItem('correctFeedback', 'Correct feedback', 'wysiwyg', false),
        new OptionItem('incorrectFeedback', 'Incorrect feedback', 'wysiwyg', false),
    ];

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private fb: FormBuilder,
        private questionDataProvider: QuestionDataProvider,
        public questionLangService: QuestionLangService) {

        if (this.activatedRoute.snapshot.data.questionResolver) {
            this.question = this.activatedRoute.snapshot.data.questionResolver;
        }
        else {
            this.question = new Question();
            this.question.quizId = history.state.quizId;
            this.question.groupId = history.state.groupId;
            this.question.type = history.state.questionType;
            this.question.settings = new BaseChoiceSettings();
            this.question.choices = [];
        }

    }

    ngOnInit(): void {
        this.questionForm = this.fb.group({
            name: [this.question.name],
            type: [this.question.type, Validators.required],
            text: [this.question.text, Validators.required],
            points: [this.question.points, Validators.required],
            feedback: [this.question.feedback],
            correctFeedback: [this.question.correctFeedback],
            incorrectFeedback: [this.question.incorrectFeedback]
        })
    }

    onOptionItemClick(event: MouseEvent, option: OptionItem): void {
        option.enabled = !option.enabled;
        event.stopPropagation();
    }

    getQuestionOptions = () => this.options.filter(x => x.enabled);

    onContentChanged(_): void {/*HACK*/ }

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
        this.question = Object.assign(this.question, this.questionForm.value);

        console.log(this.question);

        if (this.isEditMode()) {
            this.questionDataProvider.updateQuestion(this.question).subscribe(
                response => { this.navigateToParent(); });
        }
        else {
            this.questionDataProvider.createQuestion(this.question).subscribe(
                response => { this.navigateToParent(); });
        }
    }

    isDisabledBtn(): boolean {
        return !this.questionForm?.valid/* || !this.isChoicesValid()*/;
    }
}