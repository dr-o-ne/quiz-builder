import { Component, Input, OnInit, ViewChild, ComponentFactoryResolver, OnDestroy, Type, SimpleChanges } from '@angular/core';
import { QuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { QuestionType } from 'src/app/_models/_enums';
import { TrueFalseQuestionComponent } from './true-false-question/true-false-question.component';
import { MultipleChoiceQuestionComponent } from './multiple-choice-question/multiple-choice-question.component';
import { QuestionComponent } from './question.component';
import { QuestionHostDirective } from 'src/app/_directives/question-host.directive';

@Component({
  selector: 'app-question-dynamic',
  template: '<ng-template question-host></ng-template>'
})

export class QuestionDynamicComponent implements OnInit, OnDestroy {

  private readonly components = new Map();

  @Input() question: QuestionAttemptInfo;
  @ViewChild(QuestionHostDirective, { static: true }) host: QuestionHostDirective;

  constructor(private resolver: ComponentFactoryResolver) { 
    this.components.set(QuestionType.TrueFalse, TrueFalseQuestionComponent);
    this.components.set(QuestionType.MultipleChoice, MultipleChoiceQuestionComponent);

  }

  ngOnInit() {
    this.loadComponent( this.question.type );
  }

  ngOnDestroy() {
  }

  loadComponent( questionType: QuestionType ) {

    const componentFactory = this.resolver.resolveComponentFactory(this.components.get(questionType));
    const viewContainerRef = this.host.viewContainerRef;

    viewContainerRef.clear();

    const componentRef = viewContainerRef.createComponent(componentFactory);
    (<QuestionComponent>componentRef.instance).question = this.question;
  }


}
