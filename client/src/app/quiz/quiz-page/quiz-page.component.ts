import { Component, OnInit, ChangeDetectionStrategy, ElementRef, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { Quiz } from 'src/app/_models/quiz';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { QuestionType } from 'src/app/_models/question';
import { Router, ActivatedRoute } from '@angular/router';
import { Group } from 'src/app/_models/group';
import { QuizService } from 'src/app/_service/quiz.service';
import { MatTabGroup } from '@angular/material/tabs';
import { GroupForm, BtnGroupControl } from 'src/app/_models/option';
import { finalize } from 'rxjs/operators';

@Component( {
  changeDetection: ChangeDetectionStrategy.Default,
  selector: 'app-quiz-page',
  templateUrl: './quiz-page.component.html',
  styleUrls: [ './quiz-page.component.css' ]
} )
export class QuizPageComponent implements OnInit, AfterViewInit {
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
    new BtnGroupControl( 'add', this.addGroup.bind( this ), true ),
    new BtnGroupControl( 'edit', this.saveEditTab.bind( this ), false ),
    new BtnGroupControl( 'cancel', this.resetGroupForm.bind( this ), true )
  ];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private activeRoute: ActivatedRoute,
    private quizService: QuizService,
    private cdr: ChangeDetectorRef
  ) {
    this.questionTypeKeys = Object.keys( this.questionTypes ).filter( Number ).map( v => Number( v ) );
  }

  ngOnInit(): void {
    this.initValidate();
    this.activeRoute.data.subscribe( response => {
      if ( response && response.quizResolver ) {
        this.quiz = response.quizResolver.quiz;
        this.initGroups( this.quiz );
        this.initActiveGroup();
      } else {
        this.quiz = new Quiz();
      }
    } );
  }

  ngAfterViewInit(): void {
    this.cdr.detectChanges();
  }

  initGroups( quiz: Quiz ): void {
    const defaultGroup = new Group( '', quiz.id );
    this.groups.push( defaultGroup );
    this.groups.push( ...quiz.groups );
  }

  initActiveGroup(): void {
    const groupId = history.state.groupId;
    if ( groupId ) {
      this.selectedIndex = this.groups.findIndex( gr => gr.id === groupId );
    }
  }

  setActiveGroup( groupId: string ): void {
    this.selectedIndex = this.groups.findIndex( ( group => group.id === groupId ) );
  }

  addTab(): void {
    this.groupForm.visible = false;
    this.groupFormControl = new FormControl( '', [
      Validators.required
    ] );
  }

  removeTab( index: number ): void {
    const group = this.groups[index];
    this.quizService.deleteGroup( group.id ).subscribe(
      response => this.groups.splice( index, 1 ),
      error => console.log( error ) );
  }

  addGroup(): void {
    if ( this.groupFormControl.valid ) {
      const group = new Group( '', this.quiz.id, this.groupForm.name );
      this.quizService.createGroup( group ).pipe(
        finalize( () => this.resetGroupForm() )
      ).subscribe( ( response: any ) => {
          group.id = response.group.id;
          this.groups.push( group );
          this.setActiveGroup( group.id );
        },
        error => console.log( error )
      );
    }
  }

  resetGroupForm(): void {
    this.groupForm.visible = true;
    this.setupBtnGroup( 'edit' );
    this.groupFormControl.reset();
  }

  setupBtnGroup( action: string ): void {
    this.btnGroupControls.forEach( btn => {
      btn.visible = btn.name !== action;
    } );
  }

  editTab( index: number ): void {
    this.currentIndexTab = index;
    this.groupFormControl = new FormControl( '', [
      Validators.required
    ] );
    this.groupForm.name = this.groups[index].name;
    this.groupForm.visible = false;
    this.setupBtnGroup( 'add' );
  }

  saveEditTab(): void {
    if ( !this.groupFormControl.valid ) {
      return;
    }
    const currentGroup = this.groups[this.currentIndexTab];
    const newGroup = Object.assign( {}, currentGroup );
    newGroup.name = this.groupForm.name;
    this.quizService.updateGroup( newGroup ).pipe(
      finalize( () => {
        this.groupForm.visible = true;
        this.setupBtnGroup( 'edit' );
      } ) ).subscribe(
      response => this.groups[this.currentIndexTab] = newGroup,
      error => console.log( error )
    );
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
    this.quizService.createQuiz( this.quiz ).subscribe( (response: any) => {
      this.router.navigateByUrl('quizzes/' + response.quiz.id + '/edit');
    }, error => console.log( error ) );
  }

  updateQuiz(): void {
    this.quizService.updateQuiz( this.quiz ).subscribe( response => {
      this.navigateToParent();
    }, error => console.log( error ) );
  }

  addNewQuestion( tabGroup: MatTabGroup, typeQuestion: QuestionType ): void {
    this.router.navigate(
      [ 'questions' ],
      {
        relativeTo: this.activeRoute,
        state: {
          quizId: this.quiz.id,
          questionType: typeQuestion,
          groupId: this.groups[tabGroup.selectedIndex].id,
          groups: this.groups
        }
      }
    );
  }

  clickToggle( checked: boolean ): void {
    this.quiz.isVisible = checked;
  }

  replyClick(): void {
    this.navigateToParent();
  }

  navigateToParent(): void {
    if ( this.quiz.id ) {
      this.router.navigate(
        [ '../../' ],
        {
          relativeTo: this.activeRoute.parent
        }
      );
      return;
    }
    this.router.navigate(
      [ '../' ],
      {
        relativeTo: this.activeRoute
      }
    );
  }
}
