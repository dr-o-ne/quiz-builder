import { Component, OnInit, Input } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { MatTableDataSource } from '@angular/material/table';
import { Quiz } from 'src/app/_models/quiz';
import { QuizService } from 'src/app/_service/quiz.service';
import { Question } from 'src/app/_models/question';
import { Group } from 'src/app/_models/group';

@Component({
    selector: 'app-quiz-info-questions-tab',
    templateUrl: './quiz-info-questions-tab.component.html'
})
export class QuizInfoQuestionsTabComponent implements OnInit {

    @Input() quiz: Quiz;

    displayedColumns: string[] = ['name', 'type'];

    groupDataSources: MatTableDataSource<Question>[] = [];

    constructor(private quizService: QuizService) {
    }

    ngOnInit(): void {
        this.loadData(this.quiz.id);
    }

    loadData(quizId: string): void {

        this.quiz.groups.forEach(x => x.questions = new Array<Question>());

        this.quizService.getAllQuestions(quizId).subscribe((response: any) => {

            for (const question of response.questions) {
                this.quiz.groups.find(g => g.id === question.groupId).questions.push(question);
            }

            this.quiz.groups.forEach(x => this.addGroupForm(x));
        });
    }

    addGroup(): void {

        const group = new Group();
        group.quizId = this.quiz.id;
        group.name = 'TODO';

        this.quizService.createGroup( group ).subscribe(
            (response: any) => { 
                const group = response.group;
                this.quiz.groups.push(group);
                this.addGroupForm(group);   
            }
        );

    }

    addGroupForm(group: Group): void {
        this.groupDataSources.push(new MatTableDataSource(group.questions))
    }

    drop(event: CdkDragDrop<string[]>): void {
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