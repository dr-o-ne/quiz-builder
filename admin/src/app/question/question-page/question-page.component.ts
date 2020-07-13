import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Question, QuestionType } from 'src/app/_models/question';
import { Choice } from 'src/app/_models/choice';
import { BaseChoiceSettings } from 'src/app/_models/settings/answer.settings';
import { MatDialog } from '@angular/material/dialog';
import { ModalWindowPreviewQuestionComponent } from '../modal-window/modal-window-preview-question.component';
import { Option } from 'src/app/_models/option';
import { fadeInUp400ms } from 'src/@vex/animations/fade-in-up.animation';
import { QuestionDataProvider } from 'src/app/_service/dataProviders/question.dataProvider';

@Component( {
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-question-page',
  templateUrl: './question-page.component.html',
  styleUrls: [ './question-page.component.css',
  '../../../../node_modules/quill/dist/quill.snow.css',
  '../../../@vex/styles/partials/plugins/_quill.scss' ],
  encapsulation: ViewEncapsulation.None,
  animations: [fadeInUp400ms]
} )
export class QuestionPageComponent implements OnInit {

  quizId: string;
  question: Question;
  questionForm: FormGroup;
  questionTypes = QuestionType;
  questionTypeKeys: number[];

  choices: Choice[] = [];
  settings: BaseChoiceSettings = new BaseChoiceSettings();

  options: Option[] = [
    new Option( 'feedback', 'Feedback', 'wysiwyg' ),
    new Option( 'correctFeedback', 'Correct feedback', 'wysiwyg' ),
    new Option( 'incorrectFeedback', 'Incorrect feedback', 'wysiwyg' ),
  ];

  constructor(
    private questionDataProvider: QuestionDataProvider,
    private fb: FormBuilder,
    private router: Router,
    private activeRoute: ActivatedRoute,
    public dialog: MatDialog) {

    this.questionTypeKeys = Object.keys(this.questionTypes).filter(Number).map(v => Number(v));

    this.questionForm = this.fb.group({
      name: ['', Validators.nullValidator],
      type: ['', Validators.required],
      text: ['', Validators.required],
      points: ['', Validators.required],
      feedback: ['', Validators.nullValidator],
      correctFeedback: ['', Validators.nullValidator],
      incorrectFeedback: ['', Validators.nullValidator],
    });
    //TODO: add dynamic, how?
  }

  ngOnInit() {

    this.quizId = history.state.quizId;
    this.activeRoute.data.subscribe( data => {
      if ( data && data.questionResolver ) {
        this.question = data.questionResolver.question;

        this.questionForm.patchValue({
          name: this.question.name, 
          text: this.question.text,
          points: this.question.points,
          feedback: this.question.feedback,
          correctFeedback: this.question.correctFeedback,
          incorrectFeedback: this.question.incorrectFeedback
        });

        this.initAnswersAndSettings();
      } else {
        this.createNewQuestion(history.state.questionType);
      }
      this.initGroup(history.state.groupId);
    } );
  }

  createNewQuestion(questionType: QuestionType): void {
    this.question = new Question();
    this.question.quizId = this.quizId;
    this.question.type = questionType;
    this.question.name = 'Question ' + Math.floor(Math.random() * 100);
    this.question.text = 'default';
  }

  initGroup(groupId: string): void {
    if ( !this.question.groupId ) {
      this.question.groupId = groupId || '';
    }
  }

  initAnswersAndSettings(): void {
    if ( this.question.choices ) {
      this.choices = JSON.parse( this.question.choices );
    }
    this.settings = JSON.parse( this.question.settings || '{}' );
  }

  updateQuestionModel(): void {
    this.question = Object.assign( this.question, this.questionForm.value );
    this.question.choices = JSON.stringify( this.choices );
    this.question.settings = JSON.stringify( this.settings );
  }

  saveChange = () => 
    this.isEditMode() ? this.updateQuestion() : this.createQuestion();

  updateQuestion(): void {
    if ( !this.questionForm.valid ) {
      return;
    }
    this.updateQuestionModel();
    this.questionDataProvider.updateQuestion( this.question ).subscribe( response => {
      this.navigateToParent();
    }, error => console.log( error ) );
  }

  navigateToParent(): void {
    this.router.navigate(
      ['../../'],
      {
        relativeTo: this.activeRoute,
        state: {
          groupId: this.question.groupId
        }
      }
    );
  }

  onReturn = () => this.navigateToParent();

  getQuestionOptions = () => this.options.filter( x => x.enabled );

  createQuestion(): void {
    if ( !this.questionForm.valid ) {
      return;
    }
    this.updateQuestionModel();
    this.questionDataProvider.createQuestion( this.question ).subscribe( response => {
      this.navigateToParent();
    }, error => console.log( error ) );
  }

  isDisabledBtn(): boolean {
    return !this.questionForm?.valid || !this.isChoicesValid();
  }

  isChoicesValid(): boolean {
    if ( this.question.type === this.questionTypes.LongAnswer ) {
      return true;
    }
    if ( this.choices.length === 0 ) {
      return false;
    }
    const isAnyEmpty = this.choices.some( c => c.text === '' );
    const isAnyCorrect = this.choices.some( c => c.isCorrect === true );
    return !isAnyEmpty && isAnyCorrect;
  }

  selectChangeType(): void {
    this.choices = [];
  }

  openPreview(): void {
    this.updateQuestionModel();
    const dialogRef = this.dialog.open( ModalWindowPreviewQuestionComponent, {
      width: '50em',
      data: { question: this.question }
    } );

    dialogRef.afterClosed().subscribe( result => {
      console.log( 'The dialog was closed' );
    } );
  }

  clickOption( event: MouseEvent, option: Option ): void {
    option.enabled = !option.enabled;
    event.stopPropagation();
  }

  onContentChanged(_) {/*HACK*/}

  isEditMode = () => this.question.id;

}
