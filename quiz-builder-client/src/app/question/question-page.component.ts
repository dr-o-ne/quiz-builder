import {ChangeDetectionStrategy, Component, OnInit, Pipe, PipeTransform} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {Quiz} from '../_models/quiz';
import {Question, QuestionType} from '../_models/question';
import {Choice} from '../_models/choice';
import {Group} from '../_models/group';
import {QuestionService} from '../_service/question.service';
import {BaseChoiceSettings} from '../_models/settings/answer.settings';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-question-page',
  templateUrl: './question-page.component.html',
  styleUrls: ['./question-page.component.css']
})
export class QuestionPageComponent implements OnInit {
  quiz: Quiz;
  group: Group;
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
              private questionService: QuestionService) {
    this.questionTypeKeys = Object.keys(this.questionTypes).filter(Number).map(v => Number(v));
  }

  ngOnInit() {
    this.initValidate();
    this.activeRout.data.subscribe(data => {
      if (data.hasOwnProperty('quizResolver') && data.hasOwnProperty('group')) {
        this.quiz = data.quizResolver.quiz;
        this.group = data.group;
        this.initGroup();
        if (data.hasOwnProperty('questionResolver')) {
          this.question = data.questionResolver.question;
          this.initAnswersAndSettings();
          this.initFeedback();
          return;
        }
        this.createNewQuestion();
      } else {
        console.log('Not found correct quiz');
      }
    });
  }

  createNewQuestion() {
    this.isNewState = true;
    this.question = new Question();
    this.question.quizId = this.quiz.id;
    this.question.groupId = this.group.id;
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
  }

  initAnswersAndSettings() {
    this.choices = JSON.parse(this.question.choices);
    this.settings = JSON.parse(this.question.settings);
  }

  initValidate() {
    this.questionForm = this.fb.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
      groupId: [Number, Validators.required],
      text: ['', Validators.required]
    });
  }

  updateQuestionModel() {
    this.question.groupId = this.group.id;
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
      this.router.navigate(['/editquiz/', this.quiz.id, 'group', this.group.id]);
    }, error => console.log(error));
  }

  createQuestion() {
    if (!this.questionForm.valid) {
      return;
    }
    this.updateQuestionModel();
    this.questionService.createQuestion(this.question).subscribe(response => {
      this.router.navigate(['/editquiz/', this.quiz.id, 'group', this.group.id]);
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
    this.group = this.groupResurce.filter(group => group.id === select.selected.value)[0];
  }

}
