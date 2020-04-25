import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectionStrategy } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Question } from 'src/app/_models/question';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router, ActivatedRoute } from '@angular/router';
import { QuestionService } from 'src/app/_service/question.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-quiz-page',
  templateUrl: './quiz-page.component.html',
  styleUrls: ['./quiz-page.component.css']
})
export class QuizPageComponent implements OnInit {
  displayedColumns: string[] = ['name', 'type', 'menu'];

  newQuiz: Quiz;
  quizForm: FormGroup;
  resources: string[] = ['In Design'];

  dataQuestion: Question[] = [];
  dataSource: MatTableDataSource<Question>;

  filterData: string;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private fb: FormBuilder, private router: Router, private activeRout: ActivatedRoute,
              private questionService: QuestionService) {}

  ngOnInit() {
    this.activeRout.data.subscribe(data => {
      if (data.hasOwnProperty('quiz')) {
        this.newQuiz = data.quiz;
        this.initQuestion(this.newQuiz.id);
      } else {
        this.newQuiz = new Quiz();
      }
      this.initValidate();
    });
  }

  initQuestion(id: number) {
    const storage = localStorage.getItem('questionlist');
    if (!storage) {
      this.questionService.getQuestionData().subscribe((question: any) => {
        this.dataQuestion = question.questionlist.filter((obj: { quizId: number; }) => obj.quizId === id);
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
        this.dataQuestion = this.dataQuestion.filter((obj: { quizId: number; }) => obj.quizId === id);
        this.initDataSource();
        return;
      }
      if (tempSave) {
        const newQuestion: Question = JSON.parse(tempSave);
        this.dataQuestion.push(newQuestion);
        localStorage.setItem('questionlist', JSON.stringify(this.dataQuestion));
        this.dataQuestion = this.dataQuestion.filter((obj: { quizId: number; }) => obj.quizId === id);
        this.initDataSource();
        localStorage.removeItem('question-save');
        return;
      }
      const editQuestion: Question = JSON.parse(tempUpdate);
      const objIndex = this.dataQuestion.findIndex((obj => obj.id === editQuestion.id));
      this.dataQuestion[objIndex] = editQuestion;
      localStorage.setItem('questionlist', JSON.stringify(this.dataQuestion));
      this.dataQuestion = this.dataQuestion.filter((obj: { quizId: number; }) => obj.quizId === id);
      localStorage.removeItem('question-update');
      this.initDataSource();
    }
  }

  generateId() {
    return Math.floor(Math.random() * 10000) + 1;
  }

  initValidate() {
    this.quizForm = this.fb.group({
      name: ['', Validators.required],
      status: ['', Validators.required]
    });
  }

  initDataSource() {
    this.dataSource = new MatTableDataSource<Question>(this.dataQuestion);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  saveQuiz() {
    this.saveOrUpdate('save');
  }

  updateQuiz() {
    this.saveOrUpdate('update');
  }

  saveOrUpdate(operation: string) {
    if (this.quizForm.valid) {
      if (!this.newQuiz.hasOwnProperty('id')) {
        this.newQuiz.id = this.generateId();
      }
      this.newQuiz = Object.assign(this.newQuiz, this.quizForm.value);
      localStorage.setItem('quiz-' + operation, JSON.stringify(this.newQuiz));
      this.router.navigate(['/quizlist']);
    }
  }
}
