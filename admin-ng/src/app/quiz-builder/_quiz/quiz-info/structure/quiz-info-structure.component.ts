import { Component, OnInit, Input, QueryList, ViewChildren, ViewEncapsulation } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupInfoComponent, GroupInfoViewModel } from './group-info/group-info.component';
import { QuestionDataProvider } from 'app/quiz-builder/services/dataProviders/question.dataProvider';
import { GroupDataProvider } from 'app/quiz-builder/services/dataProviders/group.dataProvider';
import { QuestionLangService } from 'app/quiz-builder/services/lang/question.lang.service';
import { QuestionType, Question } from 'app/quiz-builder/model/question';
import { Quiz } from 'app/quiz-builder/model/quiz';
import { Group } from 'app/quiz-builder/model/group';
import { fuseAnimations } from '@fuse/animations';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { GroupDialogFormComponent } from '../../group-dialog-form/group-dialog-form.component';
import { FormGroup } from '@angular/forms';

export class DataInfo implements GroupInfoViewModel {
    id: string;
    name: string;
    isEnabled: boolean;
    selectAllQuestions: boolean;
    randomizeQuestions: boolean;
    countOfQuestionsToSelect?: number;
    dataSource: MatTableDataSource<Question>;
}

@Component({
    selector: 'app-quiz-info-structure',
    templateUrl: './quiz-info-structure.component.html',
    styleUrls: ['./quiz-info-structure.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizInfoStructureComponent implements OnInit {

    @ViewChildren(MatTable) tables!: QueryList<MatTable<Question>>;
    @ViewChildren(GroupInfoComponent) groupInfoControls!: QueryList<GroupInfoComponent>;

    groupEditDialogRef: MatDialogRef<GroupDialogFormComponent>;

    @Input() quiz: Quiz;

    questionType = QuestionType;
    displayedColumns: string[] = ['name', 'type', 'menu'];
    dataInfos: DataInfo[] = new Array<DataInfo>();
    questionTypeKeys: number[];

    constructor(
        private matDialog: MatDialog,
        private router: Router,
        private activeRoute: ActivatedRoute,
        public questionLangService: QuestionLangService,
        private groupDataProvider: GroupDataProvider,
        private questionDataProvider: QuestionDataProvider) {

        this.questionTypeKeys = Object.keys(this.questionType).filter(Number).map(x => Number(x));
    }

    ngOnInit(): void {
        this.quiz.groups.forEach(x => this.addGroupForm(x));
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

        const dataInfo = new DataInfo();
        dataInfo.id = group.id;
        dataInfo.name = group.name;
        dataInfo.isEnabled = group.isEnabled;
        dataInfo.selectAllQuestions = group.selectAllQuestions;
        dataInfo.countOfQuestionsToSelect = group.countOfQuestionsToSelect;
        dataInfo.randomizeQuestions = group.randomizeQuestions;
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

    dropQuestion(event: CdkDragDrop<Question[]>): void {

        if (event.previousContainer === event.container) {

            moveItemInArray(
                event.container.data,
                event.previousIndex,
                event.currentIndex);

            const questionIds = event.container.data.map(x => x.id);
            const groupId = event.container.id;
            this.questionDataProvider.reorderQuestions(
                groupId,
                questionIds).subscribe();

        } else {

            transferArrayItem(event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex);

            const groupId = event.container.id;
            const questionId = event.container.data[event.currentIndex].id;
            const questionIds = event.container.data.map(x => x.id);

            this.questionDataProvider.moveQuestion(
                groupId,
                questionId,
                questionIds).subscribe();
        }

        this.tables.forEach(x => x.renderRows());
    }

    getConnectedList(): string[] {
        return this.dataInfos.map(x => `${x.id}`);
    }

    addQuestion(groupId: string, typeQuestion: QuestionType): void {
        this.router.navigate(
            ['questions/new'],
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

    onGroupEditClick(dataInfo: DataInfo): void {
        //this.mep.expanded = !this.mep.expanded

        this.groupEditDialogRef = this.matDialog.open(GroupDialogFormComponent,
            {
                panelClass: 'group-dialog-form',
                data: {
                    dataInfo
                }
            },
        );


        this.groupEditDialogRef.afterClosed()
            .subscribe(response => {
                if (!response)
                    return;

                const form: FormGroup = response;

                console.log(form);

                const group = this.quiz.groups.find(x => x.id == dataInfo.id);

                group.name = form.value.name as string;
                group.selectAllQuestions = form.value.selectAllQuestions as boolean;
                group.countOfQuestionsToSelect = form.value.countOfQuestionsToSelect as number;
                group.randomizeQuestions = form.value.randomizeQuestions as boolean;

                dataInfo.name = group.name;
                dataInfo.selectAllQuestions = group.selectAllQuestions;
                dataInfo.countOfQuestionsToSelect = group.countOfQuestionsToSelect;
                dataInfo.randomizeQuestions = group.randomizeQuestions;

                console.log(group);
                this.groupDataProvider.updateGroup(group).subscribe();
            });
    }

    onEditClick(question: Question): void {
        this.router.navigate(
            ['questions', question.id],
            {
                relativeTo: this.activeRoute,
                state: {
                    groupId: question.groupId
                }
            });
    }

    deleteQuestion(groupId: string, questionId: string): void {

        this.questionDataProvider.deleteQuestion(this.quiz.id, questionId).subscribe(
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

    getDisplayName(question: Question): string {
        return question.name ? question.name : question.text.slice(3).slice(0, -4);
    }

    saveFormData(quiz: Quiz): void {
        for (const groupInfo of this.groupInfoControls) {

            const group = quiz.groups.find(x => x.id == groupInfo.viewModel.id);
            const isDirty = groupInfo.saveFormData(group);

            if (isDirty)
                this.groupDataProvider.updateGroup(group).subscribe();
        }
    }

    onChangeGroupState(isEnabled: boolean, dataInfo: DataInfo): void {
        const group = this.quiz.groups.find(x => x.id == dataInfo.id);
        dataInfo.isEnabled = isEnabled;
        group.isEnabled = isEnabled;
        this.groupDataProvider.updateGroup(group).subscribe();
    }

} 