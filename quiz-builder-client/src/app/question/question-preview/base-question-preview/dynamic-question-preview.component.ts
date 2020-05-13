import {Component, ComponentFactoryResolver, Input, OnChanges, SimpleChanges, Type, ViewChild} from '@angular/core';
import {Choice} from 'src/app/_models/choice';
import {BaseChoiceSettings} from 'src/app/_models/settings/answer.settings';
import {QuestionType, Question} from '../../../_models/question';
import { BaseChoiceComponent } from '../../answer/base-choice/base-choice.component';
import { TrueFalsePreviewComponent } from '../true-false-preview/true-false-preview.component';
import { BaseQuestionPreviewComponent } from './base-question-preview.component';
import { QuestionPreviewDirective } from 'src/app/_directive/question.preview.directive';
import { MultiSelectPreviewComponent } from '../multi-select-preview/multi-select-preview.component';

@Component({
  selector: 'app-dynamic-question-preview',
  template: '<ng-template appQuestionPreviewHost></ng-template>'
})

export class DynamicQuestionPreviewComponent implements OnChanges {
  @Input() question: Question;

  @ViewChild(QuestionPreviewDirective, {static: true}) questionPreviewHost: QuestionPreviewDirective;

  private components: { [id in QuestionType]: Type<BaseQuestionPreviewComponent> } = {
    [QuestionType.TrueFalse]: TrueFalsePreviewComponent,
    [QuestionType.MultipleChoice]: TrueFalsePreviewComponent,
    [QuestionType.MultiSelect]: MultiSelectPreviewComponent
  };

  constructor(private resolver: ComponentFactoryResolver) {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.hasOwnProperty('question') && changes.question.currentValue) {
      this.loadComponent(changes.question.currentValue.type);
    }
  }

  loadComponent(questionType: QuestionType): void {
    const componentFactory = this.resolver.resolveComponentFactory(this.components[questionType]);
    this.questionPreviewHost.viewContainerRef.clear();
    const componentRef = this.questionPreviewHost.viewContainerRef.createComponent(componentFactory);
    componentRef.instance.question = this.question;
  }
}
