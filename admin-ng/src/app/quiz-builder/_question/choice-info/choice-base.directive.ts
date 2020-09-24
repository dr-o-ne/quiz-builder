import { Directive } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Question } from 'app/quiz-builder/model/question';
import { OptionItem } from 'app/quiz-builder/model/UI/optionItem';
import { OptionItemsService } from 'app/quiz-builder/model/UI/optionItemService';

@Directive()
export abstract class ChoiceBaseDirective {

    constructor(protected fb: FormBuilder) {
    }

    question!: Question;
    questionForm!: FormGroup;
    options!: OptionItem[];

    optionItemsService = OptionItemsService;

    abstract save(): void;
}
