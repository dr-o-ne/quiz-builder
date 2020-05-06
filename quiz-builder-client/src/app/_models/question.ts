import {Answer} from './answer';

export class Question {
  id: number;
  name: string;
  text: string;
  feedback: string;
  correctFeedback: string;
  incorrectFeedback: string;
  type: QuestionType;
  settings?: any;
  quizId: number;
  groupId: number;
  choices?: any;
}

export enum QuestionType {
  TrueFalse = 1,
  MultipleChoice = 2
}
