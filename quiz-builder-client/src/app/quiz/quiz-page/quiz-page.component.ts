import { Component, OnInit, ChangeDetectionStrategy, ElementRef } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { QuestionType } from 'src/app/_models/question';
import { Router, ActivatedRoute } from '@angular/router';
import { Group } from 'src/app/_models/group';
import { QuizService } from 'src/app/_service/quiz.service';
import { MatTabGroup } from '@angular/material/tabs';
import { GroupForm, BtnGroupControl } from 'src/app/_models/option';

@Component( {
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-quiz-page',
  templateUrl: './quiz-page.component.html',
  styleUrls: [ './quiz-page.component.css' ]
} )
export class QuizPageComponent implements OnInit {
  quiz: Quiz;
  selectedIndex = 0;
  quizForm: FormGroup;
  groupFormControl: FormControl;

  groups: Group[] = [];
  currentIndexTab: number;

  questionTypes = QuestionType;
  questionTypeKeys: number[];

  groupForm = new GroupForm();
  btnGroupControls: BtnGroupControl[] = [
    new BtnGroupControl('add', this.addGroup.bind(this), false),
    new BtnGroupControl('edit', this.saveEditTab.bind(this), true),
    new BtnGroupControl('cancel', this.resetGroupForm.bind(this), false)
  ];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private activeRoute: ActivatedRoute,
    private quizService: QuizService
  ) {
    this.questionTypeKeys = Object.keys( this.questionTypes ).filter( Number ).map( v => Number( v ) );
  }

  ngOnInit() {
    this.initValidate();
    this.activeRoute.data.subscribe( response => {
      if ( response && response.quizResolver ) {
        this.quiz = response.quizResolver.quiz;
        this.initGroups( this.quiz );
      } else {
        this.quiz = new Quiz();
      }
    } );
  }

  initGroups( quiz: Quiz ): void {
    const defaultGroup = new Group( '', quiz.id );
    this.groups.push( defaultGroup );
    this.groups.push( ...quiz.groups );
  }

  setActiveGroup( groupId: string ): void {
    this.selectedIndex = this.groups.findIndex( ( group => group.id === groupId ) );
  }

  addTab(): void {
    this.groupForm.isHide = true;
    this.groupFormControl = new FormControl( '', [
      Validators.required
    ] );
  }

  removeTab( index: number ): void {
    this.groups.splice( index, 1 );
  }

  addGroup() {
    if ( this.groupFormControl.valid ) {
      const group = new Group( '', this.quiz.id, this.groupForm.name );
      this.quizService.createGroup( group ).subscribe( ( response: any ) => {
          group.id = response.group.groupId;
          this.groups.push( group );
          this.setActiveGroup( group.id );
        },
        error => console.log( error ),
        () => {
          this.resetGroupForm();
        } );
    }
  }

  resetGroupForm(): void {
    this.groupForm.isHide = false;
    this.setupBtnGroup('edit');
    this.groupFormControl.reset();
  }

  setupBtnGroup( action: string ) {
    this.btnGroupControls.forEach( btn => {
      btn.isHide = btn.name === action;
    });
  }

  editTab( index: number ): void {
    this.currentIndexTab = index;
    this.groupFormControl = new FormControl( '', [
      Validators.required
    ] );
    this.groupForm.name = this.groups[index].name;
    this.groupForm.isHide = true;
    this.setupBtnGroup('add');
  }

  saveEditTab(): void {
    if ( this.groupFormControl.valid ) {
      this.groupForm.isHide = false;
      this.setupBtnGroup('edit');
      this.groups[this.currentIndexTab].name = this.groupForm.name;
    }
  }

  initValidate(): void {
    this.quizForm = this.fb.group( {
      name: [ '', Validators.required ]
    } );
  }

  saveChange(): void {
    if ( !this.quiz.id ) {
      this.createQuiz();
      return;
    }
    this.updateQuiz();
  }

  createQuiz(): void {
    this.quizService.createQuiz( this.quiz ).subscribe( response => {
      this.router.navigate( [ 'quizzes' ] );
    }, error => console.log( error ) );
  }

  updateQuiz(): void {
    this.quizService.updateQuiz( this.quiz ).subscribe( response => {
      this.router.navigate( [ 'quizzes' ] );
    }, error => console.log( error ) );
  }

  addNewQuestion( tabGroup: MatTabGroup, typeQuestion: QuestionType ): void {
    this.router.navigate(
      [ 'questions' ],
      {
        relativeTo: this.activeRoute,
        state: { quizId: this.quiz.id, questionType: typeQuestion, groups: [ this.groups[tabGroup.selectedIndex] ] }
      }
    );
  }

  clickToggle( checked: boolean ): void {
    this.quiz.isVisible = checked;
  }
}
