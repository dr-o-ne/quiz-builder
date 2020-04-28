import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { Question } from 'src/app/_models/question';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Group } from 'src/app/_models/group';
import { QuizService } from 'src/app/_service/quiz.service';
import { QuestionService } from 'src/app/_service/question.service';
import clonedeep from 'lodash.clonedeep';
import { CdkDropList, CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

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

  dataQuestion: Question[] = [];
  dataSource: MatTableDataSource<Question>;

  filterData: string;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;


  constructor(private questionService: QuestionService) { }

  ngOnInit() {
    this.initQuestion(this.group.id);
  }

  initQuestion(id: number) {
    const storage = localStorage.getItem('questionlist');
    if (!storage) {
      this.questionService.getQuestionData().subscribe((question: any) => {
        this.dataQuestion = question.questionlist.filter((obj) => obj.groupId === id);
        this.initDataSource();
        localStorage.setItem('questionlist', JSON.stringify(question.questionlist));
      }, error => {
        console.log(error);
      });
    } else {
      const tempSave = localStorage.getItem('question-save');
      const tempUpdate = localStorage.getItem('question-update');
      this.dataQuestion = JSON.parse(storage);
      if (!tempSave && !tempUpdate) {
        this.dataQuestion = this.dataQuestion.filter((obj) => obj.groupId === id);
        this.initDataSource();
        return;
      }
      if (tempSave) {
        const newQuestion: Question = JSON.parse(tempSave);
        this.dataQuestion.push(newQuestion);
        localStorage.setItem('questionlist', JSON.stringify(this.dataQuestion));
        this.dataQuestion = this.dataQuestion.filter((obj) => obj.groupId === id);
        this.initDataSource();
        localStorage.removeItem('question-save');
        return;
      }
      const editQuestion: Question = JSON.parse(tempUpdate);
      const objIndex = this.dataQuestion.findIndex((obj => obj.id === editQuestion.id));
      this.dataQuestion[objIndex] = editQuestion;
      localStorage.setItem('questionlist', JSON.stringify(this.dataQuestion));
      this.dataQuestion = this.dataQuestion.filter((obj) => obj.groupId === id);
      localStorage.removeItem('question-update');
      this.initDataSource();
    }
  }

  initDataSource() {
    this.dataSource = new MatTableDataSource<Question>(this.dataQuestion);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  generateId() {
    return Math.floor(Math.random() * 10000) + 1;
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
    }

    this.dataSource.data = clonedeep(this.dataSource.data);
  }

}
