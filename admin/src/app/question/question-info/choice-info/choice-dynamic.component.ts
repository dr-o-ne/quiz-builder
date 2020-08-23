import { Component, ComponentFactoryResolver, Input, Type, ViewChild, OnInit } from '@angular/core';
import { QuestionType, Question } from '../../../_models/question';
import { TrueFalseAnswerComponent } from '../../answer/true-false-answer/true-false-answer.component';
import { MultipleChoiceAnswerComponent } from '../../answer/multiple-choice-answer/multiple-choice-answer.component';
import { MultiSelectChoiceComponent } from '../../answer/multi-select-choice/multi-select-choice.component';
import { BaseChoiceComponent } from '../../answer/base-choice/base-choice.component';
import { ChoiceHostDirective } from './choice-host.directive';
import { ChoiceBaseDirective } from './choice-base.directive';
import { ChoiceEmptyDirective } from './choice-empty.directive';

@Component({
  selector: 'app-choice-dynamic',
  template: '<ng-template choice-host></ng-template>'
})

export class ChoiceDynamicComponent implements OnInit {

  @Input() question: Question;
  
  @ViewChild(ChoiceHostDirective, {static: true}) choiceHost: ChoiceHostDirective;

  private components: { [id in QuestionType]: Type<ChoiceBaseDirective> } = {
    [QuestionType.TrueFalse]: TrueFalseAnswerComponent,
    [QuestionType.MultipleChoice]: MultipleChoiceAnswerComponent,
    [QuestionType.MultiSelect]: MultiSelectChoiceComponent,
    [QuestionType.LongAnswer]: ChoiceEmptyDirective
  };

  constructor(private resolver: ComponentFactoryResolver) {
  }

  ngOnInit(): void {
    this.loadComponent();
  }

  loadComponent(): void {
    const componentFactory = this.resolver.resolveComponentFactory(this.components[this.question.type]);
    this.choiceHost.viewContainerRef.clear();
    const componentRef = this.choiceHost.viewContainerRef.createComponent(componentFactory);

    componentRef.instance.question = this.question;

    if (componentRef.instance instanceof BaseChoiceComponent) {
      componentRef.instance.settings = this.question.settings;
      componentRef.instance.choices = this.question.choices;
    }
  }
}
