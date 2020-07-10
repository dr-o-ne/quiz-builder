import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-question-text-view',
    templateUrl: './question-text-view.component.html'
})

export class QuestionTextViewComponent {
    @Input() text!: string;
    @Input() isHtml!: boolean;

}

