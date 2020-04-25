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

  dataQuiz: Quiz[] = [];
  dataSource: MatTableDataSource<Quiz>;

  filterData: string;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private quizService: QuizService) { }

  ngOnInit() {
    this.initDataQuiz();
  }

  initDataQuiz() {
    const storage = localStorage.getItem('quizlist');
    if (!storage) {
      this.quizService.getQuizData().subscribe((quiz: any) => {
        this.dataQuiz = quiz.quizlist;
        this.initDataSource();
        localStorage.setItem('quizlist', JSON.stringify(this.dataQuiz));
      }, error => {
        console.log(error);
      });
    } else {
      const tempQuizSave = localStorage.getItem('quiz-save');
      const tempQuizUpdate = localStorage.getItem('quiz-update');
      if (!tempQuizSave && !tempQuizUpdate) {
        this.dataQuiz = JSON.parse(storage);
        this.initDataSource();
        return;
      }
      this.dataQuiz = JSON.parse(storage);
      if (tempQuizSave) {
        const newQuiz: Quiz = JSON.parse(tempQuizSave);
        this.dataQuiz.push(newQuiz);
        localStorage.setItem('quizlist', JSON.stringify(this.dataQuiz));
        this.initDataSource();
        localStorage.removeItem('quiz-save');
        return;
      }
      const editQuiz: Quiz = JSON.parse(tempQuizUpdate);
      const objIndex = this.dataQuiz.findIndex((obj => obj.id === editQuiz.id));
      this.dataQuiz[objIndex] = editQuiz;
      localStorage.setItem('quizlist', JSON.stringify(this.dataQuiz));
      localStorage.removeItem('quiz-update');
      this.initDataSource();
    }
  }

  generateId() {
    return Math.floor(Math.random() * 10000) + 1;
  }

  initDataSource() {
    this.dataSource = new MatTableDataSource<Quiz>(this.dataQuiz);
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
