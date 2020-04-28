import { Component, OnInit, ViewChild, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Quiz } from '../_models/quiz';
import { Question } from '../_models/question';
import { Answer } from '../_models/answer';
import { AnswerService } from '../_service/answer.service';
import { Group } from '../_models/group';

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
  resources: string[] = ['Single Choice (Radio Button)', 'Single Choice (Dropdown)', 'Multiple Choice', 'True/False'];

  answerData: Answer[] = [];
  groupResurce: Group[] = [];

  constructor(private fb: FormBuilder, private router: Router, private activeRout: ActivatedRoute,
              private answerService: AnswerService) {}

  ngOnInit() {
    this.initValidate();
    this.initGroup();

    this.activeRout.data.subscribe(data => {
      if (data.hasOwnProperty('quiz') && data.hasOwnProperty('group')) {
        this.quiz = data.quiz;
        this.group = data.group;
        if (data.hasOwnProperty('question')) {
          this.question = data.question;
          this.initAnswer(this.question.id);
          return;
        }
        this.question = new Question();
        this.question.quizId = this.quiz.id;
        this.question.groupId = this.group.id;
      } else {
        console.log('Not found correct quiz');
      }
    });
  }

  initGroup() {
    const storage = localStorage.getItem('grouplist');
    this.groupResurce = JSON.parse(storage);
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
      groupId: [Number, Validators.required]
    });
  }

  saveOrUpdate(operation: string) {
    if (this.questionForm.valid) {
      if (!this.question.hasOwnProperty('id')) {
        this.question.id = this.generateId();
      }
      this.question.groupId = this.group.id;
      this.question.name = this.questionForm.value.name;
      this.question.type = this.questionForm.value.type;
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

  changeRadioButton(event) {
    this.answerData.forEach(item => {
      if (item.id === event.value) {
        item.isCorrect = true;
        return;
      }
      item.isCorrect = false;
    });
  }

  addNewAnswer() {
    switch (this.question.type) {
      case 'True/False':
        if (this.answerData.length < 2) {
          this.addAnswer('True');
          this.addAnswer('False');
          return;
        }
        break;
      default:
        this.addAnswer();
        break;
    }
  }

  addAnswer(name?: string) {
    const newAnswer = new Answer();
    if (name) {
      newAnswer.name = name;
    }
    newAnswer.id = this.generateId();
    newAnswer.isCorrect = false;
    newAnswer.questionId = this.question.id;
    this.answerData.push(newAnswer);
  }

  deleteAnswer(answer: Answer) {
    this.answerData.splice(this.answerData.findIndex(ans => ans.id === answer.id), 1);
    const storageAnswer = localStorage.getItem('answerlist');
    const currenAnswerList: Answer[] = JSON.parse(storageAnswer);
    currenAnswerList.splice(currenAnswerList.findIndex(ans => ans.id === answer.id), 1);
    localStorage.setItem('answerlist', JSON.stringify(currenAnswerList));
  }

  saveAnswer() {
    const storageAnswer = localStorage.getItem('answerlist');
    const currenAnswerList: Answer[] = JSON.parse(storageAnswer);
    this.answerData.forEach(item => {
      const index = currenAnswerList.findIndex((obj) => obj.id === item.id);
      if (index === -1) {
        currenAnswerList.push(item);
        return;
      }
      currenAnswerList[index] = item;
    });
    localStorage.setItem('answerlist', JSON.stringify(currenAnswerList));
    alert('Save Succes');
  }

}
