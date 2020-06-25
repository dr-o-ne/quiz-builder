import { Component, Input, OnInit } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'app-quiz-info-settings',
    templateUrl: './quiz-info-settings.component.html'
})
export class QuizInfoSettingsComponent implements OnInit {

    @Input() quiz: Quiz;

    form: FormGroup;

    constructor(private fb: FormBuilder) {
    }

    ngOnInit(): void {

        this.form = this.fb.group({
            name: [this.quiz.name, Validators.required]
        });
        
    }

    isEditMode = () => this.quiz.id ? true : false;

    onSave = () => { }

    onReturn = () => { }

}