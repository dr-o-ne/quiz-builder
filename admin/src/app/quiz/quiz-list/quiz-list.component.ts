import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Quiz } from 'src/app/_models/quiz';
import { MatSort } from '@angular/material/sort';
import { QuizService } from 'src/app/_service/quiz.service';
import { SelectionModel } from '@angular/cdk/collections';
import { ActivatedRoute, Router } from '@angular/router';
import { AttemptService } from 'src/app/_service/attempt.service';

@Component({
  selector: 'app-quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: ['./quiz-list.component.css']
})
export class QuizListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'isEnabled', 'statistic', 'preview', 'menu'];
  filterData: string;
  isMultiSelectMode = false;
  dataSource: MatTableDataSource<Quiz>;
  selection: SelectionModel<Quiz>;

  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(
    private router: Router,
    private activeRoute: ActivatedRoute,
    private attemptService: AttemptService,
    private quizService: QuizService
  ) {
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.quizService.getAllQuizzes().subscribe((response: any) => {
      this.initDataSource(response.quizzes);
    }, error => {
      console.log(error);
    });
  }

  initDataSource(quizzes: Quiz[]): void {
    this.dataSource = new MatTableDataSource<Quiz>(quizzes);
    this.dataSource.sort = this.sort;
    this.selection = new SelectionModel<Quiz>(true, []);
  }

  isEmptyFilter(): boolean {
    return !this.filterData || this.filterData.trim() === '';
  }

  cleanFilter(): void {
    this.dataSource.filter = '';
    this.filterData = '';
  }

  applyFilter(filterValue: string): void {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  clickMultiSelection(): void {
    this.isMultiSelectMode
      ? this.displayedColumns.shift()
      : this.displayedColumns.unshift('select');

    this.isMultiSelectMode = !this.isMultiSelectMode;
  }

  isAllSelected(): boolean {
    return this.selection?.selected.length === this.dataSource?.data.length;
  }

  isAnySelected(): boolean {
    return this.selection?.hasValue();
  }

  isItemSelected(item: Quiz): boolean {
    return this.selection.isSelected(item);
  }

  checkItemToggle(item: Quiz): void {
    this.selection.toggle(item);
  }

  checkAllToggle(checked: boolean): void {
    checked
      ? this.selection.select(...this.dataSource.data)
      : this.selection.clear();
  }

  deleteQuiz(quiz: Quiz): void {
    this.quizService.deleteQuiz(quiz.id).subscribe(() => {
      this.loadData();
    }, error => console.log(error));
  }

  bulkDelete(): void {
    const ids = this.selection.selected.map(x => x.id);
    this.quizService.deleteQuizzes(ids).subscribe(
      () => {
        this.loadData();
      },
      error => console.log(error));
  }

  onChangeQuizState(isEnabled: boolean, quiz: Quiz): void {
    quiz.isEnabled = isEnabled;
    this.quizService.updateQuiz(quiz).subscribe(error => console.log(error));
  }

  bulkEnable(): void {
    this.selection.selected.forEach(x => this.onChangeQuizState(true, x));
  }

  bulkDisable(): void {
    this.selection.selected.forEach(x => this.onChangeQuizState(false, x));
  }

  onEditClick(item: Quiz): void {
    this.router.navigate(
      [item.id, 'edit'],
      {
        relativeTo: this.activeRoute,
        state: { quiz: item }
      }
    );
  }

  onPreviewClick(item: Quiz): void {
    this.attemptService.tryAttempt(item.id);
  }
}