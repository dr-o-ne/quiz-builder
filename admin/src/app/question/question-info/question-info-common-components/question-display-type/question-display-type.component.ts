import { Component, Input } from '@angular/core';
import { ChoicesDisplayType, ChoicesEnumerationType } from 'src/app/_models/settings/answer.settings';
import { Question } from 'src/app/_models/question';

@Component({
    selector: 'app-question-display-type',
    templateUrl: './question-display-type.component.html'
})

export class QuestionDisplayTypeComponent {

    @Input() question!: Question;

    choicesDisplayTypes = ChoicesDisplayType;
    choicesEnumerationTypes = ChoicesEnumerationType;

}