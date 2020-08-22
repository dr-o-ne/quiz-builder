import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Question } from 'src/app/_models/question';
import { ActivatedRoute } from '@angular/router';
import { OptionItem } from 'src/app/_models/UI/optionItem';

@Component({
    selector: 'app-question-info',
    templateUrl: './question-info.component.html',
    styleUrls: [ './question-info.component.css',
    '../../../../node_modules/quill/dist/quill.snow.css',
    '../../../@vex/styles/partials/plugins/_quill.scss' ],
})
export class QuestionInfoComponent {

    question: Question;
    questionForm: FormGroup;

    options: OptionItem[] = [
        new OptionItem( 'feedback', 'Feedback', false ),
        new OptionItem( 'correctFeedback', 'Correct feedback', false ),
        new OptionItem( 'incorrectFeedback', 'Incorrect feedback', false ),
    ];

    constructor(
        private activatedRoute: ActivatedRoute,
        private fb: FormBuilder) {

        if (this.activatedRoute.snapshot.data.questionResolver)
            this.question = this.activatedRoute.snapshot.data.questionResolver;
        else {
            this.question = new Question();
            this.question.quizId = history.state.quizId;
            this.question.groupId = history.state.groupId;
            this.question.type = history.state.questionType;

            console.log(this.question);
        }

    } 

    ngOnInit(): void {
    }

    onOptionItemClick( event: MouseEvent, option: OptionItem ): void {
        option.enabled = !option.enabled;
        event.stopPropagation();
    }
}