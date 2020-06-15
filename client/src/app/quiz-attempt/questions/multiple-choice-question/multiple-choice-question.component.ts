import { Component } from '@angular/core';
import { QuestionComponent } from '../question.component';
import { ChoicesDisplayType } from 'src/app/_models/_enums';
import { MultipleChoiceQuestionAttemptInfo } from 'src/app/_models/attemptInfo';
import { MultipleChoiceQuestionAttemptResult } from 'src/app/_models/attemptResult';

@Component({
  selector: 'app-multiple-choice-question',
  templateUrl: './multiple-choice-question.component.html'
})

export class MultipleChoiceQuestionComponent extends QuestionComponent<MultipleChoiceQuestionAttemptInfo, MultipleChoiceQuestionAttemptResult> {
  
  choicesDisplayType = ChoicesDisplayType;

}
