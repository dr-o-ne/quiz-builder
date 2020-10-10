import { Component, Input, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { Quiz } from 'app/quiz-builder/model/quiz';

@Component({
    selector: 'app-quiz-info-result-page',
    templateUrl: './quiz-info-result-page.component.html',
    styleUrls: ['./quiz-info-result-page.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizInfoResultPageComponent {

}