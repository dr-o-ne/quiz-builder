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
  isRequired: boolean;

  get isValid(): boolean {

    if (!this.text) return false;

    if (this.type === QuestionType.TrueFalse) {
      
      if (this.choices.length !== 2) return false;
      if (this.choices[0].text === '') return false;
      if (this.choices[1].text === '') return false;
      if (this.choices[0].isCorrect !== this.choices[1].isCorrect) return false;

      return true;
    }

    return true;

  }

}

export class SettingsBase {
}

export enum QuestionType {
  TrueFalse = 2,
  MultipleChoice = 3,
  MultiSelect = 5,
  LongAnswer = 6
}
