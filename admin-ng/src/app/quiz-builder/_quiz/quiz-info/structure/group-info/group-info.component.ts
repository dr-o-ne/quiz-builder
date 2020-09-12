import { Component, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Group } from 'app/quiz-builder/model/group';

export interface GroupInfoViewModel {
    id: string;
    name: string;
    selectAllQuestions: boolean;
    randomizeQuestions: boolean;
    countOfQuestionsToSelect?: number;
}

@Component({
    selector: 'app-group-info',
    templateUrl: './group-info.component.html'
})
export class GroupInfoComponent {

    @Input() viewModel: GroupInfoViewModel;
    form: FormGroup;

    constructor(private fb: FormBuilder) {
    }

    ngOnInit(): void {
        this.form = this.fb.group({
            name: [this.viewModel.name],
            selectAllQuestions: [this.viewModel.selectAllQuestions],
            randomizeQuestions: [this.viewModel.randomizeQuestions],
            countOfQuestionsToSelect: [this.viewModel.countOfQuestionsToSelect]
        }) 
    }

    saveFormData(group: Group): boolean {

        if( !this.form.dirty ) {
            return false;
        }

        group.name = this.form.value.name as string;
        group.selectAllQuestions = this.form.value.selectAllQuestions as boolean;
        group.countOfQuestionsToSelect = this.form.value.countOfQuestionsToSelect as number;
        group.randomizeQuestions = this.form.value.randomizeQuestions as boolean;

        this.form.markAsPristine();

        return true;
    }

}