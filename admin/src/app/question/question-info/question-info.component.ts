import { Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Question, QuestionType } from 'src/app/_models/question';
import { ActivatedRoute } from '@angular/router';
import { OptionItem } from 'src/app/_models/UI/optionItem';
import { fadeInUp400ms } from 'src/@vex/animations/fade-in-up.animation';

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
    questionTypes = QuestionType;
    questionTypeKeys: number[];

    options: OptionItem[] = [
        new OptionItem( 'feedback', 'Feedback', 'wysiwyg', false ),
        new OptionItem( 'correctFeedback', 'Correct feedback', 'wysiwyg', false ),
        new OptionItem( 'incorrectFeedback', 'Incorrect feedback', 'wysiwyg', false ),
    ];

    constructor(
        private activatedRoute: ActivatedRoute,
        private fb: FormBuilder) {

        this.questionTypeKeys = Object.keys(this.questionTypes).filter(Number).map(v => Number(v));

        if (this.activatedRoute.snapshot.data.questionResolver) {
            this.question = this.activatedRoute.snapshot.data.questionResolver; 
        }
        else {
            this.question = new Question();
            this.question.quizId = history.state.quizId;
            this.question.groupId = history.state.groupId;
            this.question.type = history.state.questionType;
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

    onOptionItemClick( event: MouseEvent, option: OptionItem ): void {
        option.enabled = !option.enabled;
        event.stopPropagation();
    }

    getQuestionOptions = () => this.options.filter( x => x.enabled );

    onContentChanged(_) {/*HACK*/}
}