import {Answer} from './answer';

export class Question {
  id: number;
  name: string;
  text: string;
  type: QuestionType;
  quizId: number;
  groupId: number;
  answers?: Answer[];
}

export enum QuestionType {
  TrueFalse = 1,
  MultipleChoice = 2
}
