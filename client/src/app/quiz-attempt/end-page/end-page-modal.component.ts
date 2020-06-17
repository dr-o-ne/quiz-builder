import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { QuizAttemptFeedback } from 'src/app/_models/attemptFeedback';

@Component({
    selector: 'app-end-page-modal',
    templateUrl: './end-page-modal.component.html',
})

export class EndPageModalDialog {

    constructor(@Inject(MAT_DIALOG_DATA) public data: QuizAttemptFeedback) {
    }

}