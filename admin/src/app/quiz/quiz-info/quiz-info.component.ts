import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Quiz } from 'src/app/_models/quiz';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTabGroup } from '@angular/material/tabs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { QuizService } from 'src/app/_service/quiz.service';
import { QuizInfoSettingsTabComponent } from './quiz-info-settings-tab/quiz-info-settings-tab.component';


@Component({
    selector: 'app-quiz-info',
    templateUrl: './quiz-info.component.html'
})
export class QuizInfoComponent implements OnInit, AfterViewInit {

    @ViewChild(MatTabGroup) tabGroup: MatTabGroup;
    @ViewChild(QuizInfoSettingsTabComponent) settingsControl: QuizInfoSettingsTabComponent;

    quiz: Quiz;
    quizForm: FormGroup;

    constructor(
        private route: ActivatedRoute,
        private fb: FormBuilder,
        private router: Router,
        private location: Location,
        private quizService: QuizService
    ) {
        if (this.route.snapshot.data.quizResolver)
            this.quiz = this.route.snapshot.data.quizResolver.quiz;
        else
            this.quiz = new Quiz();
    }

    ngOnInit(): void {
        this.quizForm = this.fb.group({
            settings: this.fb.group({
                name: [this.quiz.name, Validators.required]
            })
        })
    }

    ngAfterViewInit(): void {
        if (!this.isEditMode())
            this.tabGroup.selectedIndex = 1;
    }

    isEditMode = () => this.quiz.id ? true : false;

    onReturn = () => this.location.back();

    onSave() {

        this.settingsControl.SaveFormData(this.quiz);

        if (!this.isEditMode())
            this.createQuiz();
        else
            this.updateQuiz();
    }

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
