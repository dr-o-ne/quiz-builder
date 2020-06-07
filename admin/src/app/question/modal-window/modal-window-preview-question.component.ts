import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { QuestionPageComponent } from '../question-page/question-page.component';
import { QuestionType } from 'src/app/_models/question';

@Component({
  selector: 'app-modal-window-preview-question',
  templateUrl: './modal-window-preview-question.component.html',
  styleUrls: ['./modal-window-preview-question.component.css']
})
export class ModalWindowPreviewQuestionComponent implements OnInit {

  questionType = QuestionType;

  constructor(public dialogRef: MatDialogRef<QuestionPageComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }

  onClick(): void {
    this.dialogRef.close();
  }

}
