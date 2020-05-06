import {Answer} from './answer';

export class Question {
  id: string;
  name: string;
  text: string;
  feedback: string;
  correctFeedback: string;
  incorrectFeedback: string;
  type: QuestionType;
  settings?: any;
  quizId: string;
  groupId: string;
  choices?: any;
}

export enum QuestionType {
  TrueFalse = 1,
  MultipleChoice = 2
}
