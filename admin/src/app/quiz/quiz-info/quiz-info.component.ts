import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { ActivatedRoute } from '@angular/router';
import { MatTabGroup } from '@angular/material/tabs';

@Component({
    selector: 'app-quiz-info',
    templateUrl: './quiz-info.component.html'
})
export class QuizInfoComponent implements AfterViewInit {

    @ViewChild(MatTabGroup) tabGroup: MatTabGroup;

    quiz: Quiz;

    constructor(
        private route: ActivatedRoute,
    ) {
        if (this.route.snapshot.data.quizResolver)
            this.quiz = this.route.snapshot.data.quizResolver.quiz;
        else
            this.quiz = new Quiz();
    }

    ngAfterViewInit(): void {
        if (!this.isEditMode())
            this.tabGroup.selectedIndex = 1;
    }

    isEditMode = () => this.quiz.id ? true : false;

}
