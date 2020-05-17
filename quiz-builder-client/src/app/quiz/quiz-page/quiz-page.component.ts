import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { QuestionType } from 'src/app/_models/question';
import { Router, ActivatedRoute } from '@angular/router';
import { Group } from 'src/app/_models/group';
import { QuizService } from 'src/app/_service/quiz.service';

@Component( {
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-quiz-page',
  templateUrl: './quiz-page.component.html',
  styleUrls: [ './quiz-page.component.css' ]
} )
export class QuizPageComponent implements OnInit {
  quiz: Quiz;
  newGroup: Group;
  oldGroup: Group;
  selectedIndex = 0;
  quizForm: FormGroup;
  groupFormControl: FormControl;

  groups: Group[];

  hideBtnAddGroup = false;
  hideBtnNewGroup = false;
  currentIndexTab: number;
  newNameGroup: string;

  questionTypes = QuestionType;
  questionTypeKeys: number[];

  constructor( private fb: FormBuilder, private router: Router, private activeRout: ActivatedRoute,
               private quizService: QuizService ) {
    this.questionTypeKeys = Object.keys( this.questionTypes ).filter( Number ).map( v => Number( v ) );
  }

  ngOnInit() {
    this.initValidate();
    this.activeRout.data.subscribe( response => {
      if ( response.hasOwnProperty( 'quizResolver' ) ) {
        this.quiz = response.quizResolver.quiz;
        if ( response.hasOwnProperty( 'group' ) ) {
          this.oldGroup = response.group;
          this.initGroup( this.quiz.id );
          return;
        }
        this.initGroup( this.quiz.id );
      } else {
        this.quiz = new Quiz();
      }
    } );
  }

  initGroup( id: string ) {
    const storage = localStorage.getItem( 'grouplist' );
    if ( !storage ) {
      this.quizService.getGroupData().subscribe( ( group: any ) => {
        this.groups = group.grouplist.filter( ( obj ) => obj.quizId === id );
        localStorage.setItem( 'grouplist', JSON.stringify( group.grouplist ) );
        if ( !this.groups.length ) {
          this.addNewTab( true, 'Default group' );
        }
      }, error => {
        console.log( error );
      } );
    } else {
      const tempSave = localStorage.getItem( 'group-save' );
      const tempUpdate = localStorage.getItem( 'group-update' );
      this.groups = JSON.parse( storage );
      if ( !tempSave && !tempUpdate ) {
        this.groups = this.groups.filter( ( obj ) => obj.quizId === id );
        if ( !this.groups.length ) {
          this.addNewTab( true, 'Default group' );
        }
        this.setActiveGroup();
        return;
      }
      if ( tempSave ) {
        const newGroup: Group = JSON.parse( tempSave );
        this.groups.push( newGroup );
        localStorage.setItem( 'grouplist', JSON.stringify( this.groups ) );
        this.groups = this.groups.filter( ( obj ) => obj.quizId === id );
        localStorage.removeItem( 'group-save' );
        this.setActiveGroup();
        return;
      }
      const editGroup: Group = JSON.parse( tempUpdate );
      const objIndex = this.groups.findIndex( ( obj => obj.id === editGroup.id ) );
      this.groups[objIndex] = editGroup;
      localStorage.setItem( 'grouplist', JSON.stringify( this.groups ) );
      this.groups = this.groups.filter( ( obj ) => obj.quizId === id );
      localStorage.removeItem( 'group-update' );
      this.setActiveGroup();
    }
  }

  setActiveGroup() {
    if ( this.oldGroup ) {
      this.selectedIndex = this.groups.findIndex( ( obj => obj.id === this.oldGroup.id ) );
    }
  }

  addTab() {
    this.hideBtnAddGroup = true;
    this.groupFormControl = new FormControl( '', [
      Validators.required
    ] );
  }

  removeTab( index: number ) {
    const storage = localStorage.getItem( 'grouplist' );
    const listGroup = JSON.parse( storage );
    const currentGroup = this.groups[index];
    const currentIndex = listGroup.findIndex( ( obj => obj.id === currentGroup.id ) );
    this.groups.splice( index, 1 );
    listGroup.splice( currentIndex, 1 );
    localStorage.removeItem( 'grouplist' );
    localStorage.setItem( 'grouplist', JSON.stringify( listGroup ) );
  }

  generateId(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace( /[xy]/g, ( c ) => {
      const r = Math.random() * 16 | 0;
      const v = c === 'x' ? r : ( r & 0x3 | 0x8 );
      return v.toString( 16 );
    } );
  }

  addNewTab( isValid?: boolean, defaultName?: string ) {
    if ( isValid || !this.groupFormControl.invalid ) {
      this.hideBtnAddGroup = false;
      this.newGroup = new Group();
      this.newGroup.id = this.generateId();
      if ( defaultName ) {
        this.newGroup.name = defaultName;
      } else {
        this.newGroup.name = this.newNameGroup;
      }
      this.newGroup.quizId = this.quiz.id;
      localStorage.setItem( 'group-save', JSON.stringify( this.newGroup ) );
      this.initGroup( this.quiz.id );
    }
  }

  cancelData() {
    this.hideBtnAddGroup = false;
    this.hideBtnNewGroup = false;
    this.newNameGroup = '';
  }

  editTab( index: number ) {
    this.currentIndexTab = index;
    this.groupFormControl = new FormControl( '', [
      Validators.required
    ] );
    this.hideBtnAddGroup = true;
    const currentGroup = this.groups[index];
    this.newNameGroup = currentGroup.name;
    this.hideBtnNewGroup = true;
  }

  saveEditTab() {
    if ( !this.groupFormControl.invalid ) {
      this.hideBtnAddGroup = false;
      const currentGroup = this.groups[this.currentIndexTab];
      currentGroup.name = this.newNameGroup;
      localStorage.setItem( 'group-update', JSON.stringify( currentGroup ) );
      this.hideBtnNewGroup = false;
      this.initGroup( this.quiz.id );
    }
  }

  initValidate() {
    this.quizForm = this.fb.group( {
      name: [ '', Validators.required ]
    } );
  }

  createQuiz() {
    this.quizService.createQuiz( this.quiz ).subscribe( response => {
      this.router.navigate( [ '/quizzes' ] );
    }, error => console.log( error ) );
  }

  updateQuiz() {
    this.quizService.updateQuiz( this.quiz ).subscribe( response => {
      this.router.navigate( [ '/quizzes' ] );
    }, error => console.log( error ) );
  }

  addNewQuestion( tabGroup, typeQuestion ) {
    localStorage.setItem( 'typeQuestion' + this.quiz.id, typeQuestion );
    this.router.navigate( [ 'questions' ], { relativeTo: this.activeRout } );
  }

  clickToogle( toogle ) {
    this.quiz.isVisible = toogle._checked;
  }
}
