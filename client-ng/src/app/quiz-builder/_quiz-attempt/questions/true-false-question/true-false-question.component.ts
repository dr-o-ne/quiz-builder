import { Component, ViewEncapsulation } from '@angular/core';
import { MatRadioChange } from '@angular/material/radio';
import { QuestionComponent } from '../question.component';
import { ChoicesDisplayType } from '../../../model/_enums';
import { TrueFalseQuestionAttemptInfo } from '../../../model/attemptInfo';
import { TrueFalseQuestionAttemptResult } from '../../../model/attemptResult';
import { MatSelectChange } from '@angular/material/select';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector: 'qb-true-false-question',
    templateUrl: './true-false-question.component.html',
    styleUrls: ['./true-false-question.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})

export class TrueFalseQuestionComponent extends QuestionComponent<TrueFalseQuestionAttemptInfo, TrueFalseQuestionAttemptResult> {

    choicesDisplayType = ChoicesDisplayType;

    onChange(event: MatRadioChange | MatSelectChange): void {
        const value = new TrueFalseQuestionAttemptResult(
            this.question.id,
            event.value
        );

        this.answer.emit(value);
    }

}

