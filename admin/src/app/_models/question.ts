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
  TrueFalse = 2,
  MultipleChoice = 3,
  MultiSelect = 5,
  LongAnswer = 6
}
