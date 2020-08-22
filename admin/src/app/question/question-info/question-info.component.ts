import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Question } from 'src/app/_models/question';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-question-info',
    templateUrl: './question-info.component.html'
})
export class QuestionInfoComponent {

    question: Question;
    questionForm: FormGroup;

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

}