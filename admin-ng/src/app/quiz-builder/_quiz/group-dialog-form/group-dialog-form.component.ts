import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { DataInfo } from '../quiz-info/structure/quiz-info-structure.component';

@Component({
    selector: 'app-group-dialog-form',
    templateUrl: './group-dialog-form.component.html',
    styleUrls: ['./group-dialog-form.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class GroupDialogFormComponent {

    form: FormGroup;

    constructor(
        public matDialogRef: MatDialogRef<GroupDialogFormComponent>,
        @Inject(MAT_DIALOG_DATA) private data: any,
        private fb: FormBuilder
    ) {
        this.form = this.createForm();

        console.log(this.form);

    }

    createForm(): FormGroup {

        const dataInfo: DataInfo = this.data.dataInfo;

        return this.fb.group({
            name: [dataInfo.name],
            selectAllQuestions: [dataInfo.selectAllQuestions],
            randomizeQuestions: [dataInfo.randomizeQuestions],
            countOfQuestionsToSelect: [dataInfo.countOfQuestionsToSelect]
        });
    }
}