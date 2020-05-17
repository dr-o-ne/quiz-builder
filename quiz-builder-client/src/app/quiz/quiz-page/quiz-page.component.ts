import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { QuestionType } from 'src/app/_models/question';
import { Router, ActivatedRoute } from '@angular/router';
import { Group } from 'src/app/_models/group';
import { QuizService } from 'src/app/_service/quiz.service';
import { MatTabGroup } from '@angular/material/tabs';

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

  hideBtnAddGroup = false;
  hideBtnNewGroup = false;
  currentIndexTab: number;
  newNameGroup: string;

  questionTypes = QuestionType;
  questionTypeKeys: number[];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private activeRout: ActivatedRoute,
    private quizService: QuizService
  ) {
    this.questionTypeKeys = Object.keys( this.questionTypes ).filter( Number ).map( v => Number( v ) );
  }

  ngOnInit() {
    this.initValidate();
    this.activeRout.data.subscribe( response => {
      if ( response.hasOwnProperty( 'quizResolver' ) ) {
        this.quiz = response.quizResolver.quiz;
        this.initGroups( this.quiz );
      } else {
        this.quiz = new Quiz();
      }
    } );
  }

  initGroups( quiz: Quiz ): void {
    const defaultGroup = new Group( '', this.quiz.id );
    this.groups.push( defaultGroup );
    this.groups.push( ...quiz.groups );
  }

  setActiveGroup( groupId: string ): void {
    this.selectedIndex = this.groups.findIndex( ( group => group.id === groupId ) );
  }

  addTab(): void {
    this.hideBtnAddGroup = true;
    this.groupFormControl = new FormControl( '', [
      Validators.required
    ] );
  }

  removeTab( index: number ): void {
    this.groups.splice( index, 1 );
  }

  addGroup(): void {
    if ( this.groupFormControl.valid ) {
      const group = new Group( '', this.quiz.id, this.groupFormControl.value );
      this.quizService.createGroup( group ).subscribe( ( response: { groupId: string } ) => {
          group.id = response.groupId;
          this.groups.push( group );
          this.setActiveGroup( group.id );
        },
        error => console.log( error ),
        () => {
          this.cancelData();
          this.groupFormControl.reset();
        } );
    }
  }

  cancelData(): void {
    this.hideBtnAddGroup = false;
    this.hideBtnNewGroup = false;
    this.newNameGroup = '';
  }

  editTab( index: number ): void {
    this.currentIndexTab = index;
    this.groupFormControl = new FormControl( '', [
      Validators.required
    ] );
    this.hideBtnAddGroup = true;
    const currentGroup = this.groups[index];
    this.newNameGroup = currentGroup.name;
    this.hideBtnNewGroup = true;
  }

  saveEditTab(): void {
    if ( this.groupFormControl.valid ) {
      this.hideBtnAddGroup = false;
      this.hideBtnNewGroup = false;
    }
  }

  initValidate(): void {
    this.quizForm = this.fb.group( {
      name: [ '', Validators.required ]
    } );
  }

  createQuiz(): void {
    this.quizService.createQuiz( this.quiz ).subscribe( response => {
      this.router.navigate( [ '/quizzes' ] );
    }, error => console.log( error ) );
  }

  updateQuiz(): void {
    this.quizService.updateQuiz( this.quiz ).subscribe( response => {
      this.router.navigate( [ '/quizzes' ] );
    }, error => console.log( error ) );
  }

  addNewQuestion( tabGroup: MatTabGroup, typeQuestion: QuestionType ): void {
    this.router.navigate(
      [ 'questions' ],
      {
        relativeTo: this.activeRout,
        state: { quizId: this.quiz.id, questionType: typeQuestion, groups: [ this.groups[tabGroup.selectedIndex] ] }
      }
    );
  }

  clickToggle( checked: boolean ): void {
    this.quiz.isVisible = checked;
  }
}
