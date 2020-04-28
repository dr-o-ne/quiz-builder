import {Component, OnInit, ViewChild, AfterViewInit, ChangeDetectionStrategy} from '@angular/core';
import {Quiz} from 'src/app/_models/quiz';
import {FormGroup, FormBuilder, Validators, FormControl} from '@angular/forms';
import {Question} from 'src/app/_models/question';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {Router, ActivatedRoute} from '@angular/router';
import {QuestionService} from 'src/app/_service/question.service';
import {Group} from 'src/app/_models/group';
import {QuizService} from 'src/app/_service/quiz.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-quiz-page',
  templateUrl: './quiz-page.component.html',
  styleUrls: ['./quiz-page.component.css']
})
export class QuizPageComponent implements OnInit {
  quiz: Quiz;
  newGroup: Group;
  quizForm: FormGroup;
  resources: string[] = ['In design'];

  groupFormControl: FormControl;

  dataGroup: Group[];

  hideBtnAddGroup = false;
  hideBtnNewGroup = false;
  currentIndexTab: number;
  newNameGroup: string;

  constructor(private fb: FormBuilder, private router: Router, private activeRout: ActivatedRoute,
              private quizService: QuizService) {
  }

  ngOnInit() {
    this.activeRout.data.subscribe(response => {
      if (response.hasOwnProperty('quizResolver')) {
        this.quiz = response.quizResolver.quiz;
        this.initGroup(this.quiz.id);
      } else {
        this.quiz = new Quiz();
      }
      this.initValidate();
    });
  }

  initGroup(id: number) {
    const storage = localStorage.getItem('grouplist');
    if (!storage) {
      this.quizService.getGroupData().subscribe((group: any) => {
        this.dataGroup = group.grouplist.filter((obj) => obj.quizId === id);
        localStorage.setItem('grouplist', JSON.stringify(group.grouplist));
      }, error => {
        console.log(error);
      });
    } else {
      const tempSave = localStorage.getItem('group-save');
      const tempUpdate = localStorage.getItem('group-update');
      this.dataGroup = JSON.parse(storage);
      if (!tempSave && !tempUpdate) {
        this.dataGroup = this.dataGroup.filter((obj) => obj.quizId === id);
        return;
      }
      if (tempSave) {
        const newGroup: Group = JSON.parse(tempSave);
        this.dataGroup.push(newGroup);
        localStorage.setItem('grouplist', JSON.stringify(this.dataGroup));
        this.dataGroup = this.dataGroup.filter((obj) => obj.quizId === id);
        localStorage.removeItem('group-save');
        return;
      }
      const editGroup: Group = JSON.parse(tempUpdate);
      const objIndex = this.dataGroup.findIndex((obj => obj.id === editGroup.id));
      this.dataGroup[objIndex] = editGroup;
      localStorage.setItem('grouplist', JSON.stringify(this.dataGroup));
      this.dataGroup = this.dataGroup.filter((obj) => obj.quizId === id);
      localStorage.removeItem('group-update');
    }
  }

  addTab() {
    this.hideBtnAddGroup = true;
    this.groupFormControl = new FormControl('', [
      Validators.required
    ]);
  }

  removeTab(index: number) {
    const storage = localStorage.getItem('grouplist');
    const listGroup = JSON.parse(storage);
    const currentGroup = this.dataGroup[index];
    const currentIndex = listGroup.findIndex((obj => obj.id === currentGroup.id));
    this.dataGroup.splice(index, 1);
    listGroup.splice(currentIndex, 1);
    localStorage.removeItem('grouplist');
    localStorage.setItem('grouplist', JSON.stringify(listGroup));
  }

  generateId() {
    return Math.floor(Math.random() * 10000) + 1;
  }

  addNewTab(operation?: string) {
    if (!this.groupFormControl.invalid) {
      this.hideBtnAddGroup = false;
      this.newGroup = new Group();
      this.newGroup.id = this.generateId();
      this.newGroup.name = this.newNameGroup;
      this.newGroup.quizId = this.quiz.id;
      localStorage.setItem('group-save', JSON.stringify(this.newGroup));
      this.initGroup(this.quiz.id);
    }
  }

  cancelData() {
    this.hideBtnAddGroup = false;
    this.hideBtnNewGroup = false;
    this.newNameGroup = '';
  }

  editTab(index: number) {
    this.currentIndexTab = index;
    this.groupFormControl = new FormControl('', [
      Validators.required
    ]);
    this.hideBtnAddGroup = true;
    const currentGroup = this.dataGroup[index];
    this.newNameGroup = currentGroup.name;
    this.hideBtnNewGroup = true;
  }

  saveEditTab() {
    if (!this.groupFormControl.invalid) {
      this.hideBtnAddGroup = false;
      const currentGroup = this.dataGroup[this.currentIndexTab];
      currentGroup.name = this.newNameGroup;
      localStorage.setItem('group-update', JSON.stringify(currentGroup));
      this.hideBtnNewGroup = false;
      this.initGroup(this.quiz.id);
    }
  }

  initValidate() {
    this.quizForm = this.fb.group({
      name: ['', Validators.required],
      status: ['', Validators.required]
    });
  }

  createQuiz() {
    this.quizService.createQuiz(this.quiz).subscribe(response => {
      this.router.navigate(['/quizlist']);
    }, error => console.log(error));
  }

  updateQuiz() {
    this.quizService.updateQuiz(this.quiz).subscribe(response => {
      this.router.navigate(['/quizlist']);
    }, error => console.log(error));
  }
}
