import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {Quiz} from 'src/app/_models/quiz';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {QuizService} from 'src/app/_service/quiz.service';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: ['./quiz-list.component.css']
})
export class QuizListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'isVisible', 'edit', 'preview', 'statistic', 'menu'];

  quizzes: Quiz[] = [];
  dataSource: MatTableDataSource<Quiz>;
  selection: SelectionModel<Quiz>;

  filterData: string;

  colorBtnBulk = 'primary';
  isBulkEdit = true;

  tablePageSizeOptions = [15, 20];

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private quizService: QuizService) {
  }

  ngOnInit() {
    this.initDataQuiz();
  }

  initDataQuiz(): void {
    this.quizService.getAllQuizzes().subscribe((response: any) => {
      this.quizzes = response.quizzes;
      this.initDataSource();
    }, error => {
      console.log(error);
    });
  }

  initDataSource(): void {
    this.dataSource = new MatTableDataSource<Quiz>(this.quizzes);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.selection = new SelectionModel<Quiz>(true, []);
  }

  cleanFilter(): void {
    this.dataSource.filter = '';
    this.filterData = '';
  }

  applyFilter(filterValue: string): void {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  deleteQuiz(quiz: Quiz): void {
    this.quizService.deleteQuiz(quiz.id).subscribe(response => {
      this.initDataQuiz();
    }, error => console.log(error));
  }

  clickMultiSelection(): void {
    this.colorBtnBulk = this.colorBtnBulk === 'primary' ? 'accent' : 'primary';
    this.isBulkEdit = !this.isBulkEdit;
    if (!this.isBulkEdit) {
      this.displayedColumns.unshift('select');
    } else {
      this.displayedColumns.shift();
    }
    this.initDataSource();
  }

  isAllSelected(): boolean {
    return this.selection.selected.length === this.dataSource.data.length;
  }

  isAnyItemSelected(): boolean {
    return this.selection?.selected.length > 0;
  }

  isPaginatorEnabled(): boolean {
    return this.dataSource?.data?.length > 15;
  }

  masterToggle(): void {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

  checkboxLabel(row?: Quiz): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'}`;
  }

  bulkPublish(): void {
    this.selection.selected.forEach(x => this.clickToggle(true, x));
  }

  bulkDelete(): void {
    const ids = this.selection.selected.map(x => x.id);
    this.quizService.deleteQuizzes(ids).subscribe(
      response => {
        this.initDataQuiz();
      },
      error => console.log(error));
  }

  clickToggle(checked: boolean, quiz: Quiz): void {
    quiz.isVisible = checked;
    this.quizService.updateQuiz(quiz).subscribe(error => console.log(error));
  }
}
