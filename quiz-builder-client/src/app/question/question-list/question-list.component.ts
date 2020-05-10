import {Component, OnInit, Input, ViewChild} from '@angular/core';
import {Quiz} from 'src/app/_models/quiz';
import {Question, QuestionType} from 'src/app/_models/question';
import {MatTableDataSource, MatTable} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {Group} from 'src/app/_models/group';
import {QuestionService} from 'src/app/_service/question.service';
import clonedeep from 'lodash.clonedeep';
import {CdkDropList, CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-question-list',
  templateUrl: './question-list.component.html',
  styleUrls: ['./question-list.component.css']
})
export class QuestionListComponent implements OnInit {
  @Input() group: Group;
  @Input() quiz: Quiz;

  @ViewChild('table1') table1: MatTable<Question>;
  @ViewChild('list1') list1: CdkDropList;

  displayedColumns: string[] = ['name', 'type', 'edit', 'move to group', 'delete'];

  questions: Question[] = [];
  dataSource: MatTableDataSource<Question>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  questionTypes = QuestionType;

  constructor(private questionService: QuestionService) {
  }

  ngOnInit() {
    this.initQuestion(this.group);
  }

  initQuestion(group: Group) {
    this.questionService.getQuestionsByGroupId(group.id).subscribe((response: any) => {
      this.questions = response.questions;
      this.initDataSource();
    }, error => {
      console.log(error);
    });
  }

  initDataSource() {
    this.dataSource = new MatTableDataSource<Question>(this.questions);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  drop(event: CdkDragDrop<Question[], any>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }

    this.dataSource.data = clonedeep(this.dataSource.data);
  }

  deleteQuestion(question: Question) {
    this.questionService.deleteQuestion(question.id).subscribe(response => {
      this.initQuestion(this.group);
    }, error => console.log(error));
  }
}
