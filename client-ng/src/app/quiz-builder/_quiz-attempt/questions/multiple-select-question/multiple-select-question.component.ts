import { Component, ViewEncapsulation } from '@angular/core';
import { MatRadioChange } from '@angular/material/radio';
import { QuestionComponent } from '../question.component';
import { ChoicesDisplayType } from '../../../model/_enums';
import { MultipleSelectQuestionAttemptInfo } from '../../../model/attemptInfo';
import { MultipleSelectQuestionAttemptResult } from '../../../model/attemptResult';
import { MatSelectChange } from '@angular/material/select';
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

    onChange(event: MatRadioChange | MatSelectChange): void {
        const value = new MultipleSelectQuestionAttemptResult(
            this.question.id,
            event.value
        );

        this.answer.emit(value);
    }

}
