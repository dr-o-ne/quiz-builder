import { Component, ComponentFactoryResolver, Input, Type, ViewChild, OnInit } from '@angular/core';
import { QuestionType, Question } from '../../../_models/question';
import { TrueFalseAnswerComponent } from '../../answer/true-false-answer/true-false-answer.component';
import { MultipleChoiceAnswerComponent } from '../../answer/multiple-choice-answer/multiple-choice-answer.component';
import { MultiSelectChoiceComponent } from '../../answer/multi-select-choice/multi-select-choice.component';
import { LongAnswerComponent } from '../../answer/long-answer/long-answer.component';
import { BaseChoiceComponent } from '../../answer/base-choice/base-choice.component';
import { ChoiceHostDirective } from './choice-host.directive';
import { ChoiceBaseComponent } from './choice-base-component';

@Component({
  selector: 'app-choice-dynamic',
  template: '<ng-template choice-host></ng-template>'
})

export class ChoiceDynamicComponent implements OnInit {

  @Input() question: Question;
  
  @ViewChild(ChoiceHostDirective, {static: true}) choiceHost: ChoiceHostDirective;

  private components: { [id in QuestionType]: Type<ChoiceBaseComponent> } = {
    [QuestionType.TrueFalse]: TrueFalseAnswerComponent,
    [QuestionType.MultipleChoice]: MultipleChoiceAnswerComponent,
    [QuestionType.MultiSelect]: MultiSelectChoiceComponent,
    [QuestionType.LongAnswer]: LongAnswerComponent
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

    const instance = componentRef.instance as BaseChoiceComponent;

    instance.settings = this.question.settings;
    instance.choices = this.question.choices;
  }
}
