import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { Question, QuestionType } from 'src/app/_models/question';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { QuestionService } from 'src/app/_service/question.service';
import { QuizService } from 'src/app/_service/quiz.service';
import { clonedeep } from 'lodash.clonedeep';
import { CdkDropList, CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Group } from '../../_models/group';
import { ActivatedRoute, Router } from '@angular/router';

@Component( {
  selector: 'app-question-list',
  templateUrl: './question-list.component.html',
  styleUrls: [ './question-list.component.css' ]
} )
export class QuestionListComponent implements OnInit {
  @Input() group: Group;
  @Input() quizId: string;

  @ViewChild( 'table1' ) table1: MatTable<Question>;
  @ViewChild( 'list1' ) list1: CdkDropList;

  displayedColumns: string[] = [ 'name', 'type', 'edit', 'move to group', 'delete' ];

  questions: Question[] = [];
  dataSource: MatTableDataSource<Question>;
  @ViewChild( MatPaginator, { static: true } ) paginator: MatPaginator;
  @ViewChild( MatSort, { static: true } ) sort: MatSort;

  questionTypes = QuestionType;

  constructor(
    private questionService: QuestionService,
    private quizService: QuizService,
    private router: Router,
    private activeRout: ActivatedRoute ) {
  }

  ngOnInit() {
    this.initQuestions( this.quizId, this.group.id );
  }

  initQuestions( quizId: string, groupId: string ): void {
    this.questionService.getQuestionsByParent( quizId, groupId ).subscribe( ( response: any ) => {
      this.questions = response;
      this.initDataSource( this.questions );
      console.log( 'RELOAD QUESTIONS' );
      if( this.questions.length === 0){
        console.log( '0 questions' );
      }
      console.log( this.dataSource.data.length );
    }, error => {
      console.log( error );
    } );
  }

  initDataSource( questions: Question[] ): void {
    this.dataSource = new MatTableDataSource<Question>( questions );
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  drop( event: CdkDragDrop<Question[], any> ): void {
    if ( event.previousContainer === event.container ) {
      moveItemInArray( event.container.data, event.previousIndex, event.currentIndex );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex );
    }

    this.dataSource.data = clonedeep( this.dataSource.data );
  }

  deleteQuestion( question: Question ): void {
    this.quizService.deleteQuestion( this.quizId, question.id ).subscribe( response => {
      this.initQuestions( this.quizId, this.group.id );
    }, error => console.log( error ) );
  }

  onEditClick( question: Question ): void {
    this.router.navigate(
      [ 'questions', question.id ],
      {
        relativeTo: this.activeRout,
        state: { quizId: this.quizId, groups: [ this.group ] }
      } );
  }
}
