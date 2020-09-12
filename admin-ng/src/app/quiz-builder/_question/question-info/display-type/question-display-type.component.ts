import { Component, Input } from '@angular/core';
import { Question } from 'app/quiz-builder/model/question';
import { ChoicesDisplayType, ChoicesEnumerationType } from 'app/quiz-builder/model/settings/answer.settings';

@Component({
    selector: 'app-question-display-type',
    templateUrl: './question-display-type.component.html'
})

export class QuestionDisplayTypeComponent {

    @Input() question!: Question;

    choicesDisplayTypes = ChoicesDisplayType;
    choicesEnumerationTypes = ChoicesEnumerationType;

}