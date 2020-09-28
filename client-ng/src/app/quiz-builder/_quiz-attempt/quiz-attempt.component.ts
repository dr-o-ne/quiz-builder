import { Component, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector: 'qb-quiz-attempt',
    templateUrl: './quiz-attempt.component.html',
    styleUrls: ['./quiz-attempt.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizAttemptComponent {
}
