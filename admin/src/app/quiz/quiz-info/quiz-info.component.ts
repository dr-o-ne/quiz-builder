import { Component } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';

@Component({
    selector: 'app-quiz-info',
    templateUrl: './quiz-info.component.html'
})
export class QuizInfoComponent {

    quiz: Quiz;

}
