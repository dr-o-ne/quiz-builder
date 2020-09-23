import { Component, ComponentFactoryResolver, Input, Type, ViewChild, OnInit, ComponentRef } from '@angular/core';
import { ChoiceHostDirective } from './choice-host.directive';
import { ChoiceBaseDirective } from './choice-base.directive';
import { ChoiceEmptyComponent } from './choice-empty.component';
import { MultipleChoiceChoiceInfoComponent } from './multiple-choice-choice-info/multiple-choice-choice-info.component';
import { MultipleSelectChoiceInfoComponent } from './multiple-select-choice-info/multiple-select-choice-info.component';
import { QuestionType, Question } from 'app/quiz-builder/model/question';
import { TrueFalseChoiceInfoComponent } from './true-false-choice-info/true-false-choice-info.component';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-choice-dynamic',
  template: '<ng-template choice-host></ng-template>'
})

export class ChoiceDynamicComponent implements OnInit {

  @Input() question: Question;
  @Input() questionForm: FormGroup;  
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
    this.choiceComponentRef.instance.questionForm = this.questionForm;
  }

  save(): void {
    
    if (!this.choiceComponentRef) return;

    return this.choiceComponentRef.instance.save();
  }
  
}
