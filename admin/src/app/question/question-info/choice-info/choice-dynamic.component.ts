import { Component, ComponentFactoryResolver, Input, Type, ViewChild, OnInit, ComponentRef } from '@angular/core';
import { QuestionType, Question } from '../../../_models/question';
import { BaseChoiceComponent } from '../../answer/base-choice/base-choice.component';
import { ChoiceHostDirective } from './choice-host.directive';
import { ChoiceBaseDirective } from './choice-base.directive';
import { ChoiceEmptyComponent } from './choice-empty.component';
import { TrueFalseChoiceInfoComponent } from './true-false-choice-info/true-false-choice-info.component';
import { MultipleChoiceChoiceInfoComponent } from './multiple-choice-choice-info/multiple-choice-choice-info.component';
import { MultipleSelectChoiceInfoComponent } from './multiple-select-choice-info/multiple-select-choice-info.component';

@Component({
  selector: 'app-choice-dynamic',
  template: '<ng-template choice-host></ng-template>'
})

export class ChoiceDynamicComponent implements OnInit {

  @Input() question: Question;  
  @ViewChild(ChoiceHostDirective, {static: true}) choiceHost: ChoiceHostDirective;

  choiceComponentRef: ComponentRef<ChoiceBaseDirective>;

  private components: { [id in QuestionType]: Type<ChoiceBaseDirective> } = {
    [QuestionType.TrueFalse]: TrueFalseChoiceInfoComponent,
    [QuestionType.MultipleChoice]: MultipleChoiceChoiceInfoComponent,
    [QuestionType.MultiSelect]: MultipleSelectChoiceInfoComponent,
    [QuestionType.LongAnswer]: ChoiceEmptyComponent,
    [QuestionType.Empty]: ChoiceEmptyComponent
  };

  constructor(private resolver: ComponentFactoryResolver) {
  }

  ngOnInit(): void {
    this.loadComponent();
  }

  loadComponent(): void {
    const componentFactory = this.resolver.resolveComponentFactory(this.components[this.question.type]);
    this.choiceHost.viewContainerRef.clear();
    this.choiceComponentRef = this.choiceHost.viewContainerRef.createComponent(componentFactory);

    this.choiceComponentRef.instance.question = this.question;

    console.log(this.question);

    if (this.choiceComponentRef.instance instanceof BaseChoiceComponent) {
      this.choiceComponentRef.instance.settings = this.question.settings;
      this.choiceComponentRef.instance.choices = this.question.choices;
    }    

  }

  isValid(): boolean {

    if(!this.choiceComponentRef) return true;

    return this.choiceComponentRef.instance.isValid();
  }
  
}
