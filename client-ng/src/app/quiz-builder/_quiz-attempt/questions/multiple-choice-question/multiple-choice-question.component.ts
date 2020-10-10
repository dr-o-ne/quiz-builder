import { Component, ViewEncapsulation } from '@angular/core';
import { MatRadioChange } from '@angular/material/radio';
import { QuestionComponent } from '../question.component';
import { ChoicesDisplayType } from '../../../model/_enums';
import { MultipleChoiceQuestionAttemptInfo, TrueFalseQuestionAttemptInfo } from '../../../model/attemptInfo';
import { MultipleChoiceQuestionAttemptResult, TrueFalseQuestionAttemptResult } from '../../../model/attemptResult';
import { MatSelectChange } from '@angular/material/select';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector: 'qb-multiple-choice-question',
    templateUrl: './multiple-choice-question.component.html',
    styleUrls: ['./multiple-choice-question.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})

export class MultipleChoiceQuestionComponent extends QuestionComponent<MultipleChoiceQuestionAttemptInfo, MultipleChoiceQuestionAttemptResult> {

    choicesDisplayType = ChoicesDisplayType;

    onChange(event: MatRadioChange | MatSelectChange): void {
        const value = new MultipleChoiceQuestionAttemptResult(
            this.question.id,
            event.value
        );

        this.answer.emit(value);
    }

}

