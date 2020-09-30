import { EventEmitter } from '@angular/core';
import * as InlineEditor from '@ckeditor/ckeditor5-build-inline';

export class QuestionComponent<QuestionAttemptInfo, QuestionAttemptResult> {

  public Editor = InlineEditor;

  question!: QuestionAttemptInfo;
  answer = new EventEmitter<QuestionAttemptResult>();

}