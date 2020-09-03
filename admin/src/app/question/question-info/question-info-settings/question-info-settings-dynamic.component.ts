import { Component, ComponentFactoryResolver, Input, Type, ViewChild, OnInit, ComponentRef } from '@angular/core';
import { QuestionType, Question } from '../../../_models/question';
import { QuestionInfoSettingsHostDirective } from './question-info-settings-host.directive';
import { QuestionInfoSettingsBaseDirective } from './question-info-settings-base.directive';
import { EmptyQuestionInfoSettingsComponent } from './question-info-settings-empty.component';
import { TrueFalseInfoSettingsComponent } from './true-false-info-settings/true-false-info-settings.component';
import { MultipleChoiceInfoSettingsComponent } from './multiple-choice-info-settings/multiple-choice-info-settings.component';

@Component({
    selector: 'app-question-info-settings-dynamic',
    template: '<ng-template question-info-settings-host></ng-template>'
})

export class QuestionInfoSettingsDynamicComponent implements OnInit {

    @Input() question: Question;
    @ViewChild(QuestionInfoSettingsHostDirective, { static: true }) host: QuestionInfoSettingsHostDirective;

    componentRef: ComponentRef<QuestionInfoSettingsBaseDirective>;

    private components: { [id in QuestionType]: Type<QuestionInfoSettingsBaseDirective> } = {
        [QuestionType.TrueFalse]: TrueFalseInfoSettingsComponent,
        [QuestionType.MultipleChoice]: MultipleChoiceInfoSettingsComponent,
        [QuestionType.MultiSelect]: EmptyQuestionInfoSettingsComponent,
        [QuestionType.LongAnswer]: EmptyQuestionInfoSettingsComponent,
        [QuestionType.Empty]: EmptyQuestionInfoSettingsComponent
    };

    constructor(private resolver: ComponentFactoryResolver) {
    }

    ngOnInit(): void {
        this.loadComponent();
    }

    loadComponent(): void {
        const componentFactory = this.resolver.resolveComponentFactory(this.components[this.question.type]);
        this.host.viewContainerRef.clear();
        this.componentRef = this.host.viewContainerRef.createComponent(componentFactory);

        this.componentRef.instance.question = this.question;
    }

}
