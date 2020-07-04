import { Component, OnInit, Input, QueryList, ViewChildren } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { Quiz } from 'src/app/_models/quiz';
import { QuizService } from 'src/app/_service/quiz.service';
import { Question, QuestionType } from 'src/app/_models/question';
import { Group } from 'src/app/_models/group';
import { QuestionLangService } from 'src/app/_service/lang/question.lang.service';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupDataProvider } from 'src/app/_service/dataProviders/group.dataProvider';

export class DataInfo {
    id: string;
    name: string;
    dataSource: MatTableDataSource<Question>;
}

@Component({
    selector: 'app-quiz-info-questions-tab',
    templateUrl: './quiz-info-questions-tab.component.html',
    styleUrls: ['./quiz-info-questions-tab.component.css']
})
export class QuizInfoQuestionsTabComponent implements OnInit {

    @ViewChildren(MatTable) tables !: QueryList<MatTable<Question>>;

    @Input() quiz: Quiz;

    questionType = QuestionType;
    displayedColumns: string[] = ['name', 'type', 'delete'];
    dataInfos: DataInfo[] = new Array<DataInfo>();
    questionTypeKeys: number[];

    constructor(
        private router: Router,
        private activeRoute: ActivatedRoute,
        private quizService: QuizService,
        public questionLangService: QuestionLangService,
        private groupDataProvider: GroupDataProvider) {

        this.questionTypeKeys = Object.keys(this.questionType).filter(Number).map(x => Number(x));
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

        this.groupDataProvider.createGroup(group).subscribe(
            (response: any) => {
                this.addGroupData(response.group);
                this.addGroupForm(response.group);
            }
        );

    }

    addGroupData(group: Group): void {
        this.quiz.groups.push(group);
    }

    addGroupForm(group: Group): void {

        var dataInfo = new DataInfo();
        dataInfo.id = group.id;
        dataInfo.name = group.name;
        dataInfo.dataSource = new MatTableDataSource(group.questions);

        this.dataInfos.push(dataInfo);
    }

    deleteGroup(groupId: string): void {
        this.groupDataProvider.deleteGroup(groupId).subscribe(
            () => {
                this.deleteGroupData(groupId);
                this.deleteGroupForm(groupId);
            }
        );
    }

    deleteGroupData(groupId: string): void {
        const index = this.quiz.groups.map(x => x.id).indexOf(groupId);
        this.quiz.groups.splice(index, 1);
    }

    deleteGroupForm(groupId: string): void {
        const index = this.dataInfos.map(x => x.id).indexOf(groupId);
        this.dataInfos.splice(index, 1);
    }

    dropGroup(event: CdkDragDrop<DataInfo[]>): void {

        moveItemInArray(
            event.container.data,
            event.previousIndex,
            event.currentIndex);

        const groupIds = event.container.data.map(x => x.id);

        this.groupDataProvider.reorderGroups(this.quiz.id, groupIds).subscribe();
    }

    dropQuestion(event: CdkDragDrop<string[]>): void {
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

        this.tables.forEach(x => x.renderRows());
    }

    getConnectedList(): string[] {
        return this.dataInfos.map(x => `${x.id}`);
    }

    addQuestion(groupId: string, typeQuestion: QuestionType): void {
        this.router.navigate(
            ['questions'],
            {
                relativeTo: this.activeRoute,
                state: {
                    quizId: this.quiz.id,
                    groupId: groupId,
                    questionType: typeQuestion
                }
            }
        );
    }

    deleteQuestion(groupId: string, questionId: string): void {

        this.quizService.deleteQuestion(this.quiz.id, questionId).subscribe(
            () => {
                this.deleteQuestionData(groupId, questionId);
                //form data is bound to data
                this.tables.forEach(x => x.renderRows());
            }
        );
    }

    deleteQuestionData(groupId: string, questionId: string): void {
        const groupIndex = this.quiz.groups.map(x => x.id).indexOf(groupId);
        const group = this.quiz.groups[groupIndex];

        const questionIndex = group.questions.map(x => x.id).indexOf(questionId);
        group.questions.splice(questionIndex, 1);
    }

    onPageNameFocusOut(groupId: string, name: string): void {
        console.log(groupId + ' ' + name);
        this.groupDataProvider.renameGroup(groupId, name).subscribe();
    }

}