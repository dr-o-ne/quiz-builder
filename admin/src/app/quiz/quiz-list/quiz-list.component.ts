import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Quiz } from 'src/app/_models/quiz';
import { MatSort } from '@angular/material/sort';
import { QuizService } from 'src/app/_service/quiz.service';
import { SelectionModel } from '@angular/cdk/collections';
import { ActivatedRoute, Router } from '@angular/router';

@Component( {
  selector: 'app-quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: [ './quiz-list.component.css' ]
} )
export class QuizListComponent implements OnInit {
  displayedColumns: string[] = [ 'name', 'isVisible', 'statistic', 'preview', 'menu' ];
  filterData: string;
  isMultiSelectMode = false;
  dataSource: MatTableDataSource<Quiz>;
  selection: SelectionModel<Quiz>;

  @ViewChild( MatSort, { static: true } ) sort: MatSort;
  @ViewChild( MatTable, { static: true } ) table: MatTable<any>;

  constructor( private quizService: QuizService,
               private router: Router,
               private activeRoute: ActivatedRoute
  ) {
  }

  ngOnInit() {
    this.initDataQuiz();
  }

  initDataQuiz(): void {
    this.quizService.getAllQuizzes().subscribe( ( response: any ) => {
      this.initDataSource( response.quizzes );
    }, error => {
      console.log( error );
    } );
  }

  initDataSource( quizzes: Quiz[] ): void {
    this.dataSource = new MatTableDataSource<Quiz>( quizzes );
    this.dataSource.sort = this.sort;
    this.selection = new SelectionModel<Quiz>( true, [] );
  }

  isEmptyFilter(): boolean {
    return !this.filterData || this.filterData.trim() === '';
  }

  cleanFilter(): void {
    this.dataSource.filter = '';
    this.filterData = '';
  }

  applyFilter( filterValue: string ): void {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  clickMultiSelection(): void {
    this.isMultiSelectMode
      ? this.displayedColumns.shift()
      : this.displayedColumns.unshift( 'select' );

    this.isMultiSelectMode = !this.isMultiSelectMode;
    this.table.renderRows();
  }

  isAllSelected(): boolean {
    return this.selection?.selected.length === this.dataSource?.data.length;
  }

  isAnySelected(): boolean {
    return this.selection?.hasValue();
  }

  isItemSelected( item: Quiz ): boolean {
    return this.selection.isSelected( item );
  }

  checkItemToggle( item: Quiz ): void {
    this.selection.toggle( item );
  }

  checkAllToggle( checked: boolean ): void {
    checked
      ? this.selection.select( ...this.dataSource.data )
      : this.selection.clear();
  }

  clickVisibleToggle( checked: boolean, quiz: Quiz ): void {
    quiz.isVisible = checked;
    this.quizService.updateQuiz( quiz ).subscribe( error => console.log( error ) );
  }

  deleteQuiz( quiz: Quiz ): void {
    this.quizService.deleteQuiz( quiz.id ).subscribe( response => {
      this.initDataQuiz();
    }, error => console.log( error ) );
  }

  bulkDelete(): void {
    const ids = this.selection.selected.map( x => x.id );
    this.quizService.deleteQuizzes( ids ).subscribe(
      response => {
        this.initDataQuiz();
      },
      error => console.log( error ) );
  }

  bulkPublish(): void {
    this.selection.selected.forEach( x => this.clickVisibleToggle( true, x ) );
  }

  onAttemptClick(item: Quiz ) {
    this.router.navigate(
      [ item.id],
      {
        relativeTo: this.activeRoute,
        state: { quiz: item }
      }
    );
  }

  onEditClick(item: Quiz ) {
    this.router.navigate(
      [ item.id, 'edit'],
      {
        relativeTo: this.activeRoute,
        state: { quiz: item }
      }
    );
  }

  onPreviewClick( item: Quiz ) {
    this.router.navigate(
      [ item.id, 'preview' ],
      {
        relativeTo: this.activeRoute,
        state: { quiz: item }
      }
    );
  }
}
