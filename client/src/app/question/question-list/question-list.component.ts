import { Component, OnInit, Input, ViewChild, ChangeDetectorRef } from '@angular/core';
import { Question, QuestionType } from 'src/app/_models/question';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { QuestionService } from 'src/app/_service/question.service';
import { QuizService } from 'src/app/_service/quiz.service';
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
  @Input() groups: Group[];

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
    private activeRout: ActivatedRoute,
    private changeDetectorRefs: ChangeDetectorRef) {
  }

  ngOnInit() {
    this.initQuestions( this.quizId, this.group.id );
  }

  initQuestions( quizId: string, groupId: string ): void {
    this.questionService.getQuestionsByParent( quizId, groupId ).subscribe( ( response: any ) => {
      this.questions = response;
      this.initDataSource( this.questions );
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
    this.dataSource.data = event.container.data;
  }

  deleteQuestion( question: Question, index: number ): void {
    this.quizService.deleteQuestion( this.quizId, question.id ).subscribe( response => {
      this.questions.splice(index, 1);
      this.initDataSource( this.questions );
      this.changeDetectorRefs.detectChanges();
    }, error => console.log( error ) );
  }

  onEditClick( question: Question ): void {
    this.router.navigate(
      [ 'questions', question.id ],
      {
        relativeTo: this.activeRout,
        state: {
          quizId: this.quizId,
          groupId: this.group.id,
          groups: this.groups
        }
      } );
  }
}
