export class Question {
  id: string;
  quizId: string;
  groupId: string;
  name: string;
  text: string;
  points: number;
  feedback: string;
  correctFeedback: string;
  incorrectFeedback: string;
  type: QuestionType;
  settings?: any;
  choices?: any;
}

export class SettingsBase {
}

export enum QuestionType {
  TrueFalse = 2,
  MultipleChoice = 3,
  MultiSelect = 5,
  LongAnswer = 6
}
