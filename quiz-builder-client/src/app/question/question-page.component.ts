import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { QuestionService } from '../_service/question.service';
import { Quiz } from '../_models/quiz';
import { Question } from '../_models/question';
import { Answer } from '../_models/answer';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { AnswerService } from '../_service/answer.service';

@Component({
  selector: 'app-question-page',
  templateUrl: './question-page.component.html',
  styleUrls: ['./question-page.component.css']
})
export class QuestionPageComponent implements OnInit {
  displayedColumns: string[] = ['name', 'isCorrect', 'menu'];

  quiz: Quiz;
  question: Question;
  questionForm: FormGroup;
  resources: string[] = ['Single Choice (Radio Button)', 'Single Choice (Dropdown)', 'Multiple Choice'];

  answerData: Answer[];
  dataSource: MatTableDataSource<Answer>;

  filterData: string;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private fb: FormBuilder, private router: Router, private activeRout: ActivatedRoute,
              private answerService: AnswerService) {}

  ngOnInit() {
    this.initValidate();

    this.activeRout.data.subscribe(data => {
      if (data.hasOwnProperty('quiz')) {
        this.quiz = data.quiz;
        if (data.hasOwnProperty('question')) {
          this.question = data.question;
          this.initAnswer(this.question.id);
          return;
        }
        this.question = new Question();
      } else {
        console.log('Not found correct quiz');
      }
    });
  }

  initAnswer(id: number) {
    const storage = localStorage.getItem('answerlist');
    if (!storage) {
      this.answerService.getAnswerData().subscribe((answer: any) => {
        this.answerData = answer.answerlist.filter((obj) => obj.questionId === id);
        this.initDataSource();
        localStorage.setItem('answerlist', JSON.stringify(this.answerData));
      }, error => {
        console.log(error);
      });
    } else {
      const tempSave = localStorage.getItem('answer-save');
      const tempUpdate = localStorage.getItem('answer-update');
      this.answerData = JSON.parse(storage);
      if (!tempSave && !tempUpdate) {
        this.initDataSource();
        return;
      }
      if (tempSave) {
        const newAnswer: Answer = JSON.parse(tempSave);
        newAnswer.id = this.generateId();
        this.answerData.push(newAnswer);
        localStorage.setItem('answerlist', JSON.stringify(this.answerData));
        this.initDataSource();
        localStorage.removeItem('answer-save');
        return;
      }
      const editAnswer: Answer = JSON.parse(tempUpdate);
      const objIndex = this.answerData.findIndex((obj => obj.id === editAnswer.id));
      this.answerData[objIndex] = editAnswer;
      localStorage.setItem('answerlist', JSON.stringify(this.answerData));
      localStorage.removeItem('answer-update');
      this.initDataSource();
    }
  }

  generateId() {
    return Math.floor(Math.random() * 10000) + 1;
  }

  initValidate() {
    this.questionForm = this.fb.group({
      name: ['', Validators.required],
      type: ['', Validators.required]
    });
  }

  initDataSource() {
    this.dataSource = new MatTableDataSource<Answer>(this.answerData);
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

  saveOrUpdate(operation: string) {
    if (this.questionForm.valid) {
      this.question = Object.assign(this.question, this.questionForm.value);
      this.question.quizId = this.quiz.id;
      localStorage.setItem('question-' + operation, JSON.stringify(this.question));
      this.router.navigate(['/editquiz/', this.quiz.id]);
    }
  }

  saveQuestion() {
    this.saveOrUpdate('save');
  }

  updateQuestion() {
    this.saveOrUpdate('update');
  }

}
