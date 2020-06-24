import { Component } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-quiz-info',
    templateUrl: './quiz-info.component.html'
})
export class QuizInfoComponent {

    quiz: Quiz;

    constructor(
        private route: ActivatedRoute,
    ) {
        if (this.route.snapshot.data.quizResolver)
            this.quiz = this.route.snapshot.data.quizResolver.quiz;
        else
            this.quiz = new Quiz();

    }

    isEditMode = () => this.quiz.id ? true : false;

}
