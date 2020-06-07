import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { QuizAttemptComponent } from '../quiz-attempt.component';

@Component({
  selector: 'app-modal-window-attempt',
  templateUrl: './modal-window-attempt.component.html',
  styleUrls: ['./modal-window-attempt.component.css']
})
export class ModalWindowAttemptComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<QuizAttemptComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any)
  {
    dialogRef.disableClose = true;
  }

  ngOnInit() {
  }

  onClick(): void {
    this.dialogRef.close();
  }

}
