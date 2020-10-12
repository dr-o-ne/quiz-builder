import { Component, ViewEncapsulation } from '@angular/core';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatSelectChange } from '@angular/material/select';
import { QuestionComponent } from '../question.component';
import { ChoicesDisplayType } from '../../../model/_enums';
import { MultipleSelectQuestionAttemptInfo } from '../../../model/attemptInfo';
import { MultipleSelectQuestionAttemptResult } from '../../../model/attemptResult';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector: 'qb-multiple-select-question',
    templateUrl: './multiple-select-question.component.html',
    styleUrls: ['./multiple-select-question.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})

export class MultipleSelectQuestionComponent extends QuestionComponent<MultipleSelectQuestionAttemptInfo, MultipleSelectQuestionAttemptResult> {

    choicesDisplayType = ChoicesDisplayType;
    choices: Set<string>;

    constructor() {
        super();
        this.choices = new Set<string>();
    }

    onChange(event: MatCheckboxChange | MatSelectChange): void {

        if (event instanceof MatCheckboxChange) {
            if (event.checked)
                this.choices.add(event.source.value);
            else
                this.choices.delete(event.source.value);
        }

        if (event instanceof MatSelectChange) {
            this.choices = new Set<string>(event.value);
        }

        const value = new MultipleSelectQuestionAttemptResult(
            this.question.id,
            [...this.choices].map(x => parseInt(x))
        );

        this.answer.emit(value);
    }

}
