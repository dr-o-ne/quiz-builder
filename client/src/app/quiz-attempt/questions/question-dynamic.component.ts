import { Component, Input, OnInit, ViewChild, ComponentFactoryResolver } from '@angular/core';
import { QuestionHostDirective } from 'src/app/_directives/question-host.directive';
import { QuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { QuestionType } from 'src/app/_models/_enums';
import { MultipleSelectQuestionComponent } from './multiple-select-question/multiple-select-question.component';
import { TrueFalseQuestionComponent } from './true-false-question/true-false-question.component';
import { MultipleChoiceQuestionComponent } from './multiple-choice-question/multiple-choice-question.component';

@Component({
  selector: 'app-question-dynamic',
  template: '<ng-template question-host></ng-template>'
})

export class QuestionDynamicComponent implements OnInit {

  @Input() question: QuestionAttemptInfo;
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
        return;
      }
      case QuestionType.MultipleChoice: {
        const componentFactory = this.resolver.resolveComponentFactory(MultipleChoiceQuestionComponent);
        const componentRef = viewContainerRef.createComponent(componentFactory);
        (<MultipleChoiceQuestionComponent>componentRef.instance).question = this.question;
        return;
      }
      case QuestionType.MultipleSelect: {
        const componentFactory = this.resolver.resolveComponentFactory(MultipleSelectQuestionComponent);
        const componentRef = viewContainerRef.createComponent(componentFactory);
        (<MultipleSelectQuestionComponent>componentRef.instance).question = this.question;
        return;
      }
    }

    throw Error('Unsupported question type');

  }

}
