import { Component, Input, OnInit, ViewChild, ComponentFactoryResolver, Output, EventEmitter } from '@angular/core';
import { QuestionType } from '../../model/_enums';
import { QuestionAttemptInfo, TrueFalseQuestionAttemptInfo } from '../../model/attemptInfo';
import { QuestionAttemptResult } from '../../model/attemptResult';
import { QuestionHostDirective } from './question-host.directive';
//import { MultipleSelectQuestionComponent } from './multiple-select-question/multiple-select-question.component';
import { TrueFalseQuestionComponent } from './true-false-question/true-false-question.component';
//import { MultipleChoiceQuestionComponent } from './multiple-choice-question/multiple-choice-question.component';
//import { LongAnswerQuestionComponent } from './long-answer-question/long-answer-question.component';

@Component({
  selector: 'qb-question-dynamic',
  template: '<ng-template qb-question-host></ng-template>'
})

export class QuestionDynamicComponent implements OnInit {

  @Input() question!: QuestionAttemptInfo;
  @Output() answer: EventEmitter<QuestionAttemptResult> = new EventEmitter<QuestionAttemptResult>();
  @ViewChild(QuestionHostDirective, { static: true }) host!: QuestionHostDirective;

  constructor(private resolver: ComponentFactoryResolver) {
  }

  ngOnInit(): void {

    console.log('test');

    this.loadComponent();
  }

  loadComponent(): void {

    const viewContainerRef = this.host.viewContainerRef;
    viewContainerRef.clear();

    switch (+this.question.type) {
      case QuestionType.TrueFalse: {
        const componentFactory = this.resolver.resolveComponentFactory(TrueFalseQuestionComponent);
        const componentRef = viewContainerRef.createComponent(componentFactory);
        (<TrueFalseQuestionComponent>componentRef.instance).question = this.question as TrueFalseQuestionAttemptInfo;
        (<TrueFalseQuestionComponent>componentRef.instance).answer.subscribe(
          (answer: QuestionAttemptResult) => this.answer.emit(answer)
        );
        return;
      }
      /*
      case QuestionType.MultipleChoice: {
        const componentFactory = this.resolver.resolveComponentFactory(MultipleChoiceQuestionComponent);
        const componentRef = viewContainerRef.createComponent(componentFactory);
        (<MultipleChoiceQuestionComponent>componentRef.instance).question = this.question as MultipleChoiceQuestionAttemptInfo;
        (<MultipleChoiceQuestionComponent>componentRef.instance).answer.subscribe(
          (answer: QuestionAttemptResult) => this.answer.emit(answer)
        );

        return;
      }
      case QuestionType.MultipleSelect: {
        const componentFactory = this.resolver.resolveComponentFactory(MultipleSelectQuestionComponent);
        const componentRef = viewContainerRef.createComponent(componentFactory);
        (<MultipleSelectQuestionComponent>componentRef.instance).question = this.question as MultipleSelectQuestionAttemptInfo;
        (<MultipleSelectQuestionComponent>componentRef.instance).answer.subscribe(
          (answer: QuestionAttemptResult) => this.answer.emit(answer)
        );
        return;
      }
      case QuestionType.LongAnswer: {
        const componentFactory = this.resolver.resolveComponentFactory(LongAnswerQuestionComponent);
        const componentRef = viewContainerRef.createComponent(componentFactory);
        (<LongAnswerQuestionComponent>componentRef.instance).question = this.question as LongAnswerQuestionAttemptInfo;
        (<LongAnswerQuestionComponent>componentRef.instance).answer.subscribe(
          (answer: QuestionAttemptResult) => this.answer.emit(answer)
        );
        return;
      }
      */

    }

    throw Error('Unsupported question type');

  }

}