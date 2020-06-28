import { Component, OnInit, Input } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { MatTableDataSource } from '@angular/material/table';
import { Quiz } from 'src/app/_models/quiz';
import { QuizService } from 'src/app/_service/quiz.service';

@Component({
    selector: 'app-quiz-info-questions-tab',
    templateUrl: './quiz-info-questions-tab.component.html'
})
export class QuizInfoQuestionsTabComponent implements OnInit {

    @Input() quiz: Quiz;

    displayedColumns: string[] = ['name', 'type'];

    constructor(
        private quizService: QuizService) {
    }

    ngOnInit(): void {
        this.initQuestions(this.quiz.id);
        this.quiz.groups.map(x => x);

        console.log(this.quiz.groups);
    }

    initQuestions(quizId: string): void {
        this.quizService.getAllQuestions(quizId).subscribe((response: any) => {
            console.log(response);
        });
    }

    group1 = [
        { position: 7, name: 'Nitrogen', weight: 14.0067, type: 'N' },
        { position: 8, name: 'Oxygen', weight: 15.9994, type: 'O' },
        { position: 9, name: 'Fluorine', weight: 18.9984, type: 'F' },
        { position: 10, name: 'Neon', weight: 20.1797, type: 'Ne' },
    ];

    group2 = [
        { position: 1, name: 'Hydrogen', weight: 1.0079, type: 'H' },
        { position: 2, name: 'Helium', weight: 4.0026, type: 'He' },
        { position: 3, name: 'Lithium', weight: 6.941, type: 'Li' },
        { position: 4, name: 'Beryllium', weight: 9.0122, type: 'Be' },
        { position: 5, name: 'Boron', weight: 10.811, type: 'B' },
    ];

    groupDataSources = [new MatTableDataSource(this.group1), new MatTableDataSource(this.group2)];

    drop(event: CdkDragDrop<string[]>) {
        if (event.previousContainer === event.container) {
            moveItemInArray(
                event.container.data,
                event.previousIndex,
                event.currentIndex);
        } else {
            transferArrayItem(event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex);
        }

        this.groupDataSources.forEach(x => x.data = x.data);

    }

}