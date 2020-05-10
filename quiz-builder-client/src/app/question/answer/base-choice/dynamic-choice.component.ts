import {Component, ComponentFactoryResolver, Input, OnChanges, SimpleChanges, Type, ViewChild} from '@angular/core';
import {Answer} from 'src/app/_models/answer';
import {BaseChoiceSettings} from 'src/app/_models/settings/answer.settings';
import {ChoiceDirective} from '../../../_directive/choice.directive';
import {QuestionType} from '../../../_models/question';
import {TrueFalseAnswerComponent} from '../true-false-answer/true-false-answer.component';
import {MultiSelectChoiceComponent} from '../multi-select-choice/multi-select-choice.component';
import {BaseChoiceComponent} from './base-choice.component';
import {MultipleChoiceAnswerComponent} from '../multiple-choice-answer/multiple-choice-answer.component';

@Component({
  selector: 'app-dynamic-choice',
  template: '<ng-template appDynamicChoiceHost></ng-template>'
})

export class DynamicChoiceComponent implements OnChanges {
  @Input() settings: BaseChoiceSettings;
  @Input() answerData: Answer[];
  @Input() questionType: QuestionType;
  @Input() isNewState: boolean;

  @ViewChild(ChoiceDirective, {static: true}) choiceHost: ChoiceDirective;

  private components: { [id in QuestionType]: Type<BaseChoiceComponent> } = {
    [QuestionType.TrueFalse]: TrueFalseAnswerComponent,
    [QuestionType.MultipleChoice]: MultipleChoiceAnswerComponent,
    [QuestionType.MultiSelect]: MultiSelectChoiceComponent
  };

  constructor(private resolver: ComponentFactoryResolver)
  {  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.hasOwnProperty('questionType') && changes.questionType.currentValue) {
      this.loadComponent(changes.questionType.currentValue);
    }
  }

  loadComponent(questionType: QuestionType): void {
    const componentFactory = this.resolver.resolveComponentFactory(this.components[questionType]);
    this.choiceHost.viewContainerRef.clear();
    const componentRef = this.choiceHost.viewContainerRef.createComponent(componentFactory);
    componentRef.instance.settings = this.settings;
    componentRef.instance.answerData = this.answerData;
    componentRef.instance.questionType = this.questionType;
    componentRef.instance.isNewState = this.isNewState;
  }
}
