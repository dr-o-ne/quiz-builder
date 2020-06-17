import { Component, Input, OnInit, ViewChild, ComponentFactoryResolver, Output, EventEmitter } from '@angular/core';
import { QuestionType } from 'src/app/_models/_enums';
import { QuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { QuestionAttemptResult } from 'src/app/_models/attemptResult';
import { QuestionHostDirective } from 'src/app/_directives/question-host.directive';
import { MultipleSelectQuestionComponent } from './multiple-select-question/multiple-select-question.component';
import { TrueFalseQuestionComponent } from './true-false-question/true-false-question.component';
import { MultipleChoiceQuestionComponent } from './multiple-choice-question/multiple-choice-question.component';
import { QuestionComponent } from './question.component';

@Component({
  selector: 'app-question-dynamic',
  template: '<ng-template question-host></ng-template>'
})

export class QuestionDynamicComponent implements OnInit {

  @Input() question: QuestionAttemptInfo;
  @Output() answer: EventEmitter<QuestionAttemptResult> = new EventEmitter<QuestionAttemptResult>();
  @ViewChild(QuestionHostDirective, { static: true }) host: QuestionHostDirective;

  constructor(private resolver: ComponentFactoryResolver) {
  }

  ngOnInit(): void {
    this.loadComponent();
  }

  loadComponent(): void {

    const viewContainerRef = this.host.viewContainerRef;
    viewContainerRef.clear();

    switch (+this.question.type) {
      case QuestionType.TrueFalse: {
        const componentFactory = this.resolver.resolveComponentFactory(TrueFalseQuestionComponent);
        const componentRef = viewContainerRef.createComponent(componentFactory);
        (<TrueFalseQuestionComponent>componentRef.instance).question = this.question;
        (<TrueFalseQuestionComponent>componentRef.instance).answer.subscribe(
          (answer: QuestionAttemptResult) => this.answer.emit(answer)
        );

        return;
      }
      case QuestionType.MultipleChoice: {
        const componentFactory = this.resolver.resolveComponentFactory(MultipleChoiceQuestionComponent);
        const componentRef = viewContainerRef.createComponent(componentFactory);
        (<MultipleChoiceQuestionComponent>componentRef.instance).question = this.question;
        (<MultipleChoiceQuestionComponent>componentRef.instance).answer.subscribe(
          (answer: QuestionAttemptResult) => this.answer.emit(answer)
        );

        return;
      }
      case QuestionType.MultipleSelect: {
        const componentFactory = this.resolver.resolveComponentFactory(MultipleSelectQuestionComponent);
        const componentRef = viewContainerRef.createComponent(componentFactory);
        (<MultipleSelectQuestionComponent>componentRef.instance).question = this.question;
        (<MultipleSelectQuestionComponent>componentRef.instance).answer.subscribe(
          (answer: QuestionAttemptResult) => this.answer.emit(answer)
        );
        return;
      }


    }

    throw Error('Unsupported question type');

  }

}