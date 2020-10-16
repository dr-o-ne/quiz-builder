import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { ActivatedRoute, Router } from '@angular/router';
import { AttemptService } from 'app/quiz-builder/services/attempt.service';
import { QuizDataProvider } from 'app/quiz-builder/services/dataProviders/quiz.dataProvider';
import { Quiz } from 'app/quiz-builder/model/quiz';
import { fuseAnimations } from '@fuse/animations';
import { ApiResponse } from 'app/quiz-builder/services/dataProviders/apiResponse';
import { NavigationService } from 'app/quiz-builder/common/ui/nav-bar/NavigationService';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { QuizDialogFormComponent } from '../quiz-dialog-form/quiz-dialog-form.component';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: ['./quiz-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class QuizListComponent implements OnInit {

  dataSource: MatTableDataSource<Quiz>;
  selection: SelectionModel<Quiz>;
  displayedColumns: string[] = ['empty', 'name', 'isEnabled', 'statistic', 'preview', 'copyLink', 'menu'];
  filterData: string;
  isMultiSelectMode = false;
  addDialogRef: MatDialogRef<QuizDialogFormComponent>;

  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(
    private navService: NavigationService,
    private matDialog: MatDialog,
    private router: Router,
    private activeRoute: ActivatedRoute,
    private attemptService: AttemptService,
    private quizDataProvider: QuizDataProvider
  ) {
    this.createMenu();
  }

  ngOnInit(): void {
    this.refreshData();
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

    this.displayedColumns.shift()
    this.displayedColumns.unshift(this.isMultiSelectMode ? 'empty' : 'select');

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
    this.quizDataProvider.deleteQuiz(quiz.id).subscribe(
      () => { this.refreshData(); }
    );
  }

  bulkDelete(): void {
    const ids = this.selection.selected.map(x => x.id);
    this.quizDataProvider.deleteQuizzes(ids).subscribe(
      () => { this.refreshData(); }
    );
  }

  onChangeQuizState(isEnabled: boolean, quiz: Quiz): void {
    quiz.isEnabled = isEnabled;
    this.quizDataProvider.updateQuiz(quiz).subscribe();
  }

  bulkEnable(): void {
    this.selection.selected.forEach(x => this.onChangeQuizState(true, x));
  }

  bulkDisable(): void {
    this.selection.selected.forEach(x => this.onChangeQuizState(false, x));
  }

  add(): void {
    this.addDialogRef = this.matDialog.open(QuizDialogFormComponent,
      {
          panelClass: 'quiz-dialog-form',
          data: {
              name: "",
          }
      },
  );

    this.addDialogRef.afterClosed()
      .subscribe(response => {
        if (!response)
          return;

        const form: FormGroup = response;

        const quiz = new Quiz();
        quiz.name = form.value.name;
        this.quizDataProvider.createQuiz(quiz).subscribe(
          (response: ApiResponse<Quiz>) => { this.router.navigateByUrl('quizzes/' + response.payload.id); }
        );

      });
  }

  edit(item: Quiz): void {
    this.router.navigate(
      [item.id],
      {
        relativeTo: this.activeRoute,
        state: { quiz: item }
      }
    );
  }

  preview(item: Quiz): void {
    this.attemptService.tryAttempt(item.id);
  }

  copyLink(item: Quiz): void {
    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = this.attemptService.getLink(item.id);
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
  }

  private refreshData(): void {
    this.quizDataProvider.getAllQuizzes().subscribe(
      (response: ApiResponse<Quiz[]>) => { this.initDataSource(response.payload); }
    );
  }

  private initDataSource(quizzes: Quiz[]): void {
    this.dataSource = new MatTableDataSource<Quiz>(quizzes);
    this.dataSource.sort = this.sort;
    this.selection = new SelectionModel<Quiz>(true, []);
  }

  createMenu(): void {
    this.navService.clean();
  }

}