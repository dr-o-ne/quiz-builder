import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector: 'app-quiz-dialog-form',
    templateUrl: './quiz-dialog-form.component.html',
    styleUrls: ['./quiz-dialog-form.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class QuizDialogFormComponent {

    name: string;
    form: FormGroup;

    constructor(
        public matDialogRef: MatDialogRef<QuizDialogFormComponent>,
        @Inject(MAT_DIALOG_DATA) private data: any,
        private fb: FormBuilder
    ) {
        this.form = this.createForm();
    }

    createForm(): FormGroup {
        return this.fb.group({
            name: [this.data.name, Validators.required]
        });
    }
}