import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Question } from 'src/app/_models/question';

@Component({
    selector: 'app-question-info',
    templateUrl: './question-info.component.html'
})
export class QuestionInfoComponent {

    question: Question;
    questionForm: FormGroup;

    constructor(private fb: FormBuilder) {
    } 

    ngOnInit(): void {
    }

}