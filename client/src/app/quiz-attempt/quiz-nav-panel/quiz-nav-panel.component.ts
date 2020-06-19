import { Component, Input, OnInit } from '@angular/core';
import { Appearance } from 'src/app/_models/appearance';

@Component({
    selector: 'app-quiz-nav-panel',
    templateUrl: './quiz-nav-panel.component.html',
    styleUrls: ['./quiz-nav-panel.component.css']
})

export class QuizNavPanelComponent implements OnInit {

    @Input() questionCount: number;
    @Input() appearance: Appearance;

    questionIds: number[];

    ngOnInit(): void {
        console.log(this.questionCount);
        this.questionIds = [...Array(this.questionCount).keys()];
        console.log(this.questionIds);
    }

    onNavClick(i: number) {
        const elem = document.getElementById('q' + (i + 1));
        elem?.scrollIntoView({ behavior: 'smooth' });
    }

}