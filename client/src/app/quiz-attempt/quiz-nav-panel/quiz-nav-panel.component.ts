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
        this.questionIds = [...Array(this.questionCount).keys()];
        console.log(this.questionIds);
    }

    onNavClick(i: number): void {
        const elem = document.getElementById('q' + (i + 1));
        elem?.scrollIntoView({ behavior: 'smooth' });
    }

    setCheckedValue(i: number, isCompleted: boolean): void {
        const elem = document.getElementById('nav' + (i + 1));
        if (isCompleted)
            elem?.classList.add("question-border-check");
        else
            elem?.classList.remove("question-border-check");
    }

    getAnchor(i: number): string {
        return 'nav' + (i + 1);
    }


}