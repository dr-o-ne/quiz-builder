import {Choice} from './choice';

export class Question {
  id: string;
  name: string;
  text: string;
  points: number;
  feedback: string;
  correctFeedback: string;
  incorrectFeedback: string;
  type: QuestionType;
  settings?: any;
  quizId: string;
  groupId?: string;
  choices?: any;
}

export enum QuestionType {
  TrueFalse = 1,
  MultipleChoice = 2,
  MultiSelect = 4,
  LongAnswer = 5
}
