import {ChangeDetectionStrategy, Component, OnInit, Pipe, PipeTransform} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {Quiz} from '../_models/quiz';
import {Question, QuestionType} from '../_models/question';
import {Answer} from '../_models/answer';
import {AnswerService} from '../_service/answer.service';
import {Group} from '../_models/group';
import {QuestionService} from '../_service/question.service';

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

  answerData: Answer[] = [];
  groupResurce: Group[] = [];

  defaultCountAnswer = 4;
  isNewState = false;

  constructor(private fb: FormBuilder,
              private router: Router,
              private activeRout: ActivatedRoute,
              private answerService: AnswerService,
              private questionService: QuestionService)
  {
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
          this.initAnswer(this.question.id);
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
    this.question.id = this.generateId();
  }

  initGroup() {
    const storage = JSON.parse(localStorage.getItem('grouplist'));
    this.groupResurce = storage.filter(obj => obj.quizId === this.quiz.id);
  }

  initAnswer(id: number) {
    const storage = localStorage.getItem('answerlist');
    if (!storage) {
      this.answerService.getAnswerData().subscribe((answer: any) => {
        this.answerData = answer.answerlist.filter((obj) => obj.questionId === id);
        localStorage.setItem('answerlist', JSON.stringify(answer.answerlist));
      }, error => {
        console.log(error);
      });
    } else {
      const tempSave = localStorage.getItem('answer-save');
      const tempUpdate = localStorage.getItem('answer-update');
      this.answerData = JSON.parse(storage);
      if (!tempSave && !tempUpdate) {
        this.answerData = this.answerData.filter((obj) => obj.questionId === id);
        return;
      }
      if (tempSave) {
        const newAnswer: Answer = JSON.parse(tempSave);
        newAnswer.id = this.generateId();
        this.answerData.push(newAnswer);
        localStorage.setItem('answerlist', JSON.stringify(this.answerData));
        this.answerData = this.answerData.filter((obj) => obj.questionId === id);
        localStorage.removeItem('answer-save');
        return;
      }
      const editAnswer: Answer = JSON.parse(tempUpdate);
      const objIndex = this.answerData.findIndex((obj => obj.id === editAnswer.id));
      this.answerData[objIndex] = editAnswer;
      localStorage.setItem('answerlist', JSON.stringify(this.answerData));
      this.answerData = this.answerData.filter((obj) => obj.questionId === id);
      localStorage.removeItem('answer-update');
    }
  }

  generateId() {
    return Math.floor(Math.random() * 10000) + 1;
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
    this.question.name = this.questionForm.value.name;
    this.question.type = this.questionForm.value.type;
    this.question.text = this.questionForm.value.text;
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
    // this.saveAnswer(); ???
  }

  isDisabledBtn(): boolean {
    const validArray = [this.questionForm.valid, this.isValidAnswer()];
    return validArray.some(arr => arr === false);
  }

  isValidAnswer(): boolean {
    if (this.answerData.length === 0) {
      return false;
    }
    const index = this.answerData.findIndex(ans => ans.name === '');
    const isChecked = this.answerData.some(ans => ans.isCorrect === true);
    return index === -1 && isChecked;
  }

  addNewAnswer() {
    switch (this.question.type) {
      default:
        this.addAnswer();
        break;
    }
  }

  addAnswer(name?: string, isCorrect?: boolean) {
    const newAnswer = new Answer();
    newAnswer.name = name || '';
    newAnswer.id = this.generateId();
    newAnswer.isCorrect = isCorrect || false;
    newAnswer.questionId = this.question.id;
    this.answerData.push(newAnswer);
  }

  saveListAnswers(answers: Answer[]) {
    this.answerData.forEach(item => {
      const index = answers.findIndex((obj) => obj.id === item.id);
      if (index === -1) {
        answers.push(item);
        return;
      }
      answers[index] = item;
    });
    localStorage.setItem('answerlist', JSON.stringify(answers));
  }

  saveAnswer() {
    const storageAnswer = localStorage.getItem('answerlist');
    let currenAnswerList: Answer[] = [];
    if (!storageAnswer) {
      this.answerService.getAnswerData().subscribe((answers: any) => {
        currenAnswerList = answers.answerlist;
        this.saveListAnswers(currenAnswerList);
      }, error => console.log(error));
      return;
    }
    currenAnswerList = JSON.parse(storageAnswer);
    this.saveListAnswers(currenAnswerList);
  }

  makeCustomListAnswer(count: number) {
    for (let i = 0; i < count; i++) {
      this.addAnswer();
    }
  }

  selectChangeType() {
    this.answerData = [];
    switch (this.question.type) {
      case QuestionType.TrueFalse:
        this.addAnswer('True', true);
        this.addAnswer('False');
        break;
      default:
        this.makeCustomListAnswer(this.defaultCountAnswer);
        break;
    }
  }

  selectGroup(select) {
    this.group = this.groupResurce.filter(group => group.id === select.selected.value)[0];
  }

}
