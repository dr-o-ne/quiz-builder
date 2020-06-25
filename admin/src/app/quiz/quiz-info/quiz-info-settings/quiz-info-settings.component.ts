import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { Quiz } from 'src/app/_models/quiz';
import { QuizService } from 'src/app/_service/quiz.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-quiz-info-settings',
    templateUrl: './quiz-info-settings.component.html'
})
export class QuizInfoSettingsComponent implements OnInit {

    @Input() quiz: Quiz;

    form: FormGroup;

    constructor(
        private fb: FormBuilder,
        private location: Location,
        private router: Router,
        private quizService: QuizService) {

    }

    ngOnInit(): void {
        this.form = this.fb.group({
            name: [this.quiz.name, Validators.required]
        });
    }

    isEditMode = () => this.quiz.id ? true : false;

    onSubmit() {

        this.quiz.name = this.form.value.name as string;

        if (!this.isEditMode())
            this.createQuiz();
        else
            this.updateQuiz();
    }

    onReturn = () => this.location.back(); //TODO: fix: Add-Return-500

    createQuiz(): void {
        this.quizService.createQuiz(this.quiz)
            .subscribe(
                (response: any) => { this.router.navigateByUrl('quizzes/' + response.quiz.id + '/edit'); }
            );
    }

    updateQuiz(): void {
        this.quizService.updateQuiz(this.quiz).subscribe();
    }

}