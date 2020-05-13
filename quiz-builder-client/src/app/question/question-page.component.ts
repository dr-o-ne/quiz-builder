import {ChangeDetectionStrategy, Component, OnInit, Pipe, PipeTransform} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {Quiz} from '../_models/quiz';
import {Question, QuestionType} from '../_models/question';
import {Choice} from '../_models/choice';
import {Group} from '../_models/group';
import {QuestionService} from '../_service/question.service';
import {BaseChoiceSettings} from '../_models/settings/answer.settings';
import { MatDialog } from '@angular/material/dialog';
import { ModalWindowPreviewQuestionComponent } from './modal-window/modal-window-preview-question.component';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-question-page',
  templateUrl: './question-page.component.html',
  styleUrls: ['./question-page.component.css']
})
export class QuestionPageComponent implements OnInit {
  quiz: Quiz;
  question: Question;
  questionForm: FormGroup;
  questionTypes = QuestionType;
  questionTypeKeys: number[];

  groupResurce: Group[] = [];
  choices: Choice[] = [];
  settings: BaseChoiceSettings = new BaseChoiceSettings();

  isNewState = false;

  isFeedback = false;
  isCorrectFeedback = false;
  isIncorrectFeedback = false;

  constructor(private fb: FormBuilder,
              private router: Router,
              private activeRout: ActivatedRoute,
              private questionService: QuestionService,
              public dialog: MatDialog) {
    this.questionTypeKeys = Object.keys(this.questionTypes).filter(Number).map(v => Number(v));
  }

  ngOnInit() {
    this.initValidate();
    this.activeRout.data.subscribe(data => {
      if (data.hasOwnProperty('quizResolver')) {
        this.quiz = data.quizResolver.quiz;
        if (data.hasOwnProperty('questionResolver')) {
          this.question = data.questionResolver.question;
          this.initGroup();
          this.initAnswersAndSettings();
          this.initFeedback();
          return;
        }
        this.createNewQuestion();
        this.initGroup();
      } else {
        console.log('Not found correct quiz');
      }
    });
  }

  createNewQuestion() {
    this.isNewState = true;
    this.question = new Question();
    this.question.quizId = this.quiz.id;
    this.initTypeQuestion();
  }

  initTypeQuestion() {
    const key = 'typeQuestion' + this.quiz.id;
    const typeQuestion = Number(localStorage.getItem(key));
    if (typeQuestion) {
      this.question.type = typeQuestion;
      localStorage.removeItem(key);
    }
  }

  initFeedback() {
    this.isFeedback = this.question.feedback ? true : false;
    this.isCorrectFeedback = this.question.correctFeedback ? true : false;
    this.isIncorrectFeedback = this.question.incorrectFeedback ? true : false;
  }

  initGroup() {
    const storage = JSON.parse(localStorage.getItem('grouplist'));
    this.groupResurce = storage.filter(obj => obj.quizId === this.quiz.id);
    if (!this.question.groupId) {
      this.question.groupId = this.groupResurce[0]?.id || '';
    }
  }

  initAnswersAndSettings() {
    this.choices = JSON.parse(this.question.choices);
    this.settings = JSON.parse(this.question.settings);
  }

  initValidate() {
    this.questionForm = this.fb.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
      text: ['', Validators.required]
    });
  }

  updateQuestionModel() {
    this.question = Object.assign(this.question, this.questionForm.value);
    this.question.choices = JSON.stringify(this.choices);
    this.question.settings = JSON.stringify(this.settings);
  }

  updateQuestion() {
    if (!this.questionForm.valid) {
      return;
    }
    this.updateQuestionModel();
    this.questionService.updateQuestion(this.question).subscribe(response => {
      this.router.navigate(['/editquiz/', this.quiz.id]);
    }, error => console.log(error));
  }

  createQuestion() {
    if (!this.questionForm.valid) {
      return;
    }
    this.updateQuestionModel();
    this.questionService.createQuestion(this.question).subscribe(response => {
      this.router.navigate(['/editquiz/', this.quiz.id]);
    }, error => console.log(error));
  }

  isDisabledBtn(): boolean {
    return !this.questionForm.valid || !this.isChoicesValid();
  }

  isChoicesValid(): boolean {
    if (this.choices.length === 0) {
      return false;
    }
    const isAnyEmpty = this.choices.some(c => c.text === '');
    const isAnyCorrect = this.choices.some(c => c.isCorrect === true);
    return !isAnyEmpty && isAnyCorrect;
  }

  selectChangeType() {
    this.choices = [];
  }

  selectGroup(select) {
    this.question.groupId = this.groupResurce.filter(group => group.id === select.selected.value)[0].id;
  }

  openPreview() {
    this.updateQuestionModel();
    const dialogRef = this.dialog.open(ModalWindowPreviewQuestionComponent, {
      width: '500px',
      data: {question: this.question}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

}
