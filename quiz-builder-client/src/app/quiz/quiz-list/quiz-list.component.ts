import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Quiz } from 'src/app/_models/quiz';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { QuizService } from 'src/app/_service/quiz.service';

@Component({
  selector: 'app-quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: ['./quiz-list.component.css']
})
export class QuizListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'status', 'preview', 'menu'];

  quizzes: Quiz[] = [];
  dataSource: MatTableDataSource<Quiz>;

  filterData: string;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private quizService: QuizService) { }

  ngOnInit() {
    this.initDataQuiz();
  }

  initDataQuiz() {
    this.quizService.getAllQuizzes().subscribe((response: any) => {
      this.quizzes = response.quizzes;
      this.initDataSource();
    }, error => {
      console.log(error);
    });
  }

  generateId(): number {
    return Math.floor(Math.random() * 10000) + 1;
  }

  initDataSource() {
    this.dataSource = new MatTableDataSource<Quiz>(this.quizzes);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  cleanFilter() {
    this.filterData = '';
    this.initDataSource();
  }

  applyFilter(event: Event) {
      const filterValue = (event.target as HTMLInputElement).value;
      this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
