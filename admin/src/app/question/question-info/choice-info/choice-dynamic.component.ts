import { Component, ComponentFactoryResolver, Input, Type, ViewChild, OnInit } from '@angular/core';
import { ChoiceDirective } from '../../../_directive/choice.directive';
import { QuestionType, Question } from '../../../_models/question';
import { TrueFalseAnswerComponent } from '../../answer/true-false-answer/true-false-answer.component';
import { MultipleChoiceAnswerComponent } from '../../answer/multiple-choice-answer/multiple-choice-answer.component';
import { MultiSelectChoiceComponent } from '../../answer/multi-select-choice/multi-select-choice.component';
import { LongAnswerComponent } from '../../answer/long-answer/long-answer.component';
import { BaseChoiceComponent } from '../../answer/base-choice/base-choice.component';

@Component({
  selector: 'app-choice-dynamic',
  template: '<ng-template appDynamicChoiceHost></ng-template>'
})

export class ChoiceDynamicComponent implements OnInit {

  @Input() question: Question;
  
  @ViewChild(ChoiceDirective, {static: true}) choiceHost: ChoiceDirective;

  private components: { [id in QuestionType]: Type<BaseChoiceComponent> } = {
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
    componentRef.instance.settings = this.question.settings;
    componentRef.instance.choices = this.question.choices;
  }
}
