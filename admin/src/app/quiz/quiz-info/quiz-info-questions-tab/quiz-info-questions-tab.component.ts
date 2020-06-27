import { Component, Input } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';

@Component({
    selector: 'app-quiz-info-questions-tab',
    templateUrl: './quiz-info-questions-tab.component.html'
})
export class QuizInfoQuestionsTabComponent {

    @Input() quiz: Quiz;

}